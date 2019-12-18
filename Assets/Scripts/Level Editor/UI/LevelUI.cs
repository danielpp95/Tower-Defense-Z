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

            this.WaveText.text = level.Waves != null ? level.Waves.Count.ToString() : "0";
        }

        public void LoadLevel()
        {
            var canvasUI = FindObjectOfType<CanvasUI>();
            canvasUI.ShowEditor();
            canvasUI.Level.Waves = this.level.Waves;

            FindObjectOfType<LevelEditorUI>().Initialize(this.level);
        }
    }
}