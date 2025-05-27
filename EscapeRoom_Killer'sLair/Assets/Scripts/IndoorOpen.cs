using System.Collections;
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

    // You Win paneli (Inspector'dan atanacak)
    public GameObject youWinPanel;

    void Start()
    {
        if (youWinPanel != null)
            youWinPanel.SetActive(false); // Baþta kapalý olsun
    }

    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    void OnMouseOver()
    {
        if (theDistance <= 2f)
        {
            // Eðer YouWin aktifse hiçbir þey gösterme
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
                    // >>> Burayý güncelledik
                    if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasKey(requiredKeyID))
                    {
                        isLocked = false;
                        ToggleDoor();
                        CheckWinCondition();
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

    // >>> Bu fonksiyon artýk kullanýlmýyor. Ýstersen silebilirsin.
    // public static void AddKey(string keyID) {}

    void CheckWinCondition()
    {
        if (requiredKeyID == "Key4") // Son kapýnýn anahtarý (örnek)
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
