using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    private List<string> keys = new List<string>();

    public InventoryUI inventoryUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddKey(string keyID)
    {
        if (!keys.Contains(keyID))
        {
            keys.Add(keyID);
            Debug.Log("Anahtar eklendi: " + keyID);
            inventoryUI.UpdateInventory(keys);
        }
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }
}
