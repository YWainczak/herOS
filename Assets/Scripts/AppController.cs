using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
	public GameManager manager;
	public bool appOpen;
	public bool systemApp;
    public Color appColor;
    public Image appOverlayHeader;
    public Text appOverlayHeaderText;

    private RectTransform appRect;
    private float appSpeed = 16;

	// Use this for initialization
	void Start ()
	{
		appRect = gameObject.GetComponent<RectTransform> ();

		if (systemApp)
		{
			appOverlayHeader.color = appColor;
			appOverlayHeaderText.text = gameObject.name;
			appRect.anchoredPosition = new Vector2 (0, -Screen.height);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (appRect != null)
        {
            if (appOpen)
            {
                appRect.anchoredPosition = new Vector2(0, Mathf.Lerp(appRect.anchoredPosition.y, 0, appSpeed * Time.deltaTime));

				if (name == "Call" && manager.fighting == null)
				{
					CloseApp ();
				}
            }
            else
            {
                appRect.anchoredPosition = new Vector2(0, Mathf.Lerp(appRect.anchoredPosition.y, -Screen.height, appSpeed * Time.deltaTime));

				if (name == "Call" && manager.fighting != null)
				{
					OpenApp ();
				}
            }
        }
	}

    public void CloseApp()
    {
        appOpen = false;

		if (name == "Call")
		{
			manager.homeActive = true;
		}
    }

    public void OpenApp()
    {
        appOpen = true;

		if (name == "Call")
		{
			manager.homeActive = false;
		}

    }
}
