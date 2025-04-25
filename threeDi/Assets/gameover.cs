using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public GameObject gameoverscene;

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
    yield return new WaitForSeconds(1);

    gameoverscene.gameObject.SetActive(true);

    GameObject[] gameoverscenes = GameObject.FindGameObjectsWithTag("secon");

    foreach (GameObject obj in gameoverscenes)
    {
        obj.SetActive(true);
    }


}

}
