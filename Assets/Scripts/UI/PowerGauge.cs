namespace UI
{
    public class PowerGauge : UIBar
    {
        void Start()
        {
            base.Start();
            PlayerScripts.PlayerStatus.PowerLevelChangedEvent += ChangeValue;
        }
    }
}