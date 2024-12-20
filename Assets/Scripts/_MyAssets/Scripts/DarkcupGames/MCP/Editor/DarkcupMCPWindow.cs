#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.IO;
using System.Reflection;

public abstract class DarkcupTab
{
	public DarkcupWindow window;
	public GUILayoutOption buttonWidth = GUILayout.Width(DarkcupWindow.BUTTON_WIDTH);
	public DarkcupTab(DarkcupWindow window)
    {
		this.window = window;
    }
	public abstract void ShowTabMain(int width);
	public abstract void ShowTabLeft(int width);
}

public class PackageTab : DarkcupTab
{
	public PackageTab(DarkcupWindow pw) : base(pw) { }

    public override void ShowTabLeft(int width)
    {
            
    }

    public override void ShowTabMain(int width)
	{
		EditorGUILayout.Separator();

		GUILayout.Label("Particle UI", EditorStyles.boldLabel);
		EditorGUILayout.Separator();

		GUILayout.Label("\"com.coffee.ui - effect\": \"https://github.com/mob-sakai/UIEffect.git\",", EditorStyles.label);

		if (GUILayout.Button("Add Particle UGUI", buttonWidth))
		{
			AddGitPackage("", "");
			//UnityEngine.Debug.LogError("this is refresh!!");
		}

		if (GUILayout.Button("PIKACHU!!!", buttonWidth))
		{
			UnityEngine.Debug.LogError("this is refresh!!");
		}

		EditorGUILayout.BeginVertical();

		// buttons
		EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();

		if (GUILayout.Button("PIKACHU!!!", buttonWidth))
		{
			UnityEngine.Debug.LogError("this is refresh!!");
		}

		EditorGUI.EndChangeCheck();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
	}

	public static void AddGitPackage(string packageName, string githubPath)
	{
		packageName = "com.coffee.ui-effect";
		githubPath = "https://github.com/mob-sakai/UIEffect.git";

		string manifestPath = "Packages/manifest.json";
		string jsonContent = File.ReadAllText(manifestPath);

		if (!jsonContent.Contains("\"dependencies\""))
		{
			Debug.LogError("No dependencies section found in manifest.json.");
			return;
		}
		if (!jsonContent.Contains(packageName))
		{
			
			int insertIndex = jsonContent.IndexOf("\"dependencies\"") + "\"dependencies\"".Length;
			string newDependency = $",\n    \"{packageName}\": \"{githubPath}\"";
			jsonContent = jsonContent.Insert(insertIndex, newDependency);
			//File.WriteAllText(manifestPath, jsonContent);
			//AssetDatabase.Refresh();
			Debug.Log(jsonContent);

			Debug.Log($"Git package {packageName} added successfully!");
		}
		else
		{
			Debug.LogWarning($"Package {packageName} already exists in dependencies.");
		}
	}
}

public class MeoDiHiaTab : DarkcupTab
{
	public MeoDiHiaTab(DarkcupWindow pw) : base(pw) { }

    public override void ShowTabLeft(int width)
    {
		int selection = 0;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical(GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));
		EditorGUILayout.BeginVertical("box");
		var SP1 = EditorGUILayout.BeginScrollView(Vector2.zero, GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));

		string[] texts = new string[]
		{
			"item1", "item2", "item3"
		};
		var prev = selection;
		selection = GUILayout.SelectionGrid(selection, texts, 1, GUILayout.Width(DarkcupWindow.LEFT_PANEL_WIDTH));
		if (prev != selection)
		{
			GUI.FocusControl("ID");
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
	}

    public override void ShowTabMain(int i)
	{
		EditorGUILayout.Separator();

		GUILayout.Label("Welcome to the MeoDiHiaTab!", EditorStyles.boldLabel);
		EditorGUILayout.Separator();

		GUILayout.Label("Select one of the Pikachu options below:", EditorStyles.label);
			
		EditorGUILayout.Separator();
		GUILayout.Label("End of options.", EditorStyles.helpBox);
	}
}

public class DarkcupWindow : EditorWindow
{
	public const int LEFT_PANEL_WIDTH = 200;
	public const int BUTTON_WIDTH = 75;

	public bool isLoading = false;

	private Type[] types = new Type[] {
		typeof(PopupTab),
		//typeof(PackageTab),
		typeof(CreateScriptableObjectTab),
		//typeof(MeoDiHiaTab)
	};

	private Dictionary<int, DarkcupTab> dicTab = new Dictionary<int, DarkcupTab>();
	private string[] tabnames;
	private int currentTab;

	[MenuItem("Darkcup/Darkcup Window")]
	public static void ShowMainWindow()
	{
		DarkcupWindow window = GetWindow<DarkcupWindow>();
		window.titleContent = new GUIContent("Darkcup Window");
		window.Show();
	}

	public void OnGUI()
	{
		GUI.SetNextControlName("Toolbar");

		if (tabnames == null || tabnames.Length == 0)
        {
			tabnames = new string[types.Length];
            for (int i = 0; i < tabnames.Length; i++)
            {
				tabnames[i] = types[i].Name.Replace("Tab","");
			}
		}
		currentTab = GUILayout.SelectionGrid(currentTab, tabnames, tabnames.Length + 2);
		GUILayout.Box(" ", GUILayout.ExpandWidth(true));
		EditorGUILayout.Separator();

		if (dicTab.ContainsKey(currentTab) == false)
		{
			var newTab = (DarkcupTab)Activator.CreateInstance(types[currentTab], new object[] { this });
			dicTab.Add(currentTab, newTab);
		}

		EditorGUI.BeginDisabledGroup(isLoading);

		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();

		GUILayout.BeginVertical(); // Left Column
		dicTab[currentTab].ShowTabLeft(300);
		GUILayout.EndVertical();

		GUILayout.BeginVertical(); // Right Column
		dicTab[currentTab].ShowTabMain(300);
		GUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();

		EditorGUI.EndDisabledGroup();
	}
}
#endif