using UnityEngine;

namespace EclipsePhase.Character
{
    public class Reputation
    {
        public enum ReputationNetwork
        {
            CircleAList,
            CivicNet,
            Fame,
            Guanxi,
            TheEye,
            ResearchNetworkAffiliates,
            ExploreNet
        }

        private int score;
        public  int Score { get => score; private set => score = Mathf.Clamp(value, 0, 99); }

        public void Increase(int amount) => Score += amount;
        public void Decrease(int amount) => Score -= amount;
    }

    public class Rep { }
}