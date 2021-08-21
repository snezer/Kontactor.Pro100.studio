package pro100.studio.contactor;

import androidx.appcompat.app.AppCompatActivity;

import android.Manifest;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import java.util.List;

import pro100.studio.contactor.Models.Element;
import pro100.studio.contactor.permissions.Permissions;

public class WiFiSettings extends AppCompatActivity {

    private Button btnStartSkan;
    private ImageView btnBack;
    private Button btnSave;
    private WifiManager wifiManager;
    //private WifiScanReceiver wifiReceiver;
    private Element[] nets;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_wi_fi_settings);

        wifiManager = (WifiManager)getSystemService(Context.WIFI_SERVICE);
        //wifiReceiver = new WifiScanReceiver();

        btnStartSkan = (Button) findViewById(R.id.button);
        btnStartSkan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                String[] permiss = new String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                        Manifest.permission.ACCESS_FINE_LOCATION,
                        Manifest.permission.ACCESS_WIFI_STATE,
                        Manifest.permission.CHANGE_WIFI_STATE,
                        Manifest.permission.ACCESS_NETWORK_STATE,
                        Manifest.permission.INTERNET};

                Permissions perms = new Permissions(permiss, view.getContext(), wifiManager);
                if (perms.hasPermissions()) {
                    BroadcastReceiver wifiScanReceiver = new BroadcastReceiver() {
                        @Override
                        public void onReceive(Context context, Intent intent) {
                            boolean success= intent.getBooleanExtra(WifiManager.EXTRA_RESULTS_UPDATED, false);
                            if (success){
                                scanSuccess(context);
                            }else{
                                scanFailture();
                            }
                        }
                    };

                    IntentFilter intentFilter = new IntentFilter();
                    intentFilter.addAction(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION);
                    registerReceiver(wifiScanReceiver, intentFilter);
                    boolean success = wifiManager.startScan();
                    if (!success){
                        scanFailture();
                    }
                } else {
                    perms.requestPermissionWithRationale();
                }
            }
        });

        btnBack = (ImageView) findViewById(R.id.back);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), floor_editor.class);
                startActivity(intent);
            }
        });
    }

    private void scanSuccess(Context c){
        List<ScanResult> wifiScanList = wifiManager.getScanResults();
        nets = new Element[wifiScanList.size()];

        for(int i = 0; i < wifiScanList.size(); i++){

            String item = ((wifiScanList.get(i)).toString());
            String[] vector_item = item.split(",");
            String item_essid = vector_item[0];

            String item_level = vector_item[3];
            String ssid = "";
            int length = item_essid.split(": ").length;
            if (length == 2){
                ssid = item_essid.split(": ")[1];
            }

            String level = item_level.split(": ")[1];
            nets[i] = new Element(ssid, level);

        }

        AdapterElements adapterElements = new AdapterElements((Activity)c);
        ListView netList = (ListView) findViewById(R.id.WiFiItem);
        netList.setAdapter(adapterElements);
    }

    private void scanFailture(){

    }

    /*private class WifiScanReceiver extends BroadcastReceiver {
        public void onReceive(Context c, Intent intent) {
            List<ScanResult> wifiScanList = wifiManager.getScanResults();

            nets = new Element[wifiScanList.size()];

            for(int i = 0; i < wifiScanList.size(); i++){

                String item = ((wifiScanList.get(i)).toString());
                String[] vector_item = item.split(",");
                String item_essid = vector_item[0];

                String item_level = vector_item[3];
                String ssid = item_essid.split(": ")[1];
                String level = item_level.split(": ")[1];
                nets[i] = new Element(ssid, level);

            }

            AdapterElements adapterElements = new AdapterElements((Activity)c);
            ListView netList = (ListView) findViewById(R.id.WiFiItem);
            netList.setAdapter(adapterElements);
        }
    }*/

    class AdapterElements extends ArrayAdapter<Object> {
        Activity context;

        public AdapterElements(Activity context) {
            super(context, R.layout.layout_wifi_item, nets);
            this.context = context;
        }

        public View getView(int position, View convertView, ViewGroup parent){
            try {
                LayoutInflater inflater = context.getLayoutInflater();
                View item = inflater.inflate(R.layout.layout_wifi_item, null);

                TextView tvSsid = (TextView) item.findViewById(R.id.tvSSID);
                tvSsid.setText(nets[position].getTitle());


                TextView tvLevel = (TextView)item.findViewById(R.id.tvLevel);
                tvLevel.setText(nets[position].getLevel());
                return item;
            }catch (Exception e){

                Log.d("TAG", "Ошибка! " + e.getMessage());
                return new View(context);

            }
        }
    }
}