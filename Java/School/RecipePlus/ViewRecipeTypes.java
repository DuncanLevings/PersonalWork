/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2;

import android.app.Activity;
import android.os.Bundle;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;

import com.projects.duncanlevings.recipeplusv2.Model.Recipe;

import java.util.ArrayList;

public class ViewRecipeTypes extends Activity {

    private ArrayList<Recipe> recipes = new ArrayList<>();

    private RecyclerView mRecyclerView;
    private RecipeTypeViewAdapter adapter;
    private RecyclerView.LayoutManager manager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_recipe_types);

        mRecyclerView = findViewById(R.id.recycler_view_recipes);

        getRecipes();
        displayData();
    }

    //get recipe list from main activity intent
    public void getRecipes() {
        Bundle bundle = getIntent().getExtras();
        recipes = bundle.getParcelableArrayList("recipes");
    }

    //recycler view displaying data
    private void displayData() {
        mRecyclerView.setHasFixedSize(false);

        manager = new LinearLayoutManager(this,
                LinearLayoutManager.VERTICAL, false);

        mRecyclerView.setLayoutManager(manager);

        adapter = new RecipeTypeViewAdapter(recipes);
        mRecyclerView.setAdapter(adapter);
    }
}
