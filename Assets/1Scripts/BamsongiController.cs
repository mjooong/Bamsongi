using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Shoot(new Vector3(0, 200, 2000));
        Destroy(this.gameObject, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();

        Destroy(this.gameObject, 4.0f);

        if(other.gameObject.tag == "Animal")
        {
            // 밤송이 외형 안보이게
            GetComponent<SphereCollider>().enabled = false;

            MeshRenderer[] a_ChildList =
                gameObject.GetComponentsInChildren<MeshRenderer>();
            for(int ii = 0; ii< a_ChildList.Length; ii++)
            {
                a_ChildList[ii].enabled = false;
            }
            // 밤송이 외형 안보이게

            // 보상주기
            GameMgr.Inst.AddScore();

            Destroy(other.gameObject);
        }

    }// private void OnCollisionEnter(Collision other)
}
