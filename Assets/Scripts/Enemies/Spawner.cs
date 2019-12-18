﻿namespace Assets.Scripts.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        public GameObject EnemyPrefab;

        public float TimeBetweenWaves = 5f;

        private float countdown = 2f;

        private List<Wave> waves;

        // Start is called before the first frame update
        void Start()
        {
            this.waves = FindObjectOfType<GameManager>().level.Waves;
        }

        // Update is called once per frame
        void Update()
        {
            if (countdown <= 0f)
            {
                this.GetNextWave();
                countdown = TimeBetweenWaves;
            }

            countdown -= Time.deltaTime;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            Debug.Log("Wave incoming");
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
    }
}