using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace UnityEditorTools.GUITools
{ 
    public static class MyGUI 
    {
      public static void CreateButton(Rect _rect,string _label,Action _callback,Color _color)
        {
            GUI.color = _color;
            bool _click=GUI.Button(_rect, _label);
            GUI.color = Color.white;
            if (!_click) return;
            _callback?.Invoke();
        }
        public static void CreateButton(float _x,float _y,int _width,int _height, string _label, Action _callback, Color _color)
        {
            GUI.color = _color;
            bool _click = GUI.Button(new Rect(_x,_y,_width,_height), _label);
            GUI.color = Color.white;
            if (!_click) return;
            _callback?.Invoke();
           
        }
        public static void CreateLayoutButton(string _label,Action _callback,Color _color)
        {
            GUI.color = _color;
            bool _click = GUILayout.Button(_label);
            GUI.color = Color.white;
            if (!_click) return;
            _callback?.Invoke();
        }
        public static void CreateButtonScreenRelative(float _x, float _y, int _width, int _height, string _label, Action _callback, Color _color)
        {
            GUI.color = _color;
            _x = Mathf.Clamp(_x, 0, 1);
            _y = Mathf.Clamp(_y, 0, 1);
            bool _click=GUI.Button(new Rect(Screen.width*_x,
                                            Screen.height*_y,
                                            Screen.width*_width,
                                            Screen.height*_height), _label); 
            GUI.color=Color.white;
            if(!_click)return;
            _callback?.Invoke();
        }
    }
}
