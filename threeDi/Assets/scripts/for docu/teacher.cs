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
    }

    
}
