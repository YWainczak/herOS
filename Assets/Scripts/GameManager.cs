using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Player
	public float health;
    public float healthMax;

	public float attack;
	public float attackCoolDown;
	public float attackCoolDownTimer;

	public float defense;
	public float defenseCoolDown;
	public float defenseTimer;

	public float exp;
	public float expMax;

	public float money;

	//Enemy
	public float enemyHealth;
	public float enemyHealthMax;

	//Other
    public List<AppController> apps;

	public List<Enemy> enemies;

	public Enemy fighting;
	public string fightingStatus = "nada";

	public float callTimeMin;
	public float callTimeMax;

    public Image battery; 

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
    }

    public void Home()
    {
		if (homeActive)
		{
			foreach (AppController controller in apps) {
				controller.CloseApp ();
			}
		}
    }

    public void LaunchApp(string appName)
    {
        foreach (AppController controller in apps)
        {
            if (controller.gameObject.name == appName)
            {
                controller.OpenApp();
            }
        }
    }
}
