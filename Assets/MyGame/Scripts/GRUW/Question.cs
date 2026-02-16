using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 1)]
public class Question : ScriptableObject
{
    [TextArea]
    public string title;
    [TextArea]
    public string[] answers;
    public int correctAnswers;
}
