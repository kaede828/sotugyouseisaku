using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]

public class EventManager : MonoBehaviour
{
    //ED�̃^�C�����C�����Z�b�g
    [SerializeField] private TimelineAsset edtimeline;
    private PlayableDirector director;//PlayableDirector�^�̕ϐ�director��錾

    // Start is called before the first frame update
    void Start()
    {
        //�����I�u�W�F�N�g�ɕt���Ă���PlayableDirector�R���|�[�l���g���擾
        director = this.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EdStart()
    {//�ݒ肳�ꂽ�^�C�����C�����Đ�������
           director.Play(edtimeline);
    }
}
