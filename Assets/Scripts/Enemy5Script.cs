using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Script : MonoBehaviour
{
    public GameObject enemyBullet;
    public GameObject bulletSpawn;
    public float fireRate;
    private float nextFire;
    private PlayerScript playerScript;
    //public GameObject destroySoundPref;


    void FixedUpdate()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        }

    }


            /*위 코드는 enemyBullet 게임 오브젝트를 bulletSpawn 위치에서 발사하는 스크립트입니다. fixedUpdate() 함수는 매 프레임마다 일정한 시간 간격으로 호출되는 함수입니다. 이 함수를 사용하여 적을 추적하고, 일정 시간 간격으로 총알을 발사하게 됩니다.

        public GameObject enemyBullet: 발사될 총알 오브젝트를 나타내는 public 변수입니다.
        public GameObject bulletSpawn: 총알이 발사될 위치를 나타내는 public 변수입니다.
        public float fireRate: 총알이 발사되는 주기를 나타내는 public 변수입니다.
        private float nextFire: 다음 총알이 발사될 시간을 저장하는 private 변수입니다.
        FixedUpdate() 함수에서 if (Time.time > nextFire) 구문을 사용하여 fireRate 주기에 따라 총알을 발사합니다. 

        Instantiate() 함수를 사용하여 enemyBullet 오브젝트를 bulletSpawn 위치와 회전값으로 생성합니다. nextFire 변수에 현재 시간과 fireRate를 더한 값을 저장하여, 다음 총알이 발사될 시간을 계산합니다. 
            이렇게 함으로써 fireRate 주기마다 총알을 발사할 수 있습니다. */

            // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("player"); // 게임 오브젝트를 태그(‘player’)로 검색하여 저장
        if (playerObject != null)
            playerScript = playerObject.GetComponent<PlayerScript>(); // 게임 오브젝르의 PlayerScript 를 참조하기 위한 변수
    }

    // Update is called once per frame

}
