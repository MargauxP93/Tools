using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class WindowsEditor : EditorWindow
{
    public enum Choice
    {
        NONE,
        ACTOR,
        COMPONENT,
        CHARACTER,

    }


    Choice choice = Choice.NONE;

    [MenuItem("MonMenu/Cr�er une classe")]
    public static void ShowWindow()
    {
        WindowsEditor _window =(WindowsEditor)GetWindow(typeof(WindowsEditor));
        _window.minSize = new Vector2(600, 300);
        _window.Show();
        //EditorWindow.GetWindow(typeof(WindowsEditor));
    }

    private void OnGUI()
    {
        DrawSettings();
    }
    void DrawSettings()
    {
        EditorGUILayout.LabelField("Cr�er une classe", EditorStyles.boldLabel);
        choice = (Choice)EditorGUILayout.EnumPopup(choice);
        //EditorGUILayout.FloatField();

        EditorGUILayout.LabelField("Entre la classe de ton choix", EditorStyles.boldLabel);
        EditorGUILayout.TextField(string.Empty);
        EditorGUILayout.LinkButton("Cr�er la classe", GUILayout.Height(30));
            {
            //.Close();
        }
    }

        
}
