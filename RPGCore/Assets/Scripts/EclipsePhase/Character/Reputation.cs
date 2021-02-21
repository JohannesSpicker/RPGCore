using UnityEngine;

namespace EclipsePhase.Character
{
    public enum ReputationNetwork { CircleAList, CivicNet, Fame, Guanxi, TheEye, ResearchNetworkAffiliates, ExploreNet  };
    
    public class Reputation
    {
        private int score;
        public int  Score { get => score; private set => score = Mathf.Clamp(value, 0, 99); }

        public void Increase(int amount) => Score += amount;
        public void Decrease(int amount) => Score -= amount;
    }
}