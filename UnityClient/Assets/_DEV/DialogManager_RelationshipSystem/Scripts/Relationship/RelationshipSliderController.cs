using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipSliderController : MonoBehaviour
{
    int initialValue = 0;
    int currentValue = 0;
   
    public enum RelationshipLevel
    {
        Friendly,   //[50, 100]
        Neutral,    //[0, 49]
        Unfriendly, //[-50, -1]
        Hostile     //[-100, -51]
    }

    RelationshipLevel relationshipLevel = RelationshipLevel.Hostile;
    public Slider relationshipSlider;
    public Image fillArea;
    public Text RelationshipLevelText;

    void Start()
    {
        relationshipSlider.wholeNumbers = true;
        relationshipSlider.value = initialValue;
        fillArea.color = Color.yellow;
    }

    public void updateRelationshipBar(int value)
    {
        relationshipSlider.value = relationshipSlider.value + value;
        currentValue = (int)relationshipSlider.value;
        switch (currentValue)
        {
            case int n when (n >= 50):
                fillArea.color = Color.green;
                relationshipLevel = RelationshipLevel.Friendly;
                break;

            case int n when (n >= 0 && n < 50):
                fillArea.color = Color.yellow;
                relationshipLevel = RelationshipLevel.Neutral;
                break;

            case int n when (n >= -50 && n < -1):
                fillArea.color = new Color(255, 165, 0);
                relationshipLevel = RelationshipLevel.Unfriendly;

                break;

            case int n when (n < -50):
                fillArea.color = Color.red;
                relationshipLevel = RelationshipLevel.Hostile;
                break;
        }

        RelationshipLevelText.text = "Relationship points: " + relationshipSlider.value + "\nRelationship Level: " + relationshipLevel;
    }


}
