using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkillTree
{

    [System.Serializable]
    public class LockSkill
    {
        public SkillIcon icon;
        public bool lck;
    }

    public class TestSkillTreeUI : MonoBehaviour
    {
        public List<LockSkill> lockSkills;
        // Update is called once per frame
        void Update()
        {
            foreach (LockSkill ls in lockSkills)
            {
                if (ls.icon != null)
                {
                    ls.icon.SetLock(ls.lck);
                }
            }
        }
    }
}