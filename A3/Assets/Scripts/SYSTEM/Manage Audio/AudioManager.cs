using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AudioManager : SimpleSingleton<AudioManager>
{
    public SFXData[] Sounds;

    Dictionary<SoundID, SFXData> _sound = new Dictionary<SoundID, SFXData>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var sfx in Sounds)
        {
            _sound[sfx.ID] = sfx;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(SoundID id, Vector3? position = null, float volume = 1f)
    {
        if (_sound.TryGetValue(id, out SFXData data))
        {
            int r = Random.Range(0, data.Clips.Length);
            AudioClip clip = data.Clips[r];
            if (position != null && position.HasValue)
            {
                AudioSource.PlayClipAtPoint(clip, position.Value); // TODO (Don't share to Mr Ian) will replace later.
            }
            else
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
            }

        }
    }
}

public enum SoundID
{
    Button_Sound,
    Gunshot,
    TurrentShot,
    VictorySound,
    DefeatSound,
    BGMusic,
    DoorOpen,
    DoorClose,
    DoorSlideOpen,
}

[System.Serializable]
public class SFXData
{
    public SoundID ID;
    public AudioClip[] Clips;
}
