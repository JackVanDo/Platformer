using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FirstPlatformer.Components
{
    public class ControlHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _treatment;


        public void ApplyDamage(GameObject target)
        {
            var charStats = target.GetComponent<CharStats>(); //получаем доступ к компоненту где хранится ХП
            if (charStats != null)
            {
                charStats.ApplyDamage(_damage);
            }
        }

        public void ApplyTreatment(GameObject target)
        {
            var charStats = target.GetComponent<CharStats>();
            if (charStats != null)
            {
                charStats.ApplyTreatment(_treatment);
            }
        }
    }
}

