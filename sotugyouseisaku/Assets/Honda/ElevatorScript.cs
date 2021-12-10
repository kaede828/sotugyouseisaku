using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ElevatorScript : MonoBehaviour
{
    //�G���x�[�^�[�̃^�C�����C�����Z�b�g
    [SerializeField] private TimelineAsset timeline;
    private PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        director = this.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //�G���x�[�^�[����ɂ�����^�C�����C�����Đ�
    public void ElevatorUp()
    {
        director.Play(timeline);
    }
}
