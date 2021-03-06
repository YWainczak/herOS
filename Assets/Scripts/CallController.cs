﻿using System.Collections;
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

	public Image hitImage;

	public RectTransform powerRect;
	public Image powerImage;

	public RectTransform bottomBar;
	public RectTransform enemyRect;

	public RectTransform victoryBox;

	public float uiSpeed = 8;
	public float spinSpd;

	public RectTransform shield;
	public Image shieldImage;

	public Image healthBar;

	public Color hitColor;
	public Color blockColor;

	public RectTransform swordCooldown;
	public RectTransform ShieldCooldown;

	public Text victoryText;

	private float calcEXP;
	private float calcGold;

	private float callTimer;

	public GameManager manager;

	void Awake()
	{
		hitImage.color = new Color (hitImage.color.r, hitImage.color.g, hitImage.color.b, 0f);
	}

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

		if (manager.fightingStatus == "battle") {
			if (manager.enemyCooldownTimer < Time.time && !manager.enemyAttacking) {
				manager.enemyAttacking = true;
				manager.enemyAttackTimer = manager.fighting.attackTime + Time.time;
			} else if (manager.enemyAttacking && manager.enemyAttackTimer < Time.time) {
				if (manager.defenseEffectTimer > Time.time) {
					manager.health -= manager.fighting.attack * manager.defense;
					hitImage.color = blockColor;
				} else {
					manager.health -= manager.fighting.attack;
					hitImage.color = hitColor;
				}

				EnemyCooldown ();
			}
		}

		if(hitImage.color.a > 0f)
		{
			hitImage.color = new Color (hitImage.color.r, hitImage.color.g, hitImage.color.b, Mathf.Lerp(hitImage.color.a, 0f, uiSpeed/2 * Time.deltaTime));
		}

		if (manager.enemyAttacking)
		{
			powerRect.localScale = new Vector3 (Mathf.Lerp (powerRect.localScale.x, 1, uiSpeed * Time.deltaTime), Mathf.Lerp (powerRect.localScale.y, 1, uiSpeed * Time.deltaTime), 1);
		} else
		{
			powerRect.localScale = new Vector3 (Mathf.Lerp(powerRect.localScale.x, 0, uiSpeed * Time.deltaTime), Mathf.Lerp(powerRect.localScale.y, 0, uiSpeed * Time.deltaTime), 1);
		}

		if (manager.defenseEffectTimer > Time.time)
		{
			shield.localScale = new Vector3 (Mathf.Lerp (shield.localScale.x, 1, uiSpeed * Time.deltaTime), Mathf.Lerp (shield.localScale.y, 1, uiSpeed * Time.deltaTime), 1);
			shieldImage.color = new Color (shieldImage.color.r, shieldImage.color.g, shieldImage.color.b, Mathf.Lerp(shieldImage.color.a, .5f, uiSpeed * Time.deltaTime));
		} else
		{
			shield.localScale = new Vector3 (Mathf.Lerp(shield.localScale.x, 2, uiSpeed * Time.deltaTime), Mathf.Lerp(shield.localScale.y, 2, uiSpeed * Time.deltaTime), 1);
			shieldImage.color = new Color (shieldImage.color.r, shieldImage.color.g, shieldImage.color.b, Mathf.Lerp(shieldImage.color.a, 0f, uiSpeed * Time.deltaTime));
		}

		if (manager.enemyHealth <= 0 && manager.fightingStatus == "battle") {
			manager.fightingStatus = "victory";
			manager.enemyAttacking = false;

			calcEXP = Mathf.Round(manager.fighting.health / 4 + manager.attack / 2);
			calcGold = Mathf.Round(manager.fighting.health / 2 + manager.attack / 4 + Random.Range(0, 4));

			manager.exp += calcEXP;
			manager.gold += calcGold;

			victoryText.text = "+" + calcEXP.ToString () + " EXP\n" + "+" + calcGold.ToString () + " Gold";
		}

		if(manager.fightingStatus != "victory")
		{
			victoryBox.anchoredPosition = new Vector2(0, Mathf.Lerp(victoryBox.anchoredPosition.y, -Screen.height, uiSpeed/2 * Time.deltaTime));
		}

		if(manager.fightingStatus == "battle" || manager.fightingStatus == "victory")
		{
			enemyRect.anchoredPosition = new Vector2(0, Mathf.Lerp(enemyRect.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
			if(manager.fightingStatus == "victory")
			{
				enemy.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Lerp (enemy.GetComponent<RectTransform>().localScale.x, 0, uiSpeed * Time.deltaTime), Mathf.Lerp (enemy.GetComponent<RectTransform>().localScale.y, 0, uiSpeed * Time.deltaTime), 1);
				enemy.GetComponent<RectTransform> ().Rotate (Vector3.forward * spinSpd * Time.deltaTime);
				victoryBox.anchoredPosition = new Vector2(0, Mathf.Lerp(victoryBox.anchoredPosition.y, 0, uiSpeed * Time.deltaTime));
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
		ShieldCooldown.localScale = new Vector3(Mathf.Lerp (1f, 3f, (manager.defenseCoolDownTimer - Time.time)/manager.defenseCoolDown), Mathf.Lerp (1f, 3f, (manager.defenseCoolDownTimer - Time.time)/manager.defenseCoolDown), 1);

		if (manager.fightingStatus == "calling") {
			callTimer = Time.time + Random.Range (manager.callTimeMin, manager.callTimeMax);
			status.text = "Ringing...";
			manager.fightingStatus = "ringing";

			enemy.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			enemy.GetComponent<RectTransform> ().rotation = Quaternion.identity;

			spinSpd = Random.Range (128, 256);
			spinSpd *= Random.Range (0, 3) == 1 ? -1 : 1;

			powerImage.color = manager.fighting.powerColor;
			powerRect.localScale = new Vector3 (0, 0, 1);
			shield.localScale = new Vector3 (2, 2, 1);
			shieldImage.color = new Color (shieldImage.color.r, shieldImage.color.g, shieldImage.color.b, 0f);

			manager.enemyHealthMax = manager.fighting.health;
			manager.enemyHealth = manager.enemyHealthMax;
		} else if (manager.fightingStatus == "ringing") {
			if (Time.time > callTimer) {
				manager.MusicBattle ();
				manager.fightingStatus = "battle";
				status.text = "Battle!";
				EnemyCooldown ();
			}
		} else if (manager.fightingStatus == "victory") {
			status.text = "Victory!";
			manager.musicSource.Stop ();
		}
	}

	public void Sword()
	{
		if (manager.health > 0) {
			if (Time.time > manager.attackCoolDownTimer) {
				manager.enemyHealth -= manager.attack;
				slashAnim.SetTrigger ("attack");
				manager.attackCoolDownTimer = Time.time + manager.attackCoolDown;
				manager.AudioClick ();

			}
		}
	}

	public void Shield()
	{
		if (manager.health > 0) {
			if (Time.time > manager.defenseCoolDownTimer) {
				manager.defenseCoolDownTimer = Time.time + manager.defenseCoolDown;
				manager.defenseEffectTimer = Time.time + manager.defenseEffect;

				manager.AudioClick ();
			}
		}
	}

	public void HangUp()
	{
		if (manager.health > 0) {
			manager.fighting = null;
			manager.fightingStatus = "nada";
			manager.enemyAttacking = false;

			manager.AudioBack ();
			manager.MusicMain ();
		}
	}

	void EnemyCooldown()
	{
		manager.enemyCooldownTimer = Time.time + Random.Range (manager.fighting.attackCooldownMin, manager.fighting.attackCooldownMax);
		manager.enemyAttackTimer = Time.time;
		manager.enemyAttacking = false;
	}
}
