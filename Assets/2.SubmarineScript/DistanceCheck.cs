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
        //���� �÷��̾� ��ġ�� ���� ��ġ ������ �Ÿ��� ���
        float distance = Vector3.Distance(startPosition, player.position);

        //�Ÿ��� UI�� ǥ�� (0.00����)
        distanceText.text = distance.ToString("F2") + "M";
    }
}
