using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class hatudenkiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject hatudenkiPre;
    public List<GameObject> hatudenkiList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> hatudenkiHitList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> spawnPosList = new List<GameObject>();
    public List<string> spawnPosName = new List<string>();
    List<int> nums = new List<int>();
    int start = 0;
    [SerializeField]
    int spowcount = 1;
    int hatudenkiCount = 0;
    [SerializeField]
    private GameObject eventCamera;
    [SerializeField]
    private GameObject eventCamera2;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject door2;
    [SerializeField]
    private GameObject door3;
    [SerializeField]
    private FenceUpDown fence;
    [SerializeField]
    private FenceUpDown fence2;

    //Camera camera;
    //Camera camera2;
    GameObject canvasObject;
    GameObject sliderObject;
    bool cam = false;
    int ransu;
    int aCou = 0;
    public EventText eventText;//�ǉ��@���d�@�������Ƀe�L�X�g�\���p
    public EventText eventText2;//�ǉ��@���d�@�������Ƀe�L�X�g�\���p

    public enum Floor//���񂩂ŏ����𕪂��邽��
    {
        _1Floor,
        _2Floor,
    }
    //�C���X�y�N�^�[�ŕύX
    [SerializeField]
    Floor floor = Floor._1Floor;

    void Start()
    {
        if (eventCamera != null)
        {
            eventCamera.SetActive(false);
        }
        if (eventCamera2 != null)
        {
            eventCamera2.SetActive(false);
        }
        for (int i = start; i < spawnPosList.Count; i++)
        {
            //Debug.Log("a");
            nums.Add(i);
        }
        while (spowcount-- > 0)
        {

            int index = Random.Range(0, nums.Count);

            ransu = nums[index];
            //Debug.Log("���d�@���������ꂽ�ꏊ"+ spawnPosList[ransu].name);
            Vector3 pos = spawnPosList[ransu].transform.position;
            pos.y += 0.5f;
            Quaternion qua = spawnPosList[ransu].transform.rotation;
            Instantiate(hatudenkiPre, pos, qua);
            nums.RemoveAt(index);
        }

        for (int i = 0; i < nums.Count; i++)
        {
            spawnPosName.Add(spawnPosList[nums[i]].name);
            //Debug.Log("���d�@����������Ȃ������ꏊ"+ spawnPosList[nums[i]].name);
        }
        //���d�@��List�ɂ���
        if (floor == Floor._1Floor)
        {
            GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("hatudenki");
            for (int i = 0; i < gameObjects1.Length; i++)
            {
                hatudenkiList.Add(gameObjects1[i]);
            }
            hatudenkiCount = hatudenkiList.Count;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("hatudenkiHit");
            for (int i = 0; i < hatudenkiList.Count; i++)
            {
                hatudenkiHitList.Add(gameObjects[i]);
            }
        }
        if (floor == Floor._2Floor)
        {
            GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("2Fhatudenki");
            for (int i = 0; i < gameObjects1.Length; i++)
            {
                hatudenkiList.Add(gameObjects1[i]);
            }
            hatudenkiCount = hatudenkiList.Count;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("2FhatudenkiHit");
            for (int i = 0; i < hatudenkiList.Count; i++)
            {
                hatudenkiHitList.Add(gameObjects[i]);
            }
        }
        cam = true;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hatudenkiHitList.Count; i++)
        {
            if (hatudenkiHitList[i] != null && hatudenkiHitList.Count > 0)
            {
                //Debug.Log(hatudenkiHitList[i].GetComponent<hatudenkiHP>().Hp);
                if (hatudenkiHitList[i].GetComponent<hatudenkiHP>().Hp >= 99)
                {
                    hatudenkiHitList.Remove(hatudenkiHitList[i]);
                    hatudenkiCount = hatudenkiHitList.Count;
                    //Debug.Log("��ꂽ���d�@�̐�:" + hatudenkiCount);
                }
            }
        }

        switch (floor)
        {
            case Floor._1Floor:
                Floor1Event();
                break;
            case Floor._2Floor:
                Floor2Event();
                break;
        }
    }

    private IEnumerator DelayCoroutine(int Count, Action action)
    {
        for (var i = 0; i < Count; i++)
        {
            yield return null;
        }

        action?.Invoke();
    }

    //1F�̏���
    private void Floor1Event()
    {
        //1�K�̔��d�@����t������
        if (hatudenkiHitList.Count == 3)
        {
            eventCamera.SetActive(true);
            StartCoroutine(DelayCoroutine(60, () =>
            {//����J����
                //door.SetActive(false);
                fence.DownFence();
            }));
            StartCoroutine(DelayCoroutine(180, () =>
            {
                eventCamera.SetActive(false);
                //�ǉ��@�����J������̐����e�L�X�g
                eventText.SpecifiedTextNumber();//���߂̔��d�@���������Ƀe�L�X�g
            }));
        }
        //1�K�̔��d�@�����ׂĕt������
        if (hatudenkiHitList.Count <= 0 && cam)
        {
            eventCamera2.SetActive(true);
            //camera2.depth = 1;
            StartCoroutine(DelayCoroutine(60, () =>
            {//�S�i�q������
                fence2.DownFence();
                //door2.SetActive(false);
                door3.SetActive(false);
            }));
            StartCoroutine(DelayCoroutine(180, () =>
            {
                eventCamera2.SetActive(false);
                //camera2.depth = -1;
                //�ǉ��@�����J������̐����e�L�X�g
                eventText2.SpecifiedTextNumber();//1�K�̔��d�@��S�Ă������Ƀe�L�X�g
            }));
            cam = false;
        }
    }

    //2F�̏���
    private void Floor2Event()
    {
        if (hatudenkiHitList.Count <= 0 && cam)
        {
            eventCamera.SetActive(true);
            //camera.depth = 1;
            StartCoroutine(DelayCoroutine(60, () =>
            {
                door.SetActive(false);
                door2.SetActive(false);
                door3.SetActive(false);
            }));
            StartCoroutine(DelayCoroutine(180, () =>
            {
                eventCamera.SetActive(false);
                //camera.depth = -1;
                //�ǉ��@�����J������̐����e�L�X�g
                eventText2.SpecifiedTextNumber();//���d�@�����ׂĂ������̃e�L�X�g
            }));
            cam = false;
        }

    }
}
