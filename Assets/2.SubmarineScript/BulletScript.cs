using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {

        //mainCam에 카메라 부착
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        //rb 에 rigidbody 부착 (3d로)
        rb = GetComponent<Rigidbody2D>();

        //마우스의 x, y 위치값을 받아 스크린 상의 위치를 mousePos에 넣음.(vector3)
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //마우스의 위치에서 가운데에 고정되어있는 플레이어의 위치를 빼 총구의 방향을 결정
        Vector3 direction = mousePos - transform.position;

        //물체의 포지션에서 마우스의 위치를 뺴서 회전값을 받아옴
        Vector3 rotation = transform.position - mousePos;

        //총알의 속도 결정 (normalized 1)
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //총알의 발사 로테이션
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //플레이어의 좌우 위치 계산
       transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
