using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SkillTree;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    private int level, noPoints, consumedSkillPoints;
    private string skillList;
    public SkillTreeContainer skillTreeStructure;
    private bool firstUnlock;
    private GameObject clientContainter;
    private SkillTreeUI skillTreeUI;
    private List<SkillIcon> icons;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        noPoints = 1;
        consumedSkillPoints = 0;
        skillList = "";
        firstUnlock = false;
        if (IsServer) {
            UpdateLevelClientRpc(level);
            UpdateSkillPointsClientRpc(noPoints);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner) {
            UpdateSkillTree();
            if (skillTreeUI != null) {
                skillTreeUI.UpdateSkillPointCounter(noPoints - consumedSkillPoints);
            }
        }
    }

    // The method which makes the player interact with the skill tree
    private void UpdateSkillTree() {
        /* unlocking skill tree */
        if (!firstUnlock) {
            if (clientContainter == null) {
                clientContainter = GameObject.FindGameObjectWithTag("ClientInfo");
                if (clientContainter == null)
                    return;
            }
            if (skillTreeUI == null) {
                skillTreeUI = clientContainter.GetComponentInChildren<SkillTreeUI>();
            }
            icons = clientContainter.GetComponentsInChildren<SkillIcon>().ToList();
            if (icons.Count == 0) {
                return;
            } else {
                firstUnlock = true;
                string startNodeGuid = skillTreeStructure.nodeLinks[0].baseNodeGuid;
                List<NodeLinkData> nodes = skillTreeStructure.nodeLinks.Where(x => x.baseNodeGuid == startNodeGuid).ToList();
                foreach (NodeLinkData node in nodes) {
                    icons.First(x => x.Guid == node.targetNodeGuid).SetLock(false);
                }
            }
        }

        if (PressedSkillData.currentIcon == null)
            return;

        int currentLevel, maxLevel;
        int.TryParse(PressedSkillData.currentIcon.currentLevel.text, out currentLevel);
        int.TryParse(PressedSkillData.currentIcon.maxLevel.text, out maxLevel);

        if (currentLevel == maxLevel) {
            PressedSkillData.currentIcon = null;
            return;
        }

        if (consumedSkillPoints >= noPoints) {
            PressedSkillData.currentIcon = null;
            return;
        }

        consumedSkillPoints += 1;
        UpdateConsumedSkillPointsServerRpc(consumedSkillPoints);
        PressedSkillData.currentIcon.UpdateLevel(currentLevel + 1);

        if (currentLevel == 0) {
            skillList += $"{PressedSkillData.currentIcon.data.skillName} (1/{maxLevel})\n";
            // unlock all child skills
            List<NodeLinkData> children = skillTreeStructure.nodeLinks.Where(
                x => x.baseNodeGuid == PressedSkillData.currentIcon.Guid
            ).ToList();
            foreach (NodeLinkData node in children) {
                icons.First(x => x.Guid == node.targetNodeGuid).SetLock(false);
            }
        } else {
            skillList = skillList.Replace($"{PressedSkillData.currentIcon.data.skillName} ({currentLevel}/{maxLevel})\n",
                                          $"{PressedSkillData.currentIcon.data.skillName} ({currentLevel + 1}/{maxLevel})\n"
            );
        }
        UpdateSkillListServerRpc(skillList);
        PressedSkillData.currentIcon = null;
    }

    public int GetLevel() {
        return level;
    }

    public string GetSkillList() {
        return skillList;
    }

    [ClientRpc]
    private void UpdateLevelClientRpc(int level2) {
        level = level2;
    }

    [ServerRpc]
    private void UpdateConsumedSkillPointsServerRpc(int _consumedSkillPoints) {
        consumedSkillPoints = _consumedSkillPoints;
    }

    [ServerRpc]
    private void UpdateSkillListServerRpc(string _skillList) {
        skillList = _skillList;
    }

    [ClientRpc]
    private void UpdateSkillPointsClientRpc(int _noPoints) {
        noPoints = _noPoints;
    }

    public void IncrementLevel() {
        level += 1;
        UpdateLevelClientRpc(level);
    }

    public void IncrementSkillPoints() {
        noPoints += 1;
        UpdateSkillPointsClientRpc(noPoints);
    }

    public void DecrementSkillPoints() {
        consumedSkillPoints += 1;
    }
}
