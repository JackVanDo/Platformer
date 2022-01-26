using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FirstPlatformer.Components
{
    [RequireComponent(typeof(SpriteRenderer))] // ѕровер€ем есть ли у компонента такой атрибут как  spriterenderer 

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplite;


        private SpriteRenderer _renderer;
        private float _secondsPerFrame; // секунд на показ одного спрайта
        private int _currentSprite; // “екущий индекс нашего спрайта
        private float _nextFrameTime; //¬рем€ до следующего update


        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;
            _currentSprite = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return; //провер€ем флан и наступило ли врем€ дл€ показа нового спрайта

            if(_currentSprite >= _sprites.Length) // ѕровер€ем не вышли ли за пределы массива
            {
                if(_loop)
                {
                    _currentSprite = 0;
                } else
                {
                    enabled = false; //выключаем компонент
                    _onComplite?.Invoke();
                    return;
                }
            }
            _renderer.sprite = _sprites[_currentSprite]; // мен€ем спрайт
            _nextFrameTime += _secondsPerFrame; // мен€ем врем€ до следующего изменени€
            _currentSprite++; // мен€ем индекс следующего спрайта

        }


    }
}

