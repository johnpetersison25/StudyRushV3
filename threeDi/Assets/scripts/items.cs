using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public RawImage inventory1;
    public RawImage inventory2;

    private bool usableItem1 = false;
    private bool usableItem2 = false;

    private Vector2 defaultSize = new Vector2(100, 100); // Adjust based on your UI
    private Vector2 highlightSize = new Vector2(120, 120); // Size when highlighted

    void Start()
    {
        inventory1.rectTransform.sizeDelta = defaultSize;
        inventory2.rectTransform.sizeDelta = defaultSize;
    }

    void Update()
    {
        // Selecting item 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory1.rectTransform.sizeDelta = highlightSize;
            inventory2.rectTransform.sizeDelta = defaultSize;
            usableItem1 = true;
            usableItem2 = false;
            Debug.Log("Item 1 selected.");
        }

        // Selecting item 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory1.rectTransform.sizeDelta = defaultSize;
            inventory2.rectTransform.sizeDelta = highlightSize;
            usableItem1 = false;
            usableItem2 = true;
            Debug.Log("Item 2 selected.");
        }

        // Using the selected item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (usableItem1)
            {
                Debug.Log("Item 1 was used.");
                usableItem1 = false;
                inventory1.rectTransform.sizeDelta = defaultSize;
            }
            else if (usableItem2)
            {
                Debug.Log("Item 2 was used.");
                usableItem2 = false;
                inventory2.rectTransform.sizeDelta = defaultSize;
            }
            else
            {
                Debug.Log("No usable item selected.");
            }
        }
    }
}
