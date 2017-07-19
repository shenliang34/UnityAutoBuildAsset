using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor {
    public override void OnInspectorGUI()
    {
        var t = target as Test;
        t.isTest = GUILayout.Toggle(t.isTest, "IsTest");
        if(t.isTest)
        {
            t.range = EditorGUILayout.Slider("Range", t.range,1,100);
        }
    }
	
}
