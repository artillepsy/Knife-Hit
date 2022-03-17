﻿using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private List<KnifeShopItem> knifeItems;
        [SerializeField] private BuyButton buyButtonPrefab;
        private List<BuyButton> _buyButtons;
        private SaveManager _saveLoadManager;
        public List<KnifeShopItem> KnifeItems => knifeItems;

        public void Unlock(KnifeShopItem item)
        {
            _saveLoadManager.Unlock(item.Id, item.Cost);
            Events.OnBuy?.Invoke(item.Cost);
        }

        public void Equip(KnifeShopItem item)
        {
            _saveLoadManager.Equip(item);
            Events.OnEquip?.Invoke(item.Id);
        }
        
        private void Start()
        {
            _buyButtons = new List<BuyButton>();
            _saveLoadManager = FindObjectOfType<SaveManager>();
            foreach (var knife in knifeItems)
            {
                var instance = Instantiate(buyButtonPrefab, content);
                instance.SetValues(knife, IsUnlocked(knife), _saveLoadManager.AppleCount, _saveLoadManager.EquippedKnifeId);
                _buyButtons.Add(instance);
            }
            // add items player bought
        }

        private bool IsUnlocked(KnifeShopItem item)
        {
            foreach (var id in _saveLoadManager.UnlockedIds)
            {
                if (item.Id == id) return true;
            }
            return false;
        }
    }
}