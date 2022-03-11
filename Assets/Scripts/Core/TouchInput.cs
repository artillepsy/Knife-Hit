﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class TouchInput : MonoBehaviour
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        [SerializeField] private float startReloadTimeInSeconds = 2f;
        
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        private float _currentTime = 0;
        public static UnityEvent OnTouch = new UnityEvent();

        private void Awake()
        {
            _currentTime = reloadTimeInSeconds;
            Vibration.Init();
        }

        private void Update()
        {
            if (!ReadyToThrow()) return;
            if (Input.touchCount > 0) Throw();
        }

        private void Throw()
        {
            Vibration.Vibrate();
            OnTouch?.Invoke();
            _currentTime = reloadTimeInSeconds;
        }
        private bool ReadyToThrow()
        {
            if (_currentTime <= 0) return true;
            _currentTime -= Time.deltaTime;
            return false;
        }
    }
}