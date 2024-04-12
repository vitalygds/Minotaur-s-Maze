using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MyGame.Core
{
    public class LoadingScreen : MonoBehaviour
    {
        private CanvasGroup _screen;

        private void Awake()
        {
            _screen = GetComponent<CanvasGroup>();
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _screen.alpha = 1f;
        }
        
        public async UniTask Fade()
        {
            const float fadeStep = 0.03f;
            while (_screen.alpha > 0)
            {
                _screen.alpha -= fadeStep;
                await UniTask.Delay(30);
            }
        }
    }
}