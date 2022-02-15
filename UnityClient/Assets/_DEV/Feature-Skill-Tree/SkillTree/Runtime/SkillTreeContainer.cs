using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillTree;

[System.Serializable]
public class SkillTreeContainer : ScriptableObject
{
    public string baseNodeGuid;
    public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
    public List<SkillTreeNodeData> skillTreeNodeData = new List<SkillTreeNodeData>();
}
