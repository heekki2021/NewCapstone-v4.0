using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Handle : MonoBehaviour, IInteractable
{


    public void Interact()
    {
        Debug.Log("�� ��ȯ");
        SceneManager.LoadScene(1);
    }
}







