using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TowerDefence.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner _coroutineRunner) =>
            coroutineRunner = _coroutineRunner;

        public void Load(string _name, Action _onLoaded = null) => 
            coroutineRunner.StartCoroutine(LoadScene(_name, _onLoaded));

        public IEnumerator LoadScene(string _nextScene, Action _onLoaded)
        {
            if (SceneManager.GetActiveScene().name == _nextScene)
            {
                _onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScenene = SceneManager.LoadSceneAsync(_nextScene);

            while (!waitNextScenene.isDone)
            {
                yield return null;
            }

            _onLoaded?.Invoke();
        }
    }
}