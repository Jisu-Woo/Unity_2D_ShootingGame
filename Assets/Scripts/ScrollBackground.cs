using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    private float backGroundSpeed = 4.0f;
    float moveCheck;
    void Start()
    {
        moveCheck = transform.position.y; // ������Ʈ�� ���� y ��ġ�� moveCheck �� �����Ѵ�.
    }
    // Update is called once per frame
    void Update()
    {
        moveCheck -= backGroundSpeed * Time.deltaTime; // moveCheck���� 1�ʿ� backGroundSpeed ��ŭ �����ϵ��� ���� ���ش�
        transform.position = new Vector3(0.0f, moveCheck, 0.0f); // moveCheck ���� y ��ġ�� �Ͽ� ������Ʈ�� ��ġ�� ���Ѵ�
        if (moveCheck < -9.9f) // moveCheck ���� -20 ���� ������ �� �̵��� �Ÿ��� �Ʒ� �������� 20 ���� ��ŭ �Ǿ��ٸ�
            moveCheck = 9.9f; // moveCheck ���� 20 �� ���� Background2 �� ��ġ�� ���´�.
    }
}

