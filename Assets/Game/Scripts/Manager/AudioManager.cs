using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_audioSource;

    [SerializeField] AudioClip[] m_backgroundClips;
    [SerializeField] AudioClip[] m_audioClips;
    private Dictionary<string, AudioClip> m_audioClipDictionary;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        m_audioSource = GetComponent<AudioSource>();

        m_audioClipDictionary = new Dictionary<string, AudioClip>()
        {
            {"Bomber", m_audioClips[0] },
            { "Slow", m_audioClips[1] },
            { "Cannon", m_audioClips[2] },
            { "GameOver", m_audioClips[3] },
            { "CastleHit", m_audioClips[4] },
            { "EnemyHit", m_audioClips[5] },
            { "EnemySpawn", m_audioClips[6] },
            { "TowerPlace", m_audioClips[7] },
            { "TowerSell", m_audioClips[8] },
            { "TowerUpgrade", m_audioClips[9] }
        };
    }

    private void Start()
    {
        PlayBackgroundSound();
    }

    public void PlaySoundEffect(string soundName)
    {
        if (m_audioClipDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            m_audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip '{soundName}' not found in dictionary.");
        }
    }

    private void PlayBackgroundSound()
    {
        m_audioSource.clip =
            m_backgroundClips[StageManager.Instance.CurrentStageIndex];

        m_audioSource.Play();
    }
}