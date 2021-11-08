using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NarrationBehaviour : PlayableBehaviour
{
    public TMP_Text mTextUI { get; set; }

    private string mParsedText = string.Empty;
    //private PlayableDirector director;
    //private bool pauseScheduled = true;


    //public override void OnPlayableCreate(Playable playable)
    //{
    //    director = (playable.GetGraph().GetResolver() as PlayableDirector);
    //}

    //public override void OnBehaviourPause(Playable playable, FrameData info)
    //{
    //    if (pauseScheduled)
    //    {
    //        director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    //        pauseScheduled = false;
    //    }
    //}

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (mTextUI != null)
        {
            mTextUI.ForceMeshUpdate();
            mParsedText = mTextUI.GetParsedText();
        }
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (mTextUI != null)
        {
            var progress = (float)(playable.GetTime() / playable.GetDuration());
            var current = Mathf.Lerp(0, mParsedText.Length, progress);
            var count = Mathf.CeilToInt(current);
            mTextUI.maxVisibleCharacters = count;
        }
    }
}