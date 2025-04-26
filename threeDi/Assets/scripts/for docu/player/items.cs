using UnityEngine;
using UnityEngine.UI;

public class items : MonoBehaviour
{
    public RawImage inventory1;
    public RawImage inventory2;

    private GameObject slot1Item = null;
    private GameObject slot2Item = null;

    private bool usableItem1 = false;
    private bool usableItem2 = false;

    private Vector2 defaultSize = new Vector2(100, 100);
    private Vector2 highlightSize = new Vector2(120, 120);

    public FirstPersonController player;

    void Start()
    {
        inventory1.rectTransform.sizeDelta = defaultSize;
        inventory2.rectTransform.sizeDelta = defaultSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropSelectedItem();
        }
    }

    void SelectSlot(int slot)
    {
        inventory1.rectTransform.sizeDelta = slot == 1 ? highlightSize : defaultSize;
        inventory2.rectTransform.sizeDelta = slot == 2 ? highlightSize : defaultSize;

        usableItem1 = slot == 1 && slot1Item != null;
        usableItem2 = slot == 2 && slot2Item != null;
    }

    public void CollectItem(GameObject item)
    {
        RawImage targetSlot = null;

        if (slot1Item == null)
        {
            slot1Item = item;
            targetSlot = inventory1;
        }
        else if (slot2Item == null)
        {
            slot2Item = item;
            targetSlot = inventory2;
        }
        else
        {
            Debug.Log("Inventory full!");
            return;
        }

        // Assign icon (optional)
        RawImage icon = item.GetComponent<ItemData>()?.icon;
        if (icon != null)
        {
            targetSlot.texture = icon.texture;
        }

        Debug.Log(item.name + " collected.");
    }

    void UseSelectedItem()
    {
        if (usableItem1 && slot1Item != null)
        {
            slot1Item.GetComponent<ItemData>().Use(player);
            slot1Item = null;
            inventory1.texture = null;
            inventory1.rectTransform.sizeDelta = defaultSize;
            usableItem1 = false;
        }
        else if (usableItem2 && slot2Item != null)
        {
            slot2Item.GetComponent<ItemData>().Use(player);
            slot2Item = null;
            inventory2.texture = null;
            inventory2.rectTransform.sizeDelta = defaultSize;
            usableItem2 = false;
        }
        else
        {
            Debug.Log("No usable item selected.");
        }
    }

    void DropSelectedItem()
    {
        if (usableItem1 && slot1Item != null)
        {
            slot1Item.transform.position = transform.position + transform.forward;
            slot1Item.SetActive(true);
            Debug.Log("Dropped " + slot1Item.name);
            slot1Item = null;
            inventory1.texture = null;
            usableItem1 = false;
        }
        else if (usableItem2 && slot2Item != null)
        {
            slot2Item.transform.position = transform.position + transform.forward;
            slot2Item.SetActive(true);
            Debug.Log("Dropped " + slot2Item.name);
            slot2Item = null;
            inventory2.texture = null;
            usableItem2 = false;
        }
    }
}
