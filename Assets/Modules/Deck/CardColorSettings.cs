using System.Collections.Generic;
using UnityEngine;

namespace Modules.Card
{
    [CreateAssetMenu(menuName = "Create CardColorSettings", fileName = "CardColorSettings", order = 0)]
    public class CardColorSettings : ScriptableObject
    {
        public List<string> colors = new List<string>();
    }
}