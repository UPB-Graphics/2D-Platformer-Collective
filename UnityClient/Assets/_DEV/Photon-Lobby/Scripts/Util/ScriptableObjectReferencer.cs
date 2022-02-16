using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

using UnityEngine;

namespace Platformer.Utils
{
    public class ScriptableObjectReferencer : MonoBehaviour
#if UNITY_EDITOR
        ,IPreprocessBuildWithReport
#endif
    {
        [SerializeField] private string _folder;
        
        [SerializeField] public List<ScriptableObject> scriptableObjects;

#if UNITY_EDITOR
        private void AddAllRefs()
        {
            scriptableObjects = new List<ScriptableObject>(GetAllInstances<ScriptableObject>());
        }

        private List<T> GetAllInstances<T>() where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { $"Assets/{_folder}" });
            var list = new List<T>();
            for (int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                list.Add(asset);
                EditorUtility.SetDirty(asset);
            }
            return list;
        }
        
        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report)
        {
            AddAllRefs();
            Debug.Log("Preprocessing log for scriptable references " + report);
        }
#endif
    }
}