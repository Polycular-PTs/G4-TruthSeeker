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

    private List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;
    private int wrongAnswers = 0;
    private float trust = 0.5f;
    public Button next;
    public GameObject person;

    void Start()
    {
        trustBar.value = trust;
        trustBar.interactable = false;
        CreateQuestions();
        ShowQuestion();
        
    }

    void CreateQuestions()
    {
        questions.Add(new Question
        {
            questionText = "What's your name?",
            answers = new string[] { "Detectiv Walker", "Chris", "I'm not telling you!", "Uhmmm..." },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "What are you doing here?",
            answers = new string[] { "I'm spying on you", "I don't know", "Not your business!", "Trying to connect with like minded people" },
            correctAnswerIndex = 3
        });

        questions.Add(new Question
        {
            questionText = "Who sent you?",
            answers = new string[] { "I got an invite", "My boss", "I...don't remember", "The police" },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "Do you like Tyran?",
            answers = new string[] { "Oh hell no!", "Who's that?", "I LOVE him. He'll save the country.", "I'm not interested in politics." },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "Will you help us with the fake news spreading?",
            answers = new string[] { "That was you?", "NO!", "Yeah for sure!", "The what?" },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "Do you know who is responsible for spreading it?",
            answers = new string[] { "I don't care", "What? Are you admitting this is organized?", "No but I'd enjoy getting to know them!", "I don't wanna know" },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "I shouldn't be telling you so soon, but it's organized by Tyran himself.",
            answers = new string[] { "I'm gonna arrest him!", "He should be exposed", "That's amazing of him!", "Is he crazy??" },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "So are you all in?",
            answers = new string[] { "Nope", "Maybe...", "Yep!", "I'll think about it" },
            correctAnswerIndex = 2
        });
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
        next.gameObject.SetActive(true);
        person.gameObject.SetActive(false);

    }
}


