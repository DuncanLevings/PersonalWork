/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.media.ExifInterface;
import android.net.Uri;
import android.os.Build;
import android.os.Environment;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class ImageHandler {

    public static boolean isIntentHandlerAvaliable(Intent intent) {
        PackageManager pm = FileInfo.getContext().getPackageManager();

        return intent.resolveActivity(pm) != null;
    }

    //saves a image file and returns Uri of it
    public static Uri getFileUri() {
        File folder = FileInfo.getContext().getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS);

        if (!folder.canWrite()) {
            Log.e("ImageHandler", "Cannot write to " + folder.toString());
            return  null;
        }

        String fileName = new SimpleDateFormat("yyMMdd_hhmmss", Locale.CANADA).format(new Date()) + ".jpg";

        File file = new File(folder, fileName);
        return Uri.fromFile(file);
    }

    //open file, compress it, save it
    public static void compressFile(String filePath, String fileName, int height, int width) {
        File folder = FileInfo.getContext().getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS);

        if (!folder.canWrite()) {
            Log.e("ImageHandler", "Cannot write to " + folder.toString());
            return;
        }

        Bitmap img = BitmapFactory.decodeFile(filePath + fileName);

        Bitmap out = Bitmap.createScaledBitmap(img, width, height, false);

        File file = new File(folder, fileName);

        try {
            out = rotateImageIfRequired(out, Uri.fromFile(file));
        } catch (IOException e) {
            e.printStackTrace();
        }

        FileOutputStream fOut;
        try {
            fOut = new FileOutputStream(file);
            out.compress(Bitmap.CompressFormat.JPEG, 80, fOut); //# is quality of compression

            fOut.flush();
            fOut.close();

            img.recycle();
            out.recycle();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    //saving a bitmap to jpeg file on external storage
    public static void saveBitmapToJPEGFile(Bitmap tmpBitmap, File fileName) {
        if (tmpBitmap != null) {
            try {

                OutputStream fileOut = new FileOutputStream(fileName);
                tmpBitmap.compress(Bitmap.CompressFormat.JPEG, 100, fileOut);

                if (fileOut != null) {
                    try {
                        fileOut.flush();
                        fileOut.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }

            } catch (FileNotFoundException e) {
                e.printStackTrace();
            }
        }
    }

    //fixes image rotation
    public static Bitmap rotateImageIfRequired(Bitmap img, Uri selectedImage) throws IOException {

        InputStream input = FileInfo.getContext().getContentResolver().openInputStream(selectedImage);
        ExifInterface ei;
        if (Build.VERSION.SDK_INT > 23)
            ei = new ExifInterface(input);
        else
            ei = new ExifInterface(selectedImage.getPath());

        int orientation = ei.getAttributeInt(ExifInterface.TAG_ORIENTATION, ExifInterface.ORIENTATION_NORMAL);

        switch (orientation) {
            case ExifInterface.ORIENTATION_ROTATE_90:
                return rotateImage(img, 90);
            case ExifInterface.ORIENTATION_ROTATE_180:
                return rotateImage(img, 180);
            case ExifInterface.ORIENTATION_ROTATE_270:
                return rotateImage(img, 270);
            case ExifInterface.ORIENTATION_NORMAL:
            default:
                return img;
        }
    }

    //rotate matrix for bitmap
    private static Bitmap rotateImage(Bitmap img, int degree) {
        Matrix matrix = new Matrix();
        matrix.postRotate(degree);
        Bitmap rotatedImg = Bitmap.createBitmap(img, 0, 0, img.getWidth(), img.getHeight(), matrix, true);
        img.recycle();
        return rotatedImg;
    }
}
