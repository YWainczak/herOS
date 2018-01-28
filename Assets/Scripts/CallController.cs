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
	public Animator slashAnim;

	public RectTransform bottomBar;
	public RectTransform enemyRect;

	public float uiSpeed = 8;
	public float spinSpd;

	public Image healthBar;

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

			healthBar.fillAmount = manager.enemyHealth / manager.enemyHealthMax;
		}

		if (manager.enemyHealth <= 0 && manager.fightingStatus == "battle")
		{
			manager.fightingStatus = "victory";
		}

		if(manager.fightingStatus == "battle" || manager.fightingStatus == "victory")
		{
			enemyRect.anchoredPosition = new Vector2(0, Mathf.Lerp(enemyRect.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
			if(manager.fightingStatus == "victory")
			{
				enemy.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Lerp (enemy.GetComponent<RectTransform>().localScale.x, 0, uiSpeed * Time.deltaTime), Mathf.Lerp (enemy.GetComponent<RectTransform>().localScale.y, 0, uiSpeed * Time.deltaTime), 1);
				enemy.GetComponent<RectTransform> ().Rotate (Vector3.forward * spinSpd * Time.deltaTime);
			}
		}

		if (manager.fightingStatus == "battle")
		{
			bottomBar.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomBar.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
		}
		else
		{
			bottomBar.anchoredPosition = new Vector2(0, Mathf.Lerp(bottomBar.anchoredPosition.y, -Screen.height/4, uiSpeed * Time.deltaTime));
			if (manager.fightingStatus != "victory") {
				enemyRect.anchoredPosition = new Vector2 (0, Mathf.Lerp (enemyRect.anchoredPosition.y, -Screen.height, uiSpeed * Time.deltaTime));
			}
		}

		swordCooldown.localScale = new Vector3(Mathf.Lerp (1f, 3f, (manager.attackCoolDownTimer - Time.time)/manager.attackCoolDown), Mathf.Lerp (1f, 3f, (manager.attackCoolDownTimer - Time.time)/manager.attackCoolDown), 1);

		if (manager.fightingStatus == "calling") {
			callTimer = Time.time + Random.Range (manager.callTimeMin, manager.callTimeMax);
			status.text = "Ringing...";
			manager.fightingStatus = "ringing";

			spinSpd = Random.Range (128, 256);
			spinSpd *= Random.Range (0, 2) == 1 ? -1 : 1;

			manager.enemyHealthMax = manager.fighting.health;
			manager.enemyHealth = manager.enemyHealthMax;
		} else if (manager.fightingStatus == "ringing") {
			if (Time.time > callTimer) {
				manager.fightingStatus = "battle";
				status.text = "Battle!";
			}
		} else if (manager.fightingStatus == "victory") {
			status.text = "Victory!";
		}
	}

	public void Sword()
	{
		if (Time.time > manager.attackCoolDownTimer)
		{
			manager.enemyHealth -= manager.attack;
			slashAnim.SetTrigger ("attack");
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
