using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSingleton : MonoBehaviour
{
    public static MainGameSingleton singletonInstance;
    public List<ChildScript> kids;
    void Start()
    {
        singletonInstance = this;
    }
}
