using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    // 높이값 찾기 위한 변수
    public Terrain m_RefMap = null;

    public GameObject[] AnimalArr;
    public Transform AnimalGroup;

    // UI용 변수
    public float Timer = 60.0f;
    public static int Score = 0;
    public Text timerText;
    public Text scoreText;

    // 카메라 이동이 멈췄을 때 랜덤한 위치에 몬스터 생성 변수
    PlayerController PlayerCtrl;
    float span = 1.0f;  // 1초에 한번 씩 스폰
    float delta = 0;
    // 카메라 이동이 멈췄을 때 랜덤한 위치에 몬스터 생성 변수

    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        Score = 0; // 초기화

        Time.timeScale = 1.0f;  // 원래 기본 속도로 돌려놓기
        StaticRandGen();

        PlayerCtrl = FindObjectOfType<PlayerController>();
        //GameObject a_PlayerObj = GameObject.Find("Main Camera");
        //if(a_PlayerObj != null)
        //    PlayerCtrl = a_PlayerObj.GetComponent<PlayerController>();
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        timerText.text = ((int)Timer).ToString();

        if (Timer <= 0)
        {
            Time.timeScale = 0.0f;  // 일시정지 효과
            SceneManager.LoadScene("GameOverScene");
        }

        DynamicGenerator();

    }// void Update()

    void StaticRandGen()
    {
        for (int ii = 0; ii < 200; ii++)
        {
            Vector3 RandomXYZ = new Vector3(Random.Range(-250.0f, 250.0f), 10.0f, Random.Range(-250.0f, 250.0f));

            RandomXYZ.y = m_RefMap.SampleHeight(RandomXYZ) + Random.Range(0.0f, 15.0f);

            int Kind = Random.Range(0, AnimalArr.Length);
            GameObject go = Instantiate(AnimalArr[Kind]) as GameObject;
            go.transform.position = RandomXYZ;
            go.transform.eulerAngles = new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

            if (AnimalGroup != null)
            {
                go.transform.SetParent(AnimalGroup);
            }
        } // for (int ii = 0; ii < 200; ii++)
    }//void StaticRandGen()

    public void AddScore(int Value = 10)
    {
        Score += Value;

        if (scoreText != null)
        {
            scoreText.text = "Score : " + Score.ToString();
        }
    }

    void DynamicGenerator()     // 캐릭터가 멈췄을 때 랜덤하게 몬스터를 생성하는 함수
    {
        // 캐릭터가 멈춘 상태에만 몬스터 스폰 코드
        if (PlayerCtrl == null)
            return;

        if (PlayerCtrl.IsMove() == true)
        {
            delta = 0.0f;
            return;
        }
        // // 캐릭터가 멈춘 상태에만 몬스터 스폰 코드

        delta += Time.deltaTime;
        if(delta > span)
        {
            delta = 0.0f;

            int Kind = Random.Range(0, AnimalArr.Length);

            Vector3 CamForW = Camera.main.transform.forward;
            CamForW.Normalize();
            CamForW = CamForW * (float)Random.Range(10.0f, 22.0f);

            Vector3 CacX = Camera.main.transform.right;
            CacX.y = 0.0f;
            CacX.Normalize();
            CacX = CacX * (float)Random.Range(-12.0f, 12.0f);

            Vector3 CacY = Vector3.up;
            CacY.Normalize();
            CacY = CacY * (float)Random.Range(-5.0f, 5.0f);

            Vector3 SpPos = Camera.main.transform.position + CacX + CacY + CamForW;
            GameObject go = Instantiate(AnimalArr[Kind]) as GameObject;
            go.transform.position = SpPos;

            Vector3 a_Dir = Camera.main.transform.forward;
            a_Dir.y = 0.0f;
            a_Dir.Normalize();
            Vector3 a_Rot = Quaternion.LookRotation(a_Dir).eulerAngles;
            // 방향백터를 각도로 계산
            go.transform.eulerAngles = new Vector3(0.0f, a_Rot.y + Random.Range(-90.0f, 90.0f), 0.0f);

        }// if(delta > span)


    }// void DynamicGenerator()



}