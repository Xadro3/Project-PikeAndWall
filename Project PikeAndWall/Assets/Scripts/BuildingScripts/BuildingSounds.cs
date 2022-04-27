using UnityEngine;

public class BuildingSounds : MonoBehaviour
{

    public AudioSource audioSource;
    [SerializeField] AudioClip buildClip;
    //public AudioClip destroyClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        audioSource.PlayOneShot(buildClip);
    }

}
