using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Komastar.IdleMiner.Editor
{
    public class DevAssist : EditorWindow
    {
        [MenuItem("DevAssist/Init Project")]
        public static void InitProject()
        {
            EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
                {
                    new EditorBuildSettingsScene("Assets/Scenes/GameScene.unity", true)
                };
        }

        [MenuItem("DevAssist/Remove Save Data")]
        public static void RemoveSaveData()
        {
            string savePath = Constant.PlayerPath.Save;
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                EditorUtility.DisplayDialog("Save data remove", "Remove Success", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Save data remove", "Remove Fail : NO SAVE DATA", "OK");
            }
        }

        [MenuItem("DevAssist/Export/Package")]
        public static void ExportPackage()
        {
            AssetDatabase.ExportPackage($"Assets", $"{Application.dataPath}/GameBerryPreTest-komastar-{DateTime.Now.ToString("yyyy.MM.dd")}.unitypackage", ExportPackageOptions.Recurse);
        }
    }
}