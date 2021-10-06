using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Gameplay
{
    public class SelectMaximumGameRule : MonoBehaviour, IGameRule
    {
        public Dictionary<IPlayer, float> DecideWinner(Dictionary<IPlayer, float> playersValue)
        {
            var max = playersValue.Max(pv => pv.Value);
            Dictionary<IPlayer, float> _winners = new Dictionary<IPlayer, float>();

            foreach (var player in playersValue)
            {
                if (Mathf.Approximately(max, player.Value))
                    _winners.Add(player.Key, player.Value);          
            }
            
            return _winners;
        }
    }
}