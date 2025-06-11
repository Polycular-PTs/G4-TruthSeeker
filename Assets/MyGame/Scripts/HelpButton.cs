using UnityEngine;

public class ToggleTextPanel : MonoBehaviour
{
    public GameObject panelToToggle;

    public void TogglePanel()
    {
        if (panelToToggle != null)
        {
            bool isCurrentlyActive = panelToToggle.activeSelf;
            panelToToggle.SetActive(!isCurrentlyActive);
        }
    }
}


