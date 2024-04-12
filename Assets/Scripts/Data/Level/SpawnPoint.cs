using UnityEngine;

namespace MyGame.Data.Level
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] public SpawnType Type;
    }

    public enum SpawnType
    {
        Hero,
        Minotaur,
        Artifact
    }
}