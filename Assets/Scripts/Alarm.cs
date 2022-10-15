using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private int _fluencySound;
    
    private float _maxSoundValue = 1;
    private float _minSoundValue = 0;
    private Coroutine _changeVolume;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume(AlarmAction alarmAction)
    {
        if (_changeVolume != null && alarmAction == AlarmAction.Decrease)
        {
            StopCoroutine(_changeVolume);
            _changeVolume = StartCoroutine(DecreaseVolume(_minSoundValue));
        }
        else if (_changeVolume == null && alarmAction == AlarmAction.Decrease)
        {
            _changeVolume = StartCoroutine(DecreaseVolume(_minSoundValue));
        }
        else if (_changeVolume != null && alarmAction == AlarmAction.Increase)
        {
            StopCoroutine(_changeVolume);
            _changeVolume = StartCoroutine(IncreaseVolume(_maxSoundValue));
        }
        else if (_changeVolume == null && alarmAction == AlarmAction.Increase)
        {
            _changeVolume = StartCoroutine(IncreaseVolume(_maxSoundValue));
        }
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    private IEnumerator DecreaseVolume(float target)
    {
        while (_audioSource.volume > target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, Time.deltaTime / _fluencySound);
            yield return null;
        }

        Stop();
    }

    private IEnumerator IncreaseVolume(float target)
    {
        while (_audioSource.volume < target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, Time.deltaTime / _fluencySound);
            yield return null;
        }
    }
}
