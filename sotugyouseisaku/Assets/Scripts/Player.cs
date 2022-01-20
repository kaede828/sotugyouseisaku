using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int Playerhp;
    public int hp;
    [SerializeField]
    private float speed = 10.0f; //���x
    [SerializeField]
    private float jump = 2.0f; //�W�����v
    public float g = 9.8f; //�d��
    //[SerializeField]
    //private float moveS = 2.0f;
    private float x, z;
    private float L2;

    private CharacterController charaCon;//�L�����N�^�[�R���g���[���[
    private Vector3 pos = Vector3.zero; //���W

    private Animator animator;//�A�j���[�V����

    private Vector3 currentRot = Vector3.zero;//���݂̉�]����

    private float rotSpeed = 3.0f;

    private Quaternion charaRot;  //�L�����N�^�[�̊p�x

    //�J�����ړ��̃X�s�[�h
    private float cameraSpeed = 100.0f;
    //�J�������xUI
    [SerializeField]
    private GameObject pauseSoundUI;

    //�@�L��������]�����ǂ����H
    private bool charaRotFlag;
    //�@�J�����̊p�x
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //�@�J�����̊p�x�̏����l
    private Quaternion initCameraRot;

    //�@�L�����N�^�[���_�̃J�����ŉ�]�o������x
    [SerializeField]
    private float cameraRotateLimit = 30f;

    //�@�L�����N�^�[���_�̃J����
    private Transform myCamera;


    float defaultFov;
    float zoom = 2.0f;
    float waitTime = 0.5f;

    //�{�[���擾
    [SerializeField]
    private Transform spine;

    public postEffect post;

    const float min = -17.0f;
    const float max = 24.0f;
    private float spineZ;


    private PlayableDirector director;//�I�[�v�j���O�p
    bool opend;//�I�[�v�j���O���I��������ǂ���
    bool start;//���W�ς���悤

    Rigidbody rb;

    //�ǉ�
    public bool Damage = true;

    float opskipcount;//�v���C���[�̈ʒu������Ȃ��悤�ɂ��邽�߂̃J�E���g

    public BossEnemy bossEnemy;
    public bool bossevent = false;//�v���C���[���{�X�����̒��ɓ�������
    public bool bosseventend = false;//�{�X�̓o�ꉉ�o�����̃Q�[���ŏI����Ă��邩

    float bossZoom = 3.0f;//�{�X�o�ꎞ�̃Y�[��
    float bossWaitTime = 1.5f;//�{�X�o�ꎞ�̃Y�[���ɂ����鎞��

    Vector3 CameraTarget;
    EventManager eventManager;
    public ElevatorScript elevator;
    public ElevatorScript downElevator;
    bool isBossRoomEnter = false;
    bool isEnding = false;//�G���f�B���O�����ǂ���
    public GameObject BossClearDoor;
    public GameObject BossClear;
    public Transform goalTarget;
    public GameObject Bip001;//�e�̃I�u�W�F�N�g
    public GameObject cameratargt;
    bool endcamera = false;
    bool bossend = false;
    public EventText eventText;

    private AudioSource source;
    [SerializeField]
    private AudioClip damageSE;

    public int healCount;

    public bool hit=false;

    // Start is called before the first frame update
    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        charaRot = transform.localRotation;
        myCamera = GetComponentInChildren<Camera>().transform;	//�@�L�����N�^�[���_�̃J�����̎擾
        initCameraRot = myCamera.localRotation;
        cameraRot = myCamera.localRotation;

        defaultFov = GetComponentInChildren<Camera>().fieldOfView;

        charaRotFlag = false;

        //post = GetComponent<postEffect>();
        rb = GetComponent<Rigidbody>();
        //speed = moveS;

        hp = Playerhp;
        healCount = 2;

        director = GetComponent<PlayableDirector>();
        start = false;
        opend = false;
        opskipcount = 0;
        bossevent = false;
        bosseventend = false;
        eventManager = this.GetComponent<EventManager>();
        isBossRoomEnter = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        hp = Mathf.Clamp(hp, 0, 100);
        post.vigparam = Mathf.Clamp(post.vigparam, 0, 0.61f);
        if(endcamera)
        {
            CameraTarget = cameratargt.transform.position;
            myCamera.LookAt(CameraTarget);
            return;
        }

        //�{�X�o�ꉉ�o��
        if (bossevent == true && bosseventend == false)
        {
            CameraTarget = bossEnemy.transform.position;
            CameraTarget.y = -48;//y���W�����Œ肷��
            myCamera.LookAt(CameraTarget);
            //�Y�[����
            cameraSpeed = 50.0f;
            //moveS = 1.5f;
            speed = 7.5f;
            System.Console.WriteLine("L2");
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / bossZoom,
                bossWaitTime);
            StartCoroutine("BossEvent");//3�b�ԃJ�������{�X�̕�����
        }

        if(isEnding&&!bossend)
        {
            CameraTarget = BossClearDoor.transform.position;
            BossClear.SetActive(false);
            Bip001.SetActive(false);
            //*****�{�X�̓|��鉉�o*****//
            if (bossEnemy != null)
            {
                CameraTarget = bossEnemy.transform.position;
                CameraTarget.y = -50;//y���W�����Œ肷��
                myCamera.LookAt(CameraTarget);
            }
            //���̂ق�����������
            StartCoroutine("EndingEvent");
        }

        if ((!opend&&director !=null)||(bossevent&&!bosseventend)||isEnding == true)
        {//�I�[�v�j���O���A�{�X�C�x���g���A�G���f�B���O���͑���o���Ȃ��悤��
            return;
        }
        //�@�L�����N�^�[�̌�����ύX����
        RotateChara();
        //�@���_�̌�����ς���
        RotateCamera();

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


        if (charaCon.isGrounded)
        {
            pos = new Vector3(x, 0, z).normalized;
            pos = transform.TransformDirection(pos);
            pos *= speed;
           

            animator.SetFloat("Forward", Input.GetAxis("Vertical"));
            animator.SetFloat("Lateral", Input.GetAxis("Horizontal"));


            if (Input.GetButton("Jump"))
            {
                pos.y = jump;
            }
        }

        pos.y -= g * Time.deltaTime;
        charaCon.Move(pos * Time.deltaTime);

        //�Y�[��
        if (Input.GetAxis("joystick L2") > 0)
        {
            //�Y�[����
            cameraSpeed = 50.0f;
            //moveS = 1.5f;
            speed = 7.5f;
            System.Console.WriteLine("L2");
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / zoom,
                waitTime);
        }
        else
        {
            //�Y�[�����ĂȂ���
            cameraSpeed = pauseSoundUI.transform.GetChild(4).GetComponent<Slider>().value;
            //moveS = 2.0f;
            speed = 10.0f;
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / 1,
                waitTime);
        }

        if(opend&&!start)
        {
            opskipcount++;
            if (opskipcount > 5)
            {
                this.transform.position = new Vector3(11.5f, 0, 1);
                start = true;
            }
        }
        Debug.Log("hit" + hit);
        hit = false;

        Death();
    }

    IEnumerator DamageTimer(int time)
    {
        Debug.Log("�R���[�`��");
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            Damage = false;
            //mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(1);
            --time;
        }
        Damage = true;
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }



    private void OnTriggerEnter(Collider collider)
    {
        if (Damage && collider.gameObject.tag == "Attack")
        {
            StartCoroutine("DamageTimer", 1);
            hp = hp - 10;
            //�|�X�g�G�t�F�N�gVignette�̒l���Z
            post.vigparam += 0.061f;
            Debug.Log("Player@vigparam"+post.vigparam);
            source.PlayOneShot(damageSE);
            //Debug.Log("�v���C���[HP : " + hp);
        }

        if(collider.gameObject.tag == "BossEventHit")
        {
            bossevent = true;
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGM>().isBoss = true;
        }

        if(collider.gameObject.tag == "EdTimelineStart"&&isEnding)
        {//�G���f�B���O�̃X�^�[��
            endcamera = true;
            bossend = true;
            CameraTarget = cameratargt.transform.position;
            eventManager.EdStart();
            elevator.ElevatorUp();
        }

        if(collider.gameObject.tag == "BossRoomEnter"&&!isBossRoomEnter)
        {
            //�{�X�����ɍ~���^�C�����C�����Đ�
            eventManager.BossRoomEnter();
            downElevator.ElevatorDown();
            isBossRoomEnter = true;
        }

        if (collider.gameObject.tag == "Heal")
        {
             healCount+= 1;
        }

        if (collider.gameObject.tag == "GameClearFlag")
        {
            SceneManager.LoadScene("GameClear");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "hatudenkiHit")
        {
            hit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hatudenkiHit")
        {
            hit = false;
        }
    }

    void LateUpdate()
    {
        //�@�{�[�����J�����̊p�x����������
        RotateBone();
        //Debug.Log(spine.eulerAngles.y);  
    }

    /// <summary>
    /// ���̃{�[���̉�]
    /// </summary>
    void RotateBone()
    {
        spineZ = (spine.eulerAngles.z - myCamera.localEulerAngles.x);
        //spineZ = System.Math.Min(spineZ, 180);
        //spineZ = System.Math.Max(spineZ, -150);

        //spineZ = Mathf.Clamp(spine.eulerAngles.z - myCamera.localEulerAngles.x, min, max);
        //�@���̃{�[���̊p�x���J�����̌����ɂ���
        spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spineZ);
        //spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spine.eulerAngles.z + (-myCamera.localEulerAngles.x));
    }

    //�L�����N�^�[�̊p�x��ύX
    void RotateChara()
    {
        //���̉�]�l���v�Z
        float yRot = Input.GetAxis("Horizontal2") * cameraSpeed * Time.deltaTime;

        charaRot *= Quaternion.Euler(0f, yRot, 0f);

        //�L�����N�^�[����]���Ă��邩�ǂ����H

        if (yRot != 0f)
        {
            charaRotFlag = true;
        }
        else
        {
            charaRotFlag = false;
        }

        //�@�L�����N�^�[�̉�]�����s
        transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRot, rotSpeed);
    }

    //�@�J�����̊p�x��ύX
    void RotateCamera()
    {
        //�c�̉�]�l
        float xRotate = Input.GetAxis("Vertical2") * cameraSpeed * Time.deltaTime;

        //�@�}�E�X����Ɉړ��������ɏ�������������Ȃ甽�Ε����Ɋp�x�𔽓]������
        if (cameraRotForward)
        {
            xRotate *= -1;
        }
        //�@��U�p�x���v�Z����	
        cameraRot *= Quaternion.Euler(xRotate, 0f, 0f);
        //�@�J������X���̊p�x�����E�p�x�𒴂�������E�p�x�ɐݒ�
        var resultYRot = Mathf.Clamp(Mathf.DeltaAngle(initCameraRot.eulerAngles.x, cameraRot.eulerAngles.x), -cameraRotateLimit, cameraRotateLimit);
        //�@�p�x���č\�z
        cameraRot = Quaternion.Euler(resultYRot, cameraRot.eulerAngles.y, cameraRot.eulerAngles.z);
        //�@�J�����̎��_�ύX�����s
        myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRot, rotSpeed);
    }

    void Death()
    {
        if(hp <= 0 )
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void OpEnd()
    {
        opend = true;
        if(director != null)
        director.Stop();

    }

    IEnumerator BossEvent()
    {//�{�X�C�x���g�̎��Ԃ�������s�\��
        yield return new WaitForSeconds(3.0f);
        bossevent = false;
        bosseventend = true;

    }

    IEnumerator EndingEvent()
    {//�G���f�B���O
        yield return new WaitForSeconds(3.0f);
        //�G���f�B���O�p�̃e�L�X�g
        eventText.SpecifiedTextNumber();
        //�h�A�̕�������������
        myCamera.LookAt(CameraTarget);

        //���Ɍ������ĕ���
        yield return new WaitForSeconds(3.0f);
        transform.position = Vector3.MoveTowards(transform.position, goalTarget.position, 0.15f);
    }


    public void Endling()
    {//�G���f�B���O
        isEnding = true;
    }


}


