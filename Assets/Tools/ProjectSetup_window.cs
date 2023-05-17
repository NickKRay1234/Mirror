using UnityEngine;
using UnityEditor;
using System.IO;

namespace Tools
{
    // public class ProjectSetup_window : EditorWindow
    // {
    //     // #region Variables
    //     //
    //     // private static ProjectSetup_window win;
    //     // private string gameName = "Game";
    //     //
    //     // #endregion
    //     //
    //     // #region Main Methods
    //     //
    //     // public static void InitWindow()
    //     // {
    //     //     win = EditorWindow.GetWindow<ProjectSetup_window>("Project Setup");
    //     //     win.Show();
    //     // }
    //     //
    //     // void OnGUI()
    //     // {
    //     //     EditorGUILayout.BeginHorizontal();
    //     //     EditorGUILayout.LabelField("Project Setup");
    //     //     gameName = EditorGUILayout.TextField("Game name: ", gameName);
    //     //     EditorGUILayout.EndHorizontal();
    //     //
    //     //     if (GUILayout.Button("Create Project Structure", GUILayout.Height(35), GUILayout.ExpandWidth(true)))
    //     //     {
    //     //         CreateProjectFolders();
    //     //     }
    //     //
    //     //     if (win != null)
    //     //     {
    //     //         win.Repaint();
    //     //     }
    //     // }
    //     //
    //     // #endregion
    //     //
    //     // #region Custom Methods
    //     //
    //     // void CreateProjectFolders()
    //     // {
    //     //     if (string.IsNullOrEmpty(gameName)) 
    //     //         return;
    //     //     if (gameName == "Game")
    //     //         if (!EditorUtility.DisplayDialog("Project Setup Warning",
    //     //                 "Do you really want to call your project Game?", "Yes", "No"))
    //     //             return;
    //     //     Debug.Log("Creating Folders!");
    //     //     string assetPath = Application.dataPath;
    //     //     string rootPath = assetPath + "/" + gameName;
    //     //     Directory.CreateDirectory(rootPath);
    //     //     AssetDatabase.Refresh();
    //     //     CloseWindow();
    //     // }
    //     //
    //     // void CloseWindow()
    //     // {
    //     //     if(win) win.Close();
    //     // }
    //     //
    //     // #endregion
    // }
}
