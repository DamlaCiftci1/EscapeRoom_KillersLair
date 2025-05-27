using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Image[] keySlots;           // Inspector'dan 5 image slot atayacaksýn
    public Sprite[] keySprites;        // Key1-Key5’e karþýlýk gelen sprite'lar

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void UpdateInventory(List<string> collectedKeys)
    {
        for (int i = 0; i < keySlots.Length; i++)
        {
            if (i < collectedKeys.Count)
            {
                string keyID = collectedKeys[i];
                int index = int.Parse(keyID.Replace("Key", "")) - 1; // Key1 -> index 0
                keySlots[i].sprite = keySprites[index];
                keySlots[i].enabled = true;
            }
            else
            {
                keySlots[i].sprite = null;
                keySlots[i].enabled = false;
            }
        }
    }
}
