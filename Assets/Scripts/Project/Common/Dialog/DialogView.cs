using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DigitalWar.Project.Common.Dialog
{
    public class DialogView : MonoBehaviour
    {
        public Canvas Canvas { get; private set; }
        public GameObject Panel { get; private set; }
        public TMP_Text TextArea { get; private set; }
        public GameObject ChoiceContainer { get; private set; }
        public List<TMP_Text> ChoiceTexts { get; private set; } = new();

        public void Initialize()
        {
            // Canvas
            var canvasObj = new GameObject("DialogCanvas");
            Canvas = canvasObj.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            DontDestroyOnLoad(canvasObj);

            // Panel
            Panel = new GameObject("DialogPanel");
            Panel.transform.SetParent(Canvas.transform, false);
            var panelRect = Panel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(1, 0.25f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            var bg = Panel.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.7f);
            Panel.SetActive(false);

            // Text
            var textObj = new GameObject("DialogText");
            textObj.transform.SetParent(Panel.transform, false);
            TextArea = textObj.AddComponent<TextMeshProUGUI>();
            var textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.05f, 0.2f);
            textRect.anchorMax = new Vector2(0.95f, 0.95f);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            TextArea.fontSize = 32;
            TextArea.color = Color.white;
            TextArea.font = Resources.Load<TMP_FontAsset>("Fonts/ヒラギノ角ゴシック W4 SDF");

            // Choice container
            ChoiceContainer = new GameObject("Choices");
            ChoiceContainer.transform.SetParent(Panel.transform, false);
            var containerRect = ChoiceContainer.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.1f, 0.05f);
            containerRect.anchorMax = new Vector2(0.9f, 0.2f);
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;
            ChoiceContainer.SetActive(false);
        }

        public void ClearChoices()
        {
            foreach (var old in ChoiceTexts)
                Destroy(old.gameObject);
            ChoiceTexts.Clear();
        }

        public void CreateChoices(string[] choices)
        {
            ClearChoices();
            float spacing = 250f;
            float startX = -((choices.Length - 1) * spacing / 2f);

            for (int i = 0; i < choices.Length; i++)
            {
                var obj = new GameObject($"Choice_{i}");
                obj.transform.SetParent(ChoiceContainer.transform, false);
                var txt = obj.AddComponent<TextMeshProUGUI>();
                txt.text = choices[i];
                txt.fontSize = 28;
                txt.color = (i == 0) ? Color.yellow : Color.white;
                txt.alignment = TextAlignmentOptions.Center;
                txt.font = TextArea.font;
                var rect = txt.GetComponent<RectTransform>();
                rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = new Vector2(startX + i * spacing, 0);
                ChoiceTexts.Add(txt);
            }
        }
    }
}
