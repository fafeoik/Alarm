using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _treasures;

    [SerializeField] private float _speed;

    private Vector3 _startPoint;
    private Vector3 _currentTarget;

    private Coroutine _moveCoroutine;
   
    private void Start()
    {
        _currentTarget = _treasures.position;
        _startPoint = transform.position;

        _moveCoroutine = StartCoroutine(Move());
    }

    private void OnDestroy()
    {
        StopCoroutine(_moveCoroutine);
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

            if (transform.position == _treasures.position)
            {
                _currentTarget = _startPoint;
            }

            yield return null;
        }
    }
}
