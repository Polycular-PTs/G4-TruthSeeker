using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Notizbuch : MonoBehaviour
{

    private void OnMouseDown()
    {
        SceneManager.LoadScene(Scenes.ExplanationGame);
    }

}
