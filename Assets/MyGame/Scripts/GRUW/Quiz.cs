using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public int indexCurrentQuestion;

    [Header("Debugging")]
    public List<Question> questions;
    private Question currentQuestion;

    [Header("UI References")]
    public Button[] buttons;
    public TMP_Text header;

    [Header("Events")]
    public UnityEvent QuizEnd;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i + 1;
            buttons[i].onClick.AddListener(delegate { CheckAnswer(index); });
            print("i " + i);
        }

        LoadQuestion();
    }

    public void LoadQuestion()
    {
        currentQuestion = questions[indexCurrentQuestion];
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
        }

        header.text = currentQuestion.title;
    }

    public void CheckAnswer(int id)
    {
        if (id == currentQuestion.correctAnswers)
        {
            if (indexCurrentQuestion + 1 == questions.Count)
            {
                StartCoroutine(QuizEnded());
                StartCoroutine(ColorButton(id - 1, Color.green));
            }
            else
            {
                // Right
                StartCoroutine(NextQuestion(id - 1, Color.green));
                StartCoroutine(ColorButton(id - 1, Color.green));
            }
        }
        else
        {
            if (indexCurrentQuestion + 1 == questions.Count)
            {
                StartCoroutine(QuizEnded());
                StartCoroutine(ColorButton(id - 1, Color.red));
            }
            else
            {
                // Wrong
                StartCoroutine(NextQuestion(id - 1, Color.red));
                StartCoroutine(ColorButton(id - 1, Color.red));
            }
           
        }
    }

    public void ToggleButtons(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = value;
        }
    }

    IEnumerator QuizEnded()
    {
        yield return new WaitForSeconds(1f);
        QuizEnd?.Invoke();
    }

    IEnumerator NextQuestion(int id, Color col)
    {
        yield return new WaitForSeconds(1f);
        indexCurrentQuestion++;
        LoadQuestion();
    }

    IEnumerator ColorButton(int id, Color col)
    {
        ColorBlock colors = new ColorBlock();
        ColorBlock prevColors = buttons[id].colors;
        colors = prevColors;
        colors.pressedColor = col;
        colors.selectedColor = col;
        colors.disabledColor = col;
        buttons[id].colors = colors;
        ToggleButtons(false);

        yield return new WaitForSeconds(1f);
        ToggleButtons(true);
        buttons[id].colors = prevColors;

    }
}
