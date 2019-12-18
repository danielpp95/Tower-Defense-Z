namespace Assets.Scripts.LevelEditor.UI
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using UnityEngine;
    using UnityEngine.UI;

    public class WaveScrollView : MonoBehaviour
    {
        public GameObject Wave;
        public GameObject AddButton;
        public GameObject ScrollContainer;

        public List<GameObject> waves;

        // Start is called before the first frame update
        void Start()
        {
            this.waves = new List<GameObject>();
            if (!this.waves.Any())
            {
                this.InstantiateAddButton();
            }
        }

        public void AddWave()
        {
            this.AddWave(null);
        }

        public void AddWave(Wave wave)
        {
            var waveGO = Instantiate(Wave);
            waveGO.transform.SetParent(ScrollContainer.transform);
            this.waves.Add(waveGO);

            waveGO.GetComponent<WaveUI>().Initialize(wave);

            this.InstantiateAddButton();
        }

        private void InstantiateAddButton()
        {
            Destroy(GameObject.FindGameObjectWithTag("UIWaveAddButton"));

            var button = Instantiate(this.AddButton, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(ScrollContainer.transform);
            var btn = button.GetComponentInChildren<Button>();
            btn.onClick.AddListener(this.AddWave);
        }

        public void Save()
        {
            var errors = new List<string>();

            var waveList = new List<WaveUI>();

            foreach (var wv in this.waves)
            {
                waveList.Add(wv.GetComponent<WaveUI>());
            }

            errors = this.LogError(
                x => x.Wave.Enemy == EnemyEnum.None,
                "Unselected enemies",
                waveList,
                errors);

            errors = this.LogError(
                x => x.Wave.InitialCountdown <= 0,
                "Initial CountDown must be greater than zero",
                waveList,
                errors);

            errors = this.LogError(
                x => x.Wave.Level <= 0,
                "Enemy level must be greater than zero",
                waveList,
                errors);

            errors = this.LogError(
                x => x.Wave.Quantity <= 0,
                "Quantity of enemies must be greater than zero",
                waveList,
                errors);

            if (errors.Any())
            {
                return;
            }

            var canvasUI = FindObjectOfType<CanvasUI>();
            canvasUI.ShowEditor();

            var levelEditorUI = FindObjectOfType<LevelEditorUI>();
            levelEditorUI.Level.Waves = waveList.Map();
        }

        public void Back()
        {
            this.waves.Clear();

            FindObjectOfType<CanvasUI>().ShowEditor();
        }

        public void LoadWaves(List<Wave> waveList)
        {
            foreach (var w in waveList)
            {
                this.AddWave(w);
            }

            this.InstantiateAddButton();
        }

        private List<string> LogError(Func<WaveUI, bool> condition, string errorMessage, List<WaveUI> waveViews, List<string> errorList)
        {
            if (waveViews.Any(condition))
            {
                Debug.LogError(errorMessage);
                errorList.Add(errorMessage);
            }

            return errorList;
        }
    }
}