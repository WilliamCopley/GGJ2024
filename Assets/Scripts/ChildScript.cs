using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    public float currentHealth;
    [SerializeField]private float damagePerSecondRecieved = 1;
    // Start is called before the first frame update
    void Start()
    {
        MainGameSingleton.children.Add(transform);
        currentHealth = maxHealth;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            currentHealth -= damagePerSecondRecieved * Time.deltaTime;
            if(currentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            currentHealth -= damagePerSecondRecieved * Time.deltaTime;
            if (currentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
