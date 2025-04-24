using UnityEngine;

public class Manager : MonoBehaviour
{
    public static int collectedNotes;

    int score;
    public float agentspeed;

    quizV2 quiz;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("helow");
        quiz = FindObjectOfType<quizV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz == null)
        {
            quiz = FindObjectOfType<quizV2>();
            Debug.Log("Trying to find quiz...");
        if (quiz == null)
         {
            Debug.Log("quiz is still null");
            return;
         }
        }

}
}