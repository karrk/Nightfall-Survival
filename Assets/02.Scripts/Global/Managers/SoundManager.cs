using UnityEngine;
using UnityEngine.Audio;
using VS.Base.Manager;

public class SoundManager : Base_Manager
{
    [SerializeField]
    private AudioMixer mixer;

    private AudioMixerGroup[] mixerGroup;

    protected override void Logic_Init_Custom()
    {
        //mixerGroup = mixer.FindMatchingGroups("");
    }


}
