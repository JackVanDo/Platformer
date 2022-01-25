using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FirstPlatformer.Components
{
    public class CollectCoinComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private Hero _hero;




        //public void CollectCoins()
        //{
        //    switch (_tag)
        //    {
        //        case "silver":
        //            _hero.AddCoins(1);
        //            Debug.Log($"ќбщее кол-во монет {_hero._coinsCount}");
        //            break;
        //            case "gold":
        //            _hero.AddCoins(10);
        //            Debug.Log($"ќбщее кол-во монет {_hero._coinsCount}");
        //            break;
        //    }          
        //}


    }
}


