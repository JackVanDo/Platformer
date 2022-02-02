using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FirstPlatformer.Components
{
    public class CollectCoinComponent : MonoBehaviour
    {
        //[SerializeField] private string _tag;
        //[SerializeField] private Hero _hero;
        [SerializeField] private int _coinPrice;

        private int _coinsCount;


        public void CollectCoins(int coinPrice)
        {
            _coinsCount += coinPrice;
            Debug.Log($"ќбщее кол-во монет {_coinsCount}");
        }


    }
}


