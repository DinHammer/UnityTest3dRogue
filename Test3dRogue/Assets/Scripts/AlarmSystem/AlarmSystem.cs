using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSpeed;
    
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;
    private bool _isPlayerInHouse;
    private float _volumeCurrent;
    private float _volumeFinish;
    private float _volumeDelta;
    private float _volumeDeltaLimit = -0.01f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        _audioSource.volume = 0;
    }

    private IEnumerator VolumeChanging()
    {
        while (_volumeDelta>_volumeDeltaLimit)
        {
            _volumeCurrent = Mathf.MoveTowards(_volumeCurrent, _volumeFinish, _volumeChangeSpeed*Time.deltaTime);
            _volumeDelta = MathF.Abs(_volumeFinish - _volumeCurrent);
            _audioSource.volume = _volumeCurrent;
            
            yield return null;
        }
        
        if (_isPlayerInHouse == false)
        {
            _audioSource.Stop();
        }

        yield return null;
    }

    public void OnTriggering()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _isPlayerInHouse = !_isPlayerInHouse;
        _volumeFinish = 0;

        if (_isPlayerInHouse == true)
        {
            _volumeFinish = 1f;
            _audioSource.Play(0);
        }
        
        _currentCoroutine = StartCoroutine(VolumeChanging());
    }
}
