namespace Assets.Scripts.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Manager;
    using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        public GameObject EnemyPrefab;

        private float TimeBetweenWaves = 8f;

        private float countdown = 2f;
        private int actualWave = 0;
        private List<Wave> waves;
        private GameManager gameManager;

        private bool isEditing = true;

        // Start is called before the first frame update
        void Start()
        {
            if (!this.isEditing)
            {
                this.gameManager = FindObjectOfType<GameManager>();
                this.waves = this.gameManager.level.Waves;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.isEditing)
            {
                if (countdown <= 0f)
                {
                    this.GetNextWave();
                    countdown = TimeBetweenWaves;
                }

                this.SetTimer();

                this.countdown -= Time.deltaTime;
            }
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            this.actualWave++;
            this.gameManager.SetActualWave(this.actualWave);

            for (int i = 0; i < wave.Quantity; i++)
            {
                Instantiate(this.EnemyPrefab);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void GetNextWave()
        {
            if (this.waves.Count > 0)
            {
                var wave = this.waves.First();

                StartCoroutine(this.SpawnWave(wave));

                this.waves.Remove(this.waves.First());
            }
        }

        private void SetTimer()
        {
            if (this.countdown <= 6f && this.countdown > 0f)
            {
                this.gameManager.SetTimer((int)this.countdown);
                this.gameManager.TimerContainer.SetActive(true);
            }
            else
            {
                this.gameManager.TimerContainer.SetActive(false);
            }
        }
    }
}