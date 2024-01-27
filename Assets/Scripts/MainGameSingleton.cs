using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MainGameSingleton : MonoBehaviour
{
    public static MainGameSingleton singletonInstance;
    public List<ChildScript> kids;
    public MainPlayer player;
    void Start()
    {
        singletonInstance = this;
    }

    public void kidSmoked(ChildScript kidHit)
    {
        kids.Remove(kidHit);
        if (kids.Count == 0)
        {
            player.onWin();
        }
    }

}
