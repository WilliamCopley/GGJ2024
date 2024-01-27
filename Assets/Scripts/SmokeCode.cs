using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCode : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5f;
    private float currentLife = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLife+=Time.deltaTime;
        if(currentLife >= lifetime) {
            Destroy(gameObject);
        }
    }
}
