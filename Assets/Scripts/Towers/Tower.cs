namespace Assets.Scripts.Towers
{
    using Assets.Scripts.Enemies;
    using UnityEngine;

    public abstract class Tower : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public GameObject RotationPart;
        public float Damage = 1;
        public float Radius = 0;
        public float Range = 10f;
        public float BulletSpeed = 1f;

        public float RotationSpeed = 10f;

        private HitPoint target;
        private Vector3 targetDirection;

        protected float fireCooldown = 0.5f;
        protected float fireCooldownLeft = 0;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
            {
                FindTarget();
            }

            if (this.target != null)
            {
                this.LookAtTarget();
                this.ShootAt(this.target);
            }
        }

        private void FindTarget()
        {
            if (this.target == null)
            {
                var enemies = GameObject.FindObjectsOfType<HitPoint>();

                var nearestTarget = (HitPoint)default;

                float dist = Mathf.Infinity;

                foreach (var e in enemies)
                {
                    float d = Vector3.Distance(this.transform.position, e.transform.position);
                    if (nearestTarget == null || d < dist)
                    {
                        nearestTarget = e;
                        dist = d;
                    }
                }

                this.target = nearestTarget;
            }
        }

        private void LookAtTarget()
        {
            this.targetDirection = this.target.transform.position - this.transform.position;

            var lookRotation = Quaternion.LookRotation(this.targetDirection);

            var rotation = Quaternion.Lerp(
                this.RotationPart.transform.rotation,
                lookRotation,
                Time.deltaTime * this.RotationSpeed).eulerAngles;

            this.RotationPart.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        }

        protected void ShootAt(HitPoint enemy)
        {
            fireCooldownLeft -= Time.deltaTime;
            if (fireCooldownLeft <= 0 && this.targetDirection.magnitude <= this.Range)
            {
                fireCooldownLeft = fireCooldown;

                var shootPoint = this.transform.GetComponentInChildren<ShootPoint>().transform.position;

                var bulletGO = Instantiate(this.BulletPrefab, shootPoint, Quaternion.identity);

                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.target = this.target.transform;
                bullet.damage = this.Damage;
                bullet.radius = this.Radius;
                bullet.speed = this.BulletSpeed;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.RotationPart.transform.position, Range);
        }
    }
}