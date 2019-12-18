namespace Assets.Scripts.LevelEditor.UI
{
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Engine;
    using Assets.Scripts.LevelEditor.LevelScene;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelEditorUI : MonoBehaviour
    {
        public InputField NameInput;
        public InputField CashInput;
        public InputField InitialCountDownInput;

        public Level Level = new Level();

        public void SetCash(string text)
        {
            if (text == string.Empty)
            {
                text = "0";
            }

            this.Level.InitialCash = int.Parse(text);
        }

        public void SetInitialCountdown(string text)
        {
            if (text == string.Empty)
            {
                text = "0";
            }

            this.Level.InitialCountDown = float.Parse(text);
        }

        public void SetName(string text)
        {
            this.Level.Name = text;
        }

        public void SaveLevel()
        {
            this.Level.TileMap = FindObjectOfType<TilemapController>().tileMap;

            SaveEngine.SaveNewLevel(this.Level);
            //SaveEngine.SaveLevels(new List<Level> { this.Level });
        }

        public void Initialize(Level level)
        {
            this.Level = level;

            this.CashInput.text = level.InitialCash.ToString();
            this.NameInput.text = level.Name.ToString();
            this.InitialCountDownInput.text = level.InitialCountDown.ToString();
        }
    }
}