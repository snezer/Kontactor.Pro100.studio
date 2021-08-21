package pro100.studio.contactor.permissions;

import android.Manifest;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.provider.Settings;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import com.google.android.material.snackbar.Snackbar;

import java.util.List;

import pro100.studio.contactor.Models.WifiPoints;
import pro100.studio.contactor.R;

public class Permissions extends AppCompatActivity {
    private static final int PERMISSION_REQUEST_CODE = 123;

    private String[] permissions;
    private Context context;
    public WifiManager wifiManager;
    public WifiScanReceiver wifiReceiver;
    private List<ScanResult> wifiList;
    private WifiPoints[] nets;

    public WifiPoints[] getNets() {
        return nets;
    }

    public Permissions(String[] perms_list, Context context, WifiManager wifiManager){
        permissions = perms_list;
        this.context = context;
        this.wifiManager = wifiManager;
        wifiReceiver = new WifiScanReceiver();
    }

    public boolean hasPermissions(){
        permissions = new  String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                Manifest.permission.ACCESS_FINE_LOCATION,
                Manifest.permission.ACCESS_WIFI_STATE,
                Manifest.permission.CHANGE_WIFI_STATE,
                Manifest.permission.ACCESS_NETWORK_STATE};
        int res = 0;
        //Permissions to access
        for (String perm : permissions){
            try {
                res = context.checkCallingOrSelfPermission(perm);
                if (!(res == PackageManager.PERMISSION_GRANTED)){
                    return false;
                }
            }catch (Exception e){
                Log.d("TAG", "Ошибка" + e.getMessage());
                return false;
            }
        }
        return true;
    }

    private void requestPerms(){
        String[] permissions = new String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                Manifest.permission.ACCESS_FINE_LOCATION,
                Manifest.permission.ACCESS_WIFI_STATE,
                Manifest.permission.CHANGE_WIFI_STATE,
                Manifest.permission.ACCESS_NETWORK_STATE};
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M){
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M){
                try {
                    ActivityCompat.requestPermissions((Activity)context, permissions, PERMISSION_REQUEST_CODE);
                }catch (Exception e){
                    Log.d("tag", e.getMessage());
                }
            }
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        boolean allowed = true;

        switch (requestCode) {
            case PERMISSION_REQUEST_CODE:

                for (int res : grantResults) {
                    // if user granted all permissions.
                    allowed = allowed && (res == PackageManager.PERMISSION_GRANTED);
                }

                break;
            default:
                // if user not granted permissions.
                allowed = false;
                break;
        }

        if (allowed) {
            //user granted all permissions we can perform our task.
            Log.i("TAG", "Start scan...");
            wifiManager.startScan();
        } else {
            // we will give warning to user that they haven't granted permissions.
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {

                if (shouldShowRequestPermissionRationale(permissions[0])) {
                    Toast.makeText(context, "Доступ к WiFi закрыт.", Toast.LENGTH_SHORT).show();

                } else {
                    showNoStoragePermissionSnackbar();
                }
                if (shouldShowRequestPermissionRationale(permissions[1])) {
                    Toast.makeText(context, "Доступ к WiFi закрыт.", Toast.LENGTH_SHORT).show();

                } else {
                    showNoStoragePermissionSnackbar();
                }
                if (shouldShowRequestPermissionRationale(permissions[2])) {
                    Toast.makeText(context, "Доступ к WiFi закрыт.", Toast.LENGTH_SHORT).show();

                } else {
                    showNoStoragePermissionSnackbar();
                }
                if (shouldShowRequestPermissionRationale(permissions[3])) {
                    Toast.makeText(context, "Доступ к WiFi закрыт.", Toast.LENGTH_SHORT).show();

                } else {
                    showNoStoragePermissionSnackbar();
                }
                if (shouldShowRequestPermissionRationale(permissions[4])) {
                    Toast.makeText(context, "Доступ к WiFi закрыт.", Toast.LENGTH_SHORT).show();

                } else {
                    showNoStoragePermissionSnackbar();
                }
            }
        }

    }

    public void showNoStoragePermissionSnackbar() {
        Snackbar.make(((Activity)context).findViewById(R.id.main), "Доступ к WiFi не открыт." , Snackbar.LENGTH_LONG)
                .setAction("SETTINGS", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        openApplicationSettings();

                        Toast.makeText(getApplicationContext(),
                                "Откройте доступ к WiFi ",
                                Toast.LENGTH_SHORT)
                                .show();
                    }
                })
                .show();
    }

    public void openApplicationSettings() {
        Intent appSettingsIntent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS,
                Uri.parse("package:" + getPackageName()));
        startActivityForResult(appSettingsIntent, PERMISSION_REQUEST_CODE);
    }

    public void requestPermissionWithRationale() {
        String[] permissions = new String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                Manifest.permission.ACCESS_FINE_LOCATION,
                Manifest.permission.ACCESS_WIFI_STATE,
                Manifest.permission.CHANGE_WIFI_STATE,
                Manifest.permission.ACCESS_NETWORK_STATE};
        if (ActivityCompat.shouldShowRequestPermissionRationale((Activity)context, permissions[0])) {
            final String message = "Storage permission is needed to show files count";
            Snackbar.make(((Activity)context).findViewById(R.id.main), message, Snackbar.LENGTH_LONG)
                    .setAction("GRANT", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            requestPerms();
                        }
                    })
                    .show();
        } else {
            requestPerms();
        }
        if (ActivityCompat.shouldShowRequestPermissionRationale((Activity)context, permissions[1])) {
            final String message = "Storage permission is needed to show files count";
            Snackbar.make(((Activity)context).findViewById(R.id.main), message, Snackbar.LENGTH_LONG)
                    .setAction("GRANT", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            requestPerms();
                        }
                    })
                    .show();
        } else {
            requestPerms();
        }
        if (ActivityCompat.shouldShowRequestPermissionRationale((Activity)context, permissions[2])) {
            final String message = "Storage permission is needed to show files count";
            Snackbar.make(((Activity)context).findViewById(R.id.main), message, Snackbar.LENGTH_LONG)
                    .setAction("GRANT", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            requestPerms();
                        }
                    })
                    .show();
        } else {
            requestPerms();
        }
        if (ActivityCompat.shouldShowRequestPermissionRationale((Activity)context, permissions[3])) {
            final String message = "Storage permission is needed to show files count";
            Snackbar.make(((Activity)context).findViewById(R.id.main), message, Snackbar.LENGTH_LONG)
                    .setAction("GRANT", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            requestPerms();
                        }
                    })
                    .show();
        } else {
            requestPerms();
        }
        if (ActivityCompat.shouldShowRequestPermissionRationale((Activity)context, permissions[4])) {
            final String message = "Storage permission is needed to show files count";
            Snackbar.make(((Activity)context).findViewById(R.id.main), message, Snackbar.LENGTH_LONG)
                    .setAction("GRANT", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            requestPerms();
                        }
                    })
                    .show();
        } else {
            requestPerms();
        }
    }

    private class WifiScanReceiver extends BroadcastReceiver {
        public void onReceive(Context c, Intent intent) {
            List<ScanResult> wifiScanList = wifiManager.getScanResults();

            nets = new WifiPoints[wifiScanList.size()];

            for(int i = 0; i < wifiScanList.size(); i++){

                String item = ((wifiScanList.get(i)).toString());
                String[] vector_item = item.split(",");
                String item_essid = vector_item[0];

                String item_level = vector_item[3];
                String ssid = item_essid.split(": ")[1];
                String level = item_level.split(": ")[1];
                nets[i] = new WifiPoints(ssid, Double.parseDouble(level));

            }


        }


    }
}
