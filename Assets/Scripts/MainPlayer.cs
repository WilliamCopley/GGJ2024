using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isTouchingGround = true;
    private Vector2 cameraRotation;
    private Rigidbody rb;

    private Vector2 previousRotation;
    private Vector3 cameraPreviousRotation;
    private Vector3 PlayerPreviousRotation;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        movePlayer();
        rotateCamera();
        doInteractions();
    }

    private void doInteractions()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            print("DOING PING");
            foreach (ChildScript eachKid in MainGameSingleton.singletonInstance.kids)
            {
                eachKid.doPing(transform, pingDistance);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
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
        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 forwardMove = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += horizontalMove + forwardMove;

        if (isTouchingGround && Input.GetButtonDown("Jump"))
        {
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
        transform.localRotation = Quaternion.Euler(0, cameraRotation.x, 0);
        mainCamera.localRotation = Quaternion.Euler(-cameraRotation.y, 0, 0);
}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isTouchingGround = true;
        }
    }
}
