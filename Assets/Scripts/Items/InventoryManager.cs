using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using static DialogueHelperClass;
using static ItemHelperClass;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    [SerializeField, ReadOnly] List<ItemData> PlayerInventory = new List<ItemData>();

    public static event Action<ItemData> OnItemGained;

    public void GainItem(ItemData item)
    {
        item.ItemName = item.ItemName.ToLowerInvariant();
        PlayerInventory.Add(item);
        OnItemGained?.Invoke(item);
    }

    public void GainItem(string itemName)
    {
        var item = ItemSystem.Instance.GiveItem(itemName);
        item.ItemName = item.ItemName.ToLowerInvariant();
        PlayerInventory.Add(item);
        OnItemGained?.Invoke(item);
    }

    public void DiscardItem(string item)
    {
        DiscardItem(PlayerInventory.Find(x => x.ItemName == item));
    }

    public void DiscardItem(ItemData item)
    {
        item.ItemName = item.ItemName.ToLower();
        Debug.Assert(PlayerInventory.Contains(item));
        PlayerInventory.Remove(item);
    }

    public bool CheckForPlayerItem(string itemName)
    {
        return PlayerInventory.Exists(x => x.ItemName == itemName);
    }
}
