namespace Assets.Scripts.Towers
{
    using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        public float speed = 15f;
        public Transform target;
        public float damage = 1f;
        public float radius = 0;


        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            if (this.target == null)
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            Vector3 dir = this.target.position - this.transform.localPosition;

            if (dir.magnitude <= speed)
            {
                this.transform.position = target.position;

                HitEnemy();
            }
            else
            {
                // TODO: Consider ways to smooth this motion.
                transform.Translate(dir.normalized * this.speed, Space.World);
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
            }
        }

        private void HitEnemy()
        {
            //if (this.radius == 0)
            //{
            //    target.GetComponent<Enemy>().TakeDamage(this.damage);
            //    Destroy(this.gameObject);
            //}
            //else
            //{
            //    Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            //    foreach (Collider c in cols)
            //    {
            //        Enemy e = c.GetComponent<Enemy>();
            //        if (e != null)
            //        {
            //            // TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
            //            e.GetComponent<Enemy>().TakeDamage(damage);
            //        }
            //    }
            //}
        }
    }
}