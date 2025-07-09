using UnityEngine;

public class UnitStats : MonoBehaviour
{
    //A script to apply to enemy prefabs and player
    // A house for stats

    public string unitName;

    public int maxHealth;
    public int currentHealth;
    public int minHealth;
    public int attackBasicDamage;
    [Range(0f, 1f)] public float hitChance = 0.8f;


    public int xpAmount;
    public int meatAmount;

    public string loreText;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }


    private void Awake()
    {
       currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        Debug.Log(unitName + " has been defeated");
        Destroy(gameObject);
    }


}
