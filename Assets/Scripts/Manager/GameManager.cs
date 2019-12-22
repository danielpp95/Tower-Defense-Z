﻿namespace Assets.Scripts.Manager
{
    using System.Linq;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Engine;
    using Assets.Scripts.LevelEditor.LevelScene;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        public Level level;

        public Text WavesText;
        public Text LivesText;
        public Text CashText;
        public Text TimerText;

        public GameObject TimerContainer;
        public Transform StartPoint;

        private int lives = 7;
        private int cash;

        // Start is called before the first frame update
        void Awake()
        {
            this.level = SaveEngine.LoadLevels().Last();

            this.AddCash(this.level.InitialCash);

            this.SetActualWave(0);

            this.gameObject.GetComponent<TilemapController>().Initialize(this.level.TileMap, this.StartPoint);
        }

        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetActualWave(int newWave)
        {
            this.WavesText.text = $"{newWave}/{this.level.Waves.Count}";
        }

        public void LossLive()
        {
            if (this.lives > 0)
            {
                this.lives--;
                this.LivesText.text = this.lives.ToString();
            }
        }

        public void AddCash(int c)
        {
            this.cash += c;
            this.CashText.text = this.cash.ToString();
        }

        public bool SubtractCash(int c)
        {
            if (this.cash >= c)
            {
                this.cash -= c;
                this.CashText.text = this.cash.ToString();
                return true;
            }
            return false;
        }

        public void SetTimer(int seconds)
        {
            this.TimerText.text = $"{seconds}s";
        }
    }
}