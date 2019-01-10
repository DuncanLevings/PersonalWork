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
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.provider.MediaStore;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;

import com.projects.duncanlevings.recipeplusv2.Model.ImageHandler;
import com.projects.duncanlevings.recipeplusv2.Model.Recipe;

import java.io.IOException;

//initial creation of recipe
public class RecipeCreationStart extends Activity {

    private static final int FINISH_ACTIVITY = 1;
    private static final int TAKE_PICTURE = 2;
    private Uri outputFileUri;
    private RecyclerView mRecyclerView;
    private RecipeCreationAdapter adapter;
    private RecyclerView.LayoutManager manager;
    private Recipe recipe;
    private Button btnAdd;
    private Button btnRemove;
    private Button btnFinish;
    private EditText edtStep;
    private ImageView ivStepImage;
    private Drawable defaultImg;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recipe_creation_start);

        mRecyclerView = findViewById(R.id.recycler_view);
        btnAdd = findViewById(R.id.btn_add);
        btnFinish = findViewById(R.id.btn_finish);
        btnRemove = findViewById(R.id.btn_remove);
        edtStep = findViewById(R.id.edtStep);
        ivStepImage = findViewById(R.id.ivStepImage);
        defaultImg = ivStepImage.getDrawable();

        ButtonListener buttonListener = new ButtonListener();
        btnAdd.setOnClickListener(buttonListener);
        btnFinish.setOnClickListener(buttonListener);
        btnRemove.setOnClickListener(buttonListener);
        ivStepImage.setOnClickListener(buttonListener);

        recipe = new Recipe();
        displayData();
        enableDisable(false);
    }

    private class ButtonListener implements View.OnClickListener {
        @Override
        public void onClick(View view) {
            switch (view.getId()) {
                case R.id.btn_add:
                    addStep();
                    break;
                case R.id.btn_finish:
                    finishRecipe();
                    break;
                case R.id.btn_remove:
                    removeStep();
                    break;
                case R.id.ivStepImage:
                    takePicture(view);
                    break;
            }
        }
    }

    private void enableDisable(Boolean val) {
        btnRemove.setEnabled(val);
        btnFinish.setEnabled(val);
    }

    private void clearFields() {
        edtStep.setText(null);
    }

    private void addStep() {
        boolean edtStepEmpty = edtStep.getText().toString().isEmpty();

        //data validation
        if (edtStepEmpty) {
            edtStep.setError("error empty");
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

        recipe.addStepToList(hasImage, edtStep.getText().toString(), imagePath);

        adapter.notifyDataSetChanged();

        //resetting for new step
        ivStepImage.setClickable(true);
        ivStepImage.setImageDrawable(defaultImg);
        enableDisable(true);
        clearFields();
    }

    //removes step from list and updates adapter
    private void removeStep() {
        int index = recipe.getRecipeStepData().size();
        if (index > 0) {
            recipe.getRecipeStepData().remove(index - 1);
            adapter.notifyItemRemoved(index - 1);

            if (recipe.getRecipeStepData().size() == 0) {
                enableDisable(false);
            }
        }
    }

    //start final recipe creation activity for result
    private void finishRecipe() {
        Intent intentCreateFinal = new Intent(getApplicationContext(), RecipeCreationFinal.class);

        Bundle bundle = new Bundle();
        bundle.putParcelable("recipe", recipe);
        intentCreateFinal.putExtras(bundle);

        startActivityForResult(intentCreateFinal, FINISH_ACTIVITY);
    }

    //recycler data displaying
    private void displayData() {
        mRecyclerView.setHasFixedSize(false);

        manager = new LinearLayoutManager(this,
                LinearLayoutManager.VERTICAL, false);

        mRecyclerView.setLayoutManager(manager);

        adapter = new RecipeCreationAdapter(recipe.getRecipeStepData());
        mRecyclerView.setAdapter(adapter);
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
                startActivityForResult(pictureIntent, TAKE_PICTURE);
                ivStepImage.setClickable(false);
            }
        } catch (Exception e) {
            Log.d("Error in camera: ", e.toString());
            return;
        }
    }

    //saves image from camera intent to bitmap, rotates if needed
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == TAKE_PICTURE) {
            if (resultCode == RESULT_OK) {
                Bitmap bitmapFull = BitmapFactory.decodeFile(outputFileUri.getPath());

                try {
                    bitmapFull = ImageHandler.rotateImageIfRequired(bitmapFull, outputFileUri);
                } catch (IOException e) {
                    e.printStackTrace();
                }

                ivStepImage.setImageBitmap(bitmapFull);
            }
            else //picture was cancelled
            {
                outputFileUri = null;
            }
        }
        else if (requestCode == FINISH_ACTIVITY && resultCode == RESULT_OK) {
            //if created successfully, return to main activity with ok result
            setResult(RESULT_OK);
            finish();
        }
    }
}
