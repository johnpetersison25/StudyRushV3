using UnityEngine;
using UnityEngine.AI;

public class navmeshyu : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float stoppingDistance = 2f;
    Manager _manager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        _manager = FindAnyObjectByType<Manager>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);

            // Instantly face the player when within stopping distance
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = lookRotation;
                }
            }
        }

        agent.speed = _manager.FilipinoAgentSpeed;
    }
}