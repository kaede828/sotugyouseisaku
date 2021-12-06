using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]

public class EventManager : MonoBehaviour
{
    //EDのタイムラインをセット
    [SerializeField] private TimelineAsset edtimeline;
    private PlayableDirector director;//PlayableDirector型の変数directorを宣言

    // Start is called before the first frame update
    void Start()
    {
        //同じオブジェクトに付いているPlayableDirectorコンポーネントを取得
        director = this.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EdStart()
    {//設定されたタイムラインを再生させる
           director.Play(edtimeline);
    }
}
