using General;
using UnityEngine;

namespace Core
{
    internal sealed class GameStarter : MonoBehaviour, IGameStarter
    {
        [SerializeField] private Bootstrapper _bootstrapPrefab;
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _isJoysticksUse;
        [SerializeField] private bool _isAutoRun;

        private void Awake()
        {
            if (_isAutoRun)
                Initialize(_isJoysticksUse);
        }

        public void Initialize(bool isJoysticksUse)
        {
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
            {
                Instantiate(_bootstrapPrefab).Construct(_sceneName, isJoysticksUse);
            }
            else
            {
                bootstrapper.TryStartGame(_sceneName, isJoysticksUse);
            }
            Destroy(gameObject);
        }
    }
}