using Modules.Card;
using UnityEngine;

namespace Modules.Gameplay
{
    public class MultipleAllMatchRule : MonoBehaviour, ICardRule
    {
        [SerializeField] private GameObject[] _allMatchObjects;

        private ICardRule[] _allMatches;
        
        public float IsMatch(CardData[] commitCards)
        {
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

            return float.MaxValue;
        }
    }
}