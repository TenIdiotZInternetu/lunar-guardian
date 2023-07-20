namespace UI
{
    public class ScoreBar : UIBar
    {
        void Start()
        {
            base.Start();
            PlayerScripts.PlayerStatus.ScoreChangedEvent += ChangeValue;
        }
    }
}