using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    
    
    public class MultipleAllMatchRule : MonoBehaviour
    {
        [SerializeField] private GameObject[] _allMatchObjects;

        private ICardRule[] _allMatches;
        
        public float IsMatch(CardData[] commitCards)
        {
            _allMatches ??= new ICardRule[_allMatchObjects.Length];

            for (int i = 0; i < _allMatchObjects.Length; i++)
            {
                _allMatches[i] = _allMatchObjects[i].GetComponent<ICardRule>();
            }
            
            var value = 0f;

            for (int i = 0; i < _allMatches.Length; i++)
            {
                var isAlreadyAllMatch = float.IsPositiveInfinity(value);
                var isMultipleAllMatch = float.IsPositiveInfinity(_allMatches[i].IsMatch(commitCards));
                
                if (isAlreadyAllMatch && isMultipleAllMatch)
                    value = 0;
            }

            if (value < -1f)
                return 0;

            return float.PositiveInfinity;
        }
    }
}