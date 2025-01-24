using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInjectorEditor : EditorWindow
{
    public enum SessionType
    {
        Client,
        Server
    }

    [MenuItem("Window/JD/Editor Play Mode Tool")]
    static public void ShowWindow()
    {
        GetWindow<LoadInjectorEditor>("JD Editor Play Mode Tool");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Load Client"))
        {
            BuildProfile.SetActiveBuildProfile(Resources.Load<BuildProfile>("Build Profiles/Client"));
            EditorSceneManager.OpenScene("Assets/Scenes/Initialisation.unity"); //temp

            EditorApplication.isPlaying = true;
        }
    }
}
