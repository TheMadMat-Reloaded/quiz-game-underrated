using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Description : Class for defining buttons related functions.
public class ButtonManager : MonoBehaviour
{
    public GameObject UIPlayGame;
    public GameObject UIQuestion;
    public GameObject UIGameOver;
    
    public TMP_Text finalScore;
    public TMP_Text currentScore;
    public TMP_Text record;
    public TMP_Text timeText;

    public Player player;
    public QuizManager quizManager;
    public Timer timer;
    
    private float timeRemainingQuiz;
    private float timeRemainingButtonAnswer;
    private bool buttonAnswerIsPressed = false;
    private bool quizTimeIsOver = true;
    private bool isSecondChance = false;

    private GameObject currentButtonPressed;

    public GameObject skipQuestionButton;
    public GameObject endQuizButton;
    public GameObject retryButton;

    // Start is called before the first frame update
    void Start()
    {
        UIPlayGame.SetActive(true);
    }

    void Update(){
        
        if (buttonAnswerIsPressed){

            if (timeRemainingButtonAnswer > 0){

                timeRemainingButtonAnswer -= Time.deltaTime;
            }
            else{

                Debug.Log("Time has run out!");
                timeRemainingButtonAnswer = timer.timeRemainingButtonAnswer;
                buttonAnswerIsPressed = false;
                currentButtonPressed.GetComponent<Image>().color = new Color32(255, 185, 54, 255);
                quizManager.buttonsAnswer[quizManager.currentCorrectAnswer].GetComponent<Image>().color =  new Color32(255, 185, 54, 255);
                for(int i = 0; i < 4; i++){
                    quizManager.buttonsAnswer[i].GetComponent<Button>().interactable = true;
                }
                
                NextQuestion();
            }
        }

        if(!quizTimeIsOver){

            if (timeRemainingQuiz > 0){
                timeRemainingQuiz -= Time.deltaTime;
            }
            else{
                GameOver();
            }
            DisplayTime(timeRemainingQuiz);
        }
    }

    // this method starts a new game
    public void StartGame(){
        currentScore.text = "Score 0";
        UIPlayGame.SetActive(false);
        UIQuestion.SetActive(true);
        quizManager.GenerateRandomQuestionIndexes();
        quizManager.SelectQuestion();
        quizTimeIsOver = false;
        timeRemainingQuiz = timer.timeRemainingQuiz;
        timeRemainingButtonAnswer = timer.timeRemainingButtonAnswer;

        if(isSecondChance){
            skipQuestionButton.SetActive(true);
            endQuizButton.SetActive(true);
        }
        else{
            skipQuestionButton.SetActive(false);
            endQuizButton.SetActive(false);
            retryButton.SetActive(true);
        }
        Debug.Log("Start Game");
    }

    // This method chooses to show the next question or end the game.
    public void NextQuestion(){
        if(!(quizManager.questionIndexes.Length == quizManager.currentQuestion)){
            quizManager.SelectQuestion();
        }
        else{
            GameOver();
        }
    }

    /* 
        This method receives a button and determines if it contains the correct answer. 
        If the answer is correct, the color of the button changes to green for a certain 
        time and adds points to the player, otherwise the color of the button changes to 
        red without adding points.
    */
    public void EvaluateOption(GameObject buttonAnswer){

        if(!isSecondChance){

            if(buttonAnswer.GetComponent<ButtonListener>().isCorrect){
                player.score += 100;
                buttonAnswer.GetComponent<Image>().color =  Color.green;
                currentScore.text = "Score " + player.score.ToString();
                Debug.Log("Correct Answer");
            
            }
            else{
                buttonAnswer.GetComponent<Image>().color =  Color.red;
                quizManager.buttonsAnswer[quizManager.currentCorrectAnswer].GetComponent<Image>().color =  Color.green;
                Debug.Log("Wrong Answer");
            }
        }
        else{

            if(buttonAnswer.GetComponent<ButtonListener>().isCorrect){
                player.score += 25;
                buttonAnswer.GetComponent<Image>().color =  Color.green;
                currentScore.text = "Score " + player.score.ToString();
                Debug.Log("Correct Answer");
            
            }
            else{
                player.score -= 25;
                buttonAnswer.GetComponent<Image>().color =  Color.red;
                quizManager.buttonsAnswer[quizManager.currentCorrectAnswer].GetComponent<Image>().color =  Color.green;
                currentScore.text = "Score " + player.score.ToString();
                Debug.Log("Wrong Answer");
            }
        }
        

        currentButtonPressed = buttonAnswer;
        buttonAnswerIsPressed = true;

        for(int i = 0; i < 4; i++){
            quizManager.buttonsAnswer[i].GetComponent<Button>().interactable = false;
        }
    }

    // This method displays the current remaining time on the StatPanel.
    private void DisplayTime(float timeToDisplay){
        if(timeToDisplay < 0){
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("Time {0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver(){
        quizTimeIsOver = true;
        finalScore.text = player.score.ToString();
        record.text = PlayerPrefs.GetInt("Record").ToString();
        quizManager.currentQuestion = 0;
        quizManager.DeleteQuestionIndexes();
        
        if(isSecondChance){
            isSecondChance = false;
            retryButton.SetActive(false);
        }

        if(PlayerPrefs.GetInt("Record") < player.score){
            PlayerPrefs.SetInt("Record", player.score);
        }
        
        player.score = 0;
        
        UIQuestion.SetActive(false);
        UIGameOver.SetActive(true);
        Debug.Log("Game Over");
    }

    public void Retry(){
        isSecondChance = true;
        UIGameOver.SetActive(false);
        StartGame();
    }

    public void EndGame(){
        SceneManager.LoadScene("QuizGame");
    }
    
}