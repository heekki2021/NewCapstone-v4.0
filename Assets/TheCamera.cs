using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCamera : MonoBehaviour
{
    [Header("SceneChangeButton")]
    public float range = 10f;

    public GameObject handle;





    #region SceneChangeButton
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
        Debug.Log("EŰ ����");
        
        Shoot();
        }
        
    }

    void Shoot()
    {

        int layerMask = 1 << 3; // 3�� Layer�� �ش��ϴ� ��Ʈ ����ũ�� �����մϴ�.
        layerMask = ~layerMask; // �� Layer�� �����ϱ� ���� ��Ʈ ������ ����մϴ�.


        Ray r = new(transform.position, transform.GetComponent<Transform>().forward);

        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        float rayLength = 100f; // Raycast�� ���� ����
        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * rayLength, Color.red);

        if (Physics.Raycast(r, out RaycastHit hitInfo, range))
        {

            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }

    }


    #endregion



}
