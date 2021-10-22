using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBullet : MonoBehaviour
{
    //�@�e�̃Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject bulletPrefab;
    //�@�e��
    [SerializeField]
    private Transform muzzle;
    //�@�e���΂���
    [SerializeField]
    private float bulletPower = 5f;
    //�@�e��
    [SerializeField]
    private float maxBullets = 30f;
    public float bulletCount = 30f;
    [SerializeField]
    private Vector3 dis;

    private Vector3 rayCameraPos;
    private float count = 0;
    [SerializeField]
    private float shotSpeed = 0.1f;
    //���ˉ�
    [SerializeField]
    private AudioClip gunSe;
    private new AudioSource audio = null;

    //�����[�h����
    public float reloadTime = 1.5f;
    public float rlTime = 0.0f;
    public bool isReload = false;
    //�����[�hSE
    [SerializeField]
    private AudioClip reloadSe;
    void Start()
    {
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
        bulletCount = maxBullets;
        dis = new Vector3(1, 1, 1);
        audio = transform.root.GetComponent<AudioSource>();
    }

    void Update()
    {

#if UNITY_EDITOR
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
#endif

        Ray ray = Camera.main.ScreenPointToRay(rayCameraPos);

        //���C���J�������烌�C���o���f�o�b�O��
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        transform.rotation = Quaternion.LookRotation(ray.direction);
        RaycastHit hit;
        int layerMask = ~(1 << 6);
        if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
        {
            Vector3 distanc = hit.point - transform.position;
            dis = distanc.normalized;
            //Debug.Log(dis);
            transform.rotation = Quaternion.LookRotation(distanc);
        }
        else
        {
            var p = Camera.main.transform.forward * 1000.0f;
            Vector3 distanc = p - transform.position;
            dis = distanc.normalized;
            transform.rotation = Quaternion.LookRotation(distanc);
        }

        if (isReload)
        {
            if (rlTime >= reloadTime)
            {
                rlTime = 0;
                bulletCount = maxBullets;
                isReload = false;
            }
            rlTime += Time.deltaTime;
        }

        //�����[�h
        if (Input.GetButton("joystick X") && maxBullets > bulletCount)
        {
            if(!isReload)
                audio.PlayOneShot(reloadSe);
            isReload = true;
        }

        if (bulletCount <= 0 || isReload)
        {
            return;
        }

        //�@�}�E�X�̍��N���b�N�Ō���
        if (Input.GetAxis("joystick R2") > 0)
        {
            if (count > shotSpeed)
            {
                Shot();
                bulletCount--;
                count = 0.0f;
            }
            count += Time.deltaTime;
        }
    }

    //�@�G������
    void Shot()
    {
        audio.PlayOneShot(gunSe);
        var bulletInstance = Instantiate<GameObject>(bulletPrefab, muzzle.position, Quaternion.LookRotation(dis));
        // bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        bulletInstance.GetComponent<BulletAttack>().speed = dis * bulletPower;
        Destroy(bulletInstance, 5f);
    }
}
