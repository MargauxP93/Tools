using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.IO;
using Object = UnityEngine.Object;

public class ClassCreator 
{
    static readonly string TEMPLATE_CLASS_FILENAME = "TemplateNewClass";
    static readonly string className = "#className";
    static readonly string parentClassName = ": #parentClass";

    public static void CreateClassFile(string _className,string _parentClassName)
    {
        TextAsset _textAsset = Resources.Load<TextAsset>(TEMPLATE_CLASS_FILENAME);
        string _text = _textAsset.text;
        _text=_text.Replace(className,_className);
        _text=_text.Replace(parentClassName,_parentClassName=="none" ? "" : ": "+ _parentClassName);
        string _newFileName = _className + ".cs"; 
        string _filePath = Path.Combine(Application.dataPath,_newFileName);
        File.WriteAllText(_filePath, _text);
        AssetDatabase.Refresh();
    }
}
