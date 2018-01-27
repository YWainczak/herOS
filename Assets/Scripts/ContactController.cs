using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactController : MonoBehaviour
{
	public Text drawnName;
	public Image iconBack;
	public Image icon;

	public GameManager manager;

	public Enemy meEnemyDad;

	void Start()
	{
		drawnName.text = meEnemyDad.name;

		iconBack.color = meEnemyDad.accentColor;

		icon.sprite = meEnemyDad.sprite;
		icon.color = meEnemyDad.color;
	}

	public void Call()
	{
		if (manager.fighting == null)
		{
			manager.fighting = meEnemyDad;
			manager.fightingStatus = "calling";
		}
	}
}
