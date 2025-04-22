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
            inventoryUI.CollectItem(itemInRange);
            itemInRange.SetActive(false);
            itemInRange = null;
        }
    }

    void CheckForItems()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange);
        itemInRange = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Collectible"))
            {
                itemInRange = hit.gameObject;
                break;
            }
        }
    }
}
