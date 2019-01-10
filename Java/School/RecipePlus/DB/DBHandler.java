/*
Author: Upma Sharma
 */

package com.projects.duncanlevings.recipeplusv2.DB;

import android.os.AsyncTask;
import android.widget.Toast;

import com.projects.duncanlevings.recipeplusv2.Model.FileInfo;
import com.projects.duncanlevings.recipeplusv2.Model.Recipe;
import com.projects.duncanlevings.recipeplusv2.R;

import java.util.List;

//handles all database async tasks
public class DBHandler {


    public void insertRecipe(Recipe recipe) {
        MyInsertTask task = new MyInsertTask();
        task.execute(recipe);
    }

    public void updateLists() {
        MyRetrieveTask myRetrieveTask = new MyRetrieveTask();
        myRetrieveTask.execute();
    }

    private class MyInsertTask extends AsyncTask<Recipe, Void, Long> {

        @Override
        protected Long doInBackground(Recipe... values) {

            RecipeDatabase db = RecipeDatabase.getDatabase(FileInfo.getContext().getApplicationContext());

            RecipeDao dao = db.recipeDao();
            long id = dao.insert(values[0]);

            return id;
        }

        @Override
        protected void onPostExecute(Long aLong) {
            Toast.makeText(FileInfo.getContext(),
                    FileInfo.getContext().getResources().getString(R.string.insert_db_text, aLong),
                    Toast.LENGTH_LONG).show();
        }
    }

    private class MyRetrieveTask extends AsyncTask<Integer, Void, List<Recipe>> {
        @Override
        protected List<Recipe> doInBackground(Integer... values) {

            RecipeDatabase db = RecipeDatabase.getDatabase(FileInfo.getContext().getApplicationContext());

            RecipeDao dao = db.recipeDao();

            List<Recipe> recipes;

            recipes = dao.getAllRecipes();

            return recipes;
        }

        //populate recipe lists with correct type]
        @Override
        protected void onPostExecute(List<Recipe> recipes) {
            FileInfo.clearRecipes();

            for (Recipe recipe : recipes) {
                FileInfo.addRecipe(recipe, recipe.getType());
            }
        }
    }
}
