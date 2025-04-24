using UnityEngine;
using UnityEngine.AI;

public class navmesh : MonoBehaviour
{
    public Transform player;
    public float stoppingDistance = 2f;

    public quizV2.Subject mySubject; // 👈 Assign per agent in Inspector
    private NavMeshAgent agent;
    private quizV2 quiz;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        quiz = FindObjectOfType<quizV2>();
    }

    void Update()
    {
        if (quiz == null)
        {
            quiz = FindObjectOfType<quizV2>();
            return;
        }

        if (player != null)
        {
            agent.SetDestination(player.position);

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

        // 🧠 Use the subject-specific score to set speed
        if (quiz.quizdone && quiz.currentSubject == mySubject)
        {
            int myScore = quiz.GetSubjectScore(mySubject);
            agent.speed = (myScore < 3) ? 6f : 0f;
        }
        else
        {
            agent.speed = 0f; // ← Stop moving if not the subject that was just quizzed
        }

    }
}
