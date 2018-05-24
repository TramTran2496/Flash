package flashteam.career;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.NotificationCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import org.json.JSONObject;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {
    private ProgressDialog progressDialog;
    private List<Job> jobList;
    private String[] districts = {"All", "District 1", "District 2", "District 3", "District 4", "District 5", "District 6", "District 7",
            "District 8", "District 9", "District 10", "District 11", "District 12", "Phu Nhuan", "Tan Binh"};
    private int address = 0;
    DatabaseHandler dbHandler;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        progressDialog = new ProgressDialog(this);

        jobList = new ArrayList<Job>();
        dbHandler = new DatabaseHandler(this, null, null, 1);
        jobList = dbHandler.loadHandler();
        printJobs();

        ArrayAdapter<String> adapter=new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, districts);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        final Spinner loc = (Spinner) findViewById(R.id.addressSearch);
        loc.setAdapter(adapter);
        loc.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                address = position;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
                // do nothing
            }
        });

        final Button searchBtn = (Button) findViewById(R.id.searchBtn);
        searchBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                search();
            }
        });

        String path = "https://itviec.com/it-jobs/ho-chi-minh-hcm";
        String idPost = "jobs";
        URL url;
        try {
            url = new URL(path);
        } catch (MalformedURLException e) {
            e.printStackTrace();
            Toast.makeText(this, "URL is invalid", Toast.LENGTH_SHORT).show();
            return;
        }

        GetContentFromURL getContentFromURL = new GetContentFromURL(url, idPost);
        getContentFromURL.execute();
    }

    public void noti(String title, String text)  {
        Context mContext = this.getApplicationContext();
        NotificationCompat.Builder mBuilder =
                new NotificationCompat.Builder(mContext.getApplicationContext(), "notify_001");
        Intent ii = new Intent(mContext.getApplicationContext(), MainActivity.class);
        PendingIntent pendingIntent = PendingIntent.getActivity(mContext, 0, ii, 0);

        mBuilder.setContentIntent(pendingIntent);
        mBuilder.setSmallIcon(R.drawable.ic_launcher_background);
        mBuilder.setContentTitle(title);
        mBuilder.setContentText(text);
        mBuilder.setAutoCancel(true);

        NotificationManager mNotificationManager =
                (NotificationManager) mContext.getSystemService(Context.NOTIFICATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel channel = new NotificationChannel("notify_001",
                    "Channel human readable title",
                    NotificationManager.IMPORTANCE_DEFAULT);
            mNotificationManager.createNotificationChannel(channel);
        }

        mNotificationManager.notify(0, mBuilder.build());
    }

    public void search() {
        EditText jobSearch = (EditText) findViewById(R.id.jobSearch);
        EditText compSearch = (EditText) findViewById(R.id.companySearch);
        String title = jobSearch.getText().toString();
        String comp = compSearch.getText().toString();
        String addr = districts[address];
        jobList = dbHandler.searchHandler(title, comp, addr);
        printJobs();
    }

    protected void printJobs(){
        List<String> jobString = new ArrayList<String>();
        for (int i = 0; i < jobList.size(); i++) {
            String j = jobList.get(i).getTitle();
            j = j + "\nCompany: " + jobList.get(i).getCompany();
            j = j + "\nLocation: " + jobList.get(i).getAddress();
            j = j + "\nSalary: " + jobList.get(i).getSalary();
            j = j + '\n' + jobList.get(i).getDescription();
            jobString.add(j);
        }
        ListView view = (ListView) findViewById(R.id.view);
        view.setAdapter(new ArrayAdapter<String>(MainActivity.this, R.layout.row, R.id.text, jobString));
        //add view item listener
        view.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, final int position, long id) {
                //create alert dialog
                AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
                builder.setMessage("Do you want to save this job?");
                builder.setCancelable(true);

                builder.setPositiveButton(
                        "Yes",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                ListView view = (ListView) findViewById(R.id.view);
                                Job clickedJob = jobList.get(position);
                                if (!dbHandler.addSavedHandler(clickedJob)) {
                                    Toast.makeText(MainActivity.this, "Job already saved.", Toast.LENGTH_SHORT).show();
                                } else {
                                    Toast.makeText(MainActivity.this, "Job saved.", Toast.LENGTH_SHORT).show();
                                }
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

    private class GetContentFromURL extends AsyncTask<Void, Void, Document> {
        private URL url;
        private String idPost;

        public GetContentFromURL(URL url, String idPost) {
            this.url = url;
            this.idPost = idPost;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog.setMessage("Please wait ...");
            progressDialog.setCancelable(false);
            progressDialog.show();
        }

        @Override
        protected Document doInBackground(Void... params) {
            try {
                Document elements = Jsoup.connect(url.toString()).get();
                return elements;
            } catch (IOException e) {
                e.printStackTrace();
                return null;
            }
        }

        @Override
        protected void onPostExecute(Document document) {
            super.onPostExecute(document);
            if (document != null) {
                Element element = Jsoup.parse(document.toString()).select("#" + idPost).first();
                Elements listElement = element.getAllElements();
                Element childElement = listElement.get(2);
                Elements jobs = childElement.getElementsByTag("script");
                int hasNewJobs = 0;
                for(int i = 0; i < jobs.size(); i++) {
                    if(dbHandler.addHandler(parseJobJSON(jobs.get(i).html().toString()), i))
                        hasNewJobs += 1;
                }
                jobList = dbHandler.loadHandler();
                if(hasNewJobs > 0)
                    noti("New jobs", "Open Career to see " + hasNewJobs + " new jobs");
                printJobs();
            } else {
                Toast.makeText(MainActivity.this, "No connection", Toast.LENGTH_SHORT).show();
            }
            progressDialog.dismiss();
        }

        private Job parseJobJSON(String jsonString){
            Job job = new Job();
            JSONObject obj;
            try{
                obj = new JSONObject(jsonString);
                job.setTitle(obj.getString("title"));
                job.setDescription(obj.getString("description"));
                job.setSalary(obj.getJSONObject("baseSalary").getJSONObject("value").getString("value"));
                job.setCompany(obj.getJSONObject("hiringOrganization").getString("name"));
                job.setAddress(obj.getJSONArray("jobLocation").getJSONObject(0).getJSONObject("address").getString("addressLocality"));
            } catch (Exception e) {
                Toast.makeText(MainActivity.this, "Parse JSON Fail", Toast.LENGTH_SHORT).show();
            }
            return job;
        }
    }

    public void viewSavedJob(View view){
        Intent intent = new Intent(this, SavedJobActivity.class);
        startActivity(intent);
    }
}