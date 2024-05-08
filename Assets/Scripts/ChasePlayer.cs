using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject[] player;
    private Vector3 playerPos, enemyPos;

    public float moveSpeed = 0.3f;   //enemy가 좌우로 움직이는 속도를 컨트롤하는 변수

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("player");
    }
    void Update()
    {
        if (player != null && player.Length > 0)
        {
            playerPos = player[0].transform.position;
            enemyPos = transform.position;
            float moveX = playerPos.x > enemyPos.x ? 1 : -1;  //이동 방향은 playerPos와 enemyPos의 x값을 비교하여 설정. 이동 속도를 Mathf.Abs(moveX)로 곱하여 moveX가 -1인 경우 왼쪽으로, 1인 경우 오른쪽으로 이동.
            transform.position = Vector3.MoveTowards(enemyPos, new Vector3(playerPos.x, enemyPos.y, enemyPos.z), moveSpeed * Time.deltaTime * Mathf.Abs(moveX));
        }
    }
}


