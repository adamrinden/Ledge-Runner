using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance {get; private set;}
    private AudioSource audioSource;
    // Plays the sound
    private void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
