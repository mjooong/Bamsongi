using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;

    public float moveSpeed = 10.0f;     // 이동속도
    Vector3 moveDir = Vector3.zero;     // 이동방향

    // 카메라 회전을 위한 변수
    public float rotSpeed = 250.0f;
    Vector3 m_cacVec = Vector3.zero;

    // 높이값 찾기 위한 변수
    public Terrain m_RefMap = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 회전 구현부
        if (Input.GetMouseButton(1) == true)    // 마우스 우버튼 누르는 동안
        {
            m_cacVec = transform.eulerAngles;
            m_cacVec.y = m_cacVec.y + (rotSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));   // 수평회전
            m_cacVec.x = m_cacVec.x - (rotSpeed * Time.deltaTime * Input.GetAxis("Mouse Y"));   // 수직회전

            if(270.0f < m_cacVec.x && m_cacVec.x < 340.0f)
                m_cacVec.x = 340.0f;

            if (m_cacVec.x < 90.0f && 12.0f < m_cacVec.x)
                m_cacVec.x = 12.0f;

            transform.eulerAngles = m_cacVec;
        }
        // 카메라 회전 구현부

        // 이동 구현부
        h = Input.GetAxis("Horizontal");        // -1.0f ~ 1.0f 
        v = Input.GetAxis("Vertical");          // -1.0f ~ 1.0f

        // 전후좌우 이동 방향 벡터 계산
        moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();

        // Translate(이동방향 * Time.deltatime * 속도, 기준좌표)
        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        // 이동 구현부

        // 캐릭터의 높이값 찾기
        if(m_RefMap != null)
        {
            transform.position = new Vector3(transform.position.x, m_RefMap.SampleHeight(transform.position) + 5.0f, transform.position.z);
        }
        // 캐릭터의 높이값 찾기

    } // void Update()

    public bool IsMove()
    {
        if(h == 0.0f && v == 0.0f)
        {
            return false;   // 멈춘 상태
        }

        return true;    // 이동 중
    }

}
