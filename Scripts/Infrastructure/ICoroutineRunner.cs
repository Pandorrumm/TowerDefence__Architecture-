using UnityEngine;
using System.Collections;

namespace TowerDefence.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator _coroutine);
    }
}