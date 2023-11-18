using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DistanceCheck : MonoBehaviour
{

    [SerializeField]
    private Transform player;

    private Vector3 startPosition;

    [SerializeField]
    private TMP_Text distanceText;

    

    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //현재 플레이어 위치와 시작 위치 사이의 거리를 계산
        float distance = Vector3.Distance(startPosition, player.position);

        //거리를 UI에 표시 (0.00형식)
        distanceText.text = distance.ToString("F2") + "M";
    }
}
