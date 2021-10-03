using System.Collections.Generic;
using UnityEngine;

namespace Modules.Card
{
    [CreateAssetMenu(menuName = "Create CardSuitSettings", fileName = "CardSuitSettings", order = 0)]
    public class CardSuitSettings : ScriptableObject
    {
        public List<string> suits = new List<string>();
    }
}