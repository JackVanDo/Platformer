using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace FirstPlatformer.Components
{

    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action; //Это обьект для сериализации методов, в неё можем передать метод из другого компонета и вызвать его


        private void OnTriggerEnter2D(Collider2D other) // встроенная функция показывает обьект с которым мы стригерились
        {
            if (other.gameObject.CompareTag(_tag)) //проверяем попал ли нужный нам тег в коллайдер который мы задали (для рестарта уровня, для сбора монет и тд)
            {
                if(_action != null)
                {
                    _action.Invoke();
                }
            }
        }
    }
}

