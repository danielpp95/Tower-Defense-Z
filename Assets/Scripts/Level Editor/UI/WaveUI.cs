namespace Assets.Scripts.LevelEditor.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class WaveUI : MonoBehaviour
    {
        public Dropdown EnemyDropdown;
        public InputField LevelInput;
        public InputField QuantityInput;
        public InputField InitialCountDownInput;

        private string DefaultCharacterText = "-- Character";

        public Wave Wave { get; private set; }

        public void Initialize(Wave wave)
        {
            InitializeEnemyDropdown();

            this.Wave = wave == null ? new Wave() : wave;

            this.EnemyDropdown.value = (int)this.Wave.Enemy;
            this.LevelInput.text = this.Wave.Level.ToString();
            this.QuantityInput.text = this.Wave.Quantity.ToString();
            this.InitialCountDownInput.text = this.Wave.InitialCountdown.ToString();
        }

        public void SetLevel(string text)
        {
            if (text == string.Empty)
            {
                text = "0";
            }

            this.Wave.Level = int.Parse(text);
        }

        public void SetQuantity(string text)
        {
            if (text == string.Empty)
            {
                text = "0";
            }

            this.Wave.Quantity = int.Parse(text);
        }

        public void SetInitialCountDown(string text)
        {
            if (text == string.Empty)
            {
                text = "0";
            }

            this.Wave.InitialCountdown = float.Parse(text);
        }

        public void SetEnemy(int enemy)
        {
            this.Wave.Enemy = (EnemyEnum)enemy;
        }

        private void InitializeEnemyDropdown()
        {
            this.EnemyDropdown.ClearOptions();

            var optionData = new List<Dropdown.OptionData>();

            optionData.Add(new Dropdown.OptionData
            {
                text = DefaultCharacterText
            });

            var enumList = Enum.GetNames(typeof(EnemyEnum))
            .Where(x => x != "None");

            foreach (var enemy in enumList)
            {
                optionData.Add(new Dropdown.OptionData
                {
                    text = enemy.ToString()
                });
            }

            this.EnemyDropdown.AddOptions(optionData);
        }
    }
}