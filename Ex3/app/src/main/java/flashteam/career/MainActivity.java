package flashteam.career;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.Spinner;
import android.widget.TextView;
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
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

public class MainActivity extends AppCompatActivity {
    private ProgressDialog progressDialog;
    private List<Job> jobList;
    private String[] districts = {"All", "District 1", "District 2", "District 3", "District 4", "District 5", "District 6", "District 7",
            "District 8", "District 9", "District 10", "District 11", "District 12", "Tan Binh", "Phu My Hung"};
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
                jobList = new ArrayList<Job>();
                for(int i = 0; i < jobs.size(); i++){
                    jobList.add(parseJobJSON(jobs.get(i).html().toString()));
                }
                if(jobList.size() > 0) {
                    dbHandler.deleteAllHandler();
                    for(int i = 0; i < jobList.size(); i++) {
                        dbHandler.addHandler(jobList.get(i), i);
                    }
                }
                else jobList = dbHandler.loadHandler();
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
}