using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class MakeEnemy
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Enemy")]
	#endif
	public static void Create()
	{
		Enemy asset = ScriptableObject.CreateInstance<Enemy>();
		#if UNITY_EDITOR
		AssetDatabase.CreateAsset(asset, "Assets/Enemies/newEnemy.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
		#endif
	}
}