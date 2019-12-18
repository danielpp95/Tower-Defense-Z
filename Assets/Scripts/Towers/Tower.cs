namespace Assets.Scripts.Towers
{
    using Assets.Scripts.Enemies;
    using UnityEngine;

    public abstract class Tower : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float Damage = 1;
        public float Radius = 0;
        public float Range = 10f;
        public float BulletSpeed = 1f;

        private Enemy enemy;
        private Vector3 enemyDirection;

        protected float fireCooldown = 0.5f;
        protected float fireCooldownLeft = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.FindEnemy();

            this.LookAtEnemy();

            ShootAt(this.enemy);
        }

        private void FindEnemy()
        {
            if (this.enemy == null)
            {
                var enemies = GameObject.FindObjectsOfType<Enemy>();

                var nearestEnemy = (Enemy)default;

                float dist = Mathf.Infinity;

                foreach (var e in enemies)
                {
                    float d = Vector3.Distance(this.transform.position, e.transform.position);
                    if (nearestEnemy == null || d < dist)
                    {
                        nearestEnemy = e;
                        dist = d;
                    }
                }

                this.enemy = nearestEnemy;

                this.LookAtEnemy();
            }
        }

        private void LookAtEnemy()
        {
            if (this.enemy != null)
            {
                this.enemyDirection = this.enemy.transform.position - this.transform.position;

                Quaternion lookRot = Quaternion.LookRotation(this.enemyDirection); ;

                this.transform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
            }
        }

        protected void ShootAt(Enemy enemy)
        {
            if (this.enemy != null)
            {
                fireCooldownLeft -= Time.deltaTime;
                if (fireCooldownLeft <= 0 && this.enemyDirection.magnitude <= this.Range)
                {
                    fireCooldownLeft = fireCooldown;

                    var shootPoint = this.transform.GetComponentInChildren<ShootPoint>().transform.position;

                    var bulletGO = Instantiate(BulletPrefab, shootPoint, this.transform.rotation);

                    var bullet = bulletGO.GetComponent<Bullet>();
                    bullet.target = this.enemy.transform;
                    bullet.damage = this.Damage;
                    bullet.radius = this.Radius;
                    bullet.speed = this.BulletSpeed;
                }
            }
        }
    }
}