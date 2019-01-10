/*
Author: Upma Sharma
 */
package com.projects.duncanlevings.recipeplusv2;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.support.annotation.NonNull;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.projects.duncanlevings.recipeplusv2.Model.RecipeStep;

import java.util.ArrayList;

public class RecipeCreationAdapter extends RecyclerView.Adapter<RecipeCreationAdapter.MyViewHolder> {

    ArrayList<RecipeStep> mRecipeSteps;

    public RecipeCreationAdapter(ArrayList<RecipeStep> recipeSteps) {
        this.mRecipeSteps = recipeSteps;
    }

    public static class MyViewHolder extends RecyclerView.ViewHolder {
        private TextView mTxtStep;
        private ImageView mIvImage;

        public MyViewHolder(CardView view) {
            super(view);
            mTxtStep = view.findViewById(R.id.txt_step);
            mIvImage = view.findViewById(R.id.iv_image);
        }
    }

    @NonNull
    @Override
    public RecipeCreationAdapter.MyViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        CardView view = (CardView) LayoutInflater.from(parent.getContext())
                .inflate(R.layout.recipe_creation_layout, parent, false);

        MyViewHolder viewHolder = new MyViewHolder(view);

        return viewHolder;
    }

    //displays recipe step data, and image if there is one
    @Override
    public void onBindViewHolder(@NonNull RecipeCreationAdapter.MyViewHolder holder, int position) {
        if (!mRecipeSteps.isEmpty()) {
            holder.mTxtStep.setText(mRecipeSteps.get(position).getStep());

            if (mRecipeSteps.get(position).getHasImage()) {
                Bitmap bitmapFull = BitmapFactory.decodeFile(mRecipeSteps.get(position).getImagePath());

                if (bitmapFull != null) {
                    Matrix matrix = new Matrix();
                    matrix.postRotate(90);
                    bitmapFull = Bitmap.createBitmap(bitmapFull, 0, 0, bitmapFull.getWidth(), bitmapFull.getHeight(),
                            matrix, true);

                    holder.mIvImage.setImageBitmap(bitmapFull);
                }
            } else {
                holder.mIvImage.setImageBitmap(null);
                holder.mIvImage.setBackgroundResource(R.drawable.ic_launcher_background);
            }
        }
    }

    @Override
    public int getItemCount() {
        return mRecipeSteps.size();
    }
}
