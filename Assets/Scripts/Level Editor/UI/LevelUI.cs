namespace Assets.Scripts.LevelEditor.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelUI : MonoBehaviour
    {
        public Button SelectLevelButton;

        public Text NameText;
        public Text CashText;
        public Text WaveText;

        public string Name;
        public string Cash;
        public string Wave;

        public void Initialize(string name, string cash, string waves)
        {
            //SelectLevelButton.GetComponent<RectTransform>().anchorMin.Set(0, 1);
            //SelectLevelButton.GetComponent<RectTransform>().anchorMax.Set(1, 1);

            //SelectLevelButton.GetComponent<RectTransform>().sizeDelta.Set(500, 40);

            this.NameText.text = name;
            this.CashText.text = cash;
            this.WaveText.text = waves;
        }
    }
}