using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    Text text;
    Vector3 oldPos;
    Vector3 newPos;
    List<GameObject> gameobjectList = new List<GameObject>();
    List<Image> imageList = new List<Image>();
    Image ikkaiimage;
    Image nikaiimage;
    bool mapflag = false;
    public bool ikkaiimageflag = true;
    public bool nikaiimageflag = false;
    GameObject Optext;
    int value;
    TextDisplay Textdisplay;
    Player p;

    void Start()
    {
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

        if (value >= 100)
        {
            Vector3 trans = this.gameObject.GetComponent<RectTransform>().anchoredPosition3D;
            //Debug.Log(trans);
            Instantiate(batumark, new Vector3(0,0,0),Quaternion.identity, transform.parent.gameObject.transform);
            value = 0;
        }

        if(p.hit==true)
        {
            if (Input.GetButton("joystick B"))
            {
                //Debug.Log("a");
                value += 1;
            }
        }
        else
        {
            Debug.Log("”­“d‹@‚É“–‚½‚Á‚Ä‚È‚¢");
        }
    }
}