using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{
    public RawImage inventory1;
    public RawImage inventory2;
    public RawImage inventory3;

    private GameObject slot1Item = null;
    private GameObject slot2Item = null;
    private GameObject slot3Item = null;

    private bool usableItem1 = false;
    private bool usableItem2 = false;
    private bool usableItem3 = false;

    public TextMeshProUGUI coinText;
    private int coinCount = 0;

    private Vector2 defaultSize = new Vector2(100, 100);
    private Vector2 highlightSize = new Vector2(120, 120);

    public FirstPersonController player;

    void Start()
    {
        inventory1.rectTransform.sizeDelta = defaultSize;
        inventory2.rectTransform.sizeDelta = defaultSize;
        inventory3.rectTransform.sizeDelta = defaultSize;
        UpdateCoinUI(); // Make sure coin text is correct at start
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
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(3);
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
        inventory3.rectTransform.sizeDelta = slot == 3 ? highlightSize : defaultSize;

        usableItem1 = slot == 1 && slot1Item != null;
        usableItem2 = slot == 2 && slot2Item != null;
        usableItem3 = slot == 3 && slot3Item != null;
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
        else if (slot3Item == null)
        {
            slot3Item = item;
            targetSlot = inventory3;
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
        else if (usableItem3 && slot3Item != null)
        {
            slot3Item.GetComponent<ItemData>().Use(player);
            slot3Item = null;
            inventory3.texture = null;
            inventory3.rectTransform.sizeDelta = defaultSize;
            usableItem3 = false;
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
        else if (usableItem3 && slot3Item != null)
        {
            slot3Item.transform.position = transform.position + transform.forward;
            slot3Item.SetActive(true);
            Debug.Log("Dropped " + slot3Item.name);
            slot3Item = null;
            inventory3.texture = null;
            usableItem3 = false;
        }
    }

    // Extra utility for vending machines
    public bool HasItem(string itemName)
    {
        return (slot1Item != null && slot1Item.name.Contains(itemName)) ||
               (slot2Item != null && slot2Item.name.Contains(itemName)) ||
               (slot3Item != null && slot3Item.name.Contains(itemName));
    }

    public void RemoveItem(string itemName)
    {
        if (slot1Item != null && slot1Item.name.Contains(itemName))
        {
            Destroy(slot1Item);
            slot1Item = null;
            inventory1.texture = null;
            return;
        }
        if (slot2Item != null && slot2Item.name.Contains(itemName))
        {
            Destroy(slot2Item);
            slot2Item = null;
            inventory2.texture = null;
            return;
        }
        if (slot3Item != null && slot3Item.name.Contains(itemName))
        {
            Destroy(slot3Item);
            slot3Item = null;
            inventory3.texture = null;
            return;
        }
    }

    // ---------------------------
    // 🔸 Coin System Methods 🔸
    // ---------------------------

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    public bool HasCoin()
    {
        return coinCount > 0;
    }

    public void UseCoin()
    {
        if (coinCount > 0)
        {
            coinCount--;
            UpdateCoinUI();
        }
        else
        {
            Debug.Log("No coins to use.");
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }
    public void RemoveCoins(int amount)
    {
    coinCount = Mathf.Max(0, coinCount - amount);
    UpdateCoinUI();
    }

    public int GetCoinCount()
    {
    return coinCount;
    }

}
