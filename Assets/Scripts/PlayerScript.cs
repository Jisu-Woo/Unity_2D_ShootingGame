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

    public float Power = 0; //�������� �Ծ ������ ���� ������ ����

    public GameObject itemEffectPrefab;  //������ ����Ʈȿ��




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))   //�����ۿ� ������
        {
            if (Power < 4)  //�ִ� �Ŀ��� 4����
            {
                Power++;  //�Ŀ� 1�� ����

            }
            Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);  //������ ����Ʈ ����
            Destroy(collision.gameObject);   //������ ������Ʈ ����
        }
        if (collision.CompareTag("enemy") || collision.CompareTag("enemybullet"))   //"enemy"�±׸� ���� ������Ʈ�� ������
        {
            if (isRespawnTime)  //������ Ÿ���� ��� �������� X
            {
                return;
            }
           
            
            Unbeatable();  //�ٷ� Unbeatale()����
            Invoke("Unbeatable", 3);  //3�� �ڿ� �ٽ� Unbeatable() ����
        }

    }

    private void Update()
    {
        Fire();
        Reload();
    }

    void Fire()  // �Ѿ��� ����� ��� �Լ�
    {
        if (curShotDelay < maxShotDelay)  //�Ѿ��� ���������̶�� ���� ����
            return;

        switch (Power)
        {
            case 1:  //�Ŀ� 1 : ������� �ҷ� 1�� �ϳ��� �߻�
                GameObject bullet = Instantiate(playerBullet1, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //���� �÷��� 
                break;
            case 2:  //�Ŀ� 2 : �¿쿡�� �ҷ� 1�� �� �ΰ��� �߻�
                GameObject bulletR = Instantiate(playerBullet1, bulletSpawn.transform.position + Vector3.right * 0.1f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

                GameObject bulletL = Instantiate(playerBullet1, bulletSpawn.transform.position + Vector3.left * 0.1f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //���� �÷��� 
                break;
            case 3: //�Ŀ� 3 : ������� �ҷ� 2�� �ϳ��� �߻�
                GameObject bulletC = Instantiate(playerBullet2, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //���� �÷��� 
                break;

            case 4: //�Ŀ� 4 : �¿쿡�� �ҷ� 2�� �� �ΰ��� �߻�
                GameObject bulletRR = Instantiate(playerBullet2, bulletSpawn.transform.position + Vector3.right * 0.2f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

                GameObject bulletLL = Instantiate(playerBullet2, bulletSpawn.transform.position + Vector3.left * 0.2f, bulletSpawn.transform.rotation);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();  //���� �÷��� 
                break;

        }
        curShotDelay = 0; //�� �� ��� �ٽ� �����ϰ� ��
    }
    void Reload()  //�Ѿ� ������
    {
        curShotDelay += Time.deltaTime;
    }

    void FixedUpdate()
    {
        /*if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Instantiate(playerBullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            GetComponent<AudioSource>().Play();  //���� �÷��� 
        }*/
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.mousePosition;
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touchPos).x, -4.0f, 0.0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), -4.0f, 0.0f);
        }
    }

    void Unbeatable()  //player�� �����ð� �߿��� ������ ���·� ���ϰ� �ϴ� �Լ�
    {

        isRespawnTime = !isRespawnTime;  //������Ÿ���� boolŸ���� �ٲ���
        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);  //������Ÿ���̶��, �������ϰ� ��
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);  //������Ÿ���� �ƴ϶�� ������� 
        }
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}



