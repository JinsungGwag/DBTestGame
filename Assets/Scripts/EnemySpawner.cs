using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

    [SerializeField]
    private Transform _start;
    [SerializeField]
    private Transform _end;

    [SerializeField]
    private float _initPeriod;
    [SerializeField]
    private float _limitPeriod;
    [SerializeField]
    private float _reductRate;

    private float _gameTime = 0f;

    private float _minX, _maxX;
    private float _posX, _posY;

    private void Start()
    {
        _minX = _start.position.x;
        _maxX = _end.position.x;
        _posY = _end.position.y;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        
        if(_gameTime >= _initPeriod)
        {
            _posX = Random.Range(_minX, _maxX);

            Enemy newEnemy = Instantiate(_enemy);
            newEnemy.transform.position = new Vector3(_posX, _posY, 0f);

            _gameTime = 0f;
            _initPeriod *= _reductRate;

            if (_initPeriod < _limitPeriod)
                _initPeriod = _limitPeriod;
        }
    }
}
