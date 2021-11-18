using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapPos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;
    GameObject textObject;
    Text text;
    Vector3 oldPos;
    Vector3 newPos;
    List<GameObject> gameobjectList = new List<GameObject>();
    List<Image> imageList = new List<Image>();
    bool mapflag=false;

    GameObject Optext;

    TextDisplay Textdisplay;

    void Start()
    {
        oldPos = player.GetComponent<Transform>().position;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(180,-275, 0);
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("minimap");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameobjectList.Add(gameObjects[i]);
            imageList.Add(gameobjectList[i].GetComponent<Image>());
        }
        textObject = GameObject.FindGameObjectWithTag("1Ftext");
        text = textObject.GetComponent<Text>();

        Optext= GameObject.FindGameObjectWithTag("OPtext");

        Textdisplay= Optext.GetComponent<TextDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Textdisplay.opend)
        {
            newPos = player.GetComponent<Transform>().position;
            Vector3 velocity;
            velocity = new Vector3(newPos.x, newPos.z, 0) - new Vector3(oldPos.x, oldPos.z, 0);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(velocity.x * 2.3f, velocity.y * 2.3f, 0);
            oldPos = player.GetComponent<Transform>().position;
        }

        if(Input.GetButtonUp("joystick Y")&& Textdisplay.opend)
        {
            if(mapflag)
            {
                mapflag = false;
            }
            else
            {
                mapflag = true;
            }
            
        }

        if(mapflag)
        {

            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].color= new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 1);
            }
            text.color= new Color(text.color.r, text.color.g, text.color.b, 1);
        }
        else
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].color = new Color(imageList[i].color.r, imageList[i].color.g, imageList[i].color.b, 0);
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }
}