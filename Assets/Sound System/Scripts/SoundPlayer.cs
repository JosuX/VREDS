using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private SoundManager sm;
    private AudioSource source;

    private void Start()
    {
        sm = FindObjectOfType<SoundManager>();
    }

    public void PlaySound(int index)     //// Call this function and put in Index from SoundManager to play specified sound
    {
        if (!source)
        {
            GameObject sounder = new GameObject("Sounder");
            sounder.AddComponent<AudioSource>();
            source = sounder.GetComponent<AudioSource>();
        }

        source.PlayOneShot(sm.Sounds[index]);
    }
}
