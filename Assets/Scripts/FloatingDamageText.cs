using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class FloatingDamageText : MonoBehaviour
{

    public float floatSpeed = 1f;
    public float fadeDuration = 1f;

    private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Color c = textMesh.color;
            c.a = Mathf.Lerp(0, originalColor.a, timer / fadeDuration);
            textMesh.color = c;
        }

    }

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
            Debug.LogError("TextMeshProUGUI not found on DamageText prefab!");
    }

    public void SetText(int damageAmount)
    {

        textMesh.text = "-" + damageAmount.ToString();
        timer = fadeDuration;
        originalColor.a = 1;
        textMesh.color = originalColor;

    }



}

