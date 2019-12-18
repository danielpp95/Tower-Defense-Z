namespace Assets.Scripts.LevelEditor.UI
{
    using System.Collections.Generic;
    using Assets.Scripts.Contracts;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class CanvasUI : MonoBehaviour
    {
        public GameObject Main;
        public GameObject Editor;
        public GameObject WaveEditor;
        public GameObject LevelsUI;
        public GameObject SceneEditor;

        public GameObject Background;

        public Text Title;

        public Level Level = new Level();

        private void Awake()
        {
            this.DisableViewsExcept(this.Main, "Creator Mode");
        }

        private void Start()
        {
            this.Level = new Level { Waves = new List<Wave>() };
        }

        public void ShowEditor()
        {
            this.DisableViewsExcept(this.Editor, "Level Editor");
        }

        public void ShowLevels()
        {
            this.DisableViewsExcept(this.LevelsUI, "Select Level");
        }

        public void EditScene()
        {
            this.DisableViewsExcept(this.SceneEditor, disableUI: true);
        }

        public void WaveManager()
        {
            this.DisableViewsExcept(this.WaveEditor, "Configure Waves");
            FindObjectOfType<WaveScrollView>().LoadWaves(this.Level.Waves);
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        private void DisableViewsExcept(GameObject except, string title = null, bool disableUI = false)
        {
            this.Main.SetActive(false);
            this.Editor.SetActive(false);
            this.WaveEditor.SetActive(false);
            this.LevelsUI.SetActive(false);

            this.Background.SetActive(!disableUI);

            this.Title.enabled = !disableUI;
            this.Title.text = title ?? this.Title.text;

            except.SetActive(true);
        }
    }
}