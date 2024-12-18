using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    internal sealed class SceneLoader
    {
        private const int c_delayFrameCount = 10;

        public void Load(string name, Action onLoaded = null) =>
            LoadScene(name, onLoaded).Forget();

        private async UniTaskVoid LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            await LoadNext(nextScene);
            onLoaded?.Invoke();
        }

        private async UniTask LoadNext(string sceneName)
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneAsync.isDone)
            {
                await UniTask.DelayFrame(c_delayFrameCount);
            }
        }
    }
}