using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public Items inventoryUI;
    public float pickupRange = 2f;
    private GameObject itemInRange;

    void Update()
    {
        CheckForItems();

        if (Input.GetKeyDown(KeyCode.E) && itemInRange != null)
        {
            if (itemInRange.CompareTag("Coin"))
            {
                inventoryUI.AddCoin(1);
                Debug.Log("Picked up a coin.");
                itemInRange.SetActive(false);
            }
            else if (itemInRange.CompareTag("Collectible"))
            {
                inventoryUI.CollectItem(itemInRange);
                itemInRange.SetActive(false);
            }

            itemInRange = null;
        }
    }

    void CheckForItems()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        itemInRange = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Collectible") || hit.CompareTag("Coin"))
            {
                itemInRange = hit.gameObject;
                break;
            }
        }
    }
}
