using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallController : MonoBehaviour
{
	public Image background;
	public Image enemy;
	public Text enemyName;

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
	}
}
