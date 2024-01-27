using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ChildSpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnAmount = 2;
    [SerializeField]
    private List<Transform> children;
   
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        while (children.Count > spawnAmount)
        {
            int chosenItem = Random.Range(0, children.Count);
            Transform destroyObject = children[chosenItem];
            children.RemoveAt(chosenItem);
            Destroy(destroyObject.gameObject);

        }
    }

}
