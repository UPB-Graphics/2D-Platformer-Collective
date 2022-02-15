using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerModify : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxLevel = 45;
    public TextMeshProUGUI skillList;
    public TextMeshProUGUI levelText;
    private PlayerHandler playerHandler = null;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 0) {
            return;
        }
        
        if (playerHandler == null) {
            ulong idx = NetworkManager.Singleton.ConnectedClientsIds[0];
            playerHandler = NetworkManager.Singleton.ConnectedClients[idx].PlayerObject.GetComponent<PlayerHandler>();
        }
        levelText.text = $"Level: {playerHandler.GetLevel()}";
        skillList.text = $"Skill List\n{playerHandler.GetSkillList()}";
    }

    public void OnAddLevelClick() {
        playerHandler.IncrementLevel();
        playerHandler.IncrementSkillPoints();
    }
}
