namespace Assets.Scripts.LevelEditor.UI
{
    using Assets.Scripts.Contracts;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelUI : MonoBehaviour
    {
        public Text NameText;
        public Text CashText;
        public Text WaveText;

        private Level level;

        public void Initialize(Level level)
        {
            this.level = level;

            this.NameText.text = level.Name;
            this.CashText.text = level.InitialCash.ToString();
            this.WaveText.text = level.Waves.Count.ToString();
        }

        public void LoadLevel()
        {
            FindObjectOfType<CanvasUI>().ShowEditor();

        }
    }
}