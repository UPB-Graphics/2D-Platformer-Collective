using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class SkillTreeNode : Node
{
    public string GUID;
    public SkillData data;
    public bool entryPoint = false;
}
