using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Specify which script to customize though editor
[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This responsible for connecting to photon Servers", MessageType.Info);

        LoginManager loginManager = (LoginManager)target;
        if (GUILayout.Button("Connect Anonymously"))
        {
            loginManager.ConnectAnonymously();
        }
    }
}
