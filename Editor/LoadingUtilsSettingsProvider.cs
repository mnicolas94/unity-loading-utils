using UnityEditor;
using Utils.Editor;

namespace LoadingUtils.Editor
{
    public class LoadingUtilsSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider()
        {
            var so = new SerializedObject(LoadingSettings.Instance);
            var provider = new SettingsProvider("Project/Facticus/Loading utils", SettingsScope.Project)
            {
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.Space(12);
                    PropertiesUtils.DrawSerializedObject(so);
                },

                keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(so)
            };

            
            return provider;
        }
    }
}