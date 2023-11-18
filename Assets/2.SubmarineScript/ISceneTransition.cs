using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneTransition
{
    void OnSceneChanging(int sceneIndex);
}
