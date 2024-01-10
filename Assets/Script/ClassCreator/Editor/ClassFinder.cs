using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;

[Serializable]
public class ClassFinder 
{
   public static List<Type> GetAllClassesNames()
    {
        List<Type> _allTypes=new List<Type>();
        _allTypes = GetClassesFromAssembly().ToList();
        return _allTypes;
    }

    static Type[] GetClassesFromAssembly()
    {
        Assembly _assembly =typeof(ClassFinder).Assembly;
        Type[] _arrayTypes = new Type[] { };
        _arrayTypes= _assembly.GetTypes();
        return _arrayTypes;

    }
    public static List<Type>GetAllClassesNamesDerivedFromType(Type _type)
    {
        List<Type>_allTypes=new List<Type>();
        _allTypes = GetClassesFromAssemblyDerivedFromType(_type);
        return _allTypes;
    }
    static List<Type> GetClassesFromAssemblyDerivedFromType(Type _type)
    {
        Assembly _assembly=typeof(UnityEngine.Object).Assembly;
        List<Type> _allTypes = new List<Type>();
        _allTypes=_assembly.GetTypes().Where(t=>t!=_type&&_type.IsAssignableFrom(t)).ToList();
        return _allTypes;
    }
    public static List<Type> GetAllCachedClassesNamesDerivedFromTypes(Type _type)
    {
        List<Type>_allTypes=new List<Type>();
        _allTypes = TypeCache.GetTypesDerivedFrom(_type).ToList();
        return _allTypes;
    }
}
