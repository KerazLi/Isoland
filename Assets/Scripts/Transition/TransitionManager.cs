using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public void Transition(string from, string to)
    {
        StartCoroutine(TransitionToScene(from, to));
    }

    private IEnumerator TransitionToScene(string from,string goTo)
    {
        yield return SceneManager.UnloadSceneAsync(from);
        yield return  SceneManager.LoadSceneAsync(goTo,LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}
