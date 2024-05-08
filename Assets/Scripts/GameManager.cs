using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] enemy;
    public int[] enemyCount;
    public float spawnWait;
    public float startWait;

    public Text scoreText;
    private int score;  //������ ������ ����
    public int bossScore; //������ ����

    public Text isGameOverText;  //���ӿ��� �ؽ�Ʈ
    public bool isGameOver;

    public GameObject restartButton;  //restart ��ư�� ���� ����

    public PlayerScript playerScript;  //playerScript �޾ƿ� ����
    public Boss1 bossScript;
    public Image playerLifeImage;  //������ �̹��� �޴� ����
    private Image[] playerLifeImages;  //playerLifeImage�� �����ؼ� ������
    public Canvas canvas;  //ĵ���� �޾ƿ��� ����
    private int oldLife;  //���ϱ� ���� ������ ����

    public GameObject boss;
    private float startTime;  
    private float timeLimit = 120f;  //2�� ���ѽð�

    public bool bossSpawn;




    void Start()
    {
        GameObject bossObject = GameObject.FindWithTag("enemy"); // ���� ������Ʈ�� �±�(��player��)�� �˻��Ͽ� ����
        if (bossObject != null)
            bossScript = bossObject.GetComponent<Boss1>(); // ���� ���������� PlayerScript �� �����ϱ� ���� ����

        restartButton.SetActive(false);   //����۹�ư ��Ȱ��ȭ
        bossSpawn = false;
        isGameOver = false;
        if (isGameOverText != null)
        {
            isGameOverText.text = " ";  //���� �� ����α�
        }


        score = 0;  //���� 0���� ����
        UpdateScore();  //������ ǥ��

        startTime = Time.realtimeSinceStartup;  //��Ÿ�� �����ϴ� ����

        StartCoroutine(SpawnWaves());


        playerLifeImages = new Image[playerScript.playerLife]; // ���� ������ ������ ������ŭ �迭 ����
        for (int i = 0; i < playerScript.playerLife; i++)  
        {
            playerLifeImages[i] = Instantiate(playerLifeImage);  //�������̹��� ����
            playerLifeImages[i].transform.SetParent(canvas.transform);  //������ġ: ĵ������ �θ���Ͽ� ĵ���� ��ġ�� �������� ��)
            playerLifeImages[i].rectTransform.localPosition = new Vector3(-147 + (40 * i), -278, 0);  //ĵ���� ��ġ�� �������� ���� ��ġ�� �������� ��ǥ����
        }
        oldLife = playerScript.playerLife;  //oldLife�� ���� ������ ������ ����ȭ��
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + score; // �Ǵ� scoreText.text = "Score " + score.ToString();
    }
    public void AddScore(int newScoreValue)   //���� ������Ʈ�ϴ� ����
    {
        score += newScoreValue;
        UpdateScore();
    }

    IEnumerator SpawnWaves()    //�ڷ�ƾ. �ݺ��Ͽ� enemy���� spawn�ǵ��� ��
    {
        yield return new WaitForSeconds(startWait);

        while (true)  //2�� �������� ��� ������
        {

          
            for (int i = 0; i < enemy.Length; ++i)
            {
                for (int j = 0; j < enemyCount[i]; ++j)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(6.0f, 8.0f));
                    Instantiate(enemy[i], spawnPosition, Quaternion.identity);
                    yield return new WaitForSeconds(spawnWait);
                }
            }

            if (isGameOver)// Gameover�̸� �ߴ�. �� ���� ����
            {
                break;
            }
          
        }
    }

    public void GameOver()
    {
        isGameOverText.text = "Game Over";

        restartButton.SetActive(true);  //����۹�ư Ȱ��ȭ

        isGameOver = true;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //sceneManager�� ���� ���� ����ǰ� �ִ� Scene�� �ٽ� �ε�

    }

    // Update is called once per frame
    private void Update()
    {
        if((Time.realtimeSinceStartup - startTime > timeLimit) && !bossSpawn)
        {
            Debug.Log("2���� �������ϴ�.");
            StopCoroutine(SpawnWaves());  //�ڷ�ƾ ����

            Vector3 spawnPosition1 = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(6.0f, 8.0f));
            Instantiate(boss, spawnPosition1, Quaternion.identity);  //���� ����
            bossSpawn = true;  //������ �̹� �����Ǿ����� ǥ��
        }

        if (oldLife > playerScript.playerLife) //���� ���������� playerLife�� �������� ���,
        {
            
            oldLife = playerScript.playerLife;  //oldLife�� ���� ������ �� ����ȭ
            Debug.Log("OldLife Value : " + oldLife);  
            if (oldLife < 0) oldLife = 0; //
            for (int i = oldLife; i < playerLifeImages.Length; i++) //������ ������ ������ŭ �̹����� �迭���� ������ �ʰ� ��
            {
                playerLifeImages[i].enabled = false;
            }
        }
    }
}
