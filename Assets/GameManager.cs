using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var sceneTransitions = FindObjectsOfType<MonoBehaviour>().OfType<ISceneTransition>();
        foreach (var transition in sceneTransitions)
        {
            transition.OnSceneChanging(scene.buildIndex);
        }

    }

    public void Update()
    {
        SceneManager1.Instance.SceneChanging();
    }

}
