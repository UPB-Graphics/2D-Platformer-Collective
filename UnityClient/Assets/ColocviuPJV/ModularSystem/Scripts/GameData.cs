using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// things that we want to save - as long as they're serializable
[Serializable]
public class GameData
{
    public int Money;
    public Vector3 PlayerPosition;
    public Vector3 PlayerVelocity;
    public List<MoneyData> MoneyDatas = new List<MoneyData>();
}

