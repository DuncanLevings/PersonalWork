/*
Author: Upma Sharma
 */
package com.projects.duncanlevings.recipeplusv2.DB;

import android.arch.persistence.room.TypeConverter;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.projects.duncanlevings.recipeplusv2.Model.RecipeStep;

import java.lang.reflect.Type;
import java.util.ArrayList;

//converts RecipeStepData into string for storing in Room
public class StepConverter {

    //converts the gson string back into recipe array list
    @TypeConverter
    public static ArrayList<RecipeStep> fromString(String value) {
        Gson gson = new Gson();
        Type listType = new TypeToken<ArrayList<RecipeStep>>() {}.getType();
        ArrayList<RecipeStep> recipeSteps = gson.fromJson(value, listType);
        return recipeSteps;
    }

    //converts the list to string
    @TypeConverter
    public static String fromArrayList(ArrayList<RecipeStep> list) {
        Gson gson = new Gson();
        String json = gson.toJson(list);
        return json;
    }
}