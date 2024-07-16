using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 管理场景过渡和加载的类。
 * 通过控制CanvasGroup的透明度实现场景之间的淡入淡出效果。
 */
public class TransitionManager : Singleton<TransitionManager>
{
    [SceneName]
    public string startScene; // 开始场景的名称
    public CanvasGroup fadeCanvasGroup; // 用于淡入淡出效果的CanvasGroup组件
    public float fadeDuration; // 淡入淡出的持续时间
    public bool isFade; // 标记是否正在进行淡入淡出
    public bool canTransition; // 标记是否允许进行场景过渡

    /**
     * 组件启用时注册场景状态改变事件的监听。
     */
    private void OnEnable()
    {
        EventHandle.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    /**
     * 组件禁用时取消注册场景状态改变事件的监听。
     */
    private void OnDisable()
    {
        EventHandle.GameStateChangeEvent -= OnGameStateChangeEvent;
    }

    /**
     * 处理游戏状态改变事件，根据新的游戏状态更新是否可以进行场景过渡。
     * @param gameState 当前的游戏状态
     */
    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    /**
     * 初始化时开始过渡到起始场景。
     */
    private void Start()
    {
        StartCoroutine(TransitionToScene(String.Empty, startScene));
    }

    /**
     * 触发场景过渡。
     * 如果当前不在淡入淡出状态且允许进行场景过渡，则开始过渡。
     * @param from 当前场景的名称
     * @param to 目标场景的名称
     */
    public void Transition(string from, string to)
    {
        if (!isFade && canTransition)
        {
            StartCoroutine(TransitionToScene(from, to));
        }
    }

    /**
     * 进行场景过渡的协程。
     * 先淡出，然后卸载当前场景，最后加载目标场景并淡入。
     * @param from 当前场景的名称
     * @param goTo 目标场景的名称
     */
    private IEnumerator TransitionToScene(string from, string goTo)
    {
        yield return Fade(1);
        if (from != String.Empty)
        {
            EventHandle.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(goTo, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        EventHandle.CallAfterSceneLoadEvent();
        yield return Fade(0);
    }

    /**
     * 控制CanvasGroup淡入或淡出的协程。
     * @param targetAlpha 目标透明度
     */
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
