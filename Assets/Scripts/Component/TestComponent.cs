using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] private int _value; // задаем рамки для значений сериализованного поля

    public class SomeClass
    {
        private AnotherClass _another;

        public SomeClass(AnotherClass another)
        {
            _another = another;
        }
    }

    public class AnotherClass
    {

    }

}
