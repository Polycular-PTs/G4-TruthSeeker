using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meeting : MonoBehaviour
{
    // Start is called before the first frame update
    public void meeting()
    {
        SceneManager.LoadScene("Conversation");
    }
}
