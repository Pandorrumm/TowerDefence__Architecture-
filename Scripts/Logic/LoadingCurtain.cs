using System.Collections;
using UnityEngine;

namespace TowerDefence.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtainCanvasGroup;
        [SerializeField] private float interval;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            curtainCanvasGroup.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (curtainCanvasGroup.alpha > 0)
            {
                curtainCanvasGroup.alpha -= interval;
                yield return new WaitForSeconds(interval);
            }

            gameObject.SetActive(false);
        }
    }
}
