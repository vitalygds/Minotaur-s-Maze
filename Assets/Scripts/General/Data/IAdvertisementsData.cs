namespace MyGame.General.Data
{
    public interface IAdvertisementsData
    {
        string GameId { get; }
        string Interstitial { get; }
        string Banner { get; }
        string Rewarded { get; }
    }
}