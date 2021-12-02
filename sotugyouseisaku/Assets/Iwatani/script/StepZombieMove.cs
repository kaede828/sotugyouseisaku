using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StepZombieMove : MonoBehaviour
{
    // Start is called before the first frame update
    enum EnemyState
    {
        PATROL,
        CHASE,
        DAMAGE,
        ATTACK,
        STEP
    }
    [SerializeField] private EnemyState state = EnemyState.PATROL;
    [SerializeField] private int hp = 100;
    [SerializeField] private int speed = 20;
    //�_���\�W�G�t�F�N�g
    [SerializeField] private GameObject bloodObj;
    //�U���̔���
    [SerializeField] private GameObject attack;
    //����n�_�I�u�W�F�N�g
    [SerializeField] public Transform[] points;
    //����n�_�̃I�u�W�F�N�g��
    private int destPoint = 0;
    private NavMeshAgent agent;
    //�v���C���[�̍��W
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    //��������
    [SerializeField] int chaseDistance = 50;
    //�U������
    [SerializeField] int attackDistance = 0;
    //�X�e�b�v����
    [SerializeField] int stepDistance = 20;
    RaycastHit hit;

    private Animator animator;

    bool isChase = true;
    bool isAttack = true;
    bool isRightStep = false;
    bool isLeftStep = false;
    bool isLook = false;
    public bool isDeath = false;

    int rand=0;

    [SerializeField] private EnemyNum enemyNum;

    //�ǉ����܂����B�e������������
    public bool isBulletHit = false;
    public Vector3 hitPos;
    void Start()
    {
        rand=Random.Range(1, 2);
        attack.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        isBulletHit = false;
        hitPos = Vector3.zero;

        switch (rand)
        {
            case 1:isRightStep = true;
                break;
            case 2:
                isLeftStep = true;
                break;
        }
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
        //    Debug.Log("�GHP : " + hp);
        //}
    }

    void BulletHit(bool hit)
    {
        if (hit)
        {
            //Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            Instantiate(bloodObj, hitPos, Quaternion.identity);
            Damage();
            Debug.Log("�GHP : " + hp);
            isBulletHit = false;
        }
    }

    void PlayerChase()
    {
        if (isAttack)
        {
            //�������v���C���[�ɕς���
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
            isDeath = true;
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            animator.SetTrigger("death");
            StartCoroutine("Deathtimer", 3);

        }
        else
        {
            hp -= 20;
        }
        StartCoroutine("Colortimer", 0.1f);
    }

    //�U���N�[���_�E��
    IEnumerator Attacktimer(int time)
    {
        state = EnemyState.ATTACK;
        attack.SetActive(true);
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            //mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(1f);
            Debug.Log(time);
            --time;
            attack.SetActive(false);
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        agent.isStopped = false;
        isAttack = true;
    }  
    IEnumerator steptimer(int time)
    {
        state = EnemyState.STEP;
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            //mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(1f);
            Debug.Log(time);
            --time;
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        rand = Random.Range(1, 3);
        //Debug.Log(rand);
        switch (rand)
        {
            case 1:
                isRightStep = true;
                break;
            case 2:
                isLeftStep = true;
                break;
        }
    }

    //��Ԃ�\���_��
    IEnumerator Colortimer(int time)
    {
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            //mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
            --time;
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    IEnumerator Deathtimer(int time)
    {
        while (time >= 0)
        {
            //mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(1);
            --time;
        }
        Destroy(this.gameObject);
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //Call();
    }

    // �Q�[�����s���̌J��Ԃ�����
    void Update()
    {

        playerPos = (player.position - transform.position) + new Vector3(0, 1, 0);
        Ray ray = new Ray(transform.position, playerPos);
        //�f�o�b�O�p
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        ////�A�j���[�V����
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
                    if (isAttack && !isDeath)
                    {
                        StartCoroutine("Attacktimer", 1);
                        animator.SetTrigger("attack");
                        isAttack = false;
                    }
                }
                if (Physics.Raycast(ray, out hit, stepDistance))
                {
                    if (isRightStep && !isDeath)
                    {
                        StartCoroutine("steptimer", 5);
                        animator.SetTrigger("rightstep");
                        isRightStep = false;
                    }
                    if (isLeftStep && !isDeath)
                    {
                        StartCoroutine("steptimer", 5);
                        animator.SetTrigger("leftstep");
                        isLeftStep = false;
                    }
                }
                PlayerChase();
            }
            else Patrol();
        }
        else Patrol();

        //�ǉ��A�e������������
        BulletHit(isBulletHit);
    }
}
