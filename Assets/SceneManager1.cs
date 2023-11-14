using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager1 : Singleton<SceneManager1>
{
    public void LoadScene(string scene)
    {
        Debug.Log("loadscene called!!!");
        SceneManager.LoadScene(scene);
    }
}
