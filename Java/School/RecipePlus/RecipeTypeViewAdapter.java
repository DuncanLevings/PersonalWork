/*
Author: Upma Sharma
 */
package com.projects.duncanlevings.recipeplusv2;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.projects.duncanlevings.recipeplusv2.Model.FileInfo;
import com.projects.duncanlevings.recipeplusv2.Model.Recipe;

import java.util.ArrayList;

//displaying recipes of type
public class RecipeTypeViewAdapter extends RecyclerView.Adapter<RecipeTypeViewAdapter.MyViewHolder> {

    ArrayList<Recipe> mRecipes;

    public RecipeTypeViewAdapter(ArrayList<Recipe> recipes) {
        this.mRecipes = recipes;
    }

    public static class MyViewHolder extends RecyclerView.ViewHolder {
        private CardView cvRecipe;
        private TextView mTxtTitle;
        private TextView mTxtDifficulty;
        private ImageView mIvPreview;

        public MyViewHolder(CardView view) {
            super(view);
            cvRecipe = view;
            mTxtTitle = view.findViewById(R.id.txt_title);
            mTxtDifficulty = view.findViewById(R.id.txt_difficulty);
            mIvPreview = view.findViewById(R.id.iv_preview);
        }
    }

    @NonNull
    @Override
    public RecipeTypeViewAdapter.MyViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        CardView view = (CardView) LayoutInflater.from(parent.getContext())
                .inflate(R.layout.recipes_types_view_layout, parent, false);

        RecipeTypeViewAdapter.MyViewHolder viewHolder = new RecipeTypeViewAdapter.MyViewHolder(view);

        return viewHolder;
    }

    //displays recipe title, difficulty and main image on recycler
    @Override
    public void onBindViewHolder(@NonNull RecipeTypeViewAdapter.MyViewHolder holder, final int position) {
        if (!mRecipes.isEmpty()) {

            //click listener on each item in the recycler view, creates new intent with that recipes data
            holder.cvRecipe.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intentView = new Intent(FileInfo.getContext().getApplicationContext(), ViewRecipe.class);

                    Bundle bundle = new Bundle();
                    bundle.putParcelable("recipe", mRecipes.get(position));
                    intentView.putExtras(bundle);

                    FileInfo.getContext().startActivity(intentView);
                }
            });

            holder.mTxtTitle.setText(mRecipes.get(position).getRecipeTitle() + " : " + mRecipes.get(position).getType());
            holder.mTxtDifficulty.setText("Difficulty: " + mRecipes.get(position).getDifficulty());

            if (mRecipes.get(position).getHasMainImage()) {
                Bitmap bitmapFull = BitmapFactory.decodeFile(mRecipes.get(position).getMainImagePath());

                if (bitmapFull != null) {
                    holder.mIvPreview.setImageBitmap(bitmapFull);
                }
            } else {
                holder.mIvPreview.setImageBitmap(null);
                holder.mIvPreview.setBackgroundResource(R.drawable.ic_launcher_background);
            }
        }
    }

    @Override
    public int getItemCount() {
        return mRecipes.size();
    }
}
