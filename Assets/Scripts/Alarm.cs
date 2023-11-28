using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _speed;

    private float _volumeLevel = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            StartCoroutine(TurnOn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            StartCoroutine(TurnOff());
        }
    }

    private IEnumerator TurnOn()
    {
        bool isWorking = true;

        _sound.Play();
        _sound.volume = 0;

        while (isWorking)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _volumeLevel, _speed * Time.deltaTime);
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
            _sound.volume = Mathf.MoveTowards(_sound.volume, 0, _speed * Time.deltaTime);
            yield return null;

            if (_sound.volume == 0)
            {
                _sound.Stop();
                isWorking = false;
            }
        }
    }
}