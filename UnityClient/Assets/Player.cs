using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp = 100;
    public Material ironSkinMat;

    private Material defMat;
    private bool ironSkin = false;
    private float ironSkinTime = 5f;

    void Awake() {
        defMat = GetComponent<MeshRenderer>().material;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !ironSkin) {
            ironSkin = true;
            GetComponent<MeshRenderer>().material = ironSkinMat;
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            takeDamage(20);
        }

        if(ironSkinTime < 0) {
            GetComponent<MeshRenderer>().material = defMat;
            ironSkin = false;
            ironSkinTime = 5f;
        } else {
            ironSkinTime -= Time.deltaTime;
        }
    }

    void takeDamage(float damage) {
        if (ironSkin) {
            hp -= damage / 3;
        } else {
            hp -= damage;
        }
    }
}
