using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Extensions.Common;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmarineManager : Singleton<SubmarineManager> , ISceneTransition
{

    public float speedPower;
    public float jumpPower;
    private Rigidbody rb;
    bool facingRight = true;
    float moveInput;




    public void OnSceneChanging(int sceneIndex)
    {
       if(sceneIndex == 1)
        {
            Instance.gameObject.SetActive(true);
        } else if (sceneIndex == 2)
        {
            Instance.gameObject.SetActive(false);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();       
    }


    float forceGravity = 30f;
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up * forceGravity);

        moveInput = Input.GetAxis("Horizontal");
        Flip();


    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(moveInput * speedPower, rb.velocity.y);;
        Jump();

    }

    void Flip()
    {
        if(moveInput < 0 && facingRight || moveInput > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    void Jump()
    {
     if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.down * jumpPower; // (0, -1)
        }
    }
    

}
