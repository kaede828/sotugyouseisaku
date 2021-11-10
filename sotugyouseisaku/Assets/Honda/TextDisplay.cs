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

    void Start()
    {

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
            if (displayTextSpeed % 3 == 0)//10��Ɉ��v���O���������s����if��
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
                        
                        //�e�L�X�g���\������Ă��牽�b�҂�����
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
                        if (timer > second)
                        {
                            displayText = ""; //�\�������镶���������
                            textCharNumber = 0; //�����̔ԍ����ŏ��ɂ���
                            textStop = true; //�Z���t�\�����~�߂�
                            isTimer = false;
                            timer = 0;
                        }
                    }
                }

                this.GetComponent<Text>().text = displayText;//��ʏ��displayText��\��
                //click = false;//�N���b�N���ꂽ���������
            }
            //if (Input.GetMouseButton(0))//�}�E�X���N���b�N������
            //{
            //    click = true; //�N���b�N���ꂽ����ɂ���
            //}
        }
    }
}
