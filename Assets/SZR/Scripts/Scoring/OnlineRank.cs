using UnityEngine;

namespace SZR.Scripts
{
    
    public class OnlineRank : MonoBehaviour, IRetrieveRank
    {
        public int GetRank(string userID, string statLabel, float value)
        {
            return Random.Range(1, 15);
        }
    }
}