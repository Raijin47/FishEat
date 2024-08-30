using UnityEngine;

public enum ClipType
{
    click,
    eat,
    gameOver,
    coin,
    buyBonus,
    bonus,
    changeSkin
}

public class Audio : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip[] clips;
    public float[] volums;

    public static Audio Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayInterface()
    {
        Play(ClipType.click);
    }

    public static void Play(ClipType type)
    {
        print("Play = " + type);
        Play((int)type); 
    }

    public static void Play(int id)
    {
        if (Instance == null)
            return;

        AudioSource source = Instance.audioSources[0];

        if (Instance.clips.Length > 0 && id < Instance.clips.Length)
        {
            source.PlayOneShot(Instance.clips[id], Instance.volums[id]);
        }
    }

    public void ActiveAudio(bool activ)
    {
        foreach (AudioSource source in audioSources)
        {
            source.mute = !activ;
        }

        Play(0);
    }

    public void ActiveAudioMusic(bool activ)
    {
        audioSources[1].mute = !activ;

        Play(0);
    }

    public void ActiveAudioSound(bool activ)
    {
        audioSources[0].mute = !activ;

        Play(0);
    }
}
