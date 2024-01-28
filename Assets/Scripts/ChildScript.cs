using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChildScript : MonoBehaviour
{
    [SerializeField] 
    private float maxHealth = 10;
    [SerializeField]
    private float damagePerSecondRecieved = 1;
    [SerializeField] 
    private UnityEvent pinged;
    [SerializeField]
    private AudioSource kidLaugh;

    public float currentHealth;

    void Start()
    {
        MainGameSingleton.singletonInstance.kids.Add(this);
        currentHealth = maxHealth;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            currentHealth -= damagePerSecondRecieved * Time.deltaTime;
            if(currentHealth < 0)
            {
                MainGameSingleton.singletonInstance.kidSmoked(this);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if(!kidLaugh.loop)
            {
                kidLaugh.loop = true;
                kidLaugh.Play();
            }
            kidLaugh.volume = currentHealth / maxHealth;
            currentHealth -= damagePerSecondRecieved * Time.deltaTime;
            if (currentHealth < 0)
            {
                MainGameSingleton.singletonInstance.kidSmoked(this);
                Destroy(gameObject);
            }
        }
    }

    public void doPing(Transform mainPlayerPinged, float pingDistance)
    {
        print("Checking distance:" + Vector3.Distance(transform.position, mainPlayerPinged.position));
        if(Vector3.Distance(transform.position, mainPlayerPinged.position) < pingDistance)
        {
            print("I WAS HIT");
            pinged.Invoke();

        }
    }
}
