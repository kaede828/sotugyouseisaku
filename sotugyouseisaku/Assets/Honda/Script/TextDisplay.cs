using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextDisplay : MonoBehaviour
{
    public string[] texts;//Unity上で入力するstringの配列
    int textNumber;//何番目のtexts[]を表示させるか
    string displayText;//表示させるstring
    int textCharNumber;//何文字目をdisplayTextに追加するか
    int displayTextSpeed; //全体のフレームレートを落とす変数
    bool textStop; //テキスト表示を始めるか
    float timer;//
    public float second = 1;//テキスト表示を何秒待つか
    bool isTimer;//タイマーのスタート
    public Image texthaikei;//テキストの後ろの透過イメージ
    public Text opskiptext;
    [SerializeField]float count;
    public bool opend;//オープニングが終わったかどうか
    public Player player;
    public Transform door;

    void Start()
    {
        textNumber = 0;
        textCharNumber = 0;
        displayTextSpeed = 0;
        textStop = false;
        isTimer = false;
        opskiptext.enabled = true;
        count = 0;
    }

    private void Update()
    {
        if(opend)
        {
            return;
        }
        if (Input.GetKey("joystick button 1"))
        {//B長押しでスキップ
            count++;
        }
        else
        {
            count = 0;
        }

        if (count >= 60)
        {
            this.GetComponent<Text>().text = "";//テキストを消す
            displayText = ""; //表示させる文字列も消す
            textCharNumber = 0; //文字の番号を最初にする
            textStop = true; //セリフ表示を止める
            isTimer = false;
            timer = 0;
            texthaikei.enabled = false;
            opend = true;
            opskiptext.enabled = false;
            player.OpEnd();
            door.transform.eulerAngles = new Vector3(180, 360, 0);
        }
    }

    void FixedUpdate()
    {
        if(opend)
        {//opが終わっていたら何もしない
            return;
        }

        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if (textStop == false) //テキストを表示させるif文
        {
            displayTextSpeed++;
            if (displayTextSpeed % 2 == 0)//2回に一回プログラムを実行するif文
            {

                if (textCharNumber != texts[textNumber].Length)//もしtext[textNumber]の文字列の文字が最後の文字じゃなければ
                {
                    displayText = displayText + texts[textNumber][textCharNumber];//displayTextに文字を追加していく
                    textCharNumber = textCharNumber + 1;//次の文字にする
                }
                else//もしtext[textNumber]の文字列の文字が最後の文字だったら
                {
                    isTimer = true;
                    if (textNumber != texts.Length - 1)//もしtexts[]が最後のセリフじゃないときは
                    {                   
                        //テキストが表示されてからsecond待ったら
                        if (timer > second)
                        {
                            displayText = "";//表示させる文字列を消す
                            textCharNumber = 0;//文字の番号を最初にする
                            textNumber = textNumber + 1;//次のセリフにする
                            isTimer = false;
                            timer = 0;
                        }
                    }
                    else //もしtexts[]が最後のセリフになったら
                    {
                        //テキストが表示されてからsecond待ったら
                        if (timer > second)
                        {
                            displayText = ""; //表示させる文字列も消す
                            textCharNumber = 0; //文字の番号を最初にする
                            textStop = true; //セリフ表示を止める
                            isTimer = false;
                            timer = 0;
                            texthaikei.enabled = false;
                            opend = true;
                            opskiptext.enabled = false;
                            player.OpEnd();//プレイヤーを操作可能に
                        }
                    }
                }


                this.GetComponent<Text>().text = displayText;//画面上にdisplayTextを表示
            }


        }



    }
}
