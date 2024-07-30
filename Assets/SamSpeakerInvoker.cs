using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SamSpeakerInvoker : MonoBehaviour
{
    [SerializeField] private SamSpeaker speaker;
    [SerializeField] private AudioSource source;
    public void Speak(string text)
    {
        source.PlayOneShot(speaker.Speak(text));
    }
}
