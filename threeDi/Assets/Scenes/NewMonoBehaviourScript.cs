using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(next());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator next()
    {
        yield return new WaitForSeconds(23);
        SceneManager.LoadScene(2);
    }
}
