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
    [SerializeField] List<ItemData> AllItems = new List<ItemData>();

    public ItemData GiveItem(string itemName)
    {
        ItemData returnItem = null;
        foreach (ItemData item in AllItems)
        {
            if(item.ItemName == itemName)
            {
                returnItem = item;
            }
        }
        return returnItem;
    }

    public bool CheckForItem(string itemName)
    {
        return AllItems.Exists(x => x.ItemName == itemName);
    }
}
