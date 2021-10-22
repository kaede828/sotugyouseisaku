using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    //�v���C���[�̍��W
    [SerializeField] private Transform Target;
    private Vector3 TargetPos;

    [SerializeField] int Distance = 50;

    //Ray�����������I�u�W�F�N�g�̏������锠
    RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        //�������v���C���[�ɕς���
        //transform.rotation =
        //Quaternion.LookRotation(Target.position - transform.position);

        //�^�[�Q�b�g���W + �ʒu����
        TargetPos = (Target.position - transform.position) + new Vector3 (0,1,0);

        //ray�̐���
        Ray ray = new Ray(transform.position, TargetPos);

        //�f�o�b�O�p
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        //�����蔻�� 
        if (Physics.Raycast(ray, out hit, Distance))
        {

            //�v���C���[�^�O�ɓ������Ă�����
            if (hit.collider.tag == "Player")
            {

                Debug.Log("Ray��Player�ɓ�������");

            }
        }

        //�����𑪂�
        //public GameObject cubeA;
        //public GameObject cubeB;

        //void Start()
        //{
        //    Vector3 posA = cubeA.transform.position;
        //    Vector3 posB = cubeB.transform.position;
        //    float dis = Vector3.Distance(posA, posB);
        //    Debug.Log("���� : " + dis);
        //}
    }
}
