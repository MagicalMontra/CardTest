using System.Collections.Generic;
using UnityEngine;

namespace Modules.Card
{
    [CreateAssetMenu(menuName = "Create CardElementSettings", fileName = "CardElementSettings", order = 0)]
    public class CardElementSettings : ScriptableObject
    {
        public List<string> suits = new List<string>();
    }
}