/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2;

import android.os.Bundle;
import android.app.Activity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Button;

import com.projects.duncanlevings.recipeplusv2.Model.Recipe;
import com.projects.duncanlevings.recipeplusv2.Model.UploadToWeb;

public class ViewRecipe extends Activity {

    private Recipe recipe;
    private RecyclerView mRecyclerView;
    private RecipeViewAdapter adapter;
    private RecyclerView.LayoutManager manager;

    Button btnUpload;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_recipe);

        mRecyclerView = findViewById(R.id.recycler_view);

        btnUpload = findViewById(R.id.btn_upload);

        //disabled
        btnUpload.setVisibility(View.GONE);

        btnUpload.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                upload();
            }
        });

        getRecipe();
        displayData();
    }

    //not implemented from time constraints and potential issues
    private void upload() {
        //upload to website if user did not on creation
    }

    //get recipe data
    public void getRecipe() {
        Bundle bundle = getIntent().getExtras();
        recipe = bundle.getParcelable("recipe");
    }

    //recycler view displaying data
    private void displayData() {
        mRecyclerView.setHasFixedSize(false);

        manager = new LinearLayoutManager(this,
                LinearLayoutManager.VERTICAL, false);

        mRecyclerView.setLayoutManager(manager);

        adapter = new RecipeViewAdapter(recipe.getRecipeStepData());
        mRecyclerView.setAdapter(adapter);
    }
}
