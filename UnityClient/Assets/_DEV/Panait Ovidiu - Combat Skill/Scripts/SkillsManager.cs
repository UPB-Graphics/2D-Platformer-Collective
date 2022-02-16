using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    #region Singleton
    public static SkillsManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    public List<CombatSkill> skills = new List<CombatSkill>();

    [SerializeField] public CombatSkill fireballSkill;

    public const int FIREBALL_INDEX = 0;

    private void Start()
    {
        AddSkill(fireballSkill);
    }

    public void AddSkill(CombatSkill skill)
    {
        skills.Add(skill);
    }

}
