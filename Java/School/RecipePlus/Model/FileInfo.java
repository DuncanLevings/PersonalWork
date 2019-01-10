/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.content.Context;
import android.os.Environment;

import java.util.ArrayList;

//class with static variables holding important data
public class FileInfo {

    private static Context context;
    private static String fileName;
    private static ArrayList<String> imgNames = new ArrayList<>();
    private static ArrayList<Recipe> breakfastRecipes = new ArrayList<>();
    private static ArrayList<Recipe> lunchRecipes = new ArrayList<>();
    private static ArrayList<Recipe> dinnerRecipes = new ArrayList<>();

    public static void setContext(Context con) { context = con ;}

    public static void setFileName(String name) { fileName = name; }

    public static void addImageName(String name) { imgNames.add(name); }

    //mostly used to get a reference to MainActivity
    public static Context getContext() { return context; }

    //file path for external files
    public static String getFilePath() {
        if (context == null) {
            return null;
        }
        return context.getExternalFilesDir(Environment.DIRECTORY_DOWNLOADS).getPath() + "/";
    }

    public static String getFileName() {
        if (fileName == null) {
            return null;
        }
        return fileName;
    }

    //img que, returns always first img
    public static String getImage() {
        if (imgNames.size() < 1) {
            return null;
        }
        return imgNames.get(0);
    }

    public static int getImageListSize() {
        return imgNames.size();
    }

    //img que, removes always first img
    public static void removeImage() {
        if (imgNames.size() > 0) {
            imgNames.remove(0);
        }
    }

    //to get proper list based on type
    public static ArrayList<Recipe> getRecipeList(int type) {
        switch (type) {
            case 0:
                return breakfastRecipes;
            case 1:
                return lunchRecipes;
            case 2:
                return dinnerRecipes;
            default:
                return null;
        }
    }

    //adding to list based on type
    public static void addRecipe(Recipe recipe, int type) {
        switch (type) {
            case 0:
                breakfastRecipes.add(recipe);
                break;
            case 1:
                lunchRecipes.add(recipe);
                break;
            case 2:
                dinnerRecipes.add(recipe);
                break;
            default:
                break;
        }
    }

    //clears the lists before re-updating them
    public static void clearRecipes() {
        breakfastRecipes.clear();
        lunchRecipes.clear();
        dinnerRecipes.clear();
    }
}
