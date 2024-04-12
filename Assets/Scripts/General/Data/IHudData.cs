﻿namespace MyGame.General.Data
{
    public interface IHudData
    {
        string WinMessage { get; }
        string StartMessage { get; }
        string OnArtifactPickMessage { get; }
        int MessageShowDelayTimeMillisecond { get; }
        int AdsButtonShowDelayTimeMillisecond { get; }
    }
}