package flashteam.career;

import java.util.Date;
import java.util.List;

public class Job {

    private String title;
    private String description;
    private String salary;
    private String company;
    private String address;

    public Job(String title, String description, String salary, String company, String address) {
        this.title = title;
        this.description = description;
        this.salary = salary;
        this.company = company;
        this.address = address;
    }

    public Job() {
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getSalary() {
        return salary;
    }

    public void setSalary(String salary) {
        this.salary = salary;
    }

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }
}
