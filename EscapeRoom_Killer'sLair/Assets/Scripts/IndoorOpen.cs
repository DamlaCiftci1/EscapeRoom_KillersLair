using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject doorKey;
    public GameObject doorText;
    public GameObject door;

    public AudioSource doorSound;   // A��lma sesi (DoorOpen)
    public AudioSource jiggleSound; // Kilit sesi (Jiggle)

    public bool isLocked = false;
    public string requiredKeyID = "Key1";

    private bool isOpen = false;

    // Oyuncunun sahip oldu�u anahtarlar
    private static HashSet<string> playerKeys = new HashSet<string>();

    // You Win paneli (Inspector'dan atanacak)
    public GameObject youWinPanel;

    void Start()
    {
        if (youWinPanel != null)
            youWinPanel.SetActive(false); // Ba�ta kapal� olsun
    }

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    void OnMouseOver()
    {
        if (theDistance <= 2f)
        {
            // E�er YouWin aktifse hi�bir �ey g�sterme
            if (youWinPanel != null && youWinPanel.activeSelf)
                return;

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
                        CheckWinCondition();
                    }
                    else
                    {
                        Debug.Log("Kap� kilitli. Anahtar gerekli: " + requiredKeyID);
                        if (jiggleSound != null)
                        {
                            jiggleSound.Play(); // Kilit sesi �al
                        }
                    }
                }
                else
                {
                    ToggleDoor();
                    CheckWinCondition();
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
            door.GetComponent<Animation>().Play("��e do�ru kap�");
        }
        else
        {
            door.GetComponent<Animation>().Play("DoorClose");
        }

        if (doorSound != null)
        {
            doorSound.Play(); // A��lma/kapanma sesi
        }

        isOpen = !isOpen;
    }

    // Anahtar toplayan script buradan �a��racak
    public static void AddKey(string keyID)
    {
        playerKeys.Add(keyID);
        Debug.Log("Anahtar eklendi: " + keyID);
    }

    // Oyun bitirme ko�ulu
    void CheckWinCondition()
    {
        if (requiredKeyID == "Key3") // Son kap�n�n anahtar� (ID 3)
        {
            Debug.Log("You Win! Oyun bitti.");

            if (youWinPanel != null)
            {
                youWinPanel.SetActive(true);
            }

            Time.timeScale = 0f; // Oyunu durdur
        }
    }
}
