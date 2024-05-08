using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float xMin, xMax;
    public GameObject playerBullet1;
    public GameObject playerBullet2;
    public GameObject bulletSpawn;
    public float fireRate;
    private float nextFire;
    public int playerLife;
    public bool isRespawnTime = false;
    SpriteRenderer spriteRenderer;

    public GameObject player;

    private GameManager gameManager;

    public float maxShotDelay;
    public float curShotDelay;

    public float Power = 0; //아이템을 먹어서 증가한 힘을 저장할 변수

    public GameObject itemEffectPrefab;  //아이템 이펙트효과




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))   //아이템에 닿으면
        {
            if (Power < 4)  //최대 파워는 4까지
            {
                Power++;  //파워 1씩 증가

            }
            Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);  //아이템 이펙트 생성
            Destroy(collision.gameObject);   //아이템 오브젝트 삭제
        }
        if (collision.CompareTag("enemy") || collision.CompareTag("enemybullet"))   //"enemy"태그를 가진 오브젝트와 닿으면
        {
            if (isRespawnTime)  //리스폰 타임일 경우 진행하지 X
            {
                return;
            }
           
            
            Unbeatable();  //바로 Unbeatale()실행
            Invoke("Unbeatable", 3);  //3초 뒤에 다시 Unbeatable() 실행
        }

    }

    private void Update()
    {
        Fire();
        Reload();
    }

    void Fire()  // 총알을 만들어 쏘는 함수
    {
        if (curShotDelay < maxShotDelay)  //총알이 딜레이중이라면 쏘지 않음
            return;

        switch (Power)
        {
            case 1:  //파워 1 : 가운데에서 불렛 1을 하나씩 발사
                GameObject bullet = Instantiate(playerBullet1, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //사운드 플레이 
                break;
            case 2:  //파워 2 : 좌우에서 불렛 1을 총 두개씩 발사
                GameObject bulletR = Instantiate(playerBullet1, bulletSpawn.transform.position + Vector3.right * 0.1f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

                GameObject bulletL = Instantiate(playerBullet1, bulletSpawn.transform.position + Vector3.left * 0.1f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //사운드 플레이 
                break;
            case 3: //파워 3 : 가운데에서 불렛 2을 하나씩 발사
                GameObject bulletC = Instantiate(playerBullet2, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //사운드 플레이 
                break;

            case 4: //파워 4 : 좌우에서 불렛 2을 총 두개씩 발사
                GameObject bulletRR = Instantiate(playerBullet2, bulletSpawn.transform.position + Vector3.right * 0.2f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

                GameObject bulletLL = Instantiate(playerBullet2, bulletSpawn.transform.position + Vector3.left * 0.2f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //사운드 플레이 
                break;

        }
        curShotDelay = 0; //한 발 쏘고 다시 장전하게 함
    }
    void Reload()  //총알 재장전
    {
        curShotDelay += Time.deltaTime;
    }

    void FixedUpdate()
    {
        /*if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Instantiate(playerBullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            GetComponent<AudioSource>().Play();  //사운드 플레이 
        }*/
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.mousePosition;
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touchPos).x, -4.0f, 0.0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), -4.0f, 0.0f);
        }
    }

    void Unbeatable()  //player가 무적시간 중에는 반투명 상태로 변하게 하는 함수
    {

        isRespawnTime = !isRespawnTime;  //리스폰타임의 bool타입을 바꿔줌
        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);  //리스폰타임이라면, 반투명하게 함
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);  //리스폰타임이 아니라면 원래대로 
        }
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}



