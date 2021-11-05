using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderEnemyMove : MonoBehaviour
{
    enum EnemyState
    {
        PATROL,
        CHASE,
        DAMAGE,
        ATTACK
    }
    [SerializeField] private GameObject upSpider=null;
    [SerializeField] private GameObject downSpider=null;
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
    RaycastHit hit;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform transformUp;

    bool isChase = true;
    bool isAttack = true;
    bool isLook = false;
    bool isUp=true;

    [SerializeField] private EnemyNum enemyNum;


    void Start()
    {
        animator = animator.GetComponent<Animator>();
        attack.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        
        GotoNextPoint();
        agent.speed = speed;
        
    }

    void GotoNextPoint()
    {
        if (points.Length == 0) return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            Instantiate(bloodObj, hitPos, Quaternion.identity);
            Damage();
            Debug.Log("�GHP : " + hp);
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
        if(downSpider.activeSelf)
        {         
            Debug.Log("�`�F�C�X");
            animator.SetTrigger("chase");
        }  
        agent.destination = player.position;
        if(isUp)
        {
            StartCoroutine("Downtimer", 0.6);
            //rigidbody.GetComponent<Rigidbody>();
            //rigidbody.isKinematic = false;
            isUp = false;
        }
        
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
        if (isUp)
        {
            StartCoroutine("Downtimer", 1);
            //rigidbody.GetComponent<Rigidbody>();
            //rigidbody.isKinematic = false;
            isUp = false;
        }
        if (hp <= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            //animator.SetTrigger("death");
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
        transform.rotation = Quaternion.LookRotation(player.position - transform.position);
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            //mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);          
            yield return new WaitForSeconds(1);
            Debug.Log(time);
            --time;
            attack.SetActive(false);
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        agent.isStopped = false;
        isAttack = true;
    }

    IEnumerator Downtimer(int time)
    {
        while (time >= 0)
        {
            //agent.velocity = Vector3.zero;
            //agent.isStopped = true;
            rigidbody.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            
            yield return new WaitForSeconds(1f);
            Debug.Log(time);
            --time;
        }
        
        downSpider.SetActive(true);
        upSpider.SetActive(false);
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
        Call();
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
                    if (isAttack&&isUp==false)
                    {

                        StartCoroutine("Attacktimer", 1);
                        Debug.Log("�U��");
                        animator.SetTrigger("attack");
                        isAttack = false;
                    }
                }
                PlayerChase();
            }
            else Patrol();
        }
        else Patrol();

        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="pounce")
        {
            Debug.Log("��т���");
            transform.position += transform.forward * 10 * Time.deltaTime;
        }
    }

    private void Call()
    {
        enemyNum.DeathNum();
    }


}


