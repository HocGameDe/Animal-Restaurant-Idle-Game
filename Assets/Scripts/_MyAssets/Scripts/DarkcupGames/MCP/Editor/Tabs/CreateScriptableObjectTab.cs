#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;

public class CreateScriptableObjectTab : DarkcupTab
{
	private string inputText = "";
	private string pathText = "Assets/Prefabs/ScriptableObject/";

	private Vector2 scrollPosition;
	private int currentSelect;
	List<Type> scriptableTypes = new List<Type>();
    List<Type> enumTypes = new List<Type>();

	Array ids;
	public CreateScriptableObjectTab(DarkcupWindow wd) : base(wd) { }
    private int currentId;
    List<Type> enums = new List<Type>();

    public override void ShowTabLeft(int width)
	{
		scriptableTypes.Clear();
		List<Type> allPopup = GetDerivedClasses<ScriptableObject>();
		List<string> names = new List<string>();
		for (int i = 0; i < allPopup.Count; i++)
		{
			scriptableTypes.Add(allPopup[i]);
			names.Add(allPopup[i].Name);
		}
		currentSelect = GUILayout.SelectionGrid(currentSelect, names.ToArray(), 1);
	}

    public override void ShowTabMain(int width)
    {
        EditorGUILayout.Separator();
        var enumTypes = GetDerivedEnums<Popup>();

        List<string> names = new List<string>();
        for (int i = 0; i < enumTypes.Count; i++)
        {
            names.Add(enumTypes[i].Name);
        }

        pathText = EditorGUILayout.TextField("Create at directory", pathText);
        Type selectedScriptable = scriptableTypes[currentSelect];
        Type selectedEnum = enumTypes[currentId];
        var selectedEnums = Enum.GetValues(selectedEnum);

        if (GUILayout.Button($"Create Scriptable Objects"))
        {
            Debug.Log("Will create scriptable object here!");
            foreach (var item in selectedEnums)
            {
                string path = pathText + "/" + selectedScriptable.ToString();
                path = path.Replace("//", "/");
                SaveScriptableObject(Activator.CreateInstance(selectedScriptable) as UnityEngine.Object, selectedScriptable.ToString() + item.ToString(), path);
            }
        }
        int COL_COUNT = 3;

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Select Enum To Create Scriptable Object");

        currentId = GUILayout.SelectionGrid(currentId, names.ToArray(), COL_COUNT, GUILayout.Width(200 * COL_COUNT));

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Scriptable Objects Will Be Create With These Names Below");

        GUI.enabled = false;

        string s = "";
        foreach (var item in selectedEnums)
        {
            s += item + "\n";
        }

        string textContent = s;
        EditorGUILayout.TextArea(textContent);
        GUI.enabled = true;
    }

    public static List<Type> GetDerivedClasses<TBase>()
	{
		var assembly = System.Reflection.Assembly.GetAssembly(typeof(Popup));  
		var derivedClasses = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TBase))).ToList();
		return derivedClasses;
	}

    public static List<Type> GetDerivedEnums<TBase>()
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(TBase)); // && t.IsSubclassOf(typeof(TBase)))
        var derivedClasses = assembly.GetTypes().Where(t => t.IsEnum).ToList();
        return derivedClasses;
    }

    public string GetInputText()
	{
		return inputText;
	}

    void SaveScriptableObject(UnityEngine.Object data, string name, string path)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        AssetDatabase.CreateAsset(data, $"{path}/{name}.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;
    }
}
#endif