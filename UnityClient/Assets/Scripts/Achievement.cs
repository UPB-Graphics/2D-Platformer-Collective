using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    int id;
    Sprite icon;
    string title;
    string description;

    public Achievement(int id, Sprite icon, string title, string description)
    {
        this.id = id;
        this.icon = icon;
        this.title = title;
        this.description = description;
    }

    public Sprite Icon { get => icon; set => icon = value; }
    public string Title { get => title; set => title = value; }
    public string Description { get => description; set => description = value; }
}
