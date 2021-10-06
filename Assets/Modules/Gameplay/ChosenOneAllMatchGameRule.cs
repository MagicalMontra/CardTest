using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ChosenOneAllMatchGameRule : MonoBehaviour, IGameRule
    {
        public Dictionary<IPlayer, float> DecideWinner(Dictionary<IPlayer, float> playersValue)
        {
            var allMatchPlayerCount = playersValue.Count(pv => float.IsPositiveInfinity(pv.Value));
            Dictionary<IPlayer, float> _winners = new Dictionary<IPlayer, float>();
            
            if (allMatchPlayerCount < 2)
            {
                var chosenOne = playersValue.First(pv => float.IsPositiveInfinity(pv.Value));
                _winners.Add(chosenOne.Key, chosenOne.Value);
                return _winners;
            }

            foreach (var player in playersValue)
            {
                if (!float.IsPositiveInfinity(player.Value))
                    _winners.Add(player.Key, player.Value);
            }

            return _winners;
        }
    }
}