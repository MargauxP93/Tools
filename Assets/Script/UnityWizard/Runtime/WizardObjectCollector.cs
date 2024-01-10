using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class WizardObjectCollector 
{
    [SerializeField]List<WizardObject> objects= new List<WizardObject>();
    public int Count => objects.Count;

    public void Add(GameObject _refObject,WizardObjectType _type)
    {
        WizardObject _wo=new WizardObject(_refObject, _type);
        objects.Add(_wo);  
    }
    public WizardObject Get(int _index)
    {
        bool _isNotValidIndex = _index > objects.Count - 1 || _index < 0;
        return _isNotValidIndex ? null : objects[_index];
    }
    public bool Exist(int _index)
    {
        return objects[_index].IsValid;
    }
    public void Remove(int _index)
    {
        objects.RemoveAt(_index);
    }
    public void Clear()
    {
        objects.Clear();
    }
    public void ResetAllObject()
    {
        int _count = Count;
        for (int i = 0; i < _count; i++)
        {
            objects[i].SetEdition(false);
        }
    }
}
