using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(T_ExampleComponent))]
public class T_ExampleComponentEditor : Editor
{
    event Action OnInspectorGUIEvent=null;
    T_ExampleComponent example=null;
    // Cours Initial
    SerializedProperty intProperty=null;
    //SerializedProperty vectorProperty =null;

    SerializedProperty cameraObject=null;
    // Cours Initial

    //Premiere pratique
    const string FLOAT_PROPERTYNAME = "size";
    const string VECTOR_PROPERTYNAME = "position";
    SerializedProperty floatProperty = null;
    SerializedProperty vectorProperty = null;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        example = (T_ExampleComponent)target;
        OnInspectorGUIEvent += () =>
        {
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        };

        ////Cours Initial
        
        //intProperty = serializedObject.FindProperty("intValue");
        //int _value =intProperty.intValue;
        //Debug.Log(_value);

        //vectorProperty = serializedObject.FindProperty("vectorValue");
        //Vector3 _vector = vectorProperty.vector3Value;

        ////cameraObject = serializedObject.FindProperty("myCam");
        ////SerializedProperty _property = cameraObject.FindPropertyRelative("gameObject");
        ////Debug.Log(_property.objectReferenceValue);

        ////Cours Initial

        // Premiere pratique
        floatProperty = serializedObject.FindProperty(FLOAT_PROPERTYNAME);
        vectorProperty = serializedObject.FindProperty(VECTOR_PROPERTYNAME);

    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        OnInspectorGUIEvent?.Invoke();
        Debug.Log(floatProperty.floatValue);
        
    }
    void DrawProperties()
    {
        floatProperty.floatValue = EditorGUILayout.Slider("Size Value", floatProperty.floatValue, 0, 10);
        vectorProperty.vector3Value=EditorGUILayout.Vector3Field("Current Position:",vectorProperty.vector3Value);
    }
    void ClickButton()
    {
        bool _click = GUILayout.Button("Click me");
        if (!_click) return;
        
            Debug.Log("Test");
        
    }
}
