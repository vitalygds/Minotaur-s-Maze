using UnityEngine;

namespace Data
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