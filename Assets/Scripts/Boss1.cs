using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1 : MonoBehaviour
{
    public GameObject bossBullet1;
    public GameObject bossBullet2;

    public bool isRotate = true;
    public GameObject destroySoundPref;

    public int scoreValue;
    public int health;



    private GameManager gameManager;
    private PlayerScript playerScript;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        //anim = GetComponent<Animator>();

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
        //Invoke("Think", 10); //2초 후 Think()실행


        //if (isRotate)
            //bossBullet1.transform.Rotate(Vector3.forward * 10);
    }

   /* public void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;

        anim.SetTrigger("OnHit");  //트리거 설정

    }*/
    void OnEnable()
    {
        Invoke("Stop",3); //3초 후 Stop()실행

    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 0.5f); //1초 후 Think()실행

    }
    void Think()  //어떤 공격할지 생각
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;


        if (health > 200 && health <= 250 )  //초반 데미지 입었을 경우 보스 패턴
        {
            FireFoward();
            Invoke("Think", 4); //1초 후 Think()실행

        }

        else if (health <= 200 && health > 100)   //중반 데미지 입었을 경우 보스 패턴
        {
            FireShot();
            Invoke("Think", 4); //1초 후 Think()실행


        }
        else if (health <= 100 && health > 0)  //후반 데미지 입었을 경우의 보스 패턴
        {
            FireAround();
            Invoke("Think", 5); //1초 후 Think()실행

        }

    }
    void FireFoward()
    {

        GameObject bulletR = Instantiate(bossBullet1, transform.position + Vector3.right * 0.3f, transform.rotation);
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        rigidR.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

        GameObject bulletRR = Instantiate(bossBullet1, transform.position + Vector3.right * 0.6f, transform.rotation);
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        rigidRR.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

        GameObject bulletL = Instantiate(bossBullet1, transform.position + Vector3.left * 0.3f, transform.rotation);
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        rigidL.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

        GameObject bulletLL = Instantiate(bossBullet1, transform.position + Vector3.left * 0.6f, transform.rotation);
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        rigidLL.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

        //패턴 카운팅
        curPatternCount++;

    }
    void FireShot()
    {
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = Instantiate(bossBullet2, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = playerScript.player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        }



        Debug.Log("플레이어 방향으로 샷건");
        curPatternCount++;

    }

    void FireAround()
    {
        Debug.Log("원 형태로 전체 공격");
        int roundNumA = 50;

        for (int index = 0; index < roundNumA; index++)
        {
            GameObject bullet = Instantiate(bossBullet2, transform.position, Quaternion.identity);

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();


            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNumA)
                ,Mathf.Sin(Mathf.PI * 2 * index / roundNumA));
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;

    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("playerbullet"))  //적이 playerbullet에 닿은경우
        {
            
            if (health < 0)  //보스의 health가 0이 되면
            {
                gameManager.AddScore(scoreValue);   // AddScore() 호출
                Destroy(collision.gameObject);
                Instantiate(destroySoundPref, Vector3.zero, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                health--;

                anim = GetComponent<Animator>();
                anim.SetTrigger("OnHit");  //트리거 설정
            }


        }
    }
}
