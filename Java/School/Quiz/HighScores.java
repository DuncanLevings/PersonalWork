/**
 * Author:    Duncan Levings
 * Title:     Assignment 2
 *
 * Purpose: Quiz app with saved instances, orientation changes, high score functions, Bundles
 **/

package com.projects.duncanlevings.quizexercise;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

public class HighScores extends Activity {

    private Button btnReturn, btnSubmitScore;
    private TextView txtHighScoreList;
    private EditText edtNameSubmit;
    private ButtonListener buttonListener;
    private String submittedName;
    private float scorePercent;
    private ArrayList<String> scoresList = new ArrayList<>();

    private static final String BUTTON_SUBMIT = "buttonSubmit";
    private static final String SUBMIT_ENABLED = "submitEnabled";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_high_scores);

        setUp();
    }

    //initial setup of the HighScores activity
    public void setUp()
    {
        btnReturn = findViewById(R.id.btn_return);
        btnSubmitScore = findViewById(R.id.btn_submit_high_score);
        edtNameSubmit = findViewById(R.id.edt_name_high_score);
        txtHighScoreList = findViewById(R.id.txt_high_scores);

        //gets score percent value from bundle, passed from MainActivity
        Bundle bundle = getIntent().getExtras();
        scorePercent = bundle.getFloat("scorePercent");

        buttonListener = new ButtonListener();

        //setting the onClickListener for each button to this view
        btnSubmitScore.setOnClickListener(buttonListener);
        btnReturn.setOnClickListener(buttonListener);

        //displays high scores from HighScores.txt
        viewScores();
    }

    //checks if edtNameSubmit is empty before storing the name
    public void submitNameToScore()
    {
        if (edtNameSubmit.getText().toString().isEmpty()) {
            edtNameSubmit.setError(getResources().getString(R.string.error_name_empty));
            return;
        }

        //disable edtNameSubmit and button to prevent multiple entries
        submittedName = edtNameSubmit.getText().toString();
        setButtonEnabled(false);
        edtNameSubmit.setText("");

        //stores saved name with score to HighScores.txt
        saveScoresToFile(getResources().getString(R.string.txt_high_scores_text, submittedName, scorePercent));

        //displays high scores from HighScores.txt
        viewScores();
    }

    //saving the formatted name + score string to HighScores.txt
    public void saveScoresToFile(String formattedString)
    {
        try
        {
            OutputStreamWriter outputStreamWriter = new OutputStreamWriter(this.openFileOutput("HighScores.txt", Context.MODE_APPEND));
            outputStreamWriter.write(formattedString);
            outputStreamWriter.close();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    //reading the HighScores.txt file to retrieve all names + score strings and add them to scoresList ArrayList
    public void readScoresFromFile()
    {
        try
        {
            //opening the HighScores.txt file
            InputStream inputStream = this.openFileInput("HighScores.txt");

            if (inputStream != null )
            {
                InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
                String receiveString;

                //clearing scoresList before adding to it
                scoresList.clear();

                //while bufferedReader has a line to read, add the string to scoresList
                while ((receiveString = bufferedReader.readLine()) != null)
                {
                    scoresList.add(receiveString);
                }

                inputStream.close();
            }
        }
        catch (FileNotFoundException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    //displaying scores from scoresList, sorted from highest to lowest
    public void viewScores()
    {
        //populating the scoresList ArrayList
        readScoresFromFile();

        //using Collections.sort to sort the list from lowest to highest
        Collections.sort(scoresList, new Comparator<String>() {
            //compares the int values to sort from low to high
            @Override
            public int compare(String o1, String o2)
            {
                return extractInt(o1) - extractInt(o2);
            }

            //removes letter chars from the string to better compare them
            public int extractInt(String value)
            {
                String scoreNum = value.replaceAll("\\D", "");
                return Integer.parseInt(scoreNum);
            }
        });

        //reversing the list to make it highest to lowest
        Collections.reverse(scoresList);

        //appending all the strings to a StringBuilder
        StringBuilder strScoreList = new StringBuilder();

        for (String value : scoresList)
        {
            strScoreList.append(value).append("\n");
        }

        //display the output
        txtHighScoreList.setText(strScoreList);
    }

    //terminate the Activity
    public void returnToMain()
    {
        finish();
    }

    public void setButtonEnabled(boolean val)
    {
        btnSubmitScore.setEnabled(val);
        edtNameSubmit.setEnabled(val);
    }

    //switch statement to call the needed functions based on clicked button, using view id
    private class ButtonListener implements View.OnClickListener {
        @Override
        public void onClick(View view) {
            switch (view.getId()) {
                case R.id.btn_return:
                    returnToMain();
                    break;
                case R.id.btn_submit_high_score:
                    submitNameToScore();
                    break;
            }
        }
    }

    //saves the necessary values to the bundle on instance termination
    @Override
    protected void onSaveInstanceState(Bundle outState)
    {
        super.onSaveInstanceState(outState);

        boolean btnSubmitVal = btnSubmitScore.isEnabled();
        boolean edtSubmitVal = edtNameSubmit.isEnabled();

        outState.putBoolean(BUTTON_SUBMIT, btnSubmitVal);
        outState.putBoolean(SUBMIT_ENABLED, edtSubmitVal);
    }

    //restores the necessary values from the bundle on instance restoration
    @Override
    protected void onRestoreInstanceState(Bundle saveInstanceState)
    {
        super.onRestoreInstanceState(saveInstanceState);

        boolean btnSubmitVal = saveInstanceState.getBoolean(BUTTON_SUBMIT);
        boolean edtSubmitVal = saveInstanceState.getBoolean(SUBMIT_ENABLED);

        btnSubmitScore.setEnabled(btnSubmitVal);
        edtNameSubmit.setEnabled(edtSubmitVal);
    }
}
