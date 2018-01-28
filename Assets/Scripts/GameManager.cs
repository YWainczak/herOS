using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Player
	public float health;
    public float healthMax;
	public float healthAdd;

	public int level;

	public float attack;
	public float attackCoolDown;
	public float attackCoolDownTimer;
	public float attackMulti;

	public float defense;

	public float defenseEffect;
	public float defenseEffectTimer;

	public float defenseCoolDown;
	public float defenseCoolDownTimer;

	public float exp;
	public float expMax;
	public float expMulti;
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

	public Image batteryCharging;
	public float chargeSpeed;

	public Image deathFade;
	public RectTransform deathBox;
	public Image deathFade2;

	public float deathWait;
	public float deathWaitTimer;

	public Text levelText;

	public bool homeActive;
	public RectTransform home;
	public float homeSpeed = 8;


    void Start()
    {
        health = healthMax;
		deathBox.anchoredPosition = new Vector2(0, -Screen.height);
    }

    void Update()
    {
		if (exp >= expMax)
		{
			exp -= expMax;
			level++;
			expMax *= expMulti;

			healthMax += healthAdd;
			health += healthAdd;

			attack *= attackMulti;
		}

		battery.fillAmount = health / healthMax;

		levelText.text = "LEVEL " + level.ToString ();

		expBar.fillAmount = exp / expMax;

		if (fighting == null) {
			batteryCharging.enabled = true;
			health += chargeSpeed * Time.deltaTime;
			if (health > healthMax) {
				health = healthMax;
			}
		} else {
			batteryCharging.enabled = false;
		}

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

		if (health <= 0)
		{
			deathBox.anchoredPosition = new Vector2(0, Mathf.Lerp(deathBox.anchoredPosition.y, 0, homeSpeed * Time.deltaTime));
			deathFade.color = new Color (deathFade.color.r, deathFade.color.g, deathFade.color.b, Mathf.Lerp (deathFade.color.a, 1, homeSpeed/4 * Time.deltaTime));

			if (deathFade.color.a > 0.99f) {
				if (deathWaitTimer == null) {
					deathWaitTimer = deathWait + Time.time;
				} else if (Time.time > deathWaitTimer) {
					deathFade2.color = new Color (deathFade2.color.r, deathFade2.color.g, deathFade2.color.b, Mathf.Lerp (deathFade2.color.a, 1, homeSpeed/4 * Time.deltaTime));

					if (deathFade2.color.a > 0.99f) {
						SceneManager.LoadScene (0);
					}
				}

			
			}
		}
		else
		{
			deathBox.anchoredPosition = new Vector2(0, Mathf.Lerp(deathBox.anchoredPosition.y, -Screen.height, homeSpeed * Time.deltaTime));
			deathFade.color = new Color (deathFade.color.r, deathFade.color.g, deathFade.color.b, Mathf.Lerp (deathFade.color.a, 0, homeSpeed/4 * Time.deltaTime));
		}
    }

    public void Home()
    {
		if (health > 0) {
			if (homeActive) {
				AudioBack ();

				foreach (AppController controller in apps) {
					controller.CloseApp ();
				}
			}
		}
    }

    public void LaunchApp(string appName)
    {
		if (health > 0) {
			AudioClick ();

			foreach (AppController controller in apps) {
				if (controller.gameObject.name == appName) {
					controller.OpenApp ();
				}
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