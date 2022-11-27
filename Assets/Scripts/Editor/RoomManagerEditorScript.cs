using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager) target;

        if (GUILayout.Button("Join Random Room"))
        {
            roomManager.JoinRandomRoom();
        }
        
        if (GUILayout.Button("Join Beerpong Room"))
        {
            roomManager.OnEnterButtonClicked_Beerpong();
        }
        
        if (GUILayout.Button("Join DesertScene Room"))
        {
            roomManager.OnEnterButtonClicked_Desert();
        }
        
        if (GUILayout.Button("Join Beer Pong Handtrack Room"))
        {
            roomManager.OnEnterButtonClicked_HandTrack();
        }
    }
}
