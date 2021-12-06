using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventText : MonoBehaviour
{
    public string[] texts;//Unity��œ��͂���string�̔z��
    int textNumber;//���Ԗڂ�texts[]��\�������邩
    string displayText;//�\��������string
    int textCharNumber;//�������ڂ�displayText�ɒǉ����邩
    int displayTextSpeed; //�S�̂̃t���[�����[�g�𗎂Ƃ��ϐ�
    bool textStop; //�e�L�X�g�\�����n�߂邩
    float timer;//
    public float second = 3;//�e�L�X�g�\�������b�҂�
    bool isTimer;//�^�C�}�[�̃X�^�[�g
    public Image texthaikei;//�e�L�X�g�̌��̓��߃C���[�W
    bool eventStart;//�e�L�X�g�̕\�����n�߂邩�ǂ���
    public int textKind = -1;//�C���X�^���X���Ƀe�L�X�g��������(�C���X�y�N�^�[�Őݒ�)
    private int specifiedText = 0;//���s���Ɏw�肳���e�L�X�g

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
        //�Ńo�b�N�p ��ŏ���
        if(Input.GetKey(KeyCode.G))
        {
            eventStart = true;
        }
    }

    void FixedUpdate()
    {
        if (eventStart == false)
        {//�C�x���g���n�܂��Ă��Ȃ��Ȃ牽�����Ȃ�
            return;
        }

        if(textKind != specifiedText)
        {//���̃e�L�X�g�����ϐ��Ǝw�肳�ꂽ�ԍ����Ⴄ�Ȃ牽�����Ȃ�
            return;
        }
        if (isTimer)
        {
            timer += Time.deltaTime;
        }

        if (textStop == false) //�e�L�X�g��\��������if��
        {
            texthaikei.enabled = true;
            displayTextSpeed++;
            if (displayTextSpeed % 5 == 0)//5��Ɉ��v���O���������s����if��
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
                        //�e�L�X�g���\������Ă���second�҂�����
                        if (timer > second)
                        {
                            displayText = ""; //�\�������镶���������
                            textCharNumber = 0; //�����̔ԍ����ŏ��ɂ���
                            textStop = true; //�Z���t�\�����~�߂�
                            isTimer = false;
                            timer = 0;
                            texthaikei.enabled = false;
                            eventStart = false;//�e�L�X�g�̏I��
                        }
                    }
                }


                this.GetComponent<Text>().text = displayText;//��ʏ��displayText��\��
            }
        }
    }

    //���̃X�N���v�g���琔���łǂ̃e�L�X�g��\�����邩�Ăяo���֐�
    public void SpecifiedTextNumber(int i)
    {
        this.specifiedText = i;
    }
}
