using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmArea : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _alarmSystem.AlarmTriggered();
        }
    }
}
