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
    private float speed = 10.0f; //速度
    [SerializeField]
    private float jump = 2.0f; //ジャンプ
    public float g = 9.8f; //重力
    //[SerializeField]
    //private float moveS = 2.0f;
    private float x, z;
    private float L2;

    private CharacterController charaCon;//キャラクターコントローラー
    private Vector3 pos = Vector3.zero; //座標

    private Animator animator;//アニメーション

    private Vector3 currentRot = Vector3.zero;//現在の回転方向

    private float rotSpeed = 3.0f;

    private Quaternion charaRot;  //キャラクターの角度

    //カメラ移動のスピード
    private float cameraSpeed = 100.0f;
    //カメラ感度UI
    [SerializeField]
    private GameObject pauseSoundUI;

    //　キャラが回転中かどうか？
    private bool charaRotFlag;
    //　カメラの角度
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //　カメラの角度の初期値
    private Quaternion initCameraRot;

    //　キャラクター視点のカメラで回転出来る限度
    [SerializeField]
    private float cameraRotateLimit = 30f;

    //　キャラクター視点のカメラ
    private Transform myCamera;


    float defaultFov;
    float zoom = 2.0f;
    float waitTime = 0.5f;

    //ボーン取得
    [SerializeField]
    private Transform spine;

    public postEffect post;

    const float min = -17.0f;
    const float max = 24.0f;
    private float spineZ;


    private PlayableDirector director;//オープニング用
    bool opend;//オープニングが終わったかどうか
    bool start;//座標変えるよう

    Rigidbody rb;

    //追加
    public bool Damage = true;

    float opskipcount;//プレイヤーの位置をずれないようにするためのカウント

    public BossEnemy bossEnemy;
    public bool bossevent = false;//プレイヤーがボス部屋の中に入ったか
    public bool bosseventend = false;//ボスの登場演出がこのゲームで終わっているか

    float bossZoom = 3.0f;//ボス登場時のズーム
    float bossWaitTime = 1.5f;//ボス登場時のズームにかける時間

    Vector3 CameraTarget;
    EventManager eventManager;
    public ElevatorScript elevator;
    public ElevatorScript downElevator;
    bool isBossRoomEnter = false;
    bool isEnding = false;//エンディング中かどうか
    public GameObject BossClearDoor;
    public GameObject BossClear;
    public Transform goalTarget;
    public GameObject Bip001;//銃のオブジェクト
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
        myCamera = GetComponentInChildren<Camera>().transform;	//　キャラクター視点のカメラの取得
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

        //ボス登場演出中
        if (bossevent == true && bosseventend == false)
        {
            CameraTarget = bossEnemy.transform.position;
            CameraTarget.y = -48;//y座標だけ固定する
            myCamera.LookAt(CameraTarget);
            //ズーム中
            cameraSpeed = 50.0f;
            //moveS = 1.5f;
            speed = 7.5f;
            System.Console.WriteLine("L2");
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / bossZoom,
                bossWaitTime);
            StartCoroutine("BossEvent");//3秒間カメラをボスの方向へ
        }

        if(isEnding&&!bossend)
        {
            CameraTarget = BossClearDoor.transform.position;
            BossClear.SetActive(false);
            Bip001.SetActive(false);
            //*****ボスの倒れる演出*****//
            if (bossEnemy != null)
            {
                CameraTarget = bossEnemy.transform.position;
                CameraTarget.y = -50;//y座標だけ固定する
                myCamera.LookAt(CameraTarget);
            }
            //扉のほうを向かせる
            StartCoroutine("EndingEvent");
        }

        if ((!opend&&director !=null)||(bossevent&&!bosseventend)||isEnding == true)
        {//オープニング中、ボスイベント中、エンディング中は操作出来ないように
            return;
        }
        //　キャラクターの向きを変更する
        RotateChara();
        //　視点の向きを変える
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

        //ズーム
        if (Input.GetAxis("joystick L2") > 0)
        {
            //ズーム中
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
            //ズームしてない時
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
        Debug.Log("コルーチン");
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
            //ポストエフェクトVignetteの値加算
            post.vigparam += 0.061f;
            Debug.Log("Player@vigparam"+post.vigparam);
            source.PlayOneShot(damageSE);
            //Debug.Log("プレイヤーHP : " + hp);
        }

        if(collider.gameObject.tag == "BossEventHit")
        {
            bossevent = true;
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGM>().isBoss = true;
        }

        if(collider.gameObject.tag == "EdTimelineStart"&&isEnding)
        {//エンディングのスターと
            endcamera = true;
            bossend = true;
            CameraTarget = cameratargt.transform.position;
            eventManager.EdStart();
            elevator.ElevatorUp();
        }

        if(collider.gameObject.tag == "BossRoomEnter"&&!isBossRoomEnter)
        {
            //ボス部屋に降りるタイムラインを再生
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
        //　ボーンをカメラの角度を向かせる
        RotateBone();
        //Debug.Log(spine.eulerAngles.y);  
    }

    /// <summary>
    /// 腰のボーンの回転
    /// </summary>
    void RotateBone()
    {
        spineZ = (spine.eulerAngles.z - myCamera.localEulerAngles.x);
        //spineZ = System.Math.Min(spineZ, 180);
        //spineZ = System.Math.Max(spineZ, -150);

        //spineZ = Mathf.Clamp(spine.eulerAngles.z - myCamera.localEulerAngles.x, min, max);
        //　腰のボーンの角度をカメラの向きにする
        spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spineZ);
        //spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spine.eulerAngles.z + (-myCamera.localEulerAngles.x));
    }

    //キャラクターの角度を変更
    void RotateChara()
    {
        //横の回転値を計算
        float yRot = Input.GetAxis("Horizontal2") * cameraSpeed * Time.deltaTime;

        charaRot *= Quaternion.Euler(0f, yRot, 0f);

        //キャラクターが回転しているかどうか？

        if (yRot != 0f)
        {
            charaRotFlag = true;
        }
        else
        {
            charaRotFlag = false;
        }

        //　キャラクターの回転を実行
        transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRot, rotSpeed);
    }

    //　カメラの角度を変更
    void RotateCamera()
    {
        //縦の回転値
        float xRotate = Input.GetAxis("Vertical2") * cameraSpeed * Time.deltaTime;

        //　マウスを上に移動した時に上を向かせたいなら反対方向に角度を反転させる
        if (cameraRotForward)
        {
            xRotate *= -1;
        }
        //　一旦角度を計算する	
        cameraRot *= Quaternion.Euler(xRotate, 0f, 0f);
        //　カメラのX軸の角度が限界角度を超えたら限界角度に設定
        var resultYRot = Mathf.Clamp(Mathf.DeltaAngle(initCameraRot.eulerAngles.x, cameraRot.eulerAngles.x), -cameraRotateLimit, cameraRotateLimit);
        //　角度を再構築
        cameraRot = Quaternion.Euler(resultYRot, cameraRot.eulerAngles.y, cameraRot.eulerAngles.z);
        //　カメラの視点変更を実行
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
    {//ボスイベントの時間だけ操作不能に
        yield return new WaitForSeconds(3.0f);
        bossevent = false;
        bosseventend = true;

    }

    IEnumerator EndingEvent()
    {//エンディング
        yield return new WaitForSeconds(3.0f);
        //エンディング用のテキスト
        eventText.SpecifiedTextNumber();
        //ドアの方向を向かせる
        myCamera.LookAt(CameraTarget);

        //扉に向かって歩く
        yield return new WaitForSeconds(3.0f);
        transform.position = Vector3.MoveTowards(transform.position, goalTarget.position, 0.15f);
    }


    public void Endling()
    {//エンディング
        isEnding = true;
    }


}


