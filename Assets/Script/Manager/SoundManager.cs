using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    AudioSource Source;
    [SerializeField]
    AudioClip StartClip;
    [SerializeField]
    AudioClip LoopClip;

    void Start () {
        StartCoroutine(StartAudio());
	}

    IEnumerator StartAudio()
    {
        Source.clip = StartClip;
        Source.Play();
        yield return new WaitForSeconds(StartClip.length);
        Source.clip = LoopClip;
        Source.Play();
        Source.loop = true;
    }

}
