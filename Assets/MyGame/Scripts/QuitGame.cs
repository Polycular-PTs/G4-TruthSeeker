using UnityEngine;

public class QuitGame : MonoBehaviour
{
    

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }




    public void Quit()
    {
        Debug.Log("Spiel wird beendet...");

#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Wenn das Spiel als Build läuft, beende es vollständig
        Application.Quit();
#endif
    }
}

