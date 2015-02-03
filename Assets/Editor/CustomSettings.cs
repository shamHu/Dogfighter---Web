using UnityEditor;
using UnityEngine;

class CustomSettings : EditorWindow
{

	string compName = "";
	string prodName = "";
	int screenWidth = 640;
	int screenHeight = 480;
	int webScreenWidth = 640;
	int webScreenHeight = 480;
	bool fullScreen = false;
	
	[MenuItem ("Window/Custom Settings")]
	static void Init ()
	{
		CustomSettings window = (CustomSettings)EditorWindow.GetWindow (typeof(CustomSettings));
	}

	void OnGUI ()
	{
		compName = EditorGUILayout.TextField ("Company Name:", compName);
		prodName = EditorGUILayout.TextField ("Product Name:", prodName);
		EditorGUILayout.BeginHorizontal ();
		screenWidth = EditorGUILayout.IntField ("Width:", screenWidth, GUILayout.MinWidth(80));
		screenHeight = EditorGUILayout.IntField ("Web Height:", screenHeight, GUILayout.MinWidth(80));
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		webScreenWidth = EditorGUILayout.IntField ("Web Width:", webScreenWidth);
		webScreenHeight = EditorGUILayout.IntField ("Web Height:", webScreenHeight);
		EditorGUILayout.EndHorizontal ();
		fullScreen = EditorGUILayout.Toggle ("Full Screen:", fullScreen);
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Save Values"))
			SaveSettings ();
		if (GUILayout.Button ("Load Values"))
			LoadSettings ();
		EditorGUILayout.EndHorizontal ();
	}

	void SaveSettings ()
	{
		PlayerSettings.companyName = compName;
		PlayerSettings.productName = prodName;
		PlayerSettings.defaultScreenWidth = screenWidth;
		PlayerSettings.defaultScreenHeight = screenHeight;
		PlayerSettings.defaultWebScreenWidth = webScreenWidth;
		PlayerSettings.defaultWebScreenHeight = webScreenHeight;
		PlayerSettings.defaultIsFullScreen = fullScreen;

		EditorPrefs.SetString ("CompName", compName);
		EditorPrefs.SetString ("ProdName", prodName);
		EditorPrefs.SetInt ("ScreenWidth", screenWidth);
		EditorPrefs.SetInt ("ScreenHeight", screenHeight);
		EditorPrefs.SetInt ("WebScreenWidth", webScreenWidth);
		EditorPrefs.SetInt ("WebScreenHeight", webScreenHeight);
		EditorPrefs.SetBool ("FullScreen", fullScreen);
	}

	void LoadSettings ()
	{
		compName = EditorPrefs.GetString ("CompName", "");
		prodName = EditorPrefs.GetString ("ProdName", "");
		screenWidth = EditorPrefs.GetInt ("ScreenWidth", 640);
		screenHeight = EditorPrefs.GetInt ("ScreenHeight", 480);
		webScreenWidth = EditorPrefs.GetInt ("WebScreenWidth", 640);
		webScreenHeight = EditorPrefs.GetInt ("WebScreenHeiht", 480);
		fullScreen = EditorPrefs.GetBool ("FullScreen", false);
	}
}
