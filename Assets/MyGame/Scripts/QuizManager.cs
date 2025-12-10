using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("Questions System")]
    public List<QuestionsAnswers> QnA;
    public GameObject[] options;

    public GameObject Quizpanel;
    public GameObject GameOverpanel;
    public GameObject nextButton;

    public Text QuestionTxt;
    public Text ScoreText;

    int totalQuestions = 0;
    int questionNumber = 1;

    public int totalScore = 0;

    public Image FeedbackPanel;
    public Text FeedbackText;
    public float feedbackDuration = 1f;

    public Text questionCounterText;
    public Text scoreText;

    public int points = 2;

    [Header("Hints System")]
    public string[] hints;     // ← Inspector: 16 Hints eintragen!
    private string[] currentHints; // ← Hints NUR für aktuelle Frage

    private int usedHints = 0;
    private int currentHintIndex = 0;

    public Text hintText;
    public GameObject warningPanel;
    public Text warningText;

    public GameObject yesButton;
    public GameObject noButton;

    public int hintPenalty = 0;

    private int questionIndex = 0; // ← aktuelle Frage

    private void Start()
    {
        totalQuestions = QnA.Count;

        GameOverpanel.SetActive(false);
        nextButton.SetActive(false);
        warningPanel.SetActive(false);

        hintText.enabled = false;

        generateQuestion();
        UpdateUI();
    }

    public void retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void next()
    {
        SceneManager.LoadScene("OfficeMitNotizbuch");
    }

    public void gameOver()
    {
        Quizpanel.SetActive(false);
        GameOverpanel.SetActive(true);

        int finalScore = totalScore + hintPenalty;

        ScoreText.text = finalScore + "/12 (Hinweisstrafe: " + hintPenalty + ")";

        if (finalScore >= totalQuestions)
        {
            nextButton.SetActive(true);
        }
    }

    public void correct()
    {
        totalScore += points;
        StartCoroutine(ShowFeedback(Color.green, "Correct!", true));
        UpdateUI();
    }

    public void wrong()
    {
        StartCoroutine(ShowFeedback(Color.red, "Wrong!", false));
        UpdateUI();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text =
                QnA[questionIndex].Answers[i];

            if (QnA[questionIndex].correctAnswers == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (questionIndex < QnA.Count)
        {
            QuestionTxt.text = QnA[questionIndex].Question;
            SetAnswers();

            LoadHintsForQuestion();

            usedHints = 0;
            currentHintIndex = 0;
            hintText.text = "";
            hintText.enabled = false;
            warningPanel.SetActive(false);
        }
        else
        {
            gameOver();
        }
    }

    void UpdateUI()
    {
        questionCounterText.text = "Question: " + questionNumber + "/" + totalQuestions;
        scoreText.text = "Score: " + totalScore + "/12";
    }

    IEnumerator ShowFeedback(Color color, string message, bool wasCorrect)
    {
        FeedbackPanel.color = color;
        FeedbackPanel.canvasRenderer.SetAlpha(0.5f);
        FeedbackText.text = message;
        FeedbackText.gameObject.SetActive(true);

        yield return new WaitForSeconds(feedbackDuration);

        FeedbackPanel.canvasRenderer.SetAlpha(0f);
        FeedbackText.gameObject.SetActive(false);

        questionNumber++;
        questionIndex++;

        generateQuestion();
        UpdateUI();
    }


    // ----------------------------------------------------------
    // HINT SYSTEM
    // ----------------------------------------------------------
    void LoadHintsForQuestion()
    {
        int startIndex = questionIndex * 2;

        List<string> newHintsList = new List<string>();

        if (startIndex < hints.Length)
            newHintsList.Add(hints[startIndex]);

        if (startIndex + 1 < hints.Length)
            newHintsList.Add(hints[startIndex + 1]);

        currentHints = newHintsList.ToArray(); // ← DIESE Hints gelten für diese Frage
    }

    public void ApplyHintPenalty()
    {
        hintPenalty -= 1;
    }

    public void OnHelpButtonPressed()
    {
        warningPanel.SetActive(true);

        if (usedHints >= currentHints.Length)
        {
            warningText.text = "No more hints available!";
            yesButton.SetActive(false);
            noButton.SetActive(false);
            return;
        }

        yesButton.SetActive(true);
        noButton.SetActive(true);
        warningText.text = "You will lose 1 point for this hint! Do you still want it?";
    }

    public void ConfirmHint()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);

        ApplyHintPenalty();
        hintText.enabled = true;

        if (currentHintIndex >= currentHints.Length)
        {
            hintText.text = "No more hints available.";
            warningPanel.SetActive(false);
            return;
        }

        // Zweiter Hinweis → beide anzeigen
        if (usedHints == 1 && currentHints.Length > 1)
        {
            string combined = "";

            foreach (var h in currentHints)
                combined += h + "\n";

            hintText.text = combined;
        }
        else
        {
            hintText.text = currentHints[currentHintIndex];
        }

        usedHints++;
        currentHintIndex++;

        warningPanel.SetActive(false);
    }

    public void CancelHint()
    {
        warningPanel.SetActive(false);
    }

    public void exit()
    {
        warningPanel.SetActive(false);
    }
}
