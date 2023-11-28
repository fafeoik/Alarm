using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _treasures;

    private Vector3 _startPoint;

    [SerializeField] private float _speed;

    private Vector3 _currentTarget;
   
    private void Start()
    {
        _currentTarget = _treasures.position;
        _startPoint = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        if (transform.position == _treasures.position)
        {
            _currentTarget = _startPoint;
        }
    }
}
