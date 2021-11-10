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
    bool push;//テキストの最後にボタンが押されたか
    bool textend;//テキストが最後まで再生されたか
    public Image setumeisyo;//説明テキスト
    bool Ybutton;
    bool PreYbutton;//1フレーム前の入力
    public Text ytext;//右上のテキスト

    void Start()
    {
        textNumber = 0;
        textCharNumber = 0;
        displayTextSpeed = 0;
        textStop = false;
        isTimer = false;
        setumeisyo.enabled = false;
        Ybutton = false;
        ytext.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown("joystick button 3"))
        {
            Ybutton = true;
        }

        if (textend == true && Ybutton)//bボタンが押されたら
        {
            push = true; //押された判定にする
        }
        if (textend == true)
        {
            if (setumeisyo.enabled == true && Ybutton&&!PreYbutton)
            {//説明が表示されていたら非表示にする
                setumeisyo.enabled = false;
            }
            else if (setumeisyo.enabled == false && Ybutton&&!PreYbutton)
            {//説明が非表示なら表示する
                setumeisyo.enabled = true;
            }
        }

        PreYbutton = Ybutton;//1フレーム前の入力
        Ybutton = false;
    }
    void FixedUpdate()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if (textStop == false) //テキストを表示させるif文
        {
            displayTextSpeed++;
            if (displayTextSpeed % 3 == 0)//3回に一回プログラムを実行するif文
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
                        textend = true;
                        //Bボタンが押されたら
                        if (push)
                        {
                            displayText = ""; //表示させる文字列も消す
                            textCharNumber = 0; //文字の番号を最初にする
                            textStop = true; //セリフ表示を止める
                            isTimer = false;
                            timer = 0;
                            texthaikei.enabled = false;
                            ytext.enabled = true;
                        }
                    }
                }

                this.GetComponent<Text>().text = displayText;//画面上にdisplayTextを表示
                //click = false;//クリックされた判定を解除
            }


        }



    }
}
