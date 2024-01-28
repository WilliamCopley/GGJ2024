using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ChildSpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnAmount = 2;
    [SerializeField]
    private List<ChildScript> children;

   
    // Start is called before the first frame update
    void Start()
    {
        children = MainGameSingleton.singletonInstance.kids;

        while (children.Count > spawnAmount)
        {
            int chosenItem = Random.Range(0, children.Count);
            Transform destroyObject = children[chosenItem].transform;
            children.RemoveAt(chosenItem);
            Destroy(destroyObject.gameObject);

        }

        MainGameSingleton.singletonInstance.kidsSpawned(children.Count);
    }



}
