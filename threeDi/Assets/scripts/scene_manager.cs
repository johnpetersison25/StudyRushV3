using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_mananger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void quitGame()
   {
      Application.Quit();
   }


}