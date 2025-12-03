using System;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _SecondGameJam.Scripts.Editor
{
    public class GenericGameEventCreator : EditorWindow
    {
        [SerializeField] private string _typeNamespace;
        [SerializeField] private string _typeParameterName;
        [SerializeField] private string _typeParameterSubClass;

        /** Shows a window for creating a generic ScriptableObject */
        [MenuItem("Window/Generic GameEvent Creator")]
        public static void ShowWindow()
        {
            var window = GetWindow<GenericGameEventCreator>();
        }

        /** The GUI for the window - using Text Input for parameters and a Button for method call. */
        private void OnGUI()
        {
            GUILayout.Label("Create a Generic GameEvent", EditorStyles.boldLabel);

            // Input fields:
            _typeNamespace = EditorGUILayout.TextField("Event type's namespace", _typeNamespace);
            _typeParameterName = EditorGUILayout.TextField("Event type's name", _typeParameterName);
            _typeParameterSubClass = EditorGUILayout.TextField("Event type's sub class", _typeParameterSubClass);

            // Create on button click:
            if (GUILayout.Button("Create ScriptableObject"))
            {
                CreateGenericGameEvent(_typeNamespace, _typeParameterName, _typeParameterSubClass);
            }
        }

        /** The button's OnClick() method - Creates a new Generic Game Event. */
        private void CreateGenericGameEvent(string typeNamespace, string typeParameterName, string subClass)
        {
            try
            {
                // Get the generic type and the type parameter:
                Type genericType = typeof(GameEvent<>);
                var fullParameterName = $"{typeNamespace}.{typeParameterName}" +
                                        $"{(string.IsNullOrEmpty(subClass) ? "" : $"+{subClass}")}";
                Type typeParameter = null;
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    typeParameter = asm.GetType(fullParameterName);
                    if (typeParameter != null)
                    {
                        break;
                    }
                }

                if (typeParameter == null)
                {
                    Debug.LogError($"Class's type not found: {fullParameterName}");
                    return;
                }

                // Create an instance of the generic type:
                var concreteType = genericType.MakeGenericType(typeParameter);
                var instance = CreateInstance(concreteType);

                // Save the instance as an asset:
                string path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject",
                    $"New{(string.IsNullOrEmpty(subClass) ? typeParameterName : subClass)}GameEvent",
                    "asset", "Please enter a file name to save the ScriptableObject to");
                if (String.IsNullOrEmpty(path))
                {
                    return;
                }

                AssetDatabase.CreateAsset(instance, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                // Select the instance in the Project window:
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = instance;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to create a Generic GameEvent: " + e.Message);
                Debug.LogError(e.StackTrace);
            }
        }
    }
}