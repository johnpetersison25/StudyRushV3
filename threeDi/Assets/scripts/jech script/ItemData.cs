using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public enum ItemType { EnergyDrink, Spray }
    public ItemType itemType;

    public RawImage icon;

    public void Use(FirstPersonController player)
    {
        switch (itemType)
        {
            case ItemType.EnergyDrink:
                player.stamina = Mathf.Min(player.maxStamina, player.stamina + 50f);
                Debug.Log("Energy Drink used. Stamina recovered!");
                break;

            case ItemType.Spray:
                RaycastHit hit;
                if (Physics.Raycast(player.transform.position + Vector3.up, player.transform.forward, out hit, 10f))
                {
                    if (hit.collider.CompareTag("Teacher"))
                    {
                        EnemyStun stun = hit.collider.GetComponent<EnemyStun>();
                        if (stun != null)
                        {
                            stun.StunEnemy(3f); // stun for 3 seconds
                            Debug.Log("Enemy stunned with spray!");
                        }
                    }
                    else
                    {
                        Debug.Log("Spray used but no enemy in sight.");
                    }
                }
                else
                {
                    Debug.Log("Spray used but nothing hit.");
                }
                break;
        }
    }
}
