using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NarrationPlayableAsset : PlayableAsset
{
    [SerializeField]
    private ExposedReference<TMP_Text> text;

    private readonly NarrationBehaviour narration = new NarrationBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<NarrationBehaviour>.Create(graph, narration);
        var behaviour = playable.GetBehaviour();
        behaviour.mTextUI = text.Resolve(graph.GetResolver());
        return playable;
    }
}
