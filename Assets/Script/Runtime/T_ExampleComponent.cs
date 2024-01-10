using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_ExampleComponent : MonoBehaviour
{
    [SerializeField] int intValue = 5;
    [SerializeField] Vector3 vectorValue = Vector3.zero;
    [SerializeField] Camera myCam = null;

    [SerializeField] float size = 2;
    [SerializeField] Vector3 position=Vector3.zero;

    [SerializeField] Button button = null;

    public int IntValue => intValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
