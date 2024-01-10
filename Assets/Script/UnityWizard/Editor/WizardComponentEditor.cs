using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorTools.GUITools;

[CustomEditor(typeof(WizardComponent))]

public class WizardComponentEditor : ComponentTemplateEditor<WizardComponent>
{
    event Action OnInspectorOverride = null;

    const string TOOL_NAME = "[UNITY WIZARD]";

    #region UnityEditor

    protected override void OnEnable() // apellé une fois dès que l'on select l'object dans la hierarchie ou la scene
    {
        base.OnEnable();
        editorTarget.name=TOOL_NAME;

        OnInspectorOverride += () =>
        { //abonne a inspect override la creation d'une section ( classe custom native) qui définit l'ui de l'inspector pour les boutons
            //des formes primitives ,on lui donne d'abord le wizardsection qui prend un titre et la liste des noms des primitives
            //la longueur de la liste des primitives et la fonction a apeller a linkerr a chaque bouton crée 
            DrawSectionGUIHorizontal(new WizardSection("3D Primitives",editorTarget.GetPrimitives),
                editorTarget.GetPrimitives.Length,editorTarget.Create3DPrimitives);
            DrawSectionGUIHorizontal(new WizardSection("3D Lights", editorTarget.GetLights),
                editorTarget.GetLights.Length-2, editorTarget.Create3DLights);
            DrawCollectorUI();


        };
    }
    public override void OnInspectorGUI() // apellé en tick tant que l'on focus l'inspector avec l'object select dans la hierarc ou scene
    {
        //base.OnInspectorGUI();
        OnInspectorOverride?.Invoke();
    }
    private void OnSceneGUI()
    {
        DrawObjectSceneUI();
    }
    private void OnDisable() // appelé une fois des que l'on deselect l'object dans la hiérarch ou la scene 
    {
        OnInspectorOverride = null;
    }


    #endregion UnityEditor

    #region CustomSection

    void DrawSectionGUIHorizontal(WizardSection _section,int _count,Action<int> _callback)
    {
        EditorGUILayout.HelpBox(_section.Title, MessageType.None);
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < _count; i++)
        {
            string _name = _section.Names[i];
            MyGUI.CreateLayoutButton(_name, () => _callback(i), Color.white);
            //on crée un bouton a chaque tour de boucle et on lui donne le nom de la primitive à l'index actuel
        }
        EditorGUILayout.EndHorizontal();

        
    }

    #endregion CustomSection

    #region CollectorUI

    void DrawCollectorUI()
    {
        EditorGUILayout.HelpBox("Collector", MessageType.None);
        int _count = editorTarget.Collector.Count;
        if (_count == 0) return;
        WizardObjectCollector _collector = editorTarget.Collector;
        MyGUI.CreateLayoutButton("Clear All", () => ClearItemsUI(_collector), Color.red);
        for (int i = 0; i < _count; i++)
        {
            WizardObject _object = _collector.Get(i);
            if (!_object) return;
            //if(i%3==0)
            EditorGUILayout.BeginHorizontal();
            GUI.color = _object.Color;
            EditorGUILayout.HelpBox(_object.ToString(), MessageType.None);
            GUI.color=Color.white;
            EditorGUILayout.BeginVertical();
            _object.SetName(EditNameUI(_object.Name));
            _object.SetPosition(EditPositionUI(_object.Position));

            EditorGUILayout.EndVertical();
            MyGUI.CreateLayoutButton("Edit", () =>
            {
                _collector.ResetAllObject();
                _object.SetEdition(!_object.EditMode);
            }, 
            _object.EditMode?Color.cyan:Color.blue);
            MyGUI.CreateLayoutButton("Select", () => SelectItemUI(_object), Color.cyan);

            MyGUI.CreateLayoutButton("Delete", () => DeleteItemUI(_object), Color.red);
            //if(i%3==2||i==_count-1)
            EditorGUILayout.EndHorizontal();
            if (!_collector.Exist(i))
                _collector.Remove(i);
        }
        Repaint();
    }
    #endregion CollectorUI

    #region EditWizardObject

    void SelectItemUI(WizardObject _object)
    {
        Selection.activeGameObject = _object.RefObject;       
    }
    void DeleteItemUI(WizardObject _object)
    {
        DestroyImmediate(_object.RefObject);
    }
    void ClearItemsUI(WizardObjectCollector _collector)
    {
        int _count= _collector.Count;
        for (int i = 0; i < _count; i++)
        {
            WizardObject _object=_collector.Get(i);
            DestroyImmediate(_object.RefObject);
        }
        _collector.Clear();
    }
    Vector3 EditPositionUI(Vector3 _pos)
    {
        return EditorGUILayout.Vector3Field("Position", _pos);
    }
    string EditNameUI(string _name)
    {
        return EditorGUILayout.TextField(_name);
    }

    #endregion EditWizardObject
    #region EditWizardObjectScene

    void DrawObjectSceneUI()
    {
        //if (editorTarget.Collector.Count < 1) return;
        //WizardObject _object = editorTarget.Collector.Get(0);
        //Handles.DoPositionHandle(_object.Position, Quaternion.identity);

        int _count=editorTarget.Collector.Count;
        if (_count == 0) return;
        WizardObjectCollector _collector=editorTarget.Collector;
        for (int i = 0; i < _count; i++)
        {
            WizardObject _object = _collector.Get(i);
            if (!_object) continue;
            DrawWireDisc(_object.Position,Vector3.up,1f,Color.red);
            DrawSolidDisc(_object.Position, Vector3.up, 1f, new Color(1,0,0,.25f));
            if (!_object.EditMode) continue;
            _object.SetPosition(Handles.DoPositionHandle(_object.Position, Quaternion.identity));
            DrawSolidDisc(_object.Position + Vector3.up * 2, Vector3.up, 1f, Color.green);
        }
    }
    void DrawWireDisc(Vector3 _center,Vector3 _normal,float _radius,Color _color)
    {
        Handles.color = _color;
        Handles.DrawWireDisc(_center, _normal, _radius);
        Handles.color = Color.white;
    }
    void DrawSolidDisc(Vector3 _center, Vector3 _normal, float _radius, Color _color)
    {
        Handles.color = _color;
        Handles.DrawSolidDisc(_center, _normal, _radius);
        Handles.color = Color.white;
    }
    void DrawWireCube(Vector3 _center, Vector3 _size, Color _color)
    {
        Handles.color = _color;
        Handles.DrawWireCube(_center, _size);
        Handles.color = Color.white;
    }


    #endregion EditWizardObjectScene
}
