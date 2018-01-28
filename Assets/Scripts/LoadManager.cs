using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
	public string mainScene;
	public float uiSpeed = 2;

	public AudioListener listen;
	public AudioSource source;

	public Image logoImage;
	public RectTransform logoRect;

	private bool chime = false;

	public float waitTime = 2;
	public float waitTimeCurrent;

	public Image back;

	public bool ready = false;

	AsyncOperation myASync;
	private Scene meScene;

	void Awake()
	{
		logoRect.anchoredPosition = new Vector2(0, Screen.height/8);
		logoImage.color = new Color (logoImage.color.r, logoImage.color.b, logoImage.color.g, 0f);
	}

	void Start()
	{
		meScene = SceneManager.GetActiveScene ();

	}

	void Update()
	{
		if (Time.time > waitTime)
		{
			if (!ready) {
				if (!chime)
				{
					chime = true;
					source.Play ();
				}
				logoRect.anchoredPosition = new Vector2 (0, Mathf.Lerp (logoRect.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
				logoImage.color = new Color (logoImage.color.r, logoImage.color.b, logoImage.color.g, Mathf.Lerp (logoImage.color.a, 1f, uiSpeed * Time.deltaTime));

				if (logoImage.color.a >= 0.99f && !source.isPlaying)
				{
					ready = true;
					Destroy (listen);
					myASync = SceneManager.LoadSceneAsync (mainScene, LoadSceneMode.Additive);
					waitTimeCurrent = Time.time + waitTime;
				}
			} else if(myASync.isDone)
			{
				if (Time.time > waitTimeCurrent)
				{
					logoRect.anchoredPosition = new Vector2 (0, Mathf.Lerp (logoRect.anchoredPosition.y, -Screen.height/8, uiSpeed * Time.deltaTime));
					logoImage.color = new Color (logoImage.color.r, logoImage.color.b, logoImage.color.g, Mathf.Lerp (logoImage.color.a, 0f, uiSpeed * Time.deltaTime));

					if (logoImage.color.a <= 0.01f)
					{
						back.color = new Color (back.color.r, back.color.b, back.color.g, Mathf.Lerp (back.color.a, 0f, uiSpeed * Time.deltaTime));
						if (back.color.a <= 0.01f)
						{
							SceneManager.UnloadSceneAsync (meScene);
						}
					}
				}
			}
		}
	}
}