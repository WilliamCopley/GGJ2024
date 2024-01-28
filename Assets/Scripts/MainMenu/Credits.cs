using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private RectTransform creditsList;
    [SerializeField] private float scrollSpeed = 100;
    private float startYPos;
    private float yPos; 

    // Start is called before the first frame update
    void Start()
    {
        yPos = creditsList.localPosition.y;
        startYPos = creditsList.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (yPos < startYPos * -1)
        {
            yPos += scrollSpeed * Time.deltaTime;
            creditsList.transform.localPosition = new Vector3(0, yPos, 0);
        } else
        {
            returnToMenu();
        }
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
