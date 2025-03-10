using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int damage;
    public int puntuacion;

    public GameObject xpPrefab;
    //public GameObject xp10Prefab;
    //public GameObject xp20Prefab;
    public float dropRange = 1.0f;
    bool isDead = false;

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Atack")
        {
            TakeDamage(damage);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(name + " sufrió " + damage + " de daño. Salud restante: " + health);

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " ha muerto.");
        isDead = true;


        DropXPItems();

        Destroy(gameObject);
        ScoreManager.instance.AddScore(5);
    }

    private void DropXPItems()
    {
        Vector3 dropPosition1 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
       // Vector3 dropPosition2 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.1f);
       // Vector3 dropPosition3 = transform.position + new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0.2f);

        // Instanciar los objetos de XP en posiciones aleatorias
        Instantiate(xpPrefab, dropPosition1 , Quaternion.identity);
        //Instantiate(xp10Prefab, dropPosition2, Quaternion.identity);
        //Instantiate(xp20Prefab, dropPosition3, Quaternion.identity);
    }
}
