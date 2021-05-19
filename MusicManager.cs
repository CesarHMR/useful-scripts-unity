using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float volume;
    [SerializeField] List<AudioClip> songs = new List<AudioClip>();
    Queue<AudioClip> clips = new Queue<AudioClip>();
    AudioSource source;

    #region Singleton
    public static MusicManager Instance { get; private set; }
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
        SetAudioSourceConfigurations();
        CreateRandomQueueOfSongs();
        PlayNextSong();
    }

    void SetAudioSourceConfigurations()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.volume = volume;
    }
    void CreateRandomQueueOfSongs()
    {
        while (songs.Count > 0)
        {
            int rand = Random.Range(0, songs.Count);
            clips.Enqueue(songs[rand]);
            songs.RemoveAt(rand);
        }
    }
    void PlayNextSong()
    {
        AudioClip clip = clips.Dequeue();
        source.clip = clip;
        source.Play();
        Invoke("OnFinishedMusic", clip.length);
        clips.Enqueue(clip);
    }
    void OnFinishedMusic()
    {
        PlayNextSong();
    }
}
