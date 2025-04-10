using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : ActionTask<AudioSource>
{
    public BBParameter<AudioClip> audioClip;
    public BBParameter<float> volume = 1f;
    public BBParameter<bool> waitUntilFinish = false;

    protected override string info
    {
        get { return string.Format("Play Audio {0}", audioClip); }
    }

    protected override void OnExecute()
    {
        if (audioClip.value == null)
        {
            EndAction(false);
            return;
        }

        agent.clip = audioClip.value;
        agent.volume = volume.value;
        agent.Play();

        if (!waitUntilFinish.value)
        {
            EndAction(true);
        }
    }

    protected override void OnUpdate()
    {
        if (elapsedTime >= agent.clip.length)
        {
            EndAction(true);
        }
    }
}
