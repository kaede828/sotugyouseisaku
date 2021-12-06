using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventText : MonoBehaviour
{
    public string[] texts;//Unity上で入力するstringの配列
    int textNumber;//何番目のtexts[]を表示させるか
    string displayText;//表示させるstring
    int textCharNumber;//何文字目をdisplayTextに追加するか
    int displayTextSpeed; //全体のフレームレートを落とす変数
    bool textStop; //テキスト表示を始めるか
    float timer;//
    public float second = 3;//テキスト表示を何秒待つか
    bool isTimer;//タイマーのスタート
    public Image texthaikei;//テキストの後ろの透過イメージ
    bool eventStart;//テキストの表示を始めるかどうか
    public int textKind = -1;//インスタンス毎にテキストが持つ数字(インスペクターで設定)
    private int specifiedText = 0;//実行時に指定されるテキスト

    void Start()
    {
        textNumber = 0;
        textCharNumber = 0;
        displayTextSpeed = 0;
        textStop = false;
        isTimer = false;
        eventStart = false;
        texthaikei.enabled = false;
    }

    private void Update()
    {
        //でバック用 後で消す
        if(Input.GetKey(KeyCode.G))
        {
            eventStart = true;
        }
    }

    void FixedUpdate()
    {
        if (eventStart == false)
        {//イベントが始まっていないなら何もしない
            return;
        }

        if(textKind != specifiedText)
        {//このテキストがもつ変数と指定された番号が違うなら何もしない
            return;
        }
        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if (textStop == false) //テキストを表示させるif文
        {
            texthaikei.enabled = true;
            displayTextSpeed++;
            if (displayTextSpeed % 5 == 0)//5回に一回プログラムを実行するif文
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
                            eventStart = false;//テキストの終了
                        }
                    }
                }


                this.GetComponent<Text>().text = displayText;//画面上にdisplayTextを表示
            }
        }
    }

    //他のスクリプトから数字でどのテキストを表示するか呼び出す関数
    public void SpecifiedTextNumber(int i)
    {
        this.specifiedText = i;
    }
}
