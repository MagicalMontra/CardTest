using System.Collections;
using System.Collections.Generic;
using Modules.Gameplay;
using Modules.Infastructure;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Modules.Tests
{
    public class GameRuleTests
    {

        [Test]
        public void InitTest()
        {
           var ruleObject = Object.Instantiate(Resources.Load<GameObject>("PairRule"));
           var gameRule = ruleObject.GetComponent<IPairRule>();
           Assert.That(ruleObject != null);
           Assert.That(gameRule != null);
        }
        
        [Test]
        [TestCase(float.PositiveInfinity, new string[]{ "Fire", "Red", "F" }, new string[]{ "Fire", "Red", "F" })]
        [TestCase(float.PositiveInfinity, new string[]{ "Fire", "Red", "10" }, new string[]{ "Fire", "Red", "10" })]
        [TestCase(11, new string[]{ "Fire", "Brown", "10" }, new string[]{ "Fire", "Red", "10" })]
        [TestCase(20, new string[]{ "Fire", "Red", "10" }, new string[]{ "Fire", "Red", "F" })]
        [TestCase(10, new string[]{ "Fire", "Brown", "10" }, new string[]{ "Fire", "Red", "F" })]
        [TestCase(20.5f, new string[]{ "Water", "Red", "10" }, new string[]{ "Fire", "Red", "10" })]
        public void PairValueTest(float expectedValue, string[] cardOne, string[] cardTwo)
        {
            CardData[] pair = {new CardData(cardOne[0], cardOne[1], cardOne[2]), new CardData(cardTwo[0], cardTwo[1], cardTwo[2])};
            var pairRuleObject = Object.Instantiate(Resources.Load<GameObject>("PairRule"));
            var pairRule = pairRuleObject.GetComponent<IPairRule>();
            var value = pairRule.CalculatePair(pair);
            Debug.Log($"value is {value}, expected {expectedValue}");
            Assert.AreEqual(expectedValue, value);
        }
        [Test]
        public void SingleAllMatchTest()
        {
            var gameRuleObject = Object.Instantiate(Resources.Load<GameObject>("ChosenOneAllMatchRule"));
            var realPlayerObject = Object.Instantiate(Resources.Load<GameObject>("TestPlayer"));

            var pairRuleObject = Object.Instantiate(Resources.Load<GameObject>("PairRule"));
            var pairRule = pairRuleObject.GetComponent<IPairRule>();
            var gameRule = gameRuleObject.GetComponent<IGameRule>();
            var realPlayer = realPlayerObject.GetComponent<IPlayer>();

            

            var cardPair = new CardData[2];
            cardPair[0] = new CardData("Fire", "Red", "F");
            cardPair[1] = new CardData("Water", "Red", "F");
            var value = pairRule.CalculatePair(cardPair);

            Dictionary<IPlayer, float> _playerHands = new Dictionary<IPlayer, float>();
            _playerHands.Add(realPlayer, value);

            for (int i = 0; i < 5; i++)
            {
                var aiPlayerObject = Object.Instantiate(Resources.Load<GameObject>("TestAIPlayer"));
                var aiPlayer = aiPlayerObject.GetComponent<IPlayer>();
                cardPair = new CardData[2];
                cardPair[0] = new CardData("Fire", "Water", "B");
                cardPair[1] = new CardData("Water", "Red", "F");
                value = pairRule.CalculatePair(cardPair);
                _playerHands.Add(aiPlayer, value);
            }
            
            var winners = gameRule.DecideWinner(_playerHands);
            Debug.Log(winners.Count);
            Assert.AreEqual(1, winners.Count);
        }
        [Test]
        public void MultipleAllMatchTest()
        {
            var gameRuleObject = Object.Instantiate(Resources.Load<GameObject>("ChosenOneAllMatchRule"));
            var realPlayerObject = Object.Instantiate(Resources.Load<GameObject>("TestPlayer"));

            var pairRuleObject = Object.Instantiate(Resources.Load<GameObject>("PairRule"));
            var pairRule = pairRuleObject.GetComponent<IPairRule>();
            var gameRule = gameRuleObject.GetComponent<IGameRule>();
            var realPlayer = realPlayerObject.GetComponent<IPlayer>();

            

            var cardPair = new CardData[2];
            cardPair[0] = new CardData("Fire", "Red", "F");
            cardPair[1] = new CardData("Water", "Red", "F");
            var value = pairRule.CalculatePair(cardPair);

            Dictionary<IPlayer, float> _playerHands = new Dictionary<IPlayer, float>();
            _playerHands.Add(realPlayer, value);

            for (int i = 0; i < 5; i++)
            {
                var aiPlayerObject = Object.Instantiate(Resources.Load<GameObject>("TestAIPlayer"));
                var aiPlayer = aiPlayerObject.GetComponent<IPlayer>();
                _playerHands.Add(aiPlayer, value);
            }
            
            var winners = gameRule.DecideWinner(_playerHands);
            Debug.Log($"commited hands:{_playerHands.Count}  winners:{winners.Count}");
            Assert.AreEqual(0, winners.Count);
        }
    }
}
