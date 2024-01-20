using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    /// <summary>
    /// ����� �ҽ��� ��� �迭�̴�. Define�� ���ǵǾ� �ִ� MaxCount�� ����ŭ ������ ������ �����Ѵ�.
    /// </summary>
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    /// <summary>
    /// ����� Ŭ���� ��� ��ųʸ���, �̸��� ���� ��ųʸ��� �ִ� ����� Ŭ���� ������ �� �ִ�.
    /// </summary>
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    /// <summary>
    /// ���� @Sound ������Ʈ�� �����ϰ� �ı� �Ұ� ���� ��, ����� �ҽ����� ����� ���´�.
    /// </summary>
    public void init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    /// <summary>
    /// ���� �ٲ� ��, ������ ���߰� ������ҽ����� �����Ѵ�.
    /// </summary>
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    /// <summary>
    /// Resources/Music/ ������ ��ġ�� ���� �̸��� �Է��ϰ� ����ϰ��� �ϴ� type�� �����ؼ� ����Ѵ�.
    /// ������ type���� ���ķ� ����Ǹ�, ���� ó���Ǵ� ��Ŀ� ���̸� �ְ� �ִ�.
    /// </summary>
    /// <param name="path">������ �̸�</param>
    /// <param name="type">������ Ÿ��</param>
    /// <param name="pitch"></param>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    /// <summary>
    /// ȣ���Ϸ��� ���������� �ε��ϰ� ����ؾ� �Ѵ�.
    /// ���� Ÿ�Կ� ���� ������� ��������� ���� ����� �����Ǿ� �ִ�.
    /// </summary>
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            //audioSource.pitch = pitch;
            audioSource.volume = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {            
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            //audioSource.pitch = pitch;
            audioSource.volume = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    /// <summary>
    /// ������ �̸��� ���� ��, ������ ��ųʸ��� �����ϰ� ���� ������ �������� �Լ�
    /// </summary>
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        //if (path.Contains("Sounds/") == false)
        //  path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
