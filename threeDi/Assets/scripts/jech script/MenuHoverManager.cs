using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuHoverManager : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform[] buttons;     // Assign in Inspector
    public GameObject infoPanel;        // Panel that shows info
    public TMP_Text infoText;           // Info text field

    [Header("Settings")]
    public float doubleClickThreshold = 0.25f;

    private float[] lastClickTime;

    void Start()
    {
        lastClickTime = new float[buttons.Length];
        for (int i = 0; i < lastClickTime.Length; i++)
        {
            lastClickTime[i] = -doubleClickThreshold;
        }

        infoPanel.SetActive(false); // Hide info panel initially
    }

    public void OnButtonClick(int index)
    {
        if (Time.time - lastClickTime[index] < doubleClickThreshold)
        {
            // Double-click: perform action
            Debug.Log("DOUBLE CLICKED: Triggering button " + index);
            TriggerButtonAction(index);
        }
        else
        {
            // Single click: show info
            Debug.Log("Single Click: Showing info for button " + index);
            infoText.text = GetInfoForIndex(index);
            infoPanel.SetActive(true);
        }

        lastClickTime[index] = Time.time;
    }

    private void TriggerButtonAction(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Starting Easy Mode...");
                SceneManager.LoadSceneAsync(1); // Replace with actual scene index
                break;
            case 1:
                Debug.Log("Starting Medium Mode...");
                SceneManager.LoadSceneAsync(1); // If needed
                break;
            case 2:
                Debug.Log("Starting Hard Mode...");
                SceneManager.LoadSceneAsync(1); // If needed
                break;
        }
    }

    private string GetInfoForIndex(int index)
    {
        switch (index)
        {
            case 0:
                return "  EASY MODE\n<indent=7px>• More Collectibles\n<indent=7px>• Slower Enemies\n<indent=7px>• Easier Quizzes.";
            case 1:
                return "  MEDIUM MODE\n<indent=7px>• Balanced Gameplay\n<indent=7px>• Moderate Enemies\n<indent=7px>• Standard Quizzes.";
            case 2:
                return "  HARD MODE\n<indent=7px>• Fewer Items\n<indent=7px>• Faster Enemies\n<indent=7px>• Tougher Quizzes.";
            default:
                return "";
        }
    }
}
