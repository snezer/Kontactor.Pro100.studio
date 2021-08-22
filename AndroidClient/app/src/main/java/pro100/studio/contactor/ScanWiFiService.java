package pro100.studio.contactor;

import android.Manifest;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.BroadcastReceiver;
import android.content.IntentFilter;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.IBinder;
import android.sax.Element;
import android.util.Log;
import android.view.View;

import androidx.annotation.Nullable;

import java.util.List;

import pro100.studio.contactor.Models.WifiPoints;
import pro100.studio.contactor.permissions.Permissions;


public class ScanWiFiService extends Service {

    private String[] permissions;
    private Context context;
    private WifiManager wifiManager;
    private WifiScanReceiver wifiReceiver;
    private Element[] elementNets;
    private List<ScanResult> wifiList;
    private WifiPoints[] pointNets;

    public ScanWiFiService() {
        wifiManager = (WifiManager)getSystemService(Context.WIFI_SERVICE);
        wifiReceiver = new WifiScanReceiver();
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId){
        if ((flags & START_FLAG_RETRY) == 0){
            try {
                registerReceiver(
                        wifiReceiver,
                        new IntentFilter(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION)
                );
            }catch (Exception e){
                Log.d("TAG", "Ошибка! " + e.getMessage());
                unregisterReceiver(wifiReceiver);
            }
        }
        else{
            String[] permiss = new String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                    Manifest.permission.ACCESS_FINE_LOCATION,
                    Manifest.permission.ACCESS_WIFI_STATE,
                    Manifest.permission.CHANGE_WIFI_STATE,
                    Manifest.permission.ACCESS_NETWORK_STATE,
                    Manifest.permission.INTERNET};

            Permissions perms = new Permissions(permiss, this.context, wifiManager);
            if (perms.hasPermissions()){
                wifiManager.startScan();

            }else {
                perms.requestPermissionWithRationale();
            }
        }
        return Service.START_STICKY;
    }

    private class WifiScanReceiver extends BroadcastReceiver {
        public void onReceive(Context c, Intent intent) {
            List<ScanResult> wifiScanList = wifiManager.getScanResults();

            pointNets = new WifiPoints[wifiScanList.size()];

            for(int i = 0; i < wifiScanList.size(); i++){

                String item = ((wifiScanList.get(i)).toString());
                String[] vector_item = item.split(",");
                String item_essid = vector_item[0];

                String item_level = vector_item[3];
                String ssid = item_essid.split(": ")[1];
                String level = item_level.split(": ")[1];
                pointNets[i] = new WifiPoints(ssid, level);

            }


        }


    }
}