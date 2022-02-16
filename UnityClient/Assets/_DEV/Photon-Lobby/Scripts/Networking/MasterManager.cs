using System.Collections.Generic;
using Photon.Pun;
using Platformer.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Networking
{
    [CreateAssetMenu(menuName = "Platformer/Singletons/Master Manager", fileName = "MasterManager")]
    public class MasterManager : SingletonScriptableObject<MasterManager>
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private List<NetworkPrefab> _networkPrefabs = new List<NetworkPrefab>();

        public static GameSettings GameSettings
        {
            get { return Instance._gameSettings; }
        }


        public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
        {
            foreach (NetworkPrefab networkPrefab in Instance._networkPrefabs)
            {
                if (networkPrefab.Prefab == obj)
                {
                    if (networkPrefab.Path != string.Empty)
                    {
                        GameObject result = PhotonNetwork.Instantiate(networkPrefab.Path, position, rotation);
                        return result;
                    }
                    else
                    {
                        Debug.LogError("Path is empty for GameObject name " + networkPrefab.Prefab);
                        return null;
                    }
                }
            }

            return null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void PopulateNetworkedPrefabs()
        {
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().buildIndex != 0) return;
            Instance._networkPrefabs.Clear();
            GameObject[] results = Resources.LoadAll<GameObject>("");
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].GetComponent<PhotonView>() != null)
                {
                    string path = AssetDatabase.GetAssetPath(results[i]);
                    Instance._networkPrefabs.Add(new NetworkPrefab(results[i], path));
                }
            }
#endif
        }
    }
}