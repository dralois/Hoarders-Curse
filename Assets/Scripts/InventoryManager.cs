﻿using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

    private static InventoryManager _instance = null;

    [SerializeField]
    private readonly uint _maxInventoryCount;

    private Dictionary<InventoryItem.ItemType, List<InventoryItem>> _inventory = null;
    private uint _totalInventoryCount = 0;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new InventoryManager();

            return _instance;
        }
    }
    public Dictionary<InventoryItem.ItemType, List<InventoryItem>> Inventory
    {
        get
        {
            return _inventory;
        }
    }
    /// <summary>
    /// Fügt ein Item hinzu, wenn das Inventory noch nicht voll ist
    /// </summary>
    /// <param name="item">Hinzuzufügendes Item</param>
    /// <returns>True, wenn es hinzugefügt wurde; false, wenn <see cref="_maxInventoryCount"/> erreicht wurde</returns>
    public bool AddItem(InventoryItem item)
    {
        if (_inventory[item.itemType] == null)
        {
            _inventory[item.itemType] = new List<InventoryItem>();
        }

        if (_totalInventoryCount < _maxInventoryCount)
        {
            _inventory[item.itemType].Add(item);
            _totalInventoryCount++;
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Löscht ein item aus dem Inventory, wenn es vorhanden ist.
    /// </summary>
    /// <param name="item">Zu löschendes Item</param>
    /// <returns>True, wenn es vorhanden war; false, wenn nicht</returns>
    public bool RemoveItem(InventoryItem item)
    {
        if (_inventory[item.itemType] == null)
        {
            _inventory[item.itemType] = new List<InventoryItem>();
        }

        if (_inventory[item.itemType].Contains(item))
        {
            _inventory[item.itemType].Remove(item);
            _totalInventoryCount--;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private InventoryManager()
    {
        _inventory = new Dictionary<InventoryItem.ItemType, List<InventoryItem>>();
    }
}