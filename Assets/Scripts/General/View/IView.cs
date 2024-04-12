using UnityEngine;

namespace MyGame.General.View
{
    public interface IView
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
    }
}