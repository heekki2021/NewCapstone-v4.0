using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager1 : Singleton<SceneManager1>
{

    private float timePressed = 0;
    private bool isFunctionCalled = false;




    public void LoadScene(string scene)
    {
        Debug.Log("loadscene called!!!");
        SceneManager.LoadScene(scene);
    }


    public void SceneChanging()
    {
        if (Input.GetKey(KeyCode.E))
        {
            timePressed += Time.deltaTime;
            if (timePressed >= 3f && !isFunctionCalled)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                if(currentScene.name == "maingame") 
                {
                    SceneManager.LoadScene("Inside");
                }
                else
                if(currentScene.name == "Insdie")
                {
                    SceneManager.LoadScene("maingame");
                }

                
                isFunctionCalled = true;
            }
        }
        else
        {
            timePressed = 0;
            isFunctionCalled = false;
        }
    }

}
