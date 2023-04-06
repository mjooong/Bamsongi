using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;

    public float moveSpeed = 10.0f;     // �̵��ӵ�
    Vector3 moveDir = Vector3.zero;     // �̵�����

    // ī�޶� ȸ���� ���� ����
    public float rotSpeed = 250.0f;
    Vector3 m_cacVec = Vector3.zero;

    // ���̰� ã�� ���� ����
    public Terrain m_RefMap = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ȸ�� ������
        if (Input.GetMouseButton(1) == true)    // ���콺 ���ư ������ ����
        {
            m_cacVec = transform.eulerAngles;
            m_cacVec.y = m_cacVec.y + (rotSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));   // ����ȸ��
            m_cacVec.x = m_cacVec.x - (rotSpeed * Time.deltaTime * Input.GetAxis("Mouse Y"));   // ����ȸ��

            if(270.0f < m_cacVec.x && m_cacVec.x < 340.0f)
                m_cacVec.x = 340.0f;

            if (m_cacVec.x < 90.0f && 12.0f < m_cacVec.x)
                m_cacVec.x = 12.0f;

            transform.eulerAngles = m_cacVec;
        }
        // ī�޶� ȸ�� ������

        // �̵� ������
        h = Input.GetAxis("Horizontal");        // -1.0f ~ 1.0f 
        v = Input.GetAxis("Vertical");          // -1.0f ~ 1.0f

        // �����¿� �̵� ���� ���� ���
        moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.Normalize();

        // Translate(�̵����� * Time.deltatime * �ӵ�, ������ǥ)
        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        // �̵� ������

        // ĳ������ ���̰� ã��
        if(m_RefMap != null)
        {
            transform.position = new Vector3(transform.position.x, m_RefMap.SampleHeight(transform.position) + 5.0f, transform.position.z);
        }
        // ĳ������ ���̰� ã��

    } // void Update()

    public bool IsMove()
    {
        if(h == 0.0f && v == 0.0f)
        {
            return false;   // ���� ����
        }

        return true;    // �̵� ��
    }

}
