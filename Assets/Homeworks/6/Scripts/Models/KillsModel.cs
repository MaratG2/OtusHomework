namespace Homeworks6.Models
{
    public class KillsModel
    {
        private int _kills = 0;
        public int Kills => _kills;
        
        public void AddKill()
        {
            _kills++;
        }

        public void ResetKills()
        {
            _kills = 0;
        }
    }
}