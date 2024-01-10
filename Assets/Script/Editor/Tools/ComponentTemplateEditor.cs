using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public abstract class ComponentTemplateEditor<T> : Editor where T : MonoBehaviour
{
    protected T editorTarget=default(T);

   

    protected virtual void OnEnable()
    {
        editorTarget = (T)target;
       
    }
    
    

}
