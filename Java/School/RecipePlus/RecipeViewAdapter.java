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
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.projects.duncanlevings.recipeplusv2.Model.FileInfo;
import com.projects.duncanlevings.recipeplusv2.Model.RecipeStep;

import java.util.ArrayList;

//recycler for displaying a recipes steps
public class RecipeViewAdapter extends RecyclerView.Adapter<RecipeViewAdapter.MyViewHolder> {

    ArrayList<RecipeStep> mRecipeSteps;

    public RecipeViewAdapter(ArrayList<RecipeStep> recipeSteps) {
        this.mRecipeSteps = recipeSteps;
    }

    public static class MyViewHolder extends RecyclerView.ViewHolder {
        private TextView mTxtStep;
        private ImageView mIvStepImage;

        public MyViewHolder(CardView view) {
            super(view);
            mTxtStep = view.findViewById(R.id.txt_step);
            mIvStepImage = view.findViewById(R.id.iv_step_image);
        }
    }

    @NonNull
    @Override
    public RecipeViewAdapter.MyViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        CardView view = (CardView) LayoutInflater.from(parent.getContext())
                .inflate(R.layout.recipe_view_layout, parent, false);

        RecipeViewAdapter.MyViewHolder viewHolder = new RecipeViewAdapter.MyViewHolder(view);

        return viewHolder;
    }

    //displays each step from recipe step data
    @Override
    public void onBindViewHolder(@NonNull RecipeViewAdapter.MyViewHolder holder, int position) {
        if (!mRecipeSteps.isEmpty()) {
            holder.mTxtStep.setText(FileInfo.getContext().getResources().
                            getString(R.string.recipe_step_text, (position + 1),
                    mRecipeSteps.get(position).getStep()));

            if (mRecipeSteps.get(position).getHasImage()) {
                Bitmap bitmapFull = BitmapFactory.decodeFile(mRecipeSteps.get(position).getImagePath());

                if (bitmapFull != null) {
                    holder.mIvStepImage.setImageBitmap(bitmapFull);
                }
            } else {
                //reset image view and hide it from UI
                holder.mIvStepImage.setImageBitmap(null);
                holder.mIvStepImage.setBackgroundResource(R.drawable.ic_launcher_background);
                holder.mIvStepImage.setVisibility(View.GONE);
            }
        }
    }

    @Override
    public int getItemCount() {
        return mRecipeSteps.size();
    }
}
