using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ElevatorScript : MonoBehaviour
{
    //エレベーターのタイムラインをセット
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

    //エレベーターを上にあげるタイムラインを再生
    public void ElevatorUp()
    {
        director.Play(timeline);
    }
}
