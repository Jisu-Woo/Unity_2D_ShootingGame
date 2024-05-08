using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy_fire는 데미지를 입힐 수 없는 존재이므로, player와 부딫힌 경우 게임오버되도록만 함
public class DestroyByContact_Fire : MonoBehaviour
{
    public GameObject destroySoundPref;

    private GameManager gameManager;

    private PlayerScript playerScript;

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
                Destroy(collision.gameObject);   //적 오브젝트 삭제
                gameManager.GameOver();//GameOver()호출하여 게임오버시킴
            }
            Destroy(gameObject);   //플레이어 오브젝트 삭제
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
