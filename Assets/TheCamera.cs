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
        Debug.Log("E키 눌림");
        
        Shoot();
        }
        
    }

    void Shoot()
    {

        int layerMask = 1 << 3; // 3번 Layer에 해당하는 비트 마스크를 생성합니다.
        layerMask = ~layerMask; // 이 Layer를 제외하기 위해 비트 반전을 사용합니다.


        Ray r = new(transform.position, transform.GetComponent<Transform>().forward);

        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        float rayLength = 100f; // Raycast의 길이 설정
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
