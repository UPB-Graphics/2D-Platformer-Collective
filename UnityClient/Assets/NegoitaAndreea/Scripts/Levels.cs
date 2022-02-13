using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField]
    private int level;
    [SerializeField]
    private int exp;  //current experiebce
    [SerializeField]
    private int expNextLevel; //experience until the next level

    public event EventHandler OnLevelChanged; //event that is triggered when the level increases

    public Levels() //constructor that initializes the level, experience and experience until the next level
    {
        level = 0;
        exp = 0;
        expNextLevel = 100;
    }

    //add experience
    public void AddExp(int value)
    {
        exp += value;
        while(exp >= expNextLevel) //if the experience gained exceeds the experience until the next level, then gain levels until that is not the case
        {
            level += 1;
            exp -= expNextLevel;
            expNextLevel += level * 10;  //with each level, increase the exp necessary until the next level
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);  //and call all the functions subscribed to the level change event
        }
    }

    //level getter
    public int GetLevel()
    {
        return level;
    }

    //exp getter
    public int GetExp()
    {
        return exp;
    }

    //exp until the next level getter
    public int GetExpNextLevel()
    {
        return expNextLevel;
    }

    //get the current exp in comparison to the necessary exp until the next level, to be used on the UI
    public float GetExpFormatted()
    {
        return (float)exp / expNextLevel;
    }
}
