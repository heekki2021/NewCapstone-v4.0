using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Extensions.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmarineManager : MonoBehaviour
{

    public float speedPower;
    public float jumpPower;
    private Rigidbody2D rb;
    bool facingRight = true;
    float moveInput;

    string scenename = "Inside";
    private float timePressed = 0;
    private bool isFunctionCalled = false;
    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speedPower, rb.velocity.y);
        Flip();
        Jump();
        SceneChanging();

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
            rb.velocity = Vector2.down * jumpPower; // (0, -1)
        }
    }
    
    void SceneChanging()
    {
        if (Input.GetKey(KeyCode.E))
        {
            timePressed += Time.deltaTime;
            if(timePressed >= 3f && !isFunctionCalled) {
                SceneManager1.Instance.LoadScene(scenename);
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
