using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    enum EnemyState
    {
        PATROL,
        CHASE,
        DAMAGE,
        ATTACK
    }
    [SerializeField] private EnemyState state = EnemyState.PATROL;
    [SerializeField] private int hp = 1000;
    [SerializeField]
    private int speed = 10
        ;
    //ダメ―ジエフェクト
    [SerializeField] private GameObject bloodObj;
    //攻撃の判定
    [SerializeField] private GameObject rightArm;
    [SerializeField] private GameObject leftArm;
    //巡回地点オブジェクト
    [SerializeField] public Transform[] points;
    //巡回地点のオブジェクト数
    private int destPoint = 0;
    private NavMeshAgent agent;
    //プレイヤーの座標
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    //発見距離
    [SerializeField] int chaseDistance = 50;
    //攻撃距離
    [SerializeField] int attackDistance = 0;
    RaycastHit hit;

    //形態
    public int bossForm = 0;

    private Animator animator;

    bool isChase = true;
    public bool isAttack = true;
    bool isLook = false;

    bool run = false;

    //追加しました。弾があったたら
    public bool isBulletHit = false;
    public Vector3 hitPos;
    void Start()
    {
        rightArm.SetActive(false);
        leftArm.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        isBulletHit = false;
        hitPos = Vector3.zero;
    }

    void GotoNextPoint()
    {
        if (points.Length == 0) return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Bullet")
        //{
        //    Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
        //    Instantiate(bloodObj,hitPos,Quaternion.identity);
        //    Damage();
        //    Debug.Log("敵HP : " + hp);
        //}
    }

    void BulletHit(bool hit)
    {
        if (hit)
        {
            //Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            Instantiate(bloodObj, hitPos, Quaternion.identity);
            Damage();
            Debug.Log("敵HP : " + hp);
            isBulletHit = false;
        }
    }

    void PlayerChase()
    {
        if (isAttack)
        {
            //向きをプレイヤーに変える
            transform.rotation = Quaternion.LookRotation(player.position - transform.position);
        }
        state = EnemyState.CHASE;
        animator.SetTrigger("chase");

        agent.destination = player.position;
    }

    void Patrol()
    {
        state = EnemyState.PATROL;
        animator.SetTrigger("patrol");
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
    void Damage()
    {
        state = EnemyState.DAMAGE;
        if (hp <= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            animator.SetTrigger("death");
            StartCoroutine("Deathtimer", 3);

        }
        else
        {
            hp -= 10;
        }

        switch (hp)
        {
            case 300:
                bossForm = 2;
                break;
            case 600:
                bossForm = 1;
                break;
            case 1000:
                bossForm = 0;
                break;
        }

        StartCoroutine("Colortimer", 0.1f);
    }

    //攻撃クールダウン
    IEnumerator Attacktimer(float time)
    {
        state = EnemyState.ATTACK;
        isAttack = false;
        rightArm.SetActive(true);
        leftArm.SetActive(true);
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            agent.speed = 5;
            //agent.velocity = Vector3.zero;
            //agent.isStopped = true;

            //mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(1);
            Debug.Log(time);
            --time;

        }
        agent.speed = 10;
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        rightArm.SetActive(false);
        leftArm.SetActive(false);
        //agent.isStopped = false;
        isAttack = true;
    }

    IEnumerator Deathtimer(int time)
    {
        while (time >= 0)
        {
            //mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.5f);
            --time;
        }
        Destroy(this.gameObject);
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //Call();
    }

    // ゲーム実行中の繰り返し処理
    void Update()
    {

        playerPos = (player.position - transform.position) + new Vector3(0, 1, 0);
        Ray ray = new Ray(transform.position, playerPos);
        //デバッグ用
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        ////アニメーション
        //switch(EnemyState)
        //{
        //    case EnemyState.PATROL:
        //        break;
        //    case EnemyState.CHASE:
        //        break;
        //    case EnemyState.DAMAGE:
        //        break;
        //    case EnemyState.ATTACK:
        //        break;
        //}

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            if (hit.collider.tag == "Player")
            {
                if (Physics.Raycast(ray, out hit, attackDistance))
                {
                    if (isAttack)
                    {
                        switch (bossForm)
                        {
                            case 0:

                                StartCoroutine("Attacktimer", 1f);
                                animator.SetTrigger("attack");
                                break;

                            case 1:
                                StartCoroutine("Attacktimer", 1.5f);
                                animator.SetTrigger("attack2");
                                break;

                            case 2:
                                StartCoroutine("Attacktimer", 2.5f);
                                animator.SetTrigger("attack3");
                                break;
                        }


                    }
                }
                PlayerChase();
            }

            else Patrol();
        }
        else Patrol();

        //agent.velocity = (agent.steeringTarget - transform.position).normalized * agent.speed;
        //transform.forward = agent.steeringTarget - transform.position;

        //追加、弾が当たったら
        BulletHit(isBulletHit);

    }

    private void Call()
    {
        //enemyNum.DeathNum();
    }


}
