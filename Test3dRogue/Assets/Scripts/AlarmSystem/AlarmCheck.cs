using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmCheck : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarmSystem.PlayerClossAlarmArea(transform.position);
        }
    }
}
