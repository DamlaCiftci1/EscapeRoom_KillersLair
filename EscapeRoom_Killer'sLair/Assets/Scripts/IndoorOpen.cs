using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject doorKey;
    public GameObject doorText;
    public GameObject door;

    public AudioSource doorSound;   // Açýlma sesi (DoorOpen)
    public AudioSource jiggleSound; // Kilit sesi (Jiggle)

    public bool isLocked = false;
    public string requiredKeyID = "Key1";

    private bool isOpen = false;

    // Oyuncunun sahip olduðu anahtarlar
    private static HashSet<string> playerKeys = new HashSet<string>();

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
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

                if (isLocked)
                {
                    if (playerKeys.Contains(requiredKeyID))
                    {
                        isLocked = false;
                        ToggleDoor();
                    }
                    else
                    {
                        Debug.Log("Kapý kilitli. Anahtar gerekli: " + requiredKeyID);
                        if (jiggleSound != null)
                        {
                            jiggleSound.Play(); // Kilit sesi çal
                        }
                    }
                }
                else
                {
                    ToggleDoor();
                }
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

    void ToggleDoor()
    {
        if (!isOpen)
        {
            door.GetComponent<Animation>().Play("Ýçe doðru kapý");
        }
        else
        {
            door.GetComponent<Animation>().Play("DoorClose");
        }

        if (doorSound != null)
        {
            doorSound.Play(); // Açýlma/kapanma sesi
        }

        isOpen = !isOpen;
    }

    // Anahtar toplayan script buradan çaðýracak
    public static void AddKey(string keyID)
    {
        playerKeys.Add(keyID);
        Debug.Log("Anahtar eklendi: " + keyID);
    }
}
