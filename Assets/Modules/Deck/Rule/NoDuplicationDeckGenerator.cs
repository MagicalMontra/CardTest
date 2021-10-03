using UnityEngine;

namespace Modules.Card
{
    public class NoDuplicationDeckGenerator : MonoBehaviour, IDeckGenerator
    {
        private Card[] _cards;

        [SerializeField] private CardSuitSettings _suitSettings;
        [SerializeField] private CardRankSettings _rankSettings;
        [SerializeField] private CardColorSettings _colorSettings;

        public void Generate()
        {
            _cards = new Card[_suitSettings.suits.Count * _colorSettings.colors.Count * _rankSettings.ranks.Count];
            Debug.Log(_cards.Length);
        }
    }
}