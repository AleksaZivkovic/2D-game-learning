using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoinManager))]
public class CoinManagerEditor : Editor{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset diamonds")) {
            PlayerPrefs.SetInt("diamonds", 0);
        }
    }
}
