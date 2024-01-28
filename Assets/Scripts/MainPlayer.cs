using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float sensitivity = 1;
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float jumpHeight = 10;
    [SerializeField]
    private GameObject smokeObject;
    [SerializeField]
    private Transform bulletsParent;
    [SerializeField]
    private LayerMask IgnoreMe;
    [SerializeField]
    private float pingDistance = 5f;
    [SerializeField]
    private UnityEvent gameWon;
    [SerializeField]
    private float jumpCooldown = 0.5f;
    [SerializeField]
    private float fireRate = 0.3f;
    [SerializeField]
    private float pingCooldown = 10f;
    [SerializeField]
    private float pingTimerEventFrequency = 0.5f;
    [SerializeField]
    public UnityEvent<float> cooldownChanged;
    private float previousCooldownEvent;

    private bool isTouchingGround = true;
    private Vector2 cameraRotation;
    private Rigidbody rb;

    private Vector2 previousRotation;
    private Vector3 cameraPreviousRotation;
    private Vector3 PlayerPreviousRotation;
    private float jumpTimer;
    private float fireTimer;
    private float pingTimer;
    private bool hasWon = false;
    private bool isPaused = false;
    
    private void Start()
    {
        MainGameSingleton.singletonInstance.player = this;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if(!hasWon && !isPaused)
        {
            doTimers();
            movePlayer();
            rotateCamera();
            doInteractions();
        }
        
    }

    private void doTimers()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
        if (pingTimer < pingCooldown)
        {
            pingTimer += Time.deltaTime;
            if(pingTimer >= pingCooldown || pingTimer >= previousCooldownEvent + pingTimerEventFrequency)
            {
                previousCooldownEvent = pingTimer;
                cooldownChanged.Invoke(pingTimer);
            }
        }
        if (!isTouchingGround && jumpTimer < jumpCooldown)
        {
            jumpTimer += Time.deltaTime;
        }
    }

    private void doInteractions()
    {
        if (Input.GetButtonDown("Fire2") && pingTimer >= pingCooldown)
        {
            previousCooldownEvent = 0;
            pingTimer = 0;
            cooldownChanged.Invoke(pingTimer);
            print("DOING PING");
            foreach (ChildScript eachKid in MainGameSingleton.singletonInstance.kids)
            {
                if(eachKid != null) { 
                    eachKid.doPing(transform, pingDistance); 
                }
            }
        }

        if (Input.GetButton("Fire1") && fireTimer >= fireRate)
        {
            fireTimer = 0;
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~IgnoreMe))
            {
                GameObject spawnedObject = GameObject.Instantiate(smokeObject);
                spawnedObject.transform.parent = bulletsParent;
                spawnedObject.transform.position = hit.point;
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~IgnoreMe))
            {
                Interactable tempInteractable = hit.transform.GetComponent<Interactable>();
                if(tempInteractable != null)
                {
                    tempInteractable.doInteract();
                }
            }
 
        }
    }

    private void movePlayer()
    {
        Vector3 forwardsVector = new Vector3(mainCamera.forward.x, 0, mainCamera.forward.z).normalized;
        Vector3 rightVector = new Vector3(mainCamera.right.x, 0, mainCamera.right.z).normalized;
        Vector3 horizontalMove = rightVector * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 forwardMove = forwardsVector * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += horizontalMove + forwardMove;

        if (isTouchingGround && Input.GetButton("Jump"))
        {
            jumpTimer = 0;
            rb.AddForce(transform.up * jumpHeight);
            isTouchingGround = false;
        }
    }

    private void rotateCamera()
    {
        cameraRotation.x += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        cameraRotation.y += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        if (cameraRotation.y < -75) { cameraRotation.y = -75; }
        if (cameraRotation.y > 75) { cameraRotation.y = 75; }

        if(cameraRotation.x < 0)cameraRotation.x += 360;
        if (cameraRotation.x > 360) cameraRotation.x -= 360;
        //transform.localRotation = Quaternion.Euler(0, cameraRotation.x, 0);
        mainCamera.localRotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);
}

    private void OnTriggerStay(Collider other)
    {
        if (isTouchingGround == false && other.gameObject.tag == "ground" && jumpTimer > jumpCooldown)
        {
            isTouchingGround = true;
        }
    }


    public void onWin()
    {
        gameWon.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        hasWon = true;
    }
}
