using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainGameSingleton : MonoBehaviour
{
    public static MainGameSingleton singletonInstance;
    public List<ChildScript> kids;
    public MainPlayer player;
    public float kidsTotal;
    public UnityEvent<int> kidsCountChanged;
    void Start()
    {
        singletonInstance = this;
    }

    public void kidsSpawned(int totalKidsSpawned)
    {
        kidsTotal = totalKidsSpawned;
        kidsCountChanged.Invoke(totalKidsSpawned);
    }

    public void kidSmoked(ChildScript kitHit)
    {
        kids.Remove(kitHit);
        kidsCountChanged.Invoke(kids.Count);
        if (kids.Count == 0)
        {
            player.onWin();
        }
       
    }

}
