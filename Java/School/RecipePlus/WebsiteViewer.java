/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.view.View;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

//using web view to display website within phone
public class WebsiteViewer extends Activity {

    WebView webView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_website_viewer);

        webView = findViewById(R.id.web_view);
        displaySite();
    }

    //when user taps a link, gets file name from link and returns the data
    private void displaySite() {
        if (Build.VERSION.SDK_INT >= 19) {
            webView.setLayerType(View.LAYER_TYPE_HARDWARE, null);
        }
        else {
            webView.setLayerType(View.LAYER_TYPE_SOFTWARE, null);
        }

        WebSettings webSettings = webView.getSettings();
        webSettings.setJavaScriptEnabled(false);
        webView.loadUrl("https://recipeplus.000webhostapp.com/json/");
        webView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                try {
                    String[] separatedURL = url.split("/");
                    String fileName = separatedURL[separatedURL.length - 1];

                    Intent data = new Intent();
                    data.putExtra("fileName", fileName);

                    setResult(RESULT_OK, data);
                    finish();
                } catch (Exception e) {
                    e.printStackTrace();
                }
                return false;
            }
        });
    }

}
