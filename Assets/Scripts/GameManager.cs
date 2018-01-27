using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float health;
    public float healthMax;

    public List<AppController> apps;

	public List<Enemy> enemies;

    public Image battery; 

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        battery.fillAmount = health / healthMax;
    }

    public void Home()
    {
        foreach(AppController controller in apps)
        {
            controller.CloseApp();
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
