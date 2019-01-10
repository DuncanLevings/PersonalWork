/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Environment;
import android.util.Log;
import android.widget.Toast;

import com.projects.duncanlevings.recipeplusv2.DB.DBHandler;
import com.projects.duncanlevings.recipeplusv2.R;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.Scanner;

import static android.content.ContentValues.TAG;

//tasks to download a recipe from web host
public class DownloadFromWeb {
    private static final String DOWNLOAD_JSON_PATH = "https://recipeplus.000webhostapp.com/json/";
    private static final String DOWNLOAD_IMAGE_PATH = "https://recipeplus.000webhostapp.com/images/";
    private String downloadFileName;
    private String recipeName;
    private boolean uploadShown;
    private int serverResponseCode;

    public DownloadFromWeb() {
        this.serverResponseCode = 0;
    }

    //download json file
    public void downloadJSON() {
        downloadFileName = FileInfo.getFileName();
        recipeName = FileInfo.getFileName();
        uploadShown = false;
        DownloadFromWeb.DownloadRecipe downloadRecipe = new DownloadFromWeb.DownloadRecipe();
        downloadRecipe.execute(DOWNLOAD_JSON_PATH, downloadFileName, DOWNLOAD_JSON_PATH);
    }

    //download images from host
    public void downloadImage() {
        if (FileInfo.getImageListSize() < 1) {
            return;
        }

        //array que for multiple images
        String[] imgName = FileInfo.getImage().split("/");
        FileInfo.removeImage();

        downloadFileName = imgName[imgName.length - 1];
        DownloadFromWeb.DownloadRecipe downloadRecipe = new DownloadFromWeb.DownloadRecipe();
        downloadRecipe.execute(DOWNLOAD_IMAGE_PATH, downloadFileName, DOWNLOAD_IMAGE_PATH);
    }

    private class DownloadRecipe extends AsyncTask<String, Void, Integer> {

        @Override
        protected Integer doInBackground(String... strings) {
            try {
                URL url = new URL(strings[0] + strings[1]);

                URLConnection conn = url.openConnection();
                HttpURLConnection httpConn = (HttpURLConnection) conn;

                serverResponseCode = httpConn.getResponseCode();
                if (serverResponseCode == 200) {
                    InputStream is = httpConn.getInputStream();

                    //for images, download to bitmap
                    if (strings[2].equals(DOWNLOAD_IMAGE_PATH)) {
                        BufferedInputStream bufferedInputStream = new BufferedInputStream(is);
                        saveImage(BitmapFactory.decodeStream(bufferedInputStream), strings[1]);
                        return serverResponseCode;
                    }

                    Scanner scanner = new Scanner(is);

                    StringBuilder builder = new StringBuilder();

                    while (scanner.hasNextLine()) {
                        builder.append(scanner.nextLine());
                    }

                    downloadImages(builder.toString());

                    //save to database
                    insertRecipe(builder.toString());

                    httpConn.disconnect();
                    return serverResponseCode;
                }
                else
                {
                    return 0;
                }
            } catch (Exception ex) {
                ex.printStackTrace();
                return 0;
            }
        }

        //insert new recipe to database
        private void insertRecipe(String jsonData) {
            Recipe recipe = new Recipe(jsonData);

            DBHandler dbHandler = new DBHandler();
            dbHandler.insertRecipe(recipe);
            dbHandler.updateLists();
        }

        //saves a bitmap to jpeg file
        private void saveImage(Bitmap bitmap, String fileName) {
            if (bitmap != null) {
                String path =  FileInfo.getContext().getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS).toString();

                String saveImagePath = path + "/" + fileName;

                ImageHandler.saveBitmapToJPEGFile(bitmap, new File(saveImagePath));
            }
        }

        //gets images to download from json object
        private void downloadImages(String jsonData) {
            int size = JSONhandler.getJsonImages(jsonData);
            for (int i = 0; i < size; i++) {
                downloadImage();
            }
        }

        @Override
        protected void onPostExecute(Integer code) {
            if (uploadShown){
                return;
            }

            if (code == 200) {
                uploadShown = true;
                Toast.makeText(FileInfo.getContext().getApplicationContext(),
                        FileInfo.getContext().getResources().getString(
                                R.string.recipe_downloaded_success_text, recipeName),
                        Toast.LENGTH_SHORT).show();
                Log.d(TAG, "Successfully downloaded: " + recipeName);
            }
            else
            {
                Toast.makeText(FileInfo.getContext().getApplicationContext(),
                        FileInfo.getContext().getResources().getString(
                                R.string.recipe_downloaded_failed_text, recipeName),
                        Toast.LENGTH_SHORT).show();
                Log.d(TAG, "Failed to download file: " + recipeName);
            }
        }
    }
}
