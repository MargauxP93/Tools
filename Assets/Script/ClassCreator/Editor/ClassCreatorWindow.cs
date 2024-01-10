using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor;
using UnityEngine;

public class ClassCreatorWindow : EditorWindow
{
    const string WINDOWS_TITLE = "Class Creator";
    const string mainTitle = "Create a new class";
    const string defaultSearchName = "Search your parent class here";
    string newClassName = "Input your class name here";
    string selectedClassName = "Selected Class";
    string searchName = defaultSearchName;

    float labelWidth = 0;

    List<Type>allTypes=new List<Type>();
    int size = 0;
    Vector2 scrollPosition=Vector2.zero;

    [MenuItem("Class Creator/Open System")]
    public static void OpenWindow()
    {
        ClassCreatorWindow _window= GetWindow<ClassCreatorWindow>();
        _window.titleContent = new GUIContent(WINDOWS_TITLE);
    }
    private void Awake()
    {
        Init();
    }
    void Init()
    {
        searchName = defaultSearchName;       
        allTypes = ClassFinder.GetAllCachedClassesNamesDerivedFromTypes(typeof(UnityEngine.Object));
        size=allTypes.Count;
        labelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 350;
    }   
    private void OnGUI()
    {
        DrawWindowContent();
    }
    void DrawWindowContent()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(position.width),GUILayout.MaxHeight(20));
        GUILayout.FlexibleSpace();
        GUILayout.Label(mainTitle,EditorStyles.boldLabel,GUILayout.ExpandWidth(true),GUILayout.ExpandHeight(true));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        
        Rect _rectFields =EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
       
        EditorGUILayout.Space(5); 
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(false), GUILayout.Width(_rectFields.width / 2));
        EditorGUILayout.LabelField("New Class Name");
        newClassName=EditorGUILayout.TextField(newClassName);
        EditorGUILayout.EndVertical();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(false),GUILayout.MaxWidth(_rectFields.width/2));
        EditorGUILayout.LabelField("Selected Parent Class");
        selectedClassName = EditorGUILayout.TextField(selectedClassName);
        EditorGUILayout.Space(10); 
        EditorGUILayout.LabelField("Parent Class Search");
        searchName = EditorGUILayout.TextField(searchName);
        EditorGUILayout.Separator();

        DrawAllClassesList();

        EditorGUILayout.EndVertical();
        GUILayout.Space(5);



        EditorGUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (selectedClassName == "Selected Class" || selectedClassName == "")
                selectedClassName = "none";
            ClassCreator.CreateClassFile(newClassName,selectedClassName);
            Close();
        }
        if (GUILayout.Button("Close"))
            Close();

        EditorGUILayout.EndHorizontal();
    }

    void DrawAllClassesList()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        scrollPosition=EditorGUILayout.BeginScrollView(scrollPosition,GUILayout.MaxHeight(200),GUILayout.MinWidth(500));
        for (int i = 0; i < size; i++)
        {
            string _displayName = allTypes[i].Name;
            if (searchName != defaultSearchName)
            if (!_displayName.ToLower().Contains(searchName.ToLower())) continue;
            if(GUILayout.Button(_displayName,EditorStyles.miniButton))
            {
                selectedClassName = _displayName;
            }
            EditorGUILayout.Separator();
        }
        EditorGUILayout.EndScrollView();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

    }


}
