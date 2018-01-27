using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsManager : MonoBehaviour
{
	public GameManager manager;

	public GameObject contactPrefab;

	public float size;

	public float offset;
	public float bottomOffset;

	private float currentY = 1f;
	private GameObject currentObject;

	public RectTransform mask;

	private float maskSize;

	private void Awake()
	{
		maskSize += offset;

		for (int i = 0; i < manager.enemies.Count; i++)
		{
			maskSize += size;
		
			if(i < manager.enemies.Count-1)
			{
				maskSize += offset;
			} else
			{
				maskSize += bottomOffset;
			}
		}

		mask.anchorMin = new Vector2(0, 1 - maskSize);

		currentY -= offset / maskSize;

		for (int i = 0; i < manager.enemies.Count; i++)
		{
			currentObject = Instantiate(contactPrefab, mask);
			currentObject.GetComponent<RectTransform>().anchorMax = new Vector2(currentObject.GetComponent<RectTransform>().anchorMax.x, currentY);
			currentY -= size/maskSize;
			currentObject.GetComponent<RectTransform>().anchorMin = new Vector2(currentObject.GetComponent<RectTransform>().anchorMin.x, currentY);
			currentObject.GetComponent<ContactController>().meEnemyDad = manager.enemies[i];
			currentObject.GetComponent<ContactController> ().manager = manager;

			if (i < manager.enemies.Count - 1)
			{
				currentY -= offset / maskSize;
			}
		}
	}
}
