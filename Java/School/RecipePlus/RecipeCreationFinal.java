/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2;

import android.Manifest;
import android.content.ClipData;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;

import com.projects.duncanlevings.recipeplusv2.DB.DBHandler;
import com.projects.duncanlevings.recipeplusv2.Model.ImageHandler;
import com.projects.duncanlevings.recipeplusv2.Model.JSONhandler;
import com.projects.duncanlevings.recipeplusv2.Model.Recipe;

import java.io.IOException;

//final creation of recipe
public class RecipeCreationFinal extends Activity {

    private static final int TAKE_THUMBNAIL = 1;
    private Uri outputFileUri;
    private ImageView ivMainImage;
    private EditText edtTitle;
    private Spinner spnType;
    private Spinner spnDifficulty;
    private CheckBox chkUpload;
    private Button btnCreateRecipe;

    private Recipe recipe;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recipe_creation_final);

        ivMainImage = findViewById(R.id.iv_main_image);
        edtTitle = findViewById(R.id.edt_recipe_title);
        spnType = findViewById(R.id.spn_recipe_type);
        spnDifficulty = findViewById(R.id.spn_recipe_difficulty);
        chkUpload = findViewById(R.id.chk_upload);
        btnCreateRecipe = findViewById(R.id.btn_create_recipe);

        btnCreateRecipe.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                createRecipe();
            }
        });
        ivMainImage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                takePicture(v);
            }
        });

        getRecipeInstance();
    }

    //get recipe data from previous recipe creation activity
    private void getRecipeInstance() {
        Bundle bundle = getIntent().getExtras();
        recipe = bundle.getParcelable("recipe");
    }

    private void createRecipe() {
        boolean edtTitleEmpty = edtTitle.getText().toString().isEmpty();

        //data validation
        if (edtTitleEmpty) {
            edtTitle.setError("error empty");
            return;
        }

        String imagePath = null;
        boolean hasImage = false;

        //no image set
        if (outputFileUri != null) {
            imagePath = outputFileUri.getPath();
            hasImage = true;

            //clear Uri for next image
            outputFileUri = null;
        }

        //set recipe data
        recipe.setMainImagePath(imagePath);
        recipe.setHasMainImage(hasImage);
        recipe.setRecipeTitle(edtTitle.getText().toString());
        recipe.setType(spnType.getSelectedItemPosition());
        recipe.setDifficulty(spnDifficulty.getSelectedItemPosition() + 1);


        //insert to database
        DBHandler dbHandler = new DBHandler();
        dbHandler.insertRecipe(recipe);

        //upload to site
        if (chkUpload.isChecked()) {
            JSONhandler.createJSON(recipe);
        }

        setResult(RESULT_OK);
        finish();
    }

    //checking for potential issues/crashes with different versions before calling phone intent
    public void takePicture(View view) {
        try {
            if (Build.VERSION.SDK_INT >= 23) {
                int REQUEST_PERMISSION = 100;
                int cameraPermission = this.checkSelfPermission(Manifest.permission.CAMERA);
                if (cameraPermission != PackageManager.PERMISSION_GRANTED) {
                    this.requestPermissions(
                            new String[]{Manifest.permission.CAMERA},
                            REQUEST_PERMISSION
                    );
                }
            }

            Intent pictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
            outputFileUri = ImageHandler.getFileUri();

            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
                pictureIntent.addFlags(Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
            } else {
                ClipData clip = ClipData.newUri(getContentResolver(), "olderVersion", outputFileUri);

                pictureIntent.setClipData(clip);
                pictureIntent.addFlags(Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
            }

            pictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, outputFileUri);

            if (ImageHandler.isIntentHandlerAvaliable(pictureIntent)) {
                startActivityForResult(pictureIntent, TAKE_THUMBNAIL);
                ivMainImage.setClickable(false);
            }
        } catch (Exception e) {
            Log.d("Error in camera: ", e.toString());
            return;
        }
    }

    //saves image from camera intent to bitmap, rotates if needed
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == TAKE_THUMBNAIL) {
            if (resultCode == RESULT_OK) {
                Bitmap bitmapFull = BitmapFactory.decodeFile(outputFileUri.getPath());

                try {
                    bitmapFull = ImageHandler.rotateImageIfRequired(bitmapFull, outputFileUri);
                } catch (IOException e) {
                    e.printStackTrace();
                }

                ivMainImage.setImageBitmap(bitmapFull);
            }
            else //picture was cancelled
            {
                outputFileUri = null;
            }
        }
    }
}
