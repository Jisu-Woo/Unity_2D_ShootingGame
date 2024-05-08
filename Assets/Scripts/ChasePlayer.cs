using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject[] player;
    private Vector3 playerPos, enemyPos;

    public float moveSpeed = 0.3f;   //enemy�� �¿�� �����̴� �ӵ��� ��Ʈ���ϴ� ����

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
            float moveX = playerPos.x > enemyPos.x ? 1 : -1;  //�̵� ������ playerPos�� enemyPos�� x���� ���Ͽ� ����. �̵� �ӵ��� Mathf.Abs(moveX)�� ���Ͽ� moveX�� -1�� ��� ��������, 1�� ��� ���������� �̵�.
            transform.position = Vector3.MoveTowards(enemyPos, new Vector3(playerPos.x, enemyPos.y, enemyPos.z), moveSpeed * Time.deltaTime * Mathf.Abs(moveX));
        }
    }
}


