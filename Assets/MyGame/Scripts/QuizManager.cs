using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GameOverpanel;
    public GameObject nextButton;

    public Text QuestionTxt;
    public Text ScoreText;

    int totalQuestions = 0;
    public int totalScore = 0;

    private void Start()
    {
        totalQuestions = (QnA.Count);
        GameOverpanel.SetActive(false);
        generateQuestion();
        nextButton.SetActive(false);
    }
    public void retry()
    {
        SceneManager.LoadScene("Office");
    }

    public void next()
    {
        SceneManager.LoadScene("OfficeMitNotizbuch");
    }
    
    public void gameOver()
    {
        Quizpanel.SetActive(false);
        GameOverpanel.SetActive(true);
        ScoreText.text = totalScore + "/" + totalQuestions;
        if(totalScore >= totalQuestions)
        {
            nextButton.SetActive(true);
        }
       
    }

    public void correct() 
    {
        totalScore += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++) 
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].correctAnswers == i+1) 
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0) 
        {
                currentQuestion = Random.Range(0, QnA.Count);

               QuestionTxt.text = QnA[currentQuestion].Question;
               SetAnswers(); 
        }   
        else
        {
            Debug.Log("out of questions");
            gameOver();
        }
    }

}
