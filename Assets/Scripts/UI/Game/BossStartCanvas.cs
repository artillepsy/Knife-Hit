﻿using Scriptable;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class BossStartCanvas : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private TextMeshProUGUI bossName;
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Settings.IsBoss) return;
            bossName.text = level.Log.Settings.Boss.Name;
        }
    }
}