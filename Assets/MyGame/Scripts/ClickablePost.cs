using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickablePost : MonoBehaviour
{
    [System.Serializable]
    public class ClickableRegion
    {
        public string name; // z.B. "Headline", "Username"
        public RectTransform regionTransform; // UI-Element mit Button
        public GameObject infoPanel; // Panel mit Info-Text
    }

    public List<ClickableRegion> clickableRegions;

    public GameObject darkOverlay; // UI-Image über allem, halbtransparent
    private bool inFocusMode = false;
    private GameObject activeInfoPanel = null;

    void Start()
    {
        foreach (var region in clickableRegions)
        {
            Button button = region.regionTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnRegionClick(region));
            }
        }

        darkOverlay.SetActive(false);
    }

    void Update()
    {
        if (inFocusMode && Input.GetMouseButtonDown(0))
        {
            // Prüfe, ob Klick außerhalb aller Info-Panels
            if (!IsPointerOverUIElement())
            {
                ExitFocusMode();
            }
        }
    }

    void OnRegionClick(ClickableRegion region)
    {
        inFocusMode = true;
        darkOverlay.SetActive(true);

        foreach (var r in clickableRegions)
        {
            r.infoPanel.SetActive(r == region);
        }

        activeInfoPanel = region.infoPanel;
    }

    void ExitFocusMode()
    {
        inFocusMode = false;
        darkOverlay.SetActive(false);

        foreach (var r in clickableRegions)
        {
            r.infoPanel.SetActive(false);
        }

        activeInfoPanel = null;
    }

    // Prüft, ob der Mausklick über UI-Elementen liegt
    bool IsPointerOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var result in results)
        {
            if (result.gameObject == activeInfoPanel || result.gameObject.transform.IsChildOf(activeInfoPanel.transform))
                return true;
        }

        return false;
    }
}

