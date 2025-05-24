using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject doorKey;
    public GameObject doorText;
    public GameObject door; // Animator veya Animation component ta��yan obje
    public AudioSource doorSound;

    private bool isOpen = false; // Kap� a��k m�?

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget; // Oyuncuya olan mesafe
    }

    void OnMouseOver()
    {
        if (theDistance <= 2f)
        {
            doorKey.SetActive(true);
            doorText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                doorKey.SetActive(false);
                doorText.SetActive(false);

                if (!isOpen)
                {
                    door.GetComponent<Animation>().Play("��e do�ru kap�"); // A��lma animasyonu
                }
                else
                {
                    door.GetComponent<Animation>().Play("DoorClose"); // Kapanma animasyonu
                }

                doorSound.Play();
                isOpen = !isOpen; // Durumu tersine �evir
            }
        }
        else
        {
            doorKey.SetActive(false);
            doorText.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        doorKey.SetActive(false);
        doorText.SetActive(false);
    }
}