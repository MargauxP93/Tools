using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WizardObjectType
{
    NONE,
    PRIMITIVE,
    LIGHT
}

public struct WizardColor
{
    public static Color Green => Color.green;
    public static Color Yellow => Color.yellow;
    public static Color Red => Color.red;
    public static Color GetColor(WizardObjectType _type)
    {
        switch (_type)
        {
            case WizardObjectType.LIGHT:
                return Yellow;
                break;
            case WizardObjectType.PRIMITIVE:
                return Red;
                break;
            default:
                return Green;
                break;
        }
    }
}


[Serializable]
public class WizardObject 
{
    [SerializeField] GameObject refObject = null;
    [SerializeField] bool editMode = false;
    public bool EditMode => editMode;
    public Color Color { get; set; }
    public WizardObjectType Type { get; set;}
    public GameObject RefObject => refObject;

    public bool IsValid => refObject;
    public string Name => IsValid ? refObject.name : "Null";

    public Vector3 Position => IsValid ? refObject.transform.position : Vector3.zero;
    public WizardObject(GameObject _refObject,WizardObjectType _type)
    {
        refObject=_refObject;
        Type=_type;
        Color = WizardColor.GetColor(Type);
    }
    public void SetEdition(bool _status)
    {
        editMode = _status;
    }
    public void SetPosition(Vector3 _pos)
    {
        if (!IsValid) return;
        refObject.transform.position = _pos;
    }
    public void SetName(string _name)
    {
        if (!IsValid) return;
        refObject.name = _name;
    }

    public static bool operator ! (WizardObject _object)//surcharge d'operator
    {
        return _object == null;
    }
    public override string ToString()
    {
        return Type.ToString();
    }
}
