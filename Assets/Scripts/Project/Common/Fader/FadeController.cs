using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class FadeController : MonoBehaviour
{
    private static FadeController instance;
    private Image fadeImage;

    public static FadeController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("FadeCanvas");
                instance = obj.AddComponent<FadeController>();
                instance.Initialize();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        gameObject.AddComponent<GraphicRaycaster>();

        var imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(transform, false);
        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);

        var rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        canvas.sortingOrder = short.MaxValue;
    }

    public IObservable<Unit> FadeOut(float duration)
        => Fade(0, 1, duration);

    public IObservable<Unit> FadeIn(float duration)
        => Fade(1, 0, duration);

    private IObservable<Unit> Fade(float from, float to, float duration)
    {
        float startTime = Time.time;
        return Observable.EveryUpdate()
            .Select(_ => Mathf.Clamp01((Time.time - startTime) / duration))
            .TakeWhile(t => t < 1f)
            .Do(t =>
            {
                float alpha = Mathf.Lerp(from, to, t);
                fadeImage.color = new Color(0, 0, 0, alpha);
            })
            .LastOrDefault()
            .DoOnCompleted(() =>
            {
                fadeImage.color = new Color(0, 0, 0, to);
            })
            .AsUnitObservable();
    }
}
