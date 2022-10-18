using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _door.SetActive(true);
        _alarm.Play();
        _alarm.ChangeVolume(AlarmAction.Increase);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _door.SetActive(false);
        _alarm.ChangeVolume(AlarmAction.Decrease);
    }
}
