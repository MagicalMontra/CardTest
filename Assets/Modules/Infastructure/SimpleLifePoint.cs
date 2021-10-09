using System;
using TMPro;
using UnityEngine;

namespace Modules.Infastructure
{
    public class SimpleLifePoint : MonoBehaviour, ILifePoint
    {
        public float value => _currentLP;
        
        [SerializeField] private int _initLP;
        [SerializeField] private TextMeshProUGUI _lpText;
        
        private float _currentLP;
        private string _originalText;

        private void Awake()
        {
            _currentLP = _initLP;
            _originalText = _lpText.text;
            _lpText.text = $"{_originalText} {_currentLP}";
        }
        public void Modify(float value)
        {
            if (_currentLP + value <= 0)
                value = -_currentLP;

            if (_currentLP + value > _initLP)
                value = 0;
            
            _currentLP += value;
            _lpText.text = $"{_originalText} {_currentLP.ToString("F2")}";
        }
    }
}