using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class scienceAgent : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Collider TriggerOn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        TriggerOn = GetComponent<Collider>();
        TriggerOn.isTrigger = false;
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        if(scienceQ.scienceScore < 3 && scienceQ.scienceQuizIsDone == true)
        {
            agent.speed = statica.difficultySpeed;
            TriggerOn.isTrigger = true;

            BGM.Instance.PlayDangerMusic();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            FilipinoQ.FilipinoScore = 0;
            FilipinoQ.filipinoQuizIsDone = false;
            EnglishQ.EnglishScore = 0;
            EnglishQ.EnglishQuizIsDone = false;
            scienceQ.scienceScore = 0;
            scienceQ.scienceQuizIsDone = false;
            historyQ.historyScore = 0;
            historyQ.historyQuizIsDone = false;
            mathQ.mathScore = 0;
            mathQ.mathQuizIsDone = false;
            statica.collectedNotes = 0;
            statica.coin = 0;
            agent.speed = 0f;
            BGM.Instance.StopMusic();


            SceneManager.LoadSceneAsync("GAME OVER");
        }
    }
}