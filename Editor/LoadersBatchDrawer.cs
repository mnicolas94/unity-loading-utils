using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LoadingUtils.Editor
{
    [CustomPropertyDrawer(typeof(LoadersBatch))]
    public class LoadersBatchDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            
            var loadersProperty = property.FindPropertyRelative(LoadersBatch.LoadersPropertyName);
            var propertyField = new PropertyField(loadersProperty);
            
            propertyField.RegisterCallback<AttachToPanelEvent>(evt =>
            {
                propertyField.schedule.Execute(() =>
                {
                    var listView = propertyField.Q<ListView>();
                    if (listView != null)
                    {
                        listView.showFoldoutHeader = false;
                        listView.showBoundCollectionSize = false;
                    }
                });
            });
            
            root.Add(propertyField);
            
            return root;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(position, property, label, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}