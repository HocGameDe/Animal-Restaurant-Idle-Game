#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;

public class PopupTab : DarkcupTab
{
	private string inputText = "";
	private string pathText = "Scripts/Popup/";

	private Vector2 scrollPosition;
	private int currentSelect;
	List<Type> popupTypes = new List<Type>();

	Array ids;
	public PopupTab(DarkcupWindow wd) : base(wd) { }
		
	public override void ShowTabLeft(int width)
	{
		popupTypes.Clear();
		List<Type> allPopup = GetDerivedClasses<Popup>();
		List<string> names = new List<string>();
		for (int i = 0; i < allPopup.Count; i++)
		{
			popupTypes.Add(allPopup[i]);
			names.Add(allPopup[i].Name);
			Debug.Log(allPopup[i].Name);
        }
		currentSelect = GUILayout.SelectionGrid(currentSelect, names.ToArray(), 1);
	}

	public override void ShowTabMain(int width)
	{
		EditorGUILayout.Separator();

		inputText = EditorGUILayout.TextField("Enter Popup Name:", inputText);
		pathText = EditorGUILayout.TextField("Create at directory", pathText);

		string popupName = "Popup" + inputText;
		string textContent = Resources.Load<TextAsset>("ScriptTemplate/PopupTemplate").text;

		textContent = textContent.Replace("{0}", inputText);
		GUI.enabled = false;
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));
		EditorGUILayout.TextArea(textContent);
		EditorGUILayout.EndScrollView();
		GUI.enabled = true;

		if (GUILayout.Button($"Create Script \"{popupName}\""))
		{
			if (inputText == "") return;
			CheckEventSystem();
            SaveScriptData(textContent, "Popup" + inputText, pathText);
		}

		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();

		Type selectedPopup = popupTypes[currentSelect];

		if (GUILayout.Button($"Create {selectedPopup} in scene"))
		{
			CheckEventSystem();
            List<string> canvasPopupNamaes = new List<string>()
				{
					"Canvas Popup", "CanvasPopup","canvas popup","canvas Popup","canvaspopup","Popup","popup"
				};
			Canvas canvasPopup = null;
			GameObject tmp;
			int index = 0;
			while (canvasPopup == null)
			{
				tmp = GameObject.Find(canvasPopupNamaes[index]);
				if (tmp != null)
				{
					canvasPopup = tmp.GetComponent<Canvas>();
					if (canvasPopup != null) break;
				}
				index++;
				if (index >= canvasPopupNamaes.Count) break;
			}
			var prefab = Resources.Load<GameObject>("PrefabTemplate/PopupPrefabTemplate");
			if (prefab == null)
			{
				Debug.LogError("could not found prefab at: " + "PrefabTemplate/PopupPrefabTemplate");
			}
			prefab.name = "Popup" + inputText;
			if (canvasPopup == null)
            {
				canvasPopup = GameObject.Instantiate(Resources.Load<GameObject>("PrefabTemplate/CanvasPopup")).GetComponent<Canvas>();
				canvasPopup.name = "Canvas Popup";
            }
			var spawned = GameObject.Instantiate(prefab, canvasPopup.transform);
			spawned.name = selectedPopup.Name;
			spawned.AddComponent(selectedPopup);
		}
	}
	
	void CheckEventSystem()
	{
		var eventSystem = GameObject.FindObjectOfType<EventSystem>();
		if (eventSystem == null)
		{
			var spawned = GameObject.Instantiate(Resources.Load<GameObject>("PrefabTemplate/EventSystem"));
			spawned.name = "EventSystem";
        }
	}

	public static List<Type> GetDerivedClasses<TBase>()
	{
		var assembly = System.Reflection.Assembly.GetAssembly(typeof(Popup));  // Assembly.GetExecutingAssembly();

        var derivedClasses = assembly.GetTypes()
										.Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TBase)))
										.ToList();

		return derivedClasses;
	}

	public string GetInputText()
	{
		return inputText;
	}

	public static void SaveScriptData(string content, string fileName, string pathFromAsset)
	{
		string path = Path.Combine(Application.dataPath, pathFromAsset, fileName + ".cs");
		string directory = Path.Combine(Application.dataPath, pathFromAsset);
		try
		{
			if (Directory.Exists(directory) == false)
            {
				Directory.CreateDirectory(directory);
			}
			File.WriteAllText(path, content);
			Debug.Log("File saved successfully at: " + path);
			string assetPath = $"Assets/{pathFromAsset}/{fileName}.cs";
			AssetDatabase.ImportAsset(assetPath);
		}
		catch (IOException e)
		{
			Debug.LogError("Failed to save file: " + e.Message);
		}
	}
}
#endif