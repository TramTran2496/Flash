package flashteam.career;

import android.content.DialogInterface;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.List;

public class SavedJobActivity extends AppCompatActivity {
    private List<Job> jobList;
    private DatabaseHandler dbHandler;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_saved_job);
        jobList = new ArrayList<Job>();
        dbHandler = new DatabaseHandler(this, null, null, 1);
        jobList = dbHandler.loadSavedHandler();
        printJobs();
    }

    protected void printJobs(){
        List<String> jobString = new ArrayList<String>();
        if (jobList.size() == 0) {
            TextView info = (TextView) findViewById(R.id.noJobTextView);
            info.setText("No job saved.");
        }
        for (int i = 0; i < jobList.size(); i++) {
            String j = jobList.get(i).getTitle();
            j = j + "\nCompany: " + jobList.get(i).getCompany();
            j = j + "\nLocation: " + jobList.get(i).getAddress();
            j = j + "\nSalary: " + jobList.get(i).getSalary();
            j = j + '\n' + jobList.get(i).getDescription();
            jobString.add(j);
        }
        ListView view = (ListView) findViewById(R.id.savedView);
        view.setAdapter(new ArrayAdapter<String>(SavedJobActivity.this, R.layout.row, R.id.text, jobString));
        //add view item listener
        view.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, final int position, long id) {
                //create alert dialog
                AlertDialog.Builder builder = new AlertDialog.Builder(SavedJobActivity.this);
                builder.setMessage("Do you want to remove this job?");
                builder.setCancelable(true);

                builder.setPositiveButton(
                        "Yes",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                ListView view = (ListView) findViewById(R.id.savedView);
                                Job clickedJob = jobList.get(position);

                                dbHandler.deleteSavedHandler(clickedJob);
                                jobList = dbHandler.loadSavedHandler();
                                printJobs();
                            }
                        });

                builder.setNegativeButton(
                        "No",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }
                        });

                AlertDialog alert = builder.create();
                alert.show();
            }
        });
    }
}
