package pro100.studio.contactor.ListModels;

public class EventCard {
    public EventCard(String id, String subject, String body, boolean isActiv) {
        Id = id;
        Subject = subject;
        Body = body;
        IsActiv = isActiv;
    }

    public String getId() {
        return Id;
    }

    public Boolean getActiv() {
        return IsActiv;
    }

    public void setActiv(Boolean activ) {
        IsActiv = activ;
    }

    public String getSubject() {
        return Subject;
    }

    public String getBody() {
        return Body;
    }

    private  String Id;
    private  String Subject;
    private  String Body;
    private Boolean IsActiv;

    public void setId(String id) {
        Id = id;
    }

    public void setSubject(String subject) {
        Subject = subject;
    }

    public void setBody(String body) {
        Body = body;
    }
}
