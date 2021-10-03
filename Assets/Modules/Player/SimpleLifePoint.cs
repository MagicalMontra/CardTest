using UnityEngine;

namespace Modules.Player
{
    public class SimpleLifePoint : MonoBehaviour, ILifePoint
    {
        [SerializeField] private int _initLP;
        private float _currentLP;
        public void Modify(float value)
        {
            if (_currentLP + value <= 0)
                value = _currentLP;

            if (_currentLP + value > _initLP)
                value = 0;
            
            _currentLP += value;
        }
    }
}