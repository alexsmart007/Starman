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

public class ItemSystem : SingletonMonoBehavior<ItemSystem>
{
    [SerializeField, ReadOnly] List<ItemData> Inventory = new List<ItemData>();

    public static event Action<ItemData> OnItemGained;

    public void GainItem(ItemData item)
    {
        item.ItemName = item.ItemName.ToLowerInvariant();
        Inventory.Add(item);
        OnItemGained?.Invoke(item);
    }

    public void DiscardItem(string item)
    {
        DiscardItem(Inventory.Find(x => x.ItemName == item));
    }

    public void DiscardItem(ItemData item)
    {
        item.ItemName = item.ItemName.ToLower();
        Debug.Assert(Inventory.Contains(item));
        Inventory.Remove(item);
    }

    public bool CheckForItem(string itemName)
    {
        return Inventory.Exists(x => x.ItemName == itemName);
    }
}
