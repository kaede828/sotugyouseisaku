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

    [SerializeField] EnemyState state = EnemyState.PATROL;
    [SerializeField] int hp = 100;
    [SerializeField] int speed = 15;

    //�U���̔���
    [SerializeField] private GameObject attack;

    //����n�_�I�u�W�F�N�g���i�[����z��
    [SerializeField] public Transform[] points;
    //����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    //NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;

    private Animator animator;

    //�v���C���[�̍��W
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    //��������
    [SerializeField] int chaseDistance = 50;
    //�U������
    [SerializeField] int attackDistance = 5;
    //Ray�����������I�u�W�F�N�g�̏������锠
    RaycastHit hit;

    bool isChase = true;
    bool isAttack = false;
    bool isAttackCoolTime = false;
    bool isLook = false;

    bool isCeiling = true;

    // �Q�[���X�^�[�g���̏���
    void Start()
    {
        attack.SetActive(false);
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        // ���̏���n�_�̏��������s
        GotoNextPoint();

        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    // ���̏���n�_��ݒ肷�鏈��
    void GotoNextPoint()
    {
        // ����n�_���ݒ肳��Ă��Ȃ��ꍇ
        if (points.Length == 0) return;
        // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
        agent.destination = points[destPoint].position;
        // �z��̒����玟�̏���n�_��I���i�K�v�ɉ����ČJ��Ԃ��j
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Damage();
        }
    }

    void PlayerChase()
    {
        state = EnemyState.CHASE;
        animator.SetTrigger("chase");
        //�v���C���[��ǂ�������
        agent.destination = player.position;
    }

    void Patrol()
    {
        state = EnemyState.PATROL;
        animator.SetTrigger("patrol");
        // �G�[�W�F���g�����݂̏���n�_�ɓ��B������
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            // ���̏���n�_��ݒ肷�鏈�������s
            GotoNextPoint();
    }

    void Damage()
    {
        state = EnemyState.DAMAGE;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            hp -= 5;
        }
        StartCoroutine("Colortimer", 0.1f);
    }

    //�U���N�[���_�E��
    IEnumerator Attacktimer(int time)
    {
        state = EnemyState.ATTACK;
        animator.SetTrigger("attack");
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
            isAttack = true;
            attack.SetActive(false);
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        agent.isStopped = false;
        isAttackCoolTime = false;
        isAttack = false;
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

    // �Q�[�����s���̌J��Ԃ�����
    void Update()
    {
        //�������v���C���[�ɕς���
        //transform.rotation =Quaternion.LookRotation(player.position - transform.position);

        //�^�[�Q�b�g���W + �ʒu����
        playerPos = (player.position - transform.position) + new Vector3(0, 1, 0);
        //ray�̐���
        Ray ray = new Ray(transform.position, playerPos);
        //�f�o�b�O�p
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            //�v���C���[�^�O�ɓ������Ă�����
            if (hit.collider.tag == "Player")
            {
                if (Physics.Raycast(ray, out hit, attackDistance))
                {
                    if (isAttackCoolTime)
                    {
                        StartCoroutine("Attacktimer", 1);
                        isAttackCoolTime = true;
                    }
                }
                PlayerChase();
            }
            else Patrol();
        }
        else Patrol();

        if (isAttack)
        {
        }

        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }
}
