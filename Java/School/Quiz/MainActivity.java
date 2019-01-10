/**
 * Author:    Duncan Levings
 * Title:     Assignment 2
 *
 * Purpose: Quiz app with saved instances, orientation changes, high score functions, Bundles
 **/

package com.projects.duncanlevings.quizexercise;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.os.Parcelable;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.projects.duncanlevings.quizexercise.model.Question;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Random;

public class MainActivity extends Activity {

    private TextView txtQuestion, txtResponse;
    private Button btnTrue, btnFalse, btnNext, btnHighScore;
    private ArrayList<Question> questionList;
    private ButtonListener buttonListener;
    private int index, score, questionListSize;
    private float percent;
    private boolean startGame;

    //creating the static string keys needed for saving to bundle
    private static final String QUESTION_LIST = "questionList";
    private static final String BUTTON_TRUE = "buttonTrue";
    private static final String BUTTON_FALSE = "buttonFalse";
    private static final String BUTTON_NEXT = "buttonNext";
    private static final String BUTTON_HIGHSCORE = "buttonHighScore";
    private static final String CUR_HIGHSCORE_VISIBLE = "highScoreDisplay";
    private static final String CUR_INDEX = "curIndex";
    private static final String CUR_SCORE = "curScore";
    private static final String CUR_PERCENT = "curPercent";
    private static final String BOOL_START = "boolStart";
    private static final String QUESTION_TEXT = "questionText";
    private static final String RESPONSE_TEXT = "responseText";
    private static final String BUTTON_NEXT_TEXT = "buttonNextText";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        setUp();
    }

    //initial setup and declaring of variables
    public void setUp()
    {
        txtQuestion = findViewById(R.id.txt_question);
        txtResponse = findViewById(R.id.txt_response);
        btnTrue = findViewById(R.id.btn_true);
        btnFalse = findViewById(R.id.btn_false);
        btnNext = findViewById(R.id.btn_next);
        btnHighScore = findViewById(R.id.btn_high_score);

        //getting ArrayList<Question> from Question class
        questionList = Question.getQuestions(getResources());

        questionListSize = questionList.size();
        startGame = true;

        buttonListener = new ButtonListener();

        //setting the onClickListener for each button to this view
        btnTrue.setOnClickListener(buttonListener);
        btnFalse.setOnClickListener(buttonListener);
        btnNext.setOnClickListener(buttonListener);
        btnHighScore.setOnClickListener(buttonListener);

        setButtonEnabled(false);
    }

    //checks response click with correct answer of the question
    public void checkAnswer(boolean val)
    {
        //when index has reached the end of the questions, change next button text
        if (index >= questionListSize - 1)
        {
            btnNext.setText(getResources().getString(R.string.last_question));
        }

        //if the response is the correct boolean value, display correct answer and increment score, otherwise dispay wrong answer
        if (questionList.get(index).getAnswer() == val)
        {
            txtResponse.setText(getResources().getString(R.string.answer_correct));
            score++;
        }
        else
        {
            txtResponse.setText(getResources().getString(R.string.answer_wrong));
        }

        setButtonEnabled(false);
    }

    //displays next question or ends game
    public void nextQuestion()
    {
        //if true, calls setupGameStart to restart the quiz, sets startGame to false to ensure no multiple calls
        if (startGame)
        {
            startGame = false;
            setupGameStart();
            return;
        }

        //when index has reached end of the questions, display end of quiz
        if (index >= questionListSize - 1)
        {
            finishedQuizDisplay();
            return;
        }

        //setting the question to current index, incremented before setting
        txtResponse.setText("");
        txtQuestion.setText(questionList.get(++index).getQuestion());
        setButtonEnabled(true);
    }

    //declares variables for game start, hiding and enabling correct elements
    public void setupGameStart()
    {
        //generate new random
        Random randomGenList = new Random();
        //randomize question list
        Collections.shuffle(questionList, randomGenList);

        index = 0;
        score = 0;
        percent = 0;
        txtQuestion.setText(questionList.get(index).getQuestion());
        btnNext.setText(getResources().getString(R.string.next_question));
        txtResponse.setText("");
        setButtonEnabled(true);

        btnHighScore.setVisibility(View.INVISIBLE);
        btnHighScore.setEnabled(true);
    }

    //when quiz is finished, displays final score and high scores button
    public void finishedQuizDisplay()
    {
        txtQuestion.setText(getResources().getString(R.string.quiz_finished));

        //calculating percent from score
        percent = ((float)score / questionListSize) * 100;
        txtResponse.setText(getResources().getString(R.string.final_response, score, questionListSize, percent));

        //setting startGame to true and changes Next button to allow a new game
        btnNext.setText(getResources().getString(R.string.start_over));
        startGame = true;
        setButtonEnabled(false);

        //displaying the high score button
        btnHighScore.setVisibility(View.VISIBLE);
    }

    public void setButtonEnabled(boolean val)
    {
        btnTrue.setEnabled(val);
        btnFalse.setEnabled(val);
        btnNext.setEnabled(!val);
    }

    //stores percent in a bundle for sending to the HighScore activity
    public void displayHighScore()
    {
        Intent intentHighSore = new Intent(getApplicationContext(), HighScores.class);

        Bundle bundle = new Bundle();
        bundle.putFloat("scorePercent", percent);
        intentHighSore.putExtras(bundle);

        //creates instance of the HighScore activity
        startActivity(intentHighSore);

        btnHighScore.setEnabled(false);
    }

    //switch statement to call the needed functions based on clicked button, using view id
    private class ButtonListener implements View.OnClickListener {
        @Override
        public void onClick(View view) {
            switch (view.getId()) {
                case R.id.btn_true:
                    checkAnswer(true);
                    break;
                case R.id.btn_false:
                    checkAnswer(false);
                    break;
                case R.id.btn_next:
                    nextQuestion();
                    break;
                case R.id.btn_high_score:
                    displayHighScore();
                    break;
            }
        }
    }

    //saves the necessary values to the bundle on instance termination
    @Override
    protected void onSaveInstanceState(Bundle outState)
    {
        super.onSaveInstanceState(outState);

        boolean btnTrueVal = btnTrue.isEnabled();
        boolean btnFalseVal = btnFalse.isEnabled();
        boolean btnNextVal = btnNext.isEnabled();
        boolean btnHighScoreVal = btnHighScore.isEnabled();
        int btnHighScoreDisplayVal = btnHighScore.getVisibility();
        String questionTextVal = txtQuestion.getText().toString();
        String responseTextVal = txtResponse.getText().toString();
        String nextQuestionTextVal = btnNext.getText().toString();

        //saving ArrayList<Question> as Parcelable
        outState.putParcelableArrayList(QUESTION_LIST, questionList);

        outState.putBoolean(BUTTON_TRUE, btnTrueVal);
        outState.putBoolean(BUTTON_FALSE, btnFalseVal);
        outState.putBoolean(BUTTON_NEXT, btnNextVal);
        outState.putBoolean(BOOL_START, startGame);
        outState.putBoolean(BUTTON_HIGHSCORE, btnHighScoreVal);
        outState.putInt(CUR_HIGHSCORE_VISIBLE, btnHighScoreDisplayVal);
        outState.putInt(CUR_INDEX, index);
        outState.putInt(CUR_SCORE, score);
        outState.putFloat(CUR_PERCENT, percent);
        outState.putString(QUESTION_TEXT, questionTextVal);
        outState.putString(RESPONSE_TEXT, responseTextVal);
        outState.putString(BUTTON_NEXT_TEXT, nextQuestionTextVal);
    }

    //restores the necessary values from the bundle on instance restoration
    @Override
    protected void onRestoreInstanceState(Bundle saveInstanceState)
    {
        super.onRestoreInstanceState(saveInstanceState);

        //restoring ArrayList<Question> from Parcelable
        questionList = saveInstanceState.getParcelableArrayList(QUESTION_LIST);

        boolean btnTrueVal = saveInstanceState.getBoolean(BUTTON_TRUE);
        boolean btnFalseVal = saveInstanceState.getBoolean(BUTTON_FALSE);
        boolean btnNextVal = saveInstanceState.getBoolean(BUTTON_NEXT);
        boolean btnHighScoreVal = saveInstanceState.getBoolean(BUTTON_HIGHSCORE);
        int btnHighScoreDisplayVal = saveInstanceState.getInt(CUR_HIGHSCORE_VISIBLE);
        startGame = saveInstanceState.getBoolean(BOOL_START);
        index = saveInstanceState.getInt(CUR_INDEX);
        score = saveInstanceState.getInt(CUR_SCORE);
        percent = saveInstanceState.getFloat(CUR_PERCENT);
        String questionTextVal = saveInstanceState.getString(QUESTION_TEXT);
        String responseTextVal = saveInstanceState.getString(RESPONSE_TEXT);
        String nextQuestionTextVal = saveInstanceState.getString(BUTTON_NEXT_TEXT);

        btnTrue.setEnabled(btnTrueVal);
        btnFalse.setEnabled(btnFalseVal);
        btnNext.setEnabled(btnNextVal);
        btnHighScore.setEnabled(btnHighScoreVal);
        btnHighScore.setVisibility(btnHighScoreDisplayVal);
        txtQuestion.setText(questionTextVal);
        txtResponse.setText(responseTextVal);
        btnNext.setText(nextQuestionTextVal);
    }
}
