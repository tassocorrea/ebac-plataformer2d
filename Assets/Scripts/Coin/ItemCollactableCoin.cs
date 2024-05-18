using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollactableCoin : ItenCollactableBase
{
  
        protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
    }

}
