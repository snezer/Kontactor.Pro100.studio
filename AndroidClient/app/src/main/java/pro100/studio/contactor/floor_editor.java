package pro100.studio.contactor;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.view.View;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.ImageView;

public class floor_editor extends AppCompatActivity {


    private WebView webView;
    private Button btnDemo;
    private ImageView btnBack;

    private String FLOOR1 = "";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_floor_editor);

        webView = (WebView) findViewById(R.id.wv);
        btnDemo = (Button) findViewById(R.id.demo);
        btnDemo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), WiFiDemoOffise.class);
                startActivity(intent);
            }
        });
        btnBack = (ImageView) findViewById(R.id.back);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Intent intent = new Intent(getApplicationContext(), EventActivity.class);
                startActivity(intent);
            }
        });

        LoadData();
    }

    private void LoadData(){
        //webView.loadData(FLOOR1, "text/html", "UTF-8");
        webView.loadUrl("https://kreolz.github.io/#/publicmap");
        webView.setBackgroundColor(Color.TRANSPARENT);
        webView.setLayerType(WebView.LAYER_TYPE_SOFTWARE, null);
        webView.getSettings().setBuiltInZoomControls(true);
        webView.getSettings().setLoadWithOverviewMode(true);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.getSettings().setCacheMode(WebSettings.LOAD_CACHE_ELSE_NETWORK);
        webView.setWebViewClient(new WebViewClient());
    }
}