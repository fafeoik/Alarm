using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AlarmScanner _alarmScanner;
    [SerializeField] private float _volumeSpeed;

    private float _volumeLevel = 0.5f;
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

        _turnOnCoroutine = StartCoroutine(TurnOn());
    }

    private void StartTurnOff()
    {
        if (_turnOnCoroutine != null)
            StopCoroutine(_turnOnCoroutine);

        _turnOffCoroutine = StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOn()
    {
        bool isWorking = true;

        _sound.Play();

        while (isWorking)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _volumeLevel, _volumeSpeed * Time.deltaTime);
            yield return null;

            if (_sound.volume == _volumeLevel)
            {
                isWorking = false;
            }
        }
    }

    private IEnumerator TurnOff()
    {
        bool isWorking = true;

        while (isWorking)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, 0, _volumeSpeed * Time.deltaTime);
            yield return null;

            if (_sound.volume == 0)
            {
                _sound.Stop();
                isWorking = false;
            }
        }
    }
}
