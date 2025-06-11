using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public bool isTrue;
    }

    public Question[] questions;

    public Text questionText;
    public Text scoreText;
    public Button trueButton;
    public Button falseButton;
    public Button retryButton;
    public Button nextButton;
    public GameObject background;

    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        retryButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        DisplayQuestion();

        trueButton.onClick.AddListener(() => AnswerQuestion(true));
        falseButton.onClick.AddListener(() => AnswerQuestion(false));
        retryButton.onClick.AddListener(RestartQuiz);
        nextButton.onClick.AddListener(LoadNextScene);
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex].questionText;
        }
    }

    void AnswerQuestion(bool answer)
    {
        if (questions[currentQuestionIndex].isTrue == answer)
        {
            score++;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        
        questionText.text = "";
        scoreText.gameObject.SetActive(true);
        scoreText.text = score + "/" + questions.Length;

        retryButton.gameObject.SetActive(true);

        if (score == questions.Length)
        {
            nextButton.gameObject.SetActive(true);
        }

        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        background.SetActive(false);
    }

    void RestartQuiz()
    {
        SceneManager.LoadScene("ExplanationGame");
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("OfficeMitNotizbuchUndAkte");
    }
}
