using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject doorKey;
    public GameObject doorText;
    public GameObject door;
    public AudioSource doorSound;

    public bool isLocked = false;           // Bu kapý kilitli mi?
    public string requiredKeyID = "Key1";   // Açmak için gereken anahtar ID'si

    private bool isOpen = false;            // Kapý açýk mý?

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
                        // Buraya UI uyarý ekleyebilirsin
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

        doorSound.Play();
        isOpen = !isOpen;
    }

    // Anahtar toplayan script buradan çaðýracak
    public static void AddKey(string keyID)
    {
        playerKeys.Add(keyID);
        Debug.Log("Anahtar eklendi: " + keyID);
    }
}
