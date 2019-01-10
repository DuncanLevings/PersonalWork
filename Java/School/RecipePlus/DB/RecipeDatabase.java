/*
Author: Upma Sharma
 */
package com.projects.duncanlevings.recipeplusv2.DB;

import android.arch.persistence.room.Database;
import android.arch.persistence.room.Room;
import android.arch.persistence.room.RoomDatabase;
import android.arch.persistence.room.TypeConverters;
import android.content.Context;


import com.projects.duncanlevings.recipeplusv2.Model.Recipe;


@Database(entities = {Recipe.class}, version = 1, exportSchema = false)
@TypeConverters(StepConverter.class)
public abstract class RecipeDatabase extends RoomDatabase{

    private static RecipeDatabase INSTANCE;

    public abstract RecipeDao recipeDao();

    public static RecipeDatabase getDatabase(Context context) {
        if (INSTANCE == null) {
            INSTANCE = Room.databaseBuilder(context, RecipeDatabase.class, "recipe_db").build();
        }

        return INSTANCE;
    }
}


