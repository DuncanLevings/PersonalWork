/**
 * Author:    Duncan Levings
 * Title:     Assignment 2
 *
 * Purpose: Quiz app with saved instances, orientation changes, high score functions, Bundles
 **/

package com.projects.duncanlevings.quizexercise.model;

import android.content.res.Resources;
import android.os.Parcel;
import android.os.Parcelable;

import com.projects.duncanlevings.quizexercise.R;

import java.util.ArrayList;

//class implements Parcelable to enable saving it to bundle
public class Question implements Parcelable{

    private String p_question_text;
    private boolean p_answer;

    public Question (String question, boolean answer)
    {
        this.p_question_text = question;
        this.p_answer = answer;
    }

    //Parcelable constructor
    private Question(Parcel in) {
        this.p_question_text = in.readString();
        //retrieving the byte 1 or 0 and assigning to boolean p_answer
        this.p_answer = in.readByte() == 1;
    }

    public String getQuestion()
    {
        return p_question_text;
    }

    public boolean getAnswer()
    {
        return p_answer;
    }

    //Returns an ArrayList of Question objects, populated with question strings and boolean values
    public static ArrayList<Question> getQuestions(Resources resources)
    {
        //getting the question string array from strings.xml
        String[] questionsArray = resources.getStringArray(R.array.questions_array);
        ArrayList<Question> questions = new ArrayList<>();

        questions.add(new Question(questionsArray[0], false));
        questions.add(new Question(questionsArray[1], true));
        questions.add(new Question(questionsArray[2], true));
        questions.add(new Question(questionsArray[3], false));
        questions.add(new Question(questionsArray[4], false));

        return questions;
    }

    //Parcelable required function
    @Override
    public int describeContents() {
        return 0;
    }

    //Parcelable required function to write needed data for constructor to grab
    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(p_question_text);
        //since Parcel does not have writeBoolean, instead writeByte to store the boolean as 1 (true) or 0 (false)
        dest.writeByte((byte) (p_answer ? 1 : 0));
    }

    //Parcelable required function
    public static final Parcelable.Creator<Question> CREATOR = new Parcelable.Creator<Question>() {
        public Question createFromParcel(Parcel in) {
            return new Question(in);
        }

        public Question[] newArray(int size) {
            return new Question[size];
        }
    };
}
