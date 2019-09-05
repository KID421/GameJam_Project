using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] bgm;

    private AudioSource aud;

    private static GameObject audioManager;

    private void Awake()
    {
        if (audioManager == null) audioManager = gameObject;
        else Destroy(gameObject);

        DontDestroyOnLoad(audioManager);

        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeBMG();
    }

    private void ChangeBMG()
    {
        if (!aud.isPlaying)
        {
            aud.clip = bgm[Random.Range(0, bgm.Length)];
            aud.Play();
        }
    }
}
