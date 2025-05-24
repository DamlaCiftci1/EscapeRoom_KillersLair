using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public string keyID;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indoorOpen.AddKey(keyID);
            Debug.Log("Anahtar alýndý: " + keyID);
            Destroy(gameObject);
        }
    }
}
