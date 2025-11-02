using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace DigitalWar.Project.Common.Dialog
{
    public class TitleInputUI : MonoBehaviour
    {
        void Start()
        {
            // Canvasの作成
            GameObject canvasObj = new GameObject("TitleCanvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // 追加コンポーネント
            canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObj.AddComponent<GraphicRaycaster>();

            // 背景
            GameObject bg = new GameObject("Background");
            bg.transform.SetParent(canvasObj.transform);
            Image bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
            RectTransform bgRect = bg.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = Vector2.zero;
            bgRect.offsetMax = Vector2.zero;

            // InputField
            GameObject inputObj = new GameObject("NameInput");
            inputObj.transform.SetParent(canvasObj.transform);

            RectTransform inputRect = inputObj.AddComponent<RectTransform>();
            inputRect.sizeDelta = new Vector2(400, 60);
            inputRect.anchoredPosition = new Vector2(0, -50);

            InputField inputField = inputObj.AddComponent<InputField>();
            Image inputBg = inputObj.AddComponent<Image>();
            inputBg.color = Color.white;

            // プレースホルダー
            GameObject placeholder = new GameObject("Placeholder");
            placeholder.transform.SetParent(inputObj.transform);
            Text placeholderText = placeholder.AddComponent<Text>();
            placeholderText.text = "Enter your name...";
            placeholderText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            placeholderText.color = Color.gray;
            placeholderText.alignment = TextAnchor.MiddleLeft;
            RectTransform placeholderRect = placeholder.GetComponent<RectTransform>();
            placeholderRect.anchorMin = Vector2.zero;
            placeholderRect.anchorMax = Vector2.one;
            placeholderRect.offsetMin = new Vector2(10, 0);
            placeholderRect.offsetMax = new Vector2(-10, 0);

            // 入力テキスト
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(inputObj.transform);
            Text inputText = textObj.AddComponent<Text>();
            inputText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            inputText.color = Color.black;
            inputText.alignment = TextAnchor.MiddleLeft;
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(10, 0);
            textRect.offsetMax = new Vector2(-10, 0);

            // InputFieldに紐づけ
            inputField.textComponent = inputText;
            inputField.placeholder = placeholderText;

            // ラベル（"Name:"）
            GameObject labelObj = new GameObject("Label");
            labelObj.transform.SetParent(canvasObj.transform);
            Text labelText = labelObj.AddComponent<Text>();
            labelText.text = "Name:";
            labelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            labelText.color = Color.white;
            labelText.alignment = TextAnchor.MiddleCenter;
            RectTransform labelRect = labelObj.GetComponent<RectTransform>();
            labelRect.sizeDelta = new Vector2(200, 50);
            labelRect.anchoredPosition = new Vector2(0, 30);

            // ===== ボタン追加部分 =====

            // Startボタン
            CreateButton(canvasObj.transform, "StartButton", "Start", new Vector2(0, -150), StartGame);

            // Exitボタン
            CreateButton(canvasObj.transform, "ExitButton", "Exit", new Vector2(0, -230), ExitGame);
        }

        // 汎用ボタン生成関数
        void CreateButton(Transform parent, string name, string label, Vector2 position, UnityEngine.Events.UnityAction onClick)
        {
            GameObject buttonObj = new GameObject(name);
            buttonObj.transform.SetParent(parent);

            RectTransform rect = buttonObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200, 60);
            rect.anchoredPosition = position;

            Button button = buttonObj.AddComponent<Button>();
            Image image = buttonObj.AddComponent<Image>();
            image.color = new Color(0.2f, 0.5f, 1f, 0.9f);

            // ボタンテキスト
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(buttonObj.transform);
            Text text = textObj.AddComponent<Text>();
            text.text = label;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            // イベント登録
            button.onClick.AddListener(onClick);
        }

        // ===== ボタン押下時の処理 =====
        void StartGame()
        {
            Debug.Log("Start Game!");
            // SceneManager.LoadScene("MainScene"); ← 実際のシーン遷移に使える
        }

        void ExitGame()
        {
            Debug.Log("Exit Game!");
            Application.Quit();
        }
    }
}