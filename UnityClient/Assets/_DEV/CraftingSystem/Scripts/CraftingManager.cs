using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerCollective.CraftingSystem
{
    public class CraftingManager : MonoBehaviour
    {
        public CraftingRecipe[] recipes;
        public CraftingMenu craftingMenu;
        public GameObject craftText;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                craftingMenu.ClearCraftZone();
                craftingMenu.gameObject.SetActive(!craftingMenu.gameObject.activeSelf);
                craftText.SetActive(!craftingMenu.gameObject.activeSelf);
            }
        }
    }
}
