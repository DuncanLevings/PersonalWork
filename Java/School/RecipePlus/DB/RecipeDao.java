/*
Author: Upma Sharma
 */
package com.projects.duncanlevings.recipeplusv2.DB;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.OnConflictStrategy;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

import com.projects.duncanlevings.recipeplusv2.Model.Recipe;

import java.util.List;

@Dao
public interface RecipeDao {

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    long insert(Recipe recipe);

    @Query("SELECT * FROM recipe_table ORDER BY title")
    List<Recipe> getAllRecipes();

    /*
    @Query("DELETE FROM recipe_table")
    int deleteAll();

    @Delete
    int delete(Recipe recipe);

    @Update
    int update(Recipe recipe);*/
}
