using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject itemToGive; // Prefab of the item (like EnergyDrink)
    public Transform itemSpawnPoint;
    public int coinCost = 1;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (statica.coin >= coinCost)
            {
                statica.coin -= coinCost;

                if (itemToGive != null && itemSpawnPoint != null)
                {
                    Instantiate(itemToGive, itemSpawnPoint.position, Quaternion.identity);
                    Debug.Log("Item dispensed from vending machine!");
                }
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
