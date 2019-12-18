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
            this.InstantiateAddButton();
        }

        public void AddWave()
        {
            var wave = Instantiate(Wave);
            wave.transform.SetParent(ScrollContainer.transform);

            this.waves.Add(wave);

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

            var waveList = new List<WaveView>();

            foreach (var wv in this.waves)
            {
                waveList.Add(wv.GetComponent<WaveView>());
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

            var uiView = FindObjectOfType<CanvasUI>();
            uiView.Level.Waves = waveList.Map();
            uiView.ShowEditor();
        }

        public void Back()
        {
            this.waves.Clear();

            FindObjectOfType<CanvasUI>().ShowEditor();
        }

        private List<string> LogError(Func<WaveView, bool> condition, string errorMessage, List<WaveView> waveViews, List<string> errorList)
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