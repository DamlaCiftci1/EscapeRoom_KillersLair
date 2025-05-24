using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject doorKey;
    public GameObject doorText;
    public GameObject door;
    public AudioSource doorSound;

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            doorKey.SetActive(true);
            doorText.SetActive(true);
        }
        else {
            doorKey.SetActive(false);
            doorText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (theDistance <= 2)
            {
                doorKey.SetActive(false);
                doorText.SetActive(false);
                door.GetComponent<Animation>().Play("Door");
                doorSound.Play();
            }
        }

    }

    void OnMouseExit()
    {
        doorKey.SetActive(false);
        doorText.SetActive(false);
    }
}