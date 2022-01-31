using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Graphic))]
public class minimapPos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;
    GameObject textObject;
    GameObject ikkaigameObject;
    GameObject nikaigameObject;
    [SerializeField]
    GameObject batumark;
    [SerializeField]
    GameObject rootMap;
    Text text;
    Vector3 oldPos;
    Vector3 newPos;
    List<GameObject> gameobjectList = new List<GameObject>();
    List<Image> imageList = new List<Image>();
    Image ikkaiimage;
    Image nikaiimage;
    bool mapflag = false;
    bool batuflag;
    bool baturemoveflag=true;
    public bool ikkaiimageflag = true;
    public bool nikaiimageflag = false;
    GameObject Optext;
    int value;
    TextDisplay Textdisplay;
    Player p;

    int a;
    int b;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        oldPos = player.GetComponent<Transform>().position;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(177.5f, -275, 0);
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("minimap");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameobjectList.Add(gameObjects[i]);
            imageList.Add(gameobjectList[i].GetComponent<Image>());
        }
        textObject = GameObject.FindGameObjectWithTag("1Ftext");
        ikkaigameObject = GameObject.FindGameObjectWithTag("1Fimage");
        nikaigameObject = GameObject.FindGameObjectWithTag("2Fimage");

        ikkaiimage = ikkaigameObject.GetComponent<Image>();
        nikaiimage = nikaigameObject.GetComponent<Image>();
        text = textObject.GetComponent<Text>();

        Optext = GameObject.FindGameObjectWithTag("OPtext");

        Textdisplay = Optext.GetComponent<TextDisplay>();

        p= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Debug.Log("imageListの数" + imageList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Textdisplay.opend)
        {
            newPos = player.GetComponent<Transform>().position;
            Vector3 velocity;
            velocity = new Vector3(newPos.x, newPos.z, 0) - new Vector3(oldPos.x, oldPos.z, 0);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(velocity.x * 2.3f, velocity.y * 2.3f, 0);
            oldPos = player.GetComponent<Transform>().position;
        }
        //Debug.Log(mapflag);
        if (Input.GetButtonUp("joystick Y") && Textdisplay.opend)
        {
            if (mapflag)
            {
                mapflag = false;
                ikkaiimage.color = new Color(ikkaiimage.color.r, ikkaiimage.color.g, ikkaiimage.color.b, 0);
                nikaiimage.color = new Color(ikkaiimage.color.r, ikkaiimage.color.g, ikkaiimage.color.b, 0);
            }
            else
            {
                mapflag = true;

            }

        }
        if (mapflag)
        {

            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 1);
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

            if (ikkaiimageflag)
            {
                ikkaiimage.color = new Color(ikkaiimage.color.r, ikkaiimage.color.g, ikkaiimage.color.b, 1);
                nikaiimage.color = new Color(nikaiimage.color.r, nikaiimage.color.g, nikaiimage.color.b, 0);
                text.text = "1F";
            }

            if (nikaiimageflag)
            {
                if(baturemoveflag)
                {
                    //Debug.Log("imageListの数" + imageList.Count);
                    for (int i = 0; i < imageList.Count; i++)
                    {
                        //Debug.Log(i+"番目のオブジェクトの名前"+imageList[i].gameObject.name);
                        if (imageList[i].gameObject.tag == "batu")
                        {
                            b += 1;
                            Destroy(imageList[i]);
                            imageList.Remove(imageList[i]);
                        }
                    }
                    baturemoveflag = false;
                }
                

                ikkaiimage.color = new Color(ikkaiimage.color.r, ikkaiimage.color.g, ikkaiimage.color.b, 0);
                nikaiimage.color = new Color(nikaiimage.color.r, nikaiimage.color.g, nikaiimage.color.b, 1);
                text.text = "2F";
            }
        }
        else
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 0);
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        if (value >= 99)
        {
            //Debug.Log("生成しました");
            //現在のプレイヤーアイコンの位置
            Vector3 trans = this.gameObject.GetComponent<RectTransform>().anchoredPosition3D;  
            //Debug.Log(trans);
            // ローカル座標にインスタンス生成
            var instance = Instantiate(batumark, transform);
            imageList.Add(instance.GetComponent<Image>());
            //Debug.Log("imageListの数" + imageList.Count);
            instance.transform.parent = rootMap.transform;
            instance.GetComponent<RectTransform>().localPosition = trans;
            instance.SetActive(true);
            batuflag = true;
            
        }
        if (batuflag)
        {
            //Debug.Log("リセットしました");
            value = 0;
            batuflag = false;
            p.hit = false;
        }

        if(p.hit==true)
        {
            if (Input.GetButton("joystick B"))
            {
                //Debug.Log("value"+value);
                value += 1;
            }
            else
            {
                value = 0;
                //Debug.Log("value" + value);
            }
        }
        else
        {
            //Debug.Log("発電機に当たってない");
        }

        //Debug.Log("value" + value);
    }
}