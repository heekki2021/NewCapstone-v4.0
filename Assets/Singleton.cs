using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject obj = new (typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                    obj.name = typeof(T).ToString() + "(singleton)";
                }
            }
            return instance;
        }
    }

    private void Awake()
    {

        if(instance != null && instance != this)
        {
            Destroy(this.transform.root.gameObject);
        }

        else if (transform.parent != null && transform.root != null) //�ش� ��ü�� �θ� �ְ�,�ش� ��ü�� �ֻ���
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else 
        {

            DontDestroyOnLoad(this.gameObject);
        }
    }

}
