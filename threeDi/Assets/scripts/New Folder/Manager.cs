using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    public string agentTag = "Teacher"; // Tag to identify the agents
    public float lowScoreSpeed = 6f; // Speed to set when the score is less than 3

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Find all GameObjects with the "Agent" tag
        GameObject[] agents = GameObject.FindGameObjectsWithTag(agentTag);

        // Check if quiz object exists and get its score
        quizV2 quiz = FindObjectOfType<quizV2>();
        if (quiz != null)
        {
            int receivedScore = quiz.finalScore;

            if (receivedScore < 3)
            {
                Debug.Log("Score is less than 3, changing speed of agents.");
                
                // Change speed for all agents with the specified tag
                foreach (GameObject agentObj in agents)
                {
                    NavMeshAgent agent = agentObj.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.speed = lowScoreSpeed; // Set the speed
                    }
                }
            }
        }
    }
}
