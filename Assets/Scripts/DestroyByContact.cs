using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject destroySoundPref;

    public int scoreValue;
    private GameManager gameManager;

    private PlayerScript playerScript;

    public GameObject explosionPrefab;  //폭발효과


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))   //적이 player와 부딫힌 경우
        {
            if (playerScript.isRespawnTime)  //리스폰타임일 시 라이프 감소X
            {
                return;
            }

            playerScript.playerLife--; // playerLife = playerLife – 1 동일
            Debug.Log(playerScript.playerLife);

            Instantiate(destroySoundPref, Vector3.zero, Quaternion.identity);   //destroySound 생성됨

            if (playerScript.playerLife == 0)   //라이프가 0 되면
            {
                Destroy(gameObject);
                gameManager.GameOver();//GameOver()호출하여 게임오버시킴
            }
            Destroy(gameObject);   //플레이어 오브젝트 삭제
        }
        if (collision.CompareTag("playerbullet"))  //적이 playerbullet에 닿은경우
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);  //폭발 이펙트 생성

            gameManager.AddScore(scoreValue);   // AddScore() 호출

            //Destroy(collision.gameObject);
            Instantiate(destroySoundPref, Vector3.zero, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("player"); // 게임 오브젝트를 태그(‘player’)로 검색하여 저장
        if (playerObject != null)
            playerScript = playerObject.GetComponent<PlayerScript>(); // 게임 오브젝르의 PlayerScript 를 참조하기 위한 변수

        GameObject gameManagerObject = GameObject.FindWithTag("gamemanager");  //"gamemanager"태그를 가진 게임 오브젝트를 찾음
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();  //찾았다면, 해당 오브젝트에서 GameManager 컴포넌트(스트립트)를 가져옴
        }
        else
        {
            Debug.LogError("GameManager component not found on object with tag 'gamemanager'");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
