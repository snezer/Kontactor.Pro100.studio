package pro100.studio.contactor;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import pro100.studio.contactor.ListModels.EventCard;

public class EventActivity extends AppCompatActivity {

    private EventCard[] eventList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_event);

        SetDataToEventsList();
    }

    public void SetDataToEventsList(){
        eventList = new EventCard[3];
        String subject = "Администрация";
        String body = "Задолженностьпо аренде Необходимо оплатить в течении 10 рабочих дней.";
        eventList[0] = new EventCard("1", subject, body);
        String subject1 = "Коворкинг";
        String body1 = "Картинки-пати! Объявлен день настоящего питона. Заходи пиши код и получай призы!";
        eventList[1] = new EventCard("2", subject1, body1);
        String subject2 = "Администрация";
        String body2 = "Задолженностьпо аренде Необходимо оплатить в течении 10 рабочих дней.";
        eventList[2] = new EventCard("1", subject2, body2);

        AdapterEvent adapterEvent = new AdapterEvent(this);
        ListView eventList = (ListView) findViewById(R.id.events);
        eventList.setAdapter(adapterEvent);
    }

    class AdapterEvent extends ArrayAdapter<EventCard> {

        Activity context;

        public AdapterEvent(Activity context) {
            super(context, R.layout.activity_event_item, eventList);
            this.context = context;
        }

        public View getView(int pos, View convertView, ViewGroup parent){
            LayoutInflater inflater = context.getLayoutInflater();
            View item = inflater.inflate(R.layout.activity_event_item, null);
            TextView userName = (TextView) item.findViewById(R.id.user_name);
            userName.setText(eventList[pos].getSubject());

            TextView eventBody = (TextView) item.findViewById(R.id.text_body);
            eventBody.setText(eventList[pos].getBody());
            return item;
        }
    }
}