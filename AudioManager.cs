using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    Dictionary<string, Queue<AudioSource>> soundsPool = new Dictionary<string, Queue<AudioSource>>();

    #region Singleton
    public static AudioManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            Queue<AudioSource> sources = new Queue<AudioSource>();

            for (int i = 0; i < sound.amount; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = sound.clip;
                source.volume = Mathf.Clamp(sound.volume, 0, 1);
                source.pitch = Mathf.Clamp(sound.pitch, -3, 3);
                sources.Enqueue(source);
            }

            soundsPool.Add(sound.name, sources);
        }
    }

    public void PlaySound(string name)
    {
        if (soundsPool.ContainsKey(name) == false)
            return;

        AudioSource source = soundsPool[name].Dequeue();
        soundsPool[name].Enqueue(source);
        source.Play();
    }
}

[System.Serializable]
struct Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public int amount;
}