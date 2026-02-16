using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizGame : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public TMP_Text questionText;
    public TMP_Text trustText;
    public Button[] answerButtons;
    public Slider trustBar;

    public List<Question> questions = new List<Question>();
    public int currentQuestionIndex = 0;
    private int wrongAnswers = 0;
    private float trust = 0.5f;
    public Button next;
    public GameObject person;

    void Start()
    {
        trustBar.value = trust;
        trustBar.interactable = false;
        ShowQuestion();
        
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            EndGame();
            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int capturedIndex = i;
            TMP_Text btnText = answerButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                btnText.text = q.answers[i];
            }

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => AnswerSelected(capturedIndex));
        }
    }

    void AnswerSelected(int index)
    {
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            trust += 0.1f;
        }
        else
        {
            trust -= 0.1f;
            wrongAnswers++;
        }

        trust = Mathf.Clamp01(trust);
        trustBar.value = trust;

        if (wrongAnswers >= 4)
        {
            ShowFinalMessage("The members of the group got too suspicious and have killed you. You lost...");
            return;
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    void EndGame()
    {
        ShowFinalMessage("You have outsmarted the fake news spreaders and now you have enough evidence to arrest them. Congratulations you stopped the fake news wave!");
    }

    void ShowFinalMessage(string message)
    {
        questionText.text = message;

        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

        trustBar.gameObject.SetActive(false);
        trustText.gameObject.SetActive(false);
        //next.gameObject.SetActive(true);
        person.gameObject.SetActive(false);

    }
}


