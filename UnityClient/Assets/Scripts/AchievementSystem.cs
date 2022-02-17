using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    private static AchievementSystem _instance;
    public static AchievementSystem Instance { get { return _instance; } }

    [SerializeField]Transform AchievementHolder;
    [SerializeField]GameObject AchievementPrefab;

    [SerializeField] Sprite[] achievementSprites;

    Dictionary<int, Achievement> AchievementDatabase = new Dictionary<int, Achievement>(); // Key-ul reprezinta id-ul achievement-ului.


    List<int> playerAchievements = new List<int>();
    //Singleton instantiation
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        //Adding achievements to database;
        AchievementDatabase.Add(0, new Achievement(0, achievementSprites[0], "First Steps in 2D-Platformer-Collective.", "You opened the game!"));
        AchievementDatabase.Add(1, new Achievement(1, achievementSprites[1], "Roses are red,", "You pressed the red button."));
        AchievementDatabase.Add(2, new Achievement(2, achievementSprites[2], "Violets are blue,", "You pressed the blue button button."));
        AchievementDatabase.Add(3, new Achievement(2, achievementSprites[3], "This is a secret achievement!", "You unlocked this by pressing P"));


        addAchievement(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            addAchievement(3);
        }
    }
    public void addAchievement(int id)
    {
        foreach (var item in playerAchievements)
        {
            if (item == id)
            {
                return;
            }
        }
        playerAchievements.Add(id);
    }

    public void showAchievementPanel()
    {
        for (int i = 0; i < playerAchievements.Count; i++)
        {
            GameObject go = Instantiate(AchievementPrefab, AchievementHolder);
            go.transform.GetChild(0).GetComponent<Image>().sprite = AchievementDatabase[playerAchievements[i]].Icon;
            go.transform.GetChild(1).GetComponent<Text>().text = AchievementDatabase[playerAchievements[i]].Title;
            go.transform.GetChild(2).GetComponent<Text>().text = AchievementDatabase[playerAchievements[i]].Description;
        }
    }
    public void clearAchievements()
    {
        foreach (Transform child in AchievementHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
