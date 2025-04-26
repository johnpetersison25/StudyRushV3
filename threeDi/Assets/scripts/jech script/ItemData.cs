using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public enum ItemType { EnergyDrink, Spray }
    public ItemType itemType;

    public RawImage icon;
    public GameObject sprayEffectPrefab;  // Reference to the spray effect prefab

    public Transform sprayStartPoint;  // Point from where the spray originates (e.g., player's hand or camera)


    void Start()
    {
        
    }

    public void Use(FirstPersonController player)
    {
        switch (itemType)
        {
            case ItemType.EnergyDrink:
                // Increase stamina
                player.stamina = Mathf.Min(player.maxStamina, player.stamina + 50f);
                Debug.Log("Energy Drink used. Stamina recovered!");
                break;

            case ItemType.Spray:
                RaycastHit hit;
                Vector3 sprayDirection = player.transform.forward;  // Direction the player is facing
                float raycastDistance = 10f;  // Raycast distance

                // If you have a specific start point for the spray (e.g., the player's hand or camera):
                Vector3 sprayStartPos = sprayStartPoint.position;

                if (Physics.Raycast(sprayStartPos, sprayDirection, out hit, raycastDistance))
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

                    // Instantiate the spray effect at the hit point
                    InstantiateSprayEffect(hit.point);
                }
                else
                {
                    // If nothing is hit, instantiate the spray effect at the maximum range
                    Vector3 sprayEndPos = sprayStartPos + sprayDirection * raycastDistance;
                    InstantiateSprayEffect(sprayEndPos);
                }
                break;
        }
    }

    // Method to instantiate the spray effect
    void InstantiateSprayEffect(Vector3 position)
    {
        if (sprayEffectPrefab != null)
        {
            // Instantiate the spray effect at the player's hand (or any other point you want)
            GameObject spray = Instantiate(sprayEffectPrefab, position, Quaternion.LookRotation(sprayEffectPrefab.transform.forward));

            // Optional: You can set the effect's direction to match the player's facing direction
            ParticleSystem sprayParticles = spray.GetComponent<ParticleSystem>();
            if (sprayParticles != null)
            {
                // Make the particles emit in the player's facing direction
                var main = sprayParticles.main;
                main.startRotation = Mathf.Atan2(sprayEffectPrefab.transform.forward.x, sprayEffectPrefab.transform.forward.z);
            }

            Debug.Log("Instantiating spray effect at: " + position);
        }
        else
        {
            Debug.LogWarning("Spray effect prefab is not assigned!");
        }
    }
}
