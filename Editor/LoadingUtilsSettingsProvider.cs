using UnityEditor;
using UnityEngine;
using Utils.Editor;

namespace LoadingUtils.Editor
{
    public class LoadingUtilsSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider()
        {
            bool existsSettings = LoadingSettings.Instance != null;
            var so = existsSettings ? new SerializedObject(LoadingSettings.Instance) : null;
            var keywords = existsSettings ? SettingsProvider.GetSearchKeywordsFromSerializedObject(so) : new string[0];
            var provider = new SettingsProvider("Project/Facticus/Loading utils", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.Space(12);
                    
                    if (existsSettings)
                        PropertiesUtils.DrawSerializedObject(so);
                    else
                    {
                        var r = EditorGUILayout.GetControlRect();
                        if (GUI.Button(r, "Create settings"))
                        {
                            var settings = ScriptableObject.CreateInstance<LoadingSettings>();
                            AssetDatabase.CreateAsset(settings, "Assets/LoadingSettings.asset");
                        }
                    }
                },

                keywords = keywords
            };

            
            return provider;
        }
    }
}