using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullets;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

   //private SubmarineControl subCon = SubmarineControl.Instance;
    public bool flipCheck = false;
    
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
      
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();    //카메라가 두개일경우 하나를 선택할 수 있음
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);


        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            Instantiate(bullets, bulletTransform.position, Quaternion.identity);
        }

       

    }


   // public void flipF()
   // {
   //     if (public bool flip1 => subCon.)
   //     {

   //     }
   // }
}
