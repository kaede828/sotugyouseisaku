using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitBullet : MonoBehaviour
{
    //　銃口
    private Transform muzzle;
    //　弾の半径
    private float muzzleRadius;

    [SerializeField]
    private float range = 1000.0f;

    public void Start()
    {
        muzzle = gameObject.transform;
        //　弾の半径を取得
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
            Debug.Log(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
        }

    }
}
