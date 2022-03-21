﻿using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace Management
{
    public class AudioManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private List<AudioClip> knifeDropClips;
        [SerializeField] private List<AudioClip> OnThrowClips;
        [SerializeField] private List<AudioClip> appleHitClips;
        [SerializeField] private AudioClip bossFightClip;
        [SerializeField] private AudioClip buttonClick;
        
        
        [Header("Runtime changeable")]
        [SerializeField] private List<AudioClip> logHitClips;
        [SerializeField] private List<AudioClip> logDestroyClips;
        private AudioSource _audioSource;
        private bool _vibration;
        private List<AudioClip> _logHitClips;
        private List<AudioClip> _logDestroyClips;
        public void OnLevelLoad(Level level)
        {
            if (level.Log.Settings.IsBoss) PlayClip(bossFightClip);
            if (level.Log.Settings.Boss.HitClips.Count != 0)
            {
                _logHitClips = level.Log.Settings.Boss.HitClips;
            }
            else
            {
                _logHitClips = logHitClips;
            }
            if (level.Log.Settings.Boss.DestroyClips.Count != 0)
            {
                _logDestroyClips = level.Log.Settings.Boss.DestroyClips;
            }
            else
            {
                _logDestroyClips = logDestroyClips;
            }
        }
        private void OnEnable()
        {
            Vibration.Init();
            _audioSource = GetComponent<AudioSource>();
            Events.OnKnifeDrop.AddListener(()=>
            {
                PlayClip(knifeDropClips);
                if(_vibration) Vibration.Vibrate(400);
            });
            Events.OnAppleHit.AddListener(()=>PlayClip(appleHitClips));
            Events.OnKnifeHit.AddListener(()=>
            {
                PlayClip(_logHitClips);
                if(_vibration) Vibration.VibratePop();
            });
            Events.OnWinGame.AddListener(()=>
            {
                PlayClip(_logDestroyClips);
                if(_vibration) Vibration.Vibrate(400);
            });
            Events.OnThrow.AddListener(()=> PlayClip(OnThrowClips));
            Events.OnClikButton.AddListener(()=>_audioSource.PlayOneShot(buttonClick));
            Events.OnSettingsChange.AddListener(() =>
            {
                _audioSource.volume =  SaveManager.Inst.Sound.Volume;
                _vibration =  SaveManager.Inst.Sound.Vibration;
            });
        }

        private void Start()
        {
            _audioSource.volume =  SaveManager.Inst.Sound.Volume;
            _vibration =  SaveManager.Inst.Sound.Vibration;
        }

        private void PlayClip(List<AudioClip> clips)
        {
            _audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
        }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}