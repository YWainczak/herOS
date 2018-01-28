using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Player
	public float health;
    public float healthMax;

	public int level;

	public float attack;
	public float attackCoolDown;
	public float attackCoolDownTimer;

	public float defense;

	public float defenseEffect;
	public float defenseEffectTimer;

	public float defenseCoolDown;
	public float defenseCoolDownTimer;

	public float exp;
	public float expMax;
	public Image expBar;

	public float gold;

	//Enemy
	public float enemyHealth;
	public float enemyHealthMax;

	public bool enemyAttacking;

	public float enemyAttackTimer;
	public float enemyCooldownTimer;

	//Other
    public List<AppController> apps;

	public List<Enemy> enemies;

	public AudioSource source;

	public AudioClip uiBack;
	public AudioClip uiClick;

	public AudioSource musicSource;

	public AudioClip mainTheme;
	public AudioClip battleTheme;
	public AudioClip callingTheme;

	public Enemy fighting;
	public string fightingStatus = "nada";

	public float callTimeMin;
	public float callTimeMax;

    public Image battery; 

	public Text levelText;

	public bool homeActive;
	public RectTransform home;
	public float homeSpeed = 8;


    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        battery.fillAmount = health / healthMax;

		levelText.text = "LEVEL " + level.ToString ();

		expBar.fillAmount = exp / expMax;

		if (home != null)
		{
			if (homeActive)
			{
				home.anchoredPosition = new Vector2(0, Mathf.Lerp(home.anchoredPosition.y, 0, homeSpeed * Time.deltaTime));
			}
			else
			{
				home.anchoredPosition = new Vector2(0, Mathf.Lerp(home.anchoredPosition.y, -Screen.height/4, homeSpeed * Time.deltaTime));
			}
		}

		if (exp > expMax)
		{
			exp -= expMax;
			level++;
		}
    }

    public void Home()
    {
		if (homeActive)
		{
			AudioBack ();

			foreach (AppController controller in apps) {
				controller.CloseApp ();
			}
		}
    }

    public void LaunchApp(string appName)
    {
		AudioClick ();

		foreach (AppController controller in apps)
        {
            if (controller.gameObject.name == appName)
            {
                controller.OpenApp();
            }
        }
    }

	public void AudioClick()
	{
		source.clip = uiClick;
		source.Play ();
	}

	public void AudioBack()
	{
		source.clip = uiBack;
		source.Play ();
	}

	public void MusicMain()
	{
		musicSource.clip = mainTheme;
		musicSource.Play ();
	}

	public void MusicBattle()
	{
		musicSource.clip = battleTheme;
		musicSource.Play ();
	}

	public void MusicCalling()
	{
		musicSource.clip = callingTheme;
		musicSource.Play ();
	}

}
