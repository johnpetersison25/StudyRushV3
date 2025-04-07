using UnityEngine;
using UnityEngine.SceneManagement;

public class teacher : MonoBehaviour
{
  public Transform playerTarget;
  public float currentSpeed;
  quiz quizTeacher;


    void Start()
    {
        quizTeacher = FindAnyObjectByType<quiz>();
    }

    // Update is called once per frame
    void Update()
    {
         currentSpeed = quizTeacher.speedChase;

        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, currentSpeed * Time.deltaTime);
    }

     void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadSceneAsync(0);

        }
    }

    
}
