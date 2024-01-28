using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KidsCounterUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text kidsCounter;
    private void Start()
    {
        MainGameSingleton.singletonInstance.kidsCountChanged.AddListener(countChanged);
        kidsCounter.text = 0 + " / " + MainGameSingleton.singletonInstance.kidsTotal;
    }
    public void countChanged(int current)
    {
        kidsCounter.text = MainGameSingleton.singletonInstance.kidsTotal - current + " / " + MainGameSingleton.singletonInstance.kidsTotal;
    }
}
