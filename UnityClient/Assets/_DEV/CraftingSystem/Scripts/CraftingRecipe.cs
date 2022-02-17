using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlatformerCollective.CraftingSystem
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "CraftingSystem/Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        public List<RecipeSlot> recipeContent;
        public RecipeSlot recipeProduct;
    }
}