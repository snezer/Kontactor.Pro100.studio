package pro100.studio.contactor;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
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
        eventList = new EventCard[4];
        String subject = "Администрация";
        String body = "Аренда запрашиваемого вами помещения одобрена!";
        eventList[0] = new EventCard("1", subject, body, true);
        String subject1 = "Администрация";
        String body1 = "В этом месяце текущий платеж по аренде необходимо внести до 10 - го числа";
        eventList[1] = new EventCard("1", subject1, body1, false);
        String subject2 = "Коворкинг";
        String body2 = "Картинки-пати! Объявлен день настоящего питона. Заходи пиши код и получай призы!";
        eventList[2] = new EventCard("2", subject2, body2, false);
        String subject3 = "Администрация";
        String body3 = "Задолженность по аренде. Необходимо оплатить в течении 10 рабочих дней.";
        eventList[3] = new EventCard("1", subject3, body3, false);

        AdapterEvent adapterEvent = new AdapterEvent(this);
        ListView eventList1 = (ListView) findViewById(R.id.events);
        eventList1.setAdapter(adapterEvent);
        eventList1.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            public void onItemClick(AdapterView<?> parent, View view, int position, long id){
                switch (position){
                    case 0:
                        Intent intent = new Intent(getApplicationContext(), floor_editor.class);
                        startActivity(intent);
                        break;
                }
                /*for (EventCard item : eventList) {
                    item.setActiv(false);
                    ImageView icon = (ImageView) view.findViewById(R.id.icons);
                    if (item.getActiv()){
                        icon.setImageResource(R.drawable.ic_rect);
                    }else {
                        icon.setImageResource(R.drawable.ic_rect_gray);
                    }
                }
                eventList[position].setActiv(true);
                ImageView icon = (ImageView) view.findViewById(R.id.icons);
                if (eventList[position].getActiv()){
                    icon.setImageResource(R.drawable.ic_rect);
                }else {
                    icon.setImageResource(R.drawable.ic_rect_gray);
                }*/
            }
        });
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

            boolean isActive = eventList[pos].getActiv();
            ImageView icon = (ImageView) item.findViewById(R.id.icons);
            if (isActive){
                icon.setImageResource(R.drawable.ic_rect);
            }else {
                icon.setImageResource(R.drawable.ic_rect_gray);
            }

            return item;
        }
    }
}