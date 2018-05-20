package flashteam.career;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.List;

public class DatabaseHandler extends SQLiteOpenHelper {
    //information of database
    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "jobDB.db";
    public static final String TABLE_NAME = "Job";
    public static final String COLUMN_TITLE = "Title";
    public static final String COLUMN_DESC = "Description";
    public static final String COLUMN_SALARY = "Salary";
    public static final String COLUMN_COMP = "Company";
    public static final String COLUMN_ADDR = "Address";
    //initialize the database
    public DatabaseHandler(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, DATABASE_NAME, factory, DATABASE_VERSION);
    }
    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_TABLE = "CREATE TABLE " + TABLE_NAME + "(" + COLUMN_TITLE + " NTEXT,"
        + COLUMN_DESC + " NTEXT," + COLUMN_SALARY + " NTEXT," + COLUMN_COMP + " NTEXT," + COLUMN_ADDR + " NTEXT )";
        db.execSQL(CREATE_TABLE);
    }
    @Override
    public void onUpgrade(SQLiteDatabase db, int i, int i1) {}

    public List<Job> loadHandler() {
        List<Job> result = new ArrayList<Job>();
        String query = "Select * FROM " + TABLE_NAME;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(query, null);
        while (cursor.moveToNext()) {
            result.add(new Job(cursor.getString(0), cursor.getString(1), cursor.getString(2),
                    cursor.getString(3), cursor.getString(4)));
        }
        cursor.close();
        db.close();
        return result;
    }
    public void addHandler(Job job, int id) {
        ContentValues values = new ContentValues();
        values.put(COLUMN_TITLE, job.getTitle());
        values.put(COLUMN_DESC, job.getDescription());
        values.put(COLUMN_SALARY, job.getSalary());
        values.put(COLUMN_COMP, job.getCompany());
        values.put(COLUMN_ADDR, job.getAddress());
        SQLiteDatabase db = this.getWritableDatabase();
        db.insert(TABLE_NAME, null, values);
        db.close();
    }
    public void deleteAllHandler() {
        String query = "DELETE FROM " + TABLE_NAME;
        SQLiteDatabase db = this.getWritableDatabase();
        db.execSQL(query);
        db.close();
    }
    public List<Job> searchHandler(String title, String comp, String addr){
        String query = "Select * FROM " + TABLE_NAME;
        boolean checkAnd = false;
        if(!title.equals("") || !comp.equals("") || !addr.equals("All")){
            query += " WHERE ";
            if(!title.equals("")){
                query += COLUMN_TITLE + " LIKE '%" + title + "%'";
                checkAnd = true;
            }
            if(!comp.equals("")){
                if(checkAnd) query += " AND ";
                query += COLUMN_COMP + " LIKE '%" + comp + "%'";
                checkAnd = true;
            }
            if(!addr.equals("All")){
                if(checkAnd) query += " AND ";
                query += COLUMN_ADDR + " = '" + addr + "'";
            }
        }
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(query, null);
        List<Job> jobs = new ArrayList<Job>();
        if(cursor.moveToFirst()) {
            jobs.add(new Job(cursor.getString(0), cursor.getString(1), cursor.getString(2),
                    cursor.getString(3), cursor.getString(4)));
            while (cursor.moveToNext()) {
                jobs.add(new Job(cursor.getString(0), cursor.getString(1), cursor.getString(2),
                        cursor.getString(3), cursor.getString(4)));
            }
        }
        cursor.close();
        db.close();
        return jobs;
    }
}
