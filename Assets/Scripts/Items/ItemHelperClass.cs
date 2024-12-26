using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class ItemHelperClass
{

    [System.Serializable]
    public class ItemData
    {
        [SerializeField, TextArea()] public string ItemDescription;
        public string ItemName;
        public Sprite ItemSprite;

        public ItemData(string ItemName, Sprite ItemSprite)
        {
            this.ItemName = ItemName.ToLower();
            this.ItemSprite = ItemSprite;
        }
    }


}
