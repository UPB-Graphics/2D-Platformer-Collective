using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Molotov : MonoBehaviour
{
    public GameObject bottle;
    public GameObject throwPoint;
    public Text ammoDisplay;

    private void Start()
    {
        var text = GameObject.FindGameObjectWithTag("MolotovDisplay");
        ammoDisplay = text?.GetComponentInChildren<Text>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Throw"))
        {
            ThrowBottle();
        }

        ammoDisplay.text = "Ammo: " + GetComponent<CharacterStats>().currentAmmo.ToString();
    }

    public void ThrowBottle()
    {
        if (GetComponent<CharacterStats>().currentAmmo > 0)
        {
            var projectile = Instantiate(bottle, throwPoint.transform.position, throwPoint.transform.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwPoint.transform.right.x*300, 200));
            GetComponent<CharacterStats>().currentAmmo--;
        }
    }
}
