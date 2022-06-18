using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickedSound;


    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickedSound);
    }
}
