using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuiz : MonoBehaviour
{
    public void next()
    {
        SceneManager.LoadScene("Quiz");
    }

}
