﻿namespace Assets.Scripts.Enemies
{
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.LevelEditor.LevelScene;
    using UnityEngine;

    public class Enemy : MonoBehaviour
    {
        public float speed = 5f;
        public GameObject HitPoint;

        private Vector3 target;

        private List<Vector2Int> path;

        // Start is called before the first frame update
        void Start()
        {
            this.GetPath();
            this.GetNextTarget();
        }

        // Update is called once per frame
        void Update()
        {
            var direction = target - this.transform.position;
            transform.Translate(direction.normalized * this.speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) <= 0.2f)
            {
                this.GetNextTarget();
            }
        }

        private void GetNextTarget()
        {
            if (path.Count > 0)
            {
                target = new Vector3(path.First().x * 10, 0, path.First().y * 10);
                path.Remove(path.First());
            }
            else
            {
                // Todo: lost live
                Destroy(this.gameObject);
            }
        }

        private void GetPath()
        {
            var tilemap = FindObjectOfType<TilemapController>().tileMap;
            this.path = new List<Vector2Int>();
            this.path.AddRange(tilemap.FollowingPath);
            this.path.Add(tilemap.EndPoint);

        }

        public void TakeDamage(float damage)
        {

        }
    }
}