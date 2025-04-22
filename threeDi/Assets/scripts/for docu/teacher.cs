using UnityEngine;
using UnityEngine.SceneManagement;

public class teacher : MonoBehaviour
{

     void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadSceneAsync(0);
        }

        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");

        if (manager != null)
        {
            Destroy(manager);
            Debug.Log("GameManager destroyed");
        }
        else
        {
            Debug.LogWarning("GameManager not found!");
        }
    }

    

    
}
