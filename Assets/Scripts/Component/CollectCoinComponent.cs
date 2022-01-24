using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FirstPlatformer.Components
{
    public class CollectCoinComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;


        private float _coinsCount;
        

        public void CollectCoins()
        {
            switch (_tag)
            {
                case "silver":
                    _coinsCount++;
                    Debug.Log($"ќбщее кол-во монет {_coinsCount}");
                    break;
                case "gold":
                    _coinsCount += 10;
                    Debug.Log($"ќбщее кол-во монет {_coinsCount}");
                    break;
            }          
        }


    }
}


