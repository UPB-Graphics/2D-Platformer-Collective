using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{

    [System.Serializable]
    public class NodeLinkData
    {
        public string baseNodeGuid;
        public string portName;
        public string targetNodeGuid;
    }
}