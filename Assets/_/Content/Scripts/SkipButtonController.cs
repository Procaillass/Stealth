using UnityEngine;
using UnityEngine.Playables;

public class SkipButtonController : MonoBehaviour
{
    public PlayableDirector playableDirector; // Set this in the inspector

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100 , 50), "Skip Timeline"))
        {
            double timeToSkipTo = playableDirector.duration; // skip to the end
            playableDirector.time = timeToSkipTo;
            playableDirector.Evaluate();
        }
    }
}
