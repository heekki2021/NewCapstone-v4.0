//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public delegate void flipEventHandler(bool eventReceived);
//public class SubmarineControl : MonoBehaviour
//{
//
//    public float moveSpeed;
//    public float jumpPower;
//    private float horizontal;
//
//    private bool isFacingRight = true;
//
//    private Rigidbody2D rb;
//    [SerializeField] private Transform groundCheck;
//    [SerializeField] private LayerMask groundLayer;
//
//    public static bool facingCheck;
//    public static SubmarineControl instance;
//
//    public delegate void FacingChanged(bool isFacingRight);
//
//    
//
//    public static SubmarineControl Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = new SubmarineControl();
//            }
//            return instance;
//
//        }
//    }
//
//    public event flipEventHandler FlipChecking;
//    
//    public event Action<bool> OnFacingChanged;
//    
//
//    
//    //Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//
//      // instance.FlipChecking += OnFacingChanged;
//    }
//
//    private void Update()
//    {
//        horizontal = Input.GetAxisRaw("Horizontal");
//        Jump();
//        Flip();
//    }
//
//    private void FixedUpdate()
//    {
//        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
//    }
//
//    void Jump()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            rb.velocity = jumpPower * Vector2.down;
//
//        }
//    }
//
//   private void Flip()
//    {
//        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
//        {
//            // �÷��̾ �ٶ󺸴� ���� ����
//            isFacingRight = !isFacingRight;
//
//            //��������Ʈ�� ���� ���� ���� �˸�
//            FlipChecking?.Invoke(isFacingRight);
//
//            //Transform�� localScale�� �����Ͽ� ��������Ʈ/�� ������
//            Vector3 localScale = transform.localScale;
//            localScale.x *= 1f;
//            transform.localScale = localScale;  
//            
//        }
//    }
//}
