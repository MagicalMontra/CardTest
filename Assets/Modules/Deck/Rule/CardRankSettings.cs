using System.Collections.Generic;
using UnityEngine;

namespace Modules.Card
{
    [CreateAssetMenu(menuName = "Create CardRankSettings", fileName = "CardRankSettings", order = 0)]
    public class CardRankSettings : ScriptableObject
    {
        public List<string> ranks = new List<string>();
    }
}