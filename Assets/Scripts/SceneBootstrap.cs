using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    [SerializeField] private List<string> scenesToLoad;

    public void Start()
    {
        StartCoroutine(LoadLevel(() => Debug.Log("Scene loaded")));
    }

    private IEnumerator LoadLevel(System.Action callback = null)
    {
        var coroutines = new List<Coroutine>();

        var count = 0;

        foreach (var sceneName in scenesToLoad)
        {
            count++;
            var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            op.completed += (_) => count--;
        }

        yield return new WaitUntil(() => count == 0);

        callback?.Invoke();
    }
}
