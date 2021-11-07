using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBullet : MonoBehaviour
{
    //　弾のゲームオブジェクト
    [SerializeField]
    private GameObject bulletPrefab;
    //　銃口
    [SerializeField]
    private Transform muzzle;
    //　弾を飛ばす力
    [SerializeField]
    private float bulletPower = 5f;
    //　弾数
    [SerializeField]
    private float maxBullets = 30f;
    public float bulletCount = 30f;
    [SerializeField]
    private Vector3 dis;

    private Vector3 rayCameraPos;
    private float count = 0;
    [SerializeField]
    private float shotSpeed = 0.1f;
    //発射音
    [SerializeField]
    private AudioClip gunSe;
    private new AudioSource audio = null;

    //リロード時間
    public float reloadTime = 1.5f;
    public float rlTime = 0.0f;
    public bool isReload = false;

    //　弾の半径
    private float muzzleRadius;
    [SerializeField]
    private float range = 1000.0f;
    //リロードSE
    [SerializeField]
    private AudioClip reloadSe;
    void Start()
    {
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
        bulletCount = maxBullets;
        dis = new Vector3(1, 1, 1);
        audio = transform.root.GetComponent<AudioSource>();

        muzzle = gameObject.transform;
        //　弾の半径を取得
        muzzleRadius = muzzle.GetComponent<SphereCollider>().radius;
    }

    void Update()
    {

#if UNITY_EDITOR
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
#endif

        Ray ray = Camera.main.ScreenPointToRay(rayCameraPos);

        //メインカメラからレイを出すデバッグ線
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

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

        //リロード
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

        //　マウスの左クリックで撃つ
        if (Input.GetAxis("joystick R2") > 0)
        {

            if (count > shotSpeed)
            {
                Shot();
                Judge();
                bulletCount--;
                count = 0.0f;
            }
            count += Time.deltaTime;
        }
    }

    //　敵を撃つ
    void Shot()
    {
        audio.PlayOneShot(gunSe);
        var bulletInstance = Instantiate<GameObject>(bulletPrefab, muzzle.position, Quaternion.LookRotation(dis));
        //bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        bulletInstance.GetComponent<BulletAttack>().speed = dis * bulletPower;
        Destroy(bulletInstance, 5f);
    }

    //　銃を撃って敵に当たったか判定するスクリプト
    public void Judge()
    {

        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);
        //　当たったコライダを入れておく変数
        RaycastHit[] hits;
        //　Sphereの形でレイを飛ばしEnemyレイヤーのものをhitsに入れる
        hits = Physics.SphereCastAll(ray, muzzleRadius, range, LayerMask.GetMask("Enemy"));
        //　射程距離をdistanceに入れる
        float distance = range;

        foreach (var hit in hits)
        {
            //敵を消す
            //var em = hit.collider.gameObject.GetComponent<EnemyMove>();
            //em.hitPos = hit.point;
            //em.isBulletHit = true;
            ////Destroy(hit.collider.gameObject);

            var sem = hit.collider.gameObject.GetComponent<SpiderEnemyMove>();
            sem.hitPos = hit.point;
            sem.isBulletHit = true;
        }

    }
}
