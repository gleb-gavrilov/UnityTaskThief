using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryThief : MonoBehaviour
{
    [SerializeField] private GameObject _openDoor;
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _openDoor.SetActive(true);
        _alarm.Play();
        _alarm.ChangeVolume(AlarmAction.Increase);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _openDoor.SetActive(false);
        _alarm.ChangeVolume(AlarmAction.Decrease);
    }
}
