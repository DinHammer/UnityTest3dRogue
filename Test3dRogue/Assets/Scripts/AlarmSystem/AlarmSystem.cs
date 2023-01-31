using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _timeAlarmDelay;

    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;
    private float _volume;
    private bool _isPlayerInHouse;
    private float _currentTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        _audioSource.volume = 0;
    }

    private IEnumerator VolumeChanging(
        float valueStart, 
        float valueFinish)
    {
        
        while (_currentTime < _timeAlarmDelay)
        {
            _volume = Mathf.Lerp(valueStart, valueFinish,  _currentTime / _timeAlarmDelay);
            _audioSource.volume = _volume;
            _currentTime += Time.deltaTime;
            
            yield return null;
        }

        _audioSource.volume = valueFinish;

        if (_isPlayerInHouse == false)
        {
            _audioSource.Stop();
        }

        yield return null;
    }

    public void AlarmTriggered()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _isPlayerInHouse = !_isPlayerInHouse;
        float valueStart = _volume;
        float valueFinish = 0;
        _currentTime = 0;

        if (_isPlayerInHouse == true)
        {
            valueStart = 0;
            valueFinish = 1;
            _audioSource.Play(0);
        }
        
        _currentCoroutine = StartCoroutine(VolumeChanging(valueStart, valueFinish));
    }
}
