using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WizardComponent : MonoBehaviour
{
    [SerializeField] WizardObjectCollector collector=new WizardObjectCollector();
    public WizardObjectCollector Collector=>collector;
    public string[] GetPrimitives=>GetEnumNames<PrimitiveType>();
    public string[] GetLights => GetEnumNames<LightType>();

    string[] GetEnumNames<T>()where T:Enum
    {
        return Enum.GetNames(typeof(T));
    }
    public void Create3DPrimitives(int _index)
    {
        PrimitiveType _type= (PrimitiveType)_index;
        GameObject _new = GameObject.CreatePrimitive(_type);
        collector.Add(_new, WizardObjectType.PRIMITIVE);
    }
    public void Create3DLights(int _index)
    {
        LightType _type=(LightType)_index;
        string _name=_type==LightType.Area?"Area":_type.ToString(); // recupere le type et regarde si il est area, si oui le nom area sinon converti en string
        GameObject _object =new GameObject($"{_name}Light");
        Light _lightComponent=_object.AddComponent<Light>();
        _lightComponent.type = _type;
        collector.Add(_object,WizardObjectType.LIGHT);
    }
}
[Serializable]
public class WizardSection
{
    [SerializeField] string title = "";
    [SerializeField] string[] names = null;

    public string Title => title;
    public string[] Names => names;

    public WizardSection(string _title, string[]_names)
    {
        title= _title; 
        names= _names;
    }
}

