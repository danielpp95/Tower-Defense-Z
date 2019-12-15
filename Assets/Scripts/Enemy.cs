using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float lifePoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        this.lifePoint -= damage;

        if (this.lifePoint <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        // TODO: add score and money on death
        Destroy(this.gameObject);
    }
}
