using UnityEngine;

namespace Platformer.Networking
{
    
    [System.Serializable]
    public class NetworkPrefab
    {
        public GameObject Prefab;
        public string Path;

        public NetworkPrefab(GameObject obj, string path)
        {
            Prefab = obj;
            Path = ReturnPrefabPathModified(path);
        }

        //Assets/Resources/File.prefab -- this is the actual path
        //Resources/File -- this is the path i need
        private string ReturnPrefabPathModified(string path)
        {
            var extensionLength = System.IO.Path.GetExtension(path).Length;
            var additionalLength = 10;
            var startIndex = path.ToLower().IndexOf("resources");

            if (startIndex == -1)
            {
                return string.Empty;
            }
            else
            {
                return path.Substring(startIndex + additionalLength, path.Length - (startIndex + additionalLength + extensionLength));
            }
        }
    }
}