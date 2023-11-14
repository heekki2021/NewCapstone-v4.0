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

        //mainCam�� ī�޶� ����
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        //rb �� rigidbody ���� (3d��)
        rb = GetComponent<Rigidbody2D>();

        //���콺�� x, y ��ġ���� �޾� ��ũ�� ���� ��ġ�� mousePos�� ����.(vector3)
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //���콺�� ��ġ���� ����� �����Ǿ��ִ� �÷��̾��� ��ġ�� �� �ѱ��� ������ ����
        Vector3 direction = mousePos - transform.position;

        //��ü�� �����ǿ��� ���콺�� ��ġ�� ���� ȸ������ �޾ƿ�
        Vector3 rotation = transform.position - mousePos;

        //�Ѿ��� �ӵ� ���� (normalized 1)
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //�Ѿ��� �߻� �����̼�
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //�÷��̾��� �¿� ��ġ ���
       transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
