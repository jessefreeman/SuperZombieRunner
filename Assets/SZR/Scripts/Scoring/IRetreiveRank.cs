namespace SZR.Scripts
{
    public interface IRetrieveRank
    {
        int GetRank(string userID, string statLabel, float value);
    }
}