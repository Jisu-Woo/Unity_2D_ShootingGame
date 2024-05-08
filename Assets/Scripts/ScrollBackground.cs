using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    private float backGroundSpeed = 4.0f;
    float moveCheck;
    void Start()
    {
        moveCheck = transform.position.y; // 오브젝트의 현재 y 위치를 moveCheck 에 저장한다.
    }
    // Update is called once per frame
    void Update()
    {
        moveCheck -= backGroundSpeed * Time.deltaTime; // moveCheck에서 1초에 backGroundSpeed 만큼 감소하도록 값을 뺴준다
        transform.position = new Vector3(0.0f, moveCheck, 0.0f); // moveCheck 값을 y 위치로 하여 오브젝트의 위치를 정한다
        if (moveCheck < -9.9f) // moveCheck 값이 -20 보다 작으면 즉 이동한 거리가 아래 방향으로 20 유닛 만큼 되었다면
            moveCheck = 9.9f; // moveCheck 값을 20 즉 최초 Background2 의 위치에 놓는다.
    }
}

