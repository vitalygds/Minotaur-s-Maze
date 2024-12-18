using UnityEngine;

namespace General
{
    public interface IView
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
    }
}