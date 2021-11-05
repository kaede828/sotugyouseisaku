using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitBullet : MonoBehaviour
{
    //�@�e��
    private Transform muzzle;
    //�@�e�̔��a
    private float muzzleRadius;

    [SerializeField]
    private float range = 1000.0f;

    public void Start()
    {
        muzzle = gameObject.transform;
        //�@�e�̔��a���擾
        muzzleRadius = muzzle.GetComponent<SphereCollider>().radius;
    }

    public void Update()
    {
        Debug.DrawLine(muzzle.position, muzzle.position + muzzle.transform.forward * range, Color.yellow);

        if (Input.GetAxis("joystick R2") > 0)
        {
            Judge();
        }

    }

    //�@�e�������ēG�ɓ������������肷��X�N���v�g
    public void Judge()
    {

        //�@��΂��ʒu�Ɣ�΂�������ݒ�
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);
        //�@���������R���C�_�����Ă����ϐ�
        RaycastHit[] hits;
        //�@Sphere�̌`�Ń��C���΂�Enemy���C���[�̂��̂�hits�ɓ����
        hits = Physics.SphereCastAll(ray, muzzleRadius, range, LayerMask.GetMask("Enemy"));
        //�@�˒�������distance�ɓ����
        float distance = range;

        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
        }

    }
}
