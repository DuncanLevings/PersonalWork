/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.app.ProgressDialog;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Environment;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.widget.Toast;

import com.projects.duncanlevings.recipeplusv2.R;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import static android.content.ContentValues.TAG;

//tasks to upload json objects and images to web host
public class UploadToWeb {
    private static final String UPLOAD_SERVER = "https://recipeplus.000webhostapp.com/json.php/";
    private static final String UPLOAD_SERVER_IMAGES = "https://recipeplus.000webhostapp.com/images.php/";
    private static final String uploadFilePath = FileInfo.getFilePath();
    private String uploadFileName;
    private String recipeName;
    private int serverResponseCode;
    private boolean uploadShown;

    public UploadToWeb() {
        this.serverResponseCode = 0;
    }

    //uploads json object
    public void uploadJSON(final String recipe) {
        uploadFileName = FileInfo.getFileName();

        //to display toast for only correct file name
        recipeName = recipe;
        uploadShown = false;

        UploadRecipe uploadRecipe = new UploadRecipe();
        uploadRecipe.execute(uploadFilePath + uploadFileName, UPLOAD_SERVER);
    }

    //uploads images using array que
    public void uploadImage() {
        if (FileInfo.getImageListSize() < 1) {
            return;
        }

        String[] imgName = FileInfo.getImage().split("/");
        FileInfo.removeImage();

        uploadFileName = imgName[imgName.length - 1];

        ImageHandler.compressFile(uploadFilePath, uploadFileName, 800, 640);

        UploadRecipe uploadRecipe = new UploadRecipe();
        uploadRecipe.execute(uploadFilePath + uploadFileName, UPLOAD_SERVER_IMAGES);
    }

    private class UploadRecipe extends AsyncTask<String, Void, Integer> {

        @Override
        protected Integer doInBackground(String... params) {

            String sourceFileUri = params[0];
            String fileName = sourceFileUri;

            String lineEnd = "\r\n";
            String twoHyphens = "--";
            String boundary = "*****";
            int bytesRead, bytesAvailable, bufferSize;
            byte[] buffer;
            int maxBufferSize = 1 * 1024 * 1024;
            File sourceFile = new File(sourceFileUri);

            if (!sourceFile.isFile()) {

                Log.e("uploadFile", "Source File not exist :" + uploadFilePath + "" + uploadFileName);

                return 0;

            } else {
                try {
                    // open a URL connection to the Servlet
                    URL url = new URL(params[1]);

                    // Open a HTTP  connection to  the URL
                    HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                    conn.setDoInput(true);
                    conn.setDoOutput(true);
                    conn.setUseCaches(false);
                    conn.setRequestMethod("POST");
                    conn.setRequestProperty("Connection", "Keep-Alive");
                    conn.setRequestProperty("ENCTYPE", "multipart/form-data");
                    conn.setRequestProperty("Content-Type", "multipart/form-data;boundary=" + boundary);
                    conn.setRequestProperty("uploaded_file", fileName);
                    conn.setConnectTimeout(5000);
                    conn.setReadTimeout(5000);

                    FileInputStream fileInputStream = new FileInputStream(sourceFile);
                    DataOutputStream dos = new DataOutputStream(conn.getOutputStream());

                    dos.writeBytes(twoHyphens + boundary + lineEnd);
                    dos.writeBytes("Content-Disposition: form-data; name=\"uploaded_file\";filename=\"" + fileName + "" + lineEnd);

                    dos.writeBytes(lineEnd);

                    // create a buffer of  maximum size
                    bytesAvailable = fileInputStream.available();

                    bufferSize = Math.min(bytesAvailable, maxBufferSize);
                    buffer = new byte[bufferSize];

                    // read file data
                    bytesRead = fileInputStream.read(buffer, 0, bufferSize);

                    while (bytesRead > 0) {

                        dos.write(buffer, 0, bufferSize);
                        bytesAvailable = fileInputStream.available();
                        bufferSize = Math.min(bytesAvailable, maxBufferSize);
                        bytesRead = fileInputStream.read(buffer, 0, bufferSize);

                    }

                    // send multipart form data
                    dos.writeBytes(lineEnd);
                    dos.writeBytes(twoHyphens + boundary + twoHyphens + lineEnd);

                    fileInputStream.close();
                    dos.flush();
                    dos.close();

                    serverResponseCode = conn.getResponseCode();
                    conn.disconnect();
                    return serverResponseCode;

                } catch (MalformedURLException ex) {
                    ex.printStackTrace();

                    Log.e("Upload file to server", "error: " + ex.getMessage(), ex);
                } catch (java.net.SocketTimeoutException e) {
                    Log.e("Upload server Timeout", "Web server host down");
                    return 0;
                } catch (Exception e) {
                    e.printStackTrace();

                    Log.e("Upload server Exception", "Exception : "
                            + e.getMessage(), e);
                }

                return 0;
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
                                R.string.recipe_uploaded_success_text, recipeName),
                        Toast.LENGTH_SHORT).show();
                Log.d(TAG, "Uploaded file: " + uploadFileName);

                //delete json object after finished uploading
                //JSONhandler.deleteJSON(uploadFilePath, uploadFileName);
            }
            else
            {
                Toast.makeText(FileInfo.getContext().getApplicationContext(),
                        FileInfo.getContext().getResources().getString(
                                R.string.recipe_downloaded_failed_text, recipeName),
                        Toast.LENGTH_SHORT).show();
                Log.d(TAG, "Failed uploaded file: " + uploadFileName);
            }
        }
    }
}
