namespace Homeworks.h1.GameManager
{
    public interface IGameListener
    { }
    public interface IGameStartListener : IGameListener
    {
        public void OnGameStart();
    }
    public interface IGameEndListener : IGameListener
    {
        public void OnGameEnd();
    }
    public interface IGamePauseListener : IGameListener
    {
        public void OnGamePause();
    }
    public interface IGameResumeListener : IGameListener
    {
        public void OnGameResume();
    }
}