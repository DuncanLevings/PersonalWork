/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.Ignore;
import android.arch.persistence.room.Index;
import android.arch.persistence.room.PrimaryKey;

import android.os.Parcel;
import android.os.Parcelable;

import java.io.File;
import java.util.ArrayList;

//main recipe object
@Entity(tableName = "recipe_table")
public class Recipe implements Parcelable{

    @PrimaryKey(autoGenerate = true)
    @ColumnInfo(name = "_id")
    private long dbId;

    @ColumnInfo(name = "recipeSteps")
    private ArrayList<RecipeStep> recipeStepData = new ArrayList<>();

    private String mainImagePath;
    private boolean hasMainImage;

    @ColumnInfo(name = "title")
    private String recipeTitle;
    private int difficulty;

    private int type;

    public Recipe(long dbId) {
        this.dbId = dbId;
    }

    @Ignore
    //default constructor
    public Recipe() {
        this.mainImagePath = null;
        this.hasMainImage = false;
        this.recipeTitle = null;
        this.type = 0;
        this.difficulty = 0;
    }

    @Ignore
    //parse json data
    public Recipe(String jsonData) {
        JSONhandler.parseJSON(this, jsonData);
    }

    @Ignore
    protected Recipe(Parcel in) {
        this.mainImagePath = in.readString();
        this.hasMainImage = in.readByte() != 0;
        this.recipeTitle = in.readString();
        this.type = in.readInt();
        this.difficulty = in.readInt();
        this.recipeStepData = in.readArrayList(RecipeStep.class.getClassLoader());
    }

    public void addStepToList(boolean hasImage, String step, String image) {
        recipeStepData.add(new RecipeStep(hasImage, step, image));
    }

    //SETTERS==============

    public void setMainImagePath(String mainImagePath) { this.mainImagePath = mainImagePath; }

    public void setHasMainImage(boolean hasMainImage) { this.hasMainImage = hasMainImage; }

    public void setRecipeTitle(String recipeTitle) { this.recipeTitle = recipeTitle; }

    public void setDifficulty(int difficulty) { this.difficulty = difficulty; }

    public void setType(int type) { this.type = type; }

    public void setDbId(long dbId) { this.dbId = dbId; }

    public void setRecipeStepData(ArrayList<RecipeStep> recipeStepData) {
        this.recipeStepData = recipeStepData;
    }

    //GETTERS==============

    public String getMainImagePath() { return mainImagePath; }

    public boolean getHasMainImage() { return hasMainImage; }

    public String getRecipeTitle() { return recipeTitle; }

    public int getDifficulty() { return difficulty; }

    public int getType() { return type; }

    public long getDbId() { return dbId; }

    public ArrayList<RecipeStep> getRecipeStepData() { return recipeStepData; }

    //PARCELABLE METHODS=====================================================

    public static final Creator<Recipe> CREATOR = new Creator<Recipe>() {
        @Override
        public Recipe createFromParcel(Parcel in) {
            return new Recipe(in);
        }

        @Override
        public Recipe[] newArray(int size) {
            return new Recipe[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(mainImagePath);
        dest.writeByte((byte) (hasMainImage ? 1 : 0));
        dest.writeString(recipeTitle);
        dest.writeInt(type);
        dest.writeInt(difficulty);
        dest.writeList(recipeStepData);
    }
}

