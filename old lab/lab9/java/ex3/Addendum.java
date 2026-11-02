package ex3;

public class Addendum {
    private String title;
    private String  description;
    public Addendum(String title, String description){
        this.title = title;
        this.description = description;
    }
    public Addendum (Addendum other){
       title = other.title;
       description = other.description;
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

    @Override
    public String toString() {
        return (title + "\n " + description) ;
    }
}
