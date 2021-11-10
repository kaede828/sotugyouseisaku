using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextDisplay : MonoBehaviour
{
    public string[] texts;//Unity��œ��͂���string�̔z��
    int textNumber;//���Ԗڂ�texts[]��\�������邩
    string displayText;//�\��������string
    int textCharNumber;//�������ڂ�displayText�ɒǉ����邩
    int displayTextSpeed; //�S�̂̃t���[�����[�g�𗎂Ƃ��ϐ�
    bool textStop; //�e�L�X�g�\�����n�߂邩
    float timer;//
    public float second = 1;//�e�L�X�g�\�������b�҂�
    bool isTimer;//�^�C�}�[�̃X�^�[�g
    public Image texthaikei;//�e�L�X�g�̌��̓��߃C���[�W
    bool push;//�e�L�X�g�̍Ō�Ƀ{�^���������ꂽ��
    bool textend;//�e�L�X�g���Ō�܂ōĐ����ꂽ��
    public Image setumeisyo;//�����e�L�X�g
    bool Ybutton;
    bool PreYbutton;//1�t���[���O�̓���
    public Text ytext;//�E��̃e�L�X�g

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

        if (textend == true && Ybutton)//b�{�^���������ꂽ��
        {
            push = true; //�����ꂽ����ɂ���
        }
        if (textend == true)
        {
            if (setumeisyo.enabled == true && Ybutton&&!PreYbutton)
            {//�������\������Ă������\���ɂ���
                setumeisyo.enabled = false;
            }
            else if (setumeisyo.enabled == false && Ybutton&&!PreYbutton)
            {//��������\���Ȃ�\������
                setumeisyo.enabled = true;
            }
        }

        PreYbutton = Ybutton;//1�t���[���O�̓���
        Ybutton = false;
    }
    void FixedUpdate()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if (textStop == false) //�e�L�X�g��\��������if��
        {
            displayTextSpeed++;
            if (displayTextSpeed % 3 == 0)//3��Ɉ��v���O���������s����if��
            {

                if (textCharNumber != texts[textNumber].Length)//����text[textNumber]�̕�����̕������Ō�̕�������Ȃ����
                {
                    displayText = displayText + texts[textNumber][textCharNumber];//displayText�ɕ�����ǉ����Ă���
                    textCharNumber = textCharNumber + 1;//���̕����ɂ���
                }
                else//����text[textNumber]�̕�����̕������Ō�̕�����������
                {
                    isTimer = true;
                    if (textNumber != texts.Length - 1)//����texts[]���Ō�̃Z���t����Ȃ��Ƃ���
                    {                   
                        //�e�L�X�g���\������Ă���second�҂�����
                        if (timer > second)
                        {
                            displayText = "";//�\�������镶���������
                            textCharNumber = 0;//�����̔ԍ����ŏ��ɂ���
                            textNumber = textNumber + 1;//���̃Z���t�ɂ���
                            isTimer = false;
                            timer = 0;
                        }
                    }
                    else //����texts[]���Ō�̃Z���t�ɂȂ�����
                    {
                        textend = true;
                        //B�{�^���������ꂽ��
                        if (push)
                        {
                            displayText = ""; //�\�������镶���������
                            textCharNumber = 0; //�����̔ԍ����ŏ��ɂ���
                            textStop = true; //�Z���t�\�����~�߂�
                            isTimer = false;
                            timer = 0;
                            texthaikei.enabled = false;
                            ytext.enabled = true;
                        }
                    }
                }

                this.GetComponent<Text>().text = displayText;//��ʏ��displayText��\��
                //click = false;//�N���b�N���ꂽ���������
            }


        }



    }
}
