using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryThief : MonoBehaviour
{
    [SerializeField] private GameObject _openDoor;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private int _fluencySound;

    private float _volumeStep;
    private float _maxSoundValue = 1;
    private float _minSoundValue = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _openDoor.SetActive(true);
        _audio.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_audio.volume < _maxSoundValue)
        {
            _volumeStep = Time.deltaTime / _fluencySound;
            _audio.volume = Mathf.MoveTowards(_audio.volume, _maxSoundValue, _volumeStep);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _openDoor.SetActive(false);
        StartCoroutine(DownSoundVolue());
        if (_audio.volume <= _minSoundValue)
        {
           _audio.Stop();
        }
    }

    private IEnumerator DownSoundVolue()
    {
        while (_audio.volume > _minSoundValue)
        {
            _volumeStep = Time.deltaTime / _fluencySound;
            _audio.volume = Mathf.MoveTowards(_audio.volume, _minSoundValue, _volumeStep);
            yield return null;
        }
    }
}
