using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SceneChangeLogic : MonoBehaviour {
    public void StartHost() {
        NetworkManager.Singleton.StartServer();
    }

    public void StartClient() {
        NetworkManager.Singleton.StartClient();
    }
}
