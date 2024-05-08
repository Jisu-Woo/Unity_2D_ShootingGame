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
    private int score;  //점수를 저장할 변수
    public int bossScore; //보스의 점수

    public Text isGameOverText;  //게임오버 텍스트
    public bool isGameOver;

    public GameObject restartButton;  //restart 버튼을 담을 변수

    public PlayerScript playerScript;  //playerScript 받아올 변수
    public Boss1 bossScript;
    public Image playerLifeImage;  //라이프 이미지 받는 변수
    private Image[] playerLifeImages;  //playerLifeImage를 복사해서 저장함
    public Canvas canvas;  //캔버스 받아오는 변수
    private int oldLife;  //변하기 전의 라이프 개수

    public GameObject boss;
    private float startTime;  
    private float timeLimit = 120f;  //2분 제한시간

    public bool bossSpawn;




    void Start()
    {
        GameObject bossObject = GameObject.FindWithTag("enemy"); // 게임 오브젝트를 태그(‘player’)로 검색하여 저장
        if (bossObject != null)
            bossScript = bossObject.GetComponent<Boss1>(); // 게임 오브젝르의 PlayerScript 를 참조하기 위한 변수

        restartButton.SetActive(false);   //재시작버튼 비활성화
        bossSpawn = false;
        isGameOver = false;
        if (isGameOverText != null)
        {
            isGameOverText.text = " ";  //시작 시 비워두기
        }


        score = 0;  //점수 0으로 세팅
        UpdateScore();  //점수를 표시

        startTime = Time.realtimeSinceStartup;  //런타임 누적하는 변수

        StartCoroutine(SpawnWaves());


        playerLifeImages = new Image[playerScript.playerLife]; // 현재 설정된 라이프 개수만큼 배열 생성
        for (int i = 0; i < playerScript.playerLife; i++)  
        {
            playerLifeImages[i] = Instantiate(playerLifeImage);  //라이프이미지 생성
            playerLifeImages[i].transform.SetParent(canvas.transform);  //생성위치: 캔버스를 부모로하여 캔버스 위치를 기준으로 함)
            playerLifeImages[i].rectTransform.localPosition = new Vector3(-147 + (40 * i), -278, 0);  //캔버스 위치를 기준으로 잡은 위치에 놓여지는 좌표생성
        }
        oldLife = playerScript.playerLife;  //oldLife에 현재 라이프 개수를 동기화함
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + score; // 또는 scoreText.text = "Score " + score.ToString();
    }
    public void AddScore(int newScoreValue)   //점수 업데이트하는 역할
    {
        score += newScoreValue;
        UpdateScore();
    }

    IEnumerator SpawnWaves()    //코루틴. 반복하여 enemy들이 spawn되도록 함
    {
        yield return new WaitForSeconds(startWait);

        while (true)  //2분 내에서는 계속 돌게함
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

            if (isGameOver)// Gameover이면 중단. 적 스폰 멈춤
            {
                break;
            }
          
        }
    }

    public void GameOver()
    {
        isGameOverText.text = "Game Over";

        restartButton.SetActive(true);  //재시작버튼 활성화

        isGameOver = true;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //sceneManager에 의해 현재 진행되고 있는 Scene을 다시 로드

    }

    // Update is called once per frame
    private void Update()
    {
        if((Time.realtimeSinceStartup - startTime > timeLimit) && !bossSpawn)
        {
            Debug.Log("2분이 지났습니다.");
            StopCoroutine(SpawnWaves());  //코루틴 멈춤

            Vector3 spawnPosition1 = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(6.0f, 8.0f));
            Instantiate(boss, spawnPosition1, Quaternion.identity);  //보스 생성
            bossSpawn = true;  //보스가 이미 스폰되었음을 표시
        }

        if (oldLife > playerScript.playerLife) //기존 라이프보다 playerLife가 감소했을 경우,
        {
            
            oldLife = playerScript.playerLife;  //oldLife에 현재 라이프 수 동기화
            Debug.Log("OldLife Value : " + oldLife);  
            if (oldLife < 0) oldLife = 0; //
            for (int i = oldLife; i < playerLifeImages.Length; i++) //감소한 라이프 개수만큼 이미지를 배열에서 보이지 않게 함
            {
                playerLifeImages[i].enabled = false;
            }
        }
    }
}
