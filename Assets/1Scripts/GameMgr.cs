using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    // ���̰� ã�� ���� ����
    public Terrain m_RefMap = null;

    public GameObject[] AnimalArr;
    public Transform AnimalGroup;

    // UI�� ����
    public float Timer = 60.0f;
    public static int Score = 0;
    public Text timerText;
    public Text scoreText;

    // ī�޶� �̵��� ������ �� ������ ��ġ�� ���� ���� ����
    PlayerController PlayerCtrl;
    float span = 1.0f;  // 1�ʿ� �ѹ� �� ����
    float delta = 0;
    // ī�޶� �̵��� ������ �� ������ ��ġ�� ���� ���� ����

    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        Score = 0; // �ʱ�ȭ

        Time.timeScale = 1.0f;  // ���� �⺻ �ӵ��� ��������
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
            Time.timeScale = 0.0f;  // �Ͻ����� ȿ��
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

    void DynamicGenerator()     // ĳ���Ͱ� ������ �� �����ϰ� ���͸� �����ϴ� �Լ�
    {
        // ĳ���Ͱ� ���� ���¿��� ���� ���� �ڵ�
        if (PlayerCtrl == null)
            return;

        if (PlayerCtrl.IsMove() == true)
        {
            delta = 0.0f;
            return;
        }
        // // ĳ���Ͱ� ���� ���¿��� ���� ���� �ڵ�

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
            // ������͸� ������ ���
            go.transform.eulerAngles = new Vector3(0.0f, a_Rot.y + Random.Range(-90.0f, 90.0f), 0.0f);

        }// if(delta > span)


    }// void DynamicGenerator()



}