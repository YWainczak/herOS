using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public bool appOpen;
    public Color appColor;
    public Image appOverlayHeader;
    public Text appOverlayHeaderText;

    private RectTransform appRect;
    private float appSpeed = 16;

	// Use this for initialization
	void Start ()
    {
        appRect = gameObject.GetComponent<RectTransform>();
        appOverlayHeader.color = appColor;
        appOverlayHeaderText.text = gameObject.name;

    }

    // Update is called once per frame
    void Update()
    {
        if (appRect != null)
        {
            if (appOpen)
            {
                appRect.anchoredPosition = new Vector2(0, Mathf.Lerp(appRect.anchoredPosition.y, 0, appSpeed * Time.deltaTime));
            }
            else
            {
                appRect.anchoredPosition = new Vector2(0, Mathf.Lerp(appRect.anchoredPosition.y, -Screen.height, appSpeed * Time.deltaTime));
            }
        }
	}

    public void CloseApp()
    {
        appOpen = false;
    }

    public void OpenApp()
    {
        appOpen = true;
    }
}
