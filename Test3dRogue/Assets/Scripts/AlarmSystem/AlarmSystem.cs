using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _actionDistance;
    [SerializeField] private Player _player;
    
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;
    private float _distance;
    private float _volume;
    private bool _isPlayerInHouse;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        _audioSource.volume = 0;
    }

    IEnumerator VolumeChanging(
        Vector3 positionAlarm, 
        float valueStart, 
        float valueFinish)
    {
        
        while (_distance < _actionDistance)
        {
            _distance = GetDistanceXZ(positionAlarm, _player.transform.position);
            _volume = Mathf.Lerp(valueStart, valueFinish, _distance / _actionDistance);
            _audioSource.volume = _volume;
            
            yield return null;
        }

        _audioSource.volume = valueFinish;

        if (_isPlayerInHouse == false)
        {
            _audioSource.Stop();
        }

        yield return null;
    }

    private float GetDistanceXZ(Vector3 pointStart, Vector3 pointEnd)
    {
        Vector2 v2Start = GetVector2XZByVector3(pointStart);
        Vector2 v3End = GetVector2XZByVector3(pointEnd);
        float distance = Vector2.Distance(v2Start, v3End);
        return distance;
    }

    private Vector2 GetVector2XZByVector3(Vector3 point)
        => new Vector2(point.x, point.y);

    public void PlayerClossAlarmArea(Vector3 position)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _isPlayerInHouse = !_isPlayerInHouse;
        float valueStart = _volume;
        float valueFinish = 0;
        _distance = 0;

        if (_isPlayerInHouse == true)
        {
            valueStart = 0;
            valueFinish = 1;
            _audioSource.Play(0);
        }
        
        _currentCoroutine = StartCoroutine(VolumeChanging(position, valueStart, valueFinish));
    }
}
