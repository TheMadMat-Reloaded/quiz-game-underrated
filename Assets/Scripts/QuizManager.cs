using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Description : Class for defining quiz questions related control functions.
public class QuizManager : MonoBehaviour
{
    public List<Question> questionsQuiz;
    public int currentQuestion = 0;
    public int currentCorrectAnswer;
    public TMP_Text questionText;
    public GameObject[] buttonsAnswer;

    // Array of indexes determining the calling order of questions.
    // The positions initialize with -1 to later assign the indices correctly.
    public int[] questionIndexes = new int[9] {-1,-1,-1,-1,-1,-1,-1,-1,-1}; 

    private bool isInArray(int[] array, int number){
        foreach (int i in array){
            if(i == number){
                return true;
            }
        }
        return false;
    }

    // This method assigns the indices randomly to the questionIndexes array.
    public void GenerateRandomQuestionIndexes(){
        int aux;

        for ( int i = 0; i < questionIndexes.Length; i++ ){
            do{
                aux = Random.Range(0, questionIndexes.Length); 
                
            }while( isInArray(questionIndexes, aux) );
            questionIndexes[i] = aux;
        }
    }

    public void DeleteQuestionIndexes(){
        for ( int i = 0; i < questionIndexes.Length; i++ ){
            questionIndexes[i] = -1;
        }
    }

    // This method displays the question in the QuestionPanel and randomly displays the 
    // answer options in the AnswersPanel.
    public void SelectQuestion(){
        
        int[] answersIndexes = new int[4] {-1,-1,-1,-1};
        int aux;

        for ( int i = 0; i < answersIndexes.Length; i++ ){
            do{
                aux = Random.Range(0, 4); 
                
            }while( isInArray(answersIndexes, aux) );
            answersIndexes[i] = aux;
            
            if(questionsQuiz[questionIndexes[currentQuestion]].correctAnswer == aux){
                currentCorrectAnswer = i;
            }
        }
        
        questionText.text = questionsQuiz[questionIndexes[currentQuestion]].question;
        for(int i = 0; i < buttonsAnswer.Length; i++){
            buttonsAnswer[i].GetComponent<ButtonListener>().isCorrect = false;
            buttonsAnswer[i].GetComponentInChildren<TMP_Text>().text = questionsQuiz[questionIndexes[currentQuestion]].answers[answersIndexes[i]];
            if(currentCorrectAnswer == i){
                buttonsAnswer[i].GetComponent<ButtonListener>().isCorrect = true;
            }
        }
        currentQuestion++;
        
    }
}
