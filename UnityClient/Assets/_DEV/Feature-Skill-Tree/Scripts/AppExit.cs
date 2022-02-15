using UnityEngine;

public class AppExit : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}