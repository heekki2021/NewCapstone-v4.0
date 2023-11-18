using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : Singleton<CameraFollow> , ISceneTransition
{
    public float FollowSpeed = 2f;
    //public float yOffset = 1f;
    public Transform target;
    // Start is called before the first frame update


    public void OnSceneChanging(int sceneIndex)
    {
        if (sceneIndex == 1)
        {
            Instance.gameObject.SetActive(true);
        }
        else if (sceneIndex == 2)
        {
            Instance.gameObject.SetActive(false);

        }
    }





    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f); y오프셋 필요없을지도
        Vector3 newPos = new (target.position.x, target.position.y, -10f);

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
