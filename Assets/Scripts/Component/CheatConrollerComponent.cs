using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace FirstPlatformer.Components
{
    public class CheatConrollerComponent : MonoBehaviour
    {
        private string _currentInput;
        [SerializeField] private float _inputTimeToLive;
        [SerializeField] private CheatItem[] _cheats;

        private float _inputTime; 

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput; //подписываемся на ввод текста с клавиатуры
        }

        private void OnDestroy()
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }

        private void OnTextInput(char inputChar) // то что мы вводим на клавиатуре подается в inputChar
        {
            _currentInput += inputChar;
            _inputTime = _inputTimeToLive;
            FindAnyCheats();
        }


        private void FindAnyCheats()
        {
            foreach (var cheatItem in _cheats)
            {
                if(_currentInput.Contains(cheatItem.Name))
                {
                    cheatItem.Action.Invoke(); // перебераем масив с читами и в случае совпадения выполняем action который указан
                    _currentInput = string.Empty;
                }
            }
        }

        private void Update()
        {
            if (_inputTime < 0)
            {
                _currentInput = string.Empty; // сброс строки
            }
            else
            {
                _inputTime -= Time.deltaTime;
            }
        }
    }
}


[Serializable]
public class CheatItem
{
    public string Name; // последовательность клавиш для читов
    public UnityEvent Action; // сюда передаем метод что выполняется после ввода чита
}


