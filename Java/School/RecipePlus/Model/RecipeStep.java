/*
Author: Duncan Levings
 */
package com.projects.duncanlevings.recipeplusv2.Model;

import android.os.Parcel;
import android.os.Parcelable;

//holds recipe step data
public class RecipeStep implements Parcelable {

    private boolean hasImage;
    private String step;
    private String imagePath;

    public RecipeStep(boolean hasImage, String step, String imagePath) {
        this.hasImage = hasImage;
        this.step = step;
        this.imagePath = imagePath;
    }

    protected RecipeStep(Parcel in) {
        this.hasImage = in.readByte() != 0;
        this.step = in.readString();
        this.imagePath = in.readString();
    }

    public boolean getHasImage() { return hasImage; }

    public String getStep() {
        return step;
    }

    public String getImagePath() {
        return imagePath;
    }

    //PARCELABLE METHODS=====================================================

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeByte((byte) (hasImage ? 1 : 0));
        dest.writeString(step);
        dest.writeString(imagePath);
    }

    @Override
    public int describeContents() {
        return 0;
    }

    public static final Creator<RecipeStep> CREATOR = new Creator<RecipeStep>() {
        @Override
        public RecipeStep createFromParcel(Parcel in) {
            return new RecipeStep(in);
        }

        @Override
        public RecipeStep[] newArray(int size) {
            return new RecipeStep[size];
        }
    };
}
