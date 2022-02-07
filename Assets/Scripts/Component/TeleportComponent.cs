using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FirstPlatformer.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;


        public void Teleport(GameObject target)
        {
            target.transform.position = _destTransform.position;


        }
    }
}

