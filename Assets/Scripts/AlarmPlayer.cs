using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AlarmScanner _alarmScanner;
    [SerializeField] private float _volumeSpeed;

    private float _volumeMinLevel = 0;
    private float _volumeMaxLevel = 0.5f;
    private Coroutine _turnOnCoroutine;
    private Coroutine _turnOffCoroutine;

    private void OnEnable()
    {
        _alarmScanner.ThiefCameIn += StartTurnOn;
        _alarmScanner.ThiefLeft += StartTurnOff;
    }

    private void OnDisable()
    {
        _alarmScanner.ThiefCameIn -= StartTurnOn;
        _alarmScanner.ThiefLeft -= StartTurnOff;

        if (_turnOnCoroutine != null)
            StopCoroutine(_turnOnCoroutine);

        if (_turnOffCoroutine != null)
            StopCoroutine(_turnOffCoroutine);
    }

    private void StartTurnOn()
    {
        if (_turnOffCoroutine != null)
            StopCoroutine(_turnOffCoroutine);

        _sound.Play();

        _turnOnCoroutine = StartCoroutine(ChangeVolume(_volumeMaxLevel));
    }

    private void StartTurnOff()
    {
        if (_turnOnCoroutine != null)
            StopCoroutine(_turnOnCoroutine);

        _turnOffCoroutine = StartCoroutine(ChangeVolume(_volumeMinLevel));
    }

    private IEnumerator ChangeVolume(float requiredValue)
    {
        while (_sound.volume != requiredValue)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, requiredValue, _volumeSpeed * Time.deltaTime);
            yield return null;
        }

        if (_sound.volume == 0)
        {
            _sound.Stop();
        }
    }
}
