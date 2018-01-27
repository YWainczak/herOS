using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallController : MonoBehaviour
{
	public Image background;
	public Image enemy;
	public Text enemyName;
	public Text status;

	public RectTransform bottomBar;
	public RectTransform enemyRect;

	public float uiSpeed = 8;

	public RectTransform swordCooldown;
	public Image ShieldCooldown;

	private float callTimer;

	public GameManager manager;

	// Update is called once per frame
	void Update ()
	{
		if (manager.fighting != null)
		{
			enemyName.text = manager.fighting.name;
			background.color = manager.fighting.accentColor;
			enemy.color = manager.fighting.color;
			enemy.sprite = manager.fighting.sprite;
		}

		if (manager.fightingStatus == "battle")
		{
			bottomBar.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomBar.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
			enemyRect.anchoredPosition = new Vector2(0, Mathf.Lerp(enemyRect.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
		}
		else
		{
			bottomBar.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomBar.anchoredPosition.y, -Screen.height/4, uiSpeed * Time.deltaTime));
			enemyRect.anchoredPosition = new Vector2(0, Mathf.Lerp(enemyRect.anchoredPosition.y, -Screen.height, uiSpeed * Time.deltaTime));
		}

		swordCooldown.localScale = new Vector3(Mathf.Lerp (1f, 1.5f, (manager.attackCoolDownTimer - Time.time)/manager.attackCoolDown), Mathf.Lerp (1f, 1.5f, (manager.attackCoolDownTimer - Time.time)/manager.attackCoolDown), 1);

		if (manager.fightingStatus == "calling")
		{
			callTimer = Time.time + Random.Range (manager.callTimeMin, manager.callTimeMax);
			status.text = "Ringing...";
			manager.fightingStatus = "ringing";
		} else if (manager.fightingStatus == "ringing")
		{
			if (Time.time > callTimer)
			{
				manager.fightingStatus = "battle";
				status.text = "Battle!";
			}
		}
	}

	public void Sword()
	{
		if (Time.time > manager.attackCoolDownTimer)
		{
			manager.attackCoolDownTimer = Time.time + manager.attackCoolDown;
		}
	}

	public void Shield()
	{
		manager.fighting = null;
	}

	public void HangUp()
	{
		manager.fighting = null;
		manager.fightingStatus = "nada";
	}
}
