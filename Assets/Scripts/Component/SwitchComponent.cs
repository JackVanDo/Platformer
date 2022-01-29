using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace FirstPlatformer.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;

        [SerializeField] private string _animationKey;
        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        [ContextMenu("Switch")] // появляется доступ к методу в контекст меню
        public void SwitchIt()
        {
            Switch();
        }
    }

}

