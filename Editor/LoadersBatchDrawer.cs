using LoadingUtils;
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
            
            propertyField.RegisterCallbackOnce<GeometryChangedEvent>(evt =>
            {
                var listView = propertyField.Q<ListView>();
                if (listView != null)
                {
                    listView.showFoldoutHeader = false;
                    listView.showBoundCollectionSize = false;
                }
            });
            
            root.Add(propertyField);
            
            return root;
        }
    }
}