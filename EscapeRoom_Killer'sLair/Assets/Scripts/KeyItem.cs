using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public string keyID;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddKey(keyID);
            Destroy(gameObject);
        }
    }
}
