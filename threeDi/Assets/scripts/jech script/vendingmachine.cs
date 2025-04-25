using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject itemToGive; // Prefab of the item (like EnergyDrink)
    public Transform itemSpawnPoint;
    public int coinCost = 1;

    private bool playerNearby = false;
    private Items playerInventory;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory.GetCoinCount() >= coinCost)
            {
                playerInventory.RemoveCoins(coinCost);

                if (itemToGive != null && itemSpawnPoint != null)
                {
                    GameObject spawnedItem = Instantiate(itemToGive, itemSpawnPoint.position, Quaternion.identity);
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
            playerInventory = other.GetComponent<ItemCollector>()?.inventoryUI;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerInventory = null;
        }
    }
}
