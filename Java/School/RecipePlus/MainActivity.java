/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;

import com.projects.duncanlevings.recipeplusv2.DB.DBHandler;
import com.projects.duncanlevings.recipeplusv2.Model.DownloadFromWeb;
import com.projects.duncanlevings.recipeplusv2.Model.FileInfo;


public class MainActivity extends Activity {

    private static final int CREATION_ACTIVITY = 1;
    private static final int WEBSITE_ACTIVITY = 2;

    private LinearLayout btnBreakfastRecipes;
    private LinearLayout btnLunchRecipes;
    private LinearLayout btnDinnerRecipes;
    private Button btnCreate;
    private Button btnDownload;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        btnBreakfastRecipes = findViewById(R.id.btn_breakfast);
        btnLunchRecipes = findViewById(R.id.btn_lunch);
        btnDinnerRecipes = findViewById(R.id.btn_dinner);
        btnCreate = findViewById(R.id.btn_create_new);
        btnDownload = findViewById(R.id.btn_download);

        //setting up all listeners
        ButtonListener buttonListener = new ButtonListener();
        btnBreakfastRecipes.setOnClickListener(buttonListener);
        btnLunchRecipes.setOnClickListener(buttonListener);
        btnDinnerRecipes.setOnClickListener(buttonListener);
        btnCreate.setOnClickListener(buttonListener);
        btnDownload.setOnClickListener(buttonListener);

        FileInfo.setContext(this);
        updateRecipeLists();
    }

    private class ButtonListener implements View.OnClickListener {
        @Override
        public void onClick(View view) {
            switch (view.getId()) {
                case R.id.btn_breakfast:
                    displayRecipes(0);
                    break;
                case R.id.btn_lunch:
                    displayRecipes(1);
                    break;
                case R.id.btn_dinner:
                    displayRecipes(2);
                    break;
                case R.id.btn_create_new:
                    create();
                    break;
                case R.id.btn_download:
                    getFileToDownload();
                    break;
            }
        }
    }

    //updates lists from database
    private void updateRecipeLists() {
        DBHandler dbHandler = new DBHandler();
        dbHandler.updateLists();
    }

    //display activity with correct list type
    private void displayRecipes(int type) {
        Intent intentView = new Intent(getApplicationContext(), ViewRecipeTypes.class);

        Bundle bundle = new Bundle();
        bundle.putParcelableArrayList("recipes", FileInfo.getRecipeList(type));
        intentView.putExtras(bundle);

        startActivity(intentView);
    }

    //intent to create a new recipe
    private void create() {
        Intent intentCreate = new Intent(getApplicationContext(), RecipeCreationStart.class);
        startActivityForResult(intentCreate, CREATION_ACTIVITY);
    }

    //intent to download recipe from web
    private void getFileToDownload() {
        Intent intentDownload = new Intent(getApplicationContext(), WebsiteViewer.class);
        startActivityForResult(intentDownload, WEBSITE_ACTIVITY);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == WEBSITE_ACTIVITY && resultCode == RESULT_OK) {
            //download from website
            FileInfo.setFileName(data.getStringExtra("fileName"));
            DownloadFromWeb download = new DownloadFromWeb();
            download.downloadJSON();
        } else if (requestCode == CREATION_ACTIVITY && resultCode == RESULT_OK) {
            //successful creation of new recipe, update database
            updateRecipeLists();
        }
    }
}

