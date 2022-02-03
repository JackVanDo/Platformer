using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace FirstPlatformer.Components
{
    public class CollectCoinComponent : MonoBehaviour
    {

        [SerializeField] private int _coinPrice;



        public void CollectCoin(GameObject target)
        {
            var charStats = target.GetComponent<CharStats>();
            if(charStats != null)
            {
                charStats.CollectCoins(_coinPrice);
            }
        }


        


    }
}


