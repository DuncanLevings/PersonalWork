/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.Scanner;

//handles all JSON data
public class JSONhandler {

    //calls upload tasks, including images if any
    public static void uploadRecipe(String title, ArrayList<String> imgUploads) {

        //upload json object to web
        FileInfo.setFileName(title + ".json");
        UploadToWeb uploadToWeb = new UploadToWeb();
        uploadToWeb.uploadJSON(title);

        //upload any images recipe may have to web
        for (int i = 0; i < imgUploads.size(); i ++) {
            FileInfo.addImageName(imgUploads.get(i));
            uploadToWeb.uploadImage();
        }
    }

    //get image path strings
    public static ArrayList<String> imgUpload(Recipe recipe) {
        ArrayList<String> imgToUpload = new ArrayList<>();

        //main image
        if (recipe.getHasMainImage()) {
            imgToUpload.add(recipe.getMainImagePath());
        }

        //step images
        for (RecipeStep step : recipe.getRecipeStepData()) {
            if (step.getHasImage()) {
                imgToUpload.add(step.getImagePath());
            }
        }

        return imgToUpload;
    }

    //create a json object of recipe data
    public static void createJSON(Recipe recipe) {
        //in case function is called outside of intended areas
        if (recipe.getRecipeStepData().size() < 1) {
            return;
        }

        try {
            JSONObject jsonObj = new JSONObject();
            JSONObject jsonData = new JSONObject();

            jsonData.put("hasMainImage", recipe.getHasMainImage());
            jsonData.put("mainImagePath", recipe.getMainImagePath());
            jsonData.put("recipeTitle", recipe.getRecipeTitle());
            jsonData.put("recipeType", recipe.getType());
            jsonData.put("recipeDifficulty", recipe.getDifficulty());

            jsonObj.put("recipeData", jsonData);

            JSONArray jsonStepArr = new JSONArray();

            for (RecipeStep step : recipe.getRecipeStepData()) {
                JSONObject stepObj = new JSONObject();
                stepObj.put("hasStepImage", step.getHasImage());
                stepObj.put("stepImagePath", step.getImagePath());
                stepObj.put("step", step.getStep());
                jsonStepArr.put(stepObj);
            }

            jsonObj.put("stepData", jsonStepArr);

            //saving to external file storage
            saveJSONtoFile(jsonObj.toString(), recipe.getRecipeTitle());
            uploadRecipe(recipe.getRecipeTitle(), imgUpload(recipe));

        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    //create a JSON object and saves to device
    private static void saveJSONtoFile(String jsonData, final String title) {
        try {
            Writer output;
            File file = new File(FileInfo.getFilePath() + title + ".json");
            output = new BufferedWriter(new FileWriter(file));
            output.write(jsonData);
            output.close();

        } catch (Exception e) {
            return;
        }
    }

    //populates recipe object with json data
    public static void parseJSON(Recipe recipe, String jsonData) {
        try {
            JSONObject wrapper = new JSONObject(jsonData);

            JSONObject mainData = wrapper.getJSONObject("recipeData");

            boolean hasImage = mainData.getBoolean("hasMainImage");

            recipe.setHasMainImage(hasImage);
            recipe.setRecipeTitle(mainData.getString("recipeTitle"));

            if (hasImage) {
                recipe.setMainImagePath(mainData.getString("mainImagePath"));
            }

            recipe.setType(mainData.getInt("recipeType"));
            recipe.setDifficulty(mainData.getInt("recipeDifficulty"));

            JSONArray stepArray = wrapper.getJSONArray("stepData");
            String imagePath;

            //gets json step data array
            for (int i = 0; i < stepArray.length(); i++) {
                JSONObject stepData = new JSONObject(stepArray.get(i).toString());

                hasImage = stepData.getBoolean("hasStepImage");
                if (hasImage) {
                    imagePath = stepData.getString("stepImagePath");
                }
                else
                {
                    imagePath = null;
                }

                recipe.addStepToList(hasImage, stepData.getString("step"), imagePath);
            }

        } catch (JSONException e) {
            return;
        }
    }

    //parses json data, adds images to static list for later uploading/downloading
    public static int getJsonImages(String jsonString) {
        try {
            JSONObject wrapper = new JSONObject(jsonString);

            JSONObject data = wrapper.getJSONObject("recipeData");

            boolean hasMainImage = data.getBoolean("hasMainImage");

            if (hasMainImage) {
                FileInfo.addImageName(data.getString("mainImagePath"));
            }

            JSONArray dataArray = wrapper.getJSONArray("stepData");

            for (int i = 0; i < dataArray.length(); i++) {
                JSONObject stepData = new JSONObject(dataArray.get(i).toString());
                boolean hasStepImage = stepData.getBoolean("hasStepImage");

                if (hasStepImage) {
                    FileInfo.addImageName(stepData.getString("stepImagePath"));
                }
            }
            return FileInfo.getImageListSize();
        } catch (JSONException e) {
            return 0;
        }
    }

    //reads json data and returns string of entire json
    private static String readJsonFile(File file){
        Scanner scanner = null;

        try {
            scanner = new Scanner(file);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

        StringBuilder builder = new StringBuilder();

        while (scanner.hasNextLine()) {
            builder.append(scanner.nextLine());
        }

        return builder.toString();
    }

    //delete json object
    public static void deleteJSON(String path, String name) {
        File file = new File(path, name);
        file.delete();
    }
}
