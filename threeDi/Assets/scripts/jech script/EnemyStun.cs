using UnityEngine;
using UnityEngine.AI;

public class EnemyStun : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isStunned = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StunEnemy(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private System.Collections.IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        if (agent != null)
        {
            agent.isStopped = true;
        }

        yield return new WaitForSeconds(duration);

        if (agent != null)
        {
            agent.isStopped = false;
        }

        isStunned = false;
    }
}

