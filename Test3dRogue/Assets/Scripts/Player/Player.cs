using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _timeIncreaseVolume;
    
    private bool _isAlarmActive;
    private AudioSource _audioSource;
    private float _volume;
    private float _currentTime;
    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _volume = 0;
    }

    private void Update()
    {
        _audioSource.volume = _volume;
    }

    public void ActionAlarm()
    {
        _isAlarmActive = !_isAlarmActive;
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentTime = 0;
        }

        if (_isAlarmActive == true)
        {
            _currentCoroutine = StartCoroutine(VolumeIncrease());
            _audioSource.Play(0);
        }
        else
        {
            _currentCoroutine = StartCoroutine(VolumeDecrease());
            
        }
    }

    IEnumerator VolumeIncrease()
    {
        while (_currentTime < _timeIncreaseVolume)
        {
            _volume = Mathf.Lerp(0, 1, _currentTime / _timeIncreaseVolume);
            _currentTime += Time.deltaTime;
            yield return null;
        }

        _volume = 1;
        _currentTime = 0;
        yield return null;
    }
    
    IEnumerator VolumeDecrease()
    {
        while (_currentTime < _timeIncreaseVolume)
        {
            _volume = Mathf.Lerp(_volume, 0, _currentTime / _timeIncreaseVolume);
            _currentTime += Time.deltaTime;
            yield return null;
        }

        _audioSource.Stop();
        _volume = 0;
        _currentTime = 0;
        yield return null;
    }


}
