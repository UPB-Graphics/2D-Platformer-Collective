using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "ScriptableObjects/Skill Info", order = 1)]

public class SkillData : ScriptableObject
{
    public string skillName;
    [TextArea]
    public string description;
    public int maxLevels;
    public Sprite icon;
}
