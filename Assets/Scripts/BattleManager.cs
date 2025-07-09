using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Timeline;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public GameObject[] testPrefabs;
    public GameObject[] tierOnePrefabs;
    public GameObject[] tierTwoPrefabs;
    public GameObject[] tierThreePrefabs;



    public GameObject playerPrefab;

    public Transform monsterSpawnPoint;
    public Transform playerSpawnPoint;

    private GameObject currentMonster;
    private GameObject currentPlayer;

    private UnitStats playerUnit;
    private UnitStats enemyUnit;

    public PlayerData playerData;

    private enum BattleState { START, PLAYER_TURN, MONSTER_TURN, WIN, LOSE, BUSY }
    private BattleState state;

    [SerializeField] public GameObject damageText;
    [SerializeField] public GameObject missText;


    //UI

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerManaText;
    
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyLoreText;

    //Battle Conclusion Text

    public GameObject battleOverWin;
    public TextMeshProUGUI winMeatText;
    public TextMeshProUGUI winXPText;

    public GameObject battleOverLose;


    void Start()
    {
        SetUpBattle();
    }

    void Update()
    {
        if (state != BattleState.PLAYER_TURN) return;

        playerHealthText.text = $"{playerUnit.unitName}: {playerUnit.currentHealth} HP";
        playerManaText.text = $"Mana: {playerData.currentMana}/ {playerData.maxMana}";
    }

    void SetUpBattle()
    {
        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerUnit = currentPlayer.GetComponent<UnitStats>();

        playerUnit.maxHealth = playerData.maxHealth;
        playerUnit.currentHealth = playerData.currentHealth;
        playerUnit.unitName = playerData.playerName;
        playerUnit.attackBasicDamage = playerData.attackBasicDamage;

        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        currentMonster = Instantiate(monsterPrefabs[randomIndex], monsterSpawnPoint.position, Quaternion.identity);
        enemyUnit = currentMonster.GetComponent<UnitStats>();

        //Quest Triggers

        if (enemyUnit.unitName == "Sword in Stone")
        {
            playerData.SwordInStoneQuest();
        }
        if (enemyUnit.unitName == "Arc of the Covenant")
        {
            playerData.ManaQuestTrigger();
        }
        if (enemyUnit.unitName == "Gates of Eden")
        {
            playerData.GatesOfEdenTrigger();
        }
        
       
        enemyHealthText.text = $"{enemyUnit.unitName}: {enemyUnit.currentHealth} HP";
        enemyLoreText.text = $"{enemyUnit.loreText}";

        state = BattleState.PLAYER_TURN;
    }


    IEnumerator PlayerAttackRoutine()
    {
        state = BattleState.BUSY;

        Vector3 originalPos = playerUnit.transform.position;
        AudioManager.instance.PlayPlayerHit();
        Vector3 attackOffset = new Vector3(0.5f, 0, 0);



        yield return StartCoroutine(AttackAnimation(playerUnit.transform, originalPos, attackOffset));
        //yield return new WaitForSeconds(0.2f);

        if (Random.value <= playerData.hitChance)
        {
            bool monsterDead = enemyUnit.TakeDamage(playerUnit.attackBasicDamage);
            ShowFloatingDamage(playerUnit.attackBasicDamage, enemyUnit.transform.position + Vector3.up * 2f);
            enemyHealthText.text = $"{enemyUnit.unitName}: {enemyUnit.currentHealth} HP";
            Debug.Log("Hit!");

            yield return new WaitForSeconds(1f);


            if (monsterDead)
            {
                playerData.currentHealth = playerUnit.currentHealth;
                playerData.experience += enemyUnit.xpAmount;
                playerData.meat += enemyUnit.meatAmount;
                playerData.timeTokenCurrent += 2;
                AudioManager.instance.PlayVictorySound();
                ShowWinMenu();
                yield break;
            }
        }
        else
        {
            ShowFloatingMiss(enemyUnit.transform.position + Vector3.up * 2f);
            Debug.Log("Miss!");
            yield return new WaitForSeconds(1f);
        }
            state = BattleState.MONSTER_TURN;
        StartCoroutine(MonsterAttackRoutine());
    }

    IEnumerator PlayerAbilityRoutine()
    {
        state = BattleState.BUSY;

        playerData.currentMana -= 5;  // Spend mana

        Vector3 originalPos = playerUnit.transform.position;
        Vector3 attackOffset = new Vector3(0.5f, 0, 0);

        yield return StartCoroutine(AttackAnimation(playerUnit.transform, originalPos, attackOffset));

        yield return new WaitForSeconds(0.1f);

        int abilityDamage = playerUnit.attackBasicDamage * 2;  // Double damage

        bool monsterDead = enemyUnit.TakeDamage(abilityDamage);
        ShowFloatingDamage(abilityDamage, enemyUnit.transform.position + Vector3.up * 2f);
        enemyHealthText.text = $"{enemyUnit.unitName}: {enemyUnit.currentHealth} HP";

        yield return new WaitForSeconds(0.2f);

        if (monsterDead)
        {
            playerData.currentHealth = playerUnit.currentHealth;
            playerData.experience += enemyUnit.xpAmount;
            playerData.meat += enemyUnit.meatAmount;
            playerData.experience += enemyUnit.xpAmount;
            playerData.timeTokenCurrent += 2;
            AudioManager.instance.PlayVictorySound();
            ShowWinMenu();

        }
        else
        {
            state = BattleState.MONSTER_TURN;
            StartCoroutine(MonsterAttackRoutine());
        }
    }

    IEnumerator MonsterAttackRoutine()
    {
        state = BattleState.BUSY;

        Vector3 originalPos = enemyUnit.transform.position;
        AudioManager.instance.PlayEnemyHit();
        Vector3 attackOffset = new Vector3(-0.5f, 0, 0);

        yield return StartCoroutine(AttackAnimation(enemyUnit.transform, originalPos, attackOffset));
        //yield return new WaitForSeconds(0.2f);

        if (Random.value <= enemyUnit.hitChance)
        {
            bool playerDead = playerUnit.TakeDamage(enemyUnit.attackBasicDamage);
            ShowFloatingDamage(enemyUnit.attackBasicDamage, playerUnit.transform.position + Vector3.up * 2f);

            playerData.currentHealth = playerUnit.currentHealth;

            playerHealthText.text = $"{playerUnit.unitName}: {playerUnit.currentHealth} HP";
            Debug.Log("Hit!");

            yield return new WaitForSeconds(1f);


            if (playerDead)
            {
                state = BattleState.LOSE;
                playerData.ResetStats();
                AudioManager.instance.PlayDeathSound();
                ShowLoseMenu();
            }
            else
            {

                state = BattleState.PLAYER_TURN;
            }
        }
        else
        {
            ShowFloatingMiss(playerUnit.transform.position + Vector3.up * 2f);
            Debug.Log("Miss!");
            state = BattleState.PLAYER_TURN;
        }
    }

    void RunFromBattle()
    {
        Debug.Log("You ran away!");
        playerData.timeTokenCurrent += 1;
        SceneManager.LoadScene("HomeScene");
    }

    void ShowFloatingDamage(int amount, Vector3 worldPosition)
    {
        if (damageText == null)
        {
            return;
        }

        GameObject textObj = Instantiate(damageText, worldPosition, Quaternion.identity);
        textObj.GetComponent<FloatingDamageText>().SetText(amount);
    }
    void ShowFloatingMiss(Vector3 worldPosition)
    {
        if (missText == null) return;
        GameObject textObj = Instantiate(missText, worldPosition, Quaternion.identity);
        var floatingText = textObj.GetComponent<FloatingMissText>();

        if (floatingText != null)
        {
            floatingText.SetText("Miss");
        }
    }

    IEnumerator AttackAnimation(Transform attacker, Vector3 originalPos, Vector3 attackOffset)
    {
        Vector3 targetPos = originalPos + attackOffset;
        float speed = 10f;
        float step = 0f;

        // Move forward
        while (step < 1f)
        {
            step += Time.deltaTime * speed;
            attacker.position = Vector3.Lerp(originalPos, targetPos, step);
            yield return null;
        }

        step = 0f;

        // Move back
        while (step < 1f)
        {
            step += Time.deltaTime * speed;
            attacker.position = Vector3.Lerp(targetPos, originalPos, step);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
    }

    //Display Win/Lose Menu

    public void ShowWinMenu()
    {
        if (battleOverWin != null)
        {
            battleOverWin.SetActive(true);
            winMeatText.text = $"+ {enemyUnit.meatAmount} Meat";
            winXPText.text = $"+ {enemyUnit.xpAmount} XP";
        }
    }

    public void ShowLoseMenu()
    {
        if (battleOverLose != null)
        {
            playerData.ResetStats();
            battleOverLose.SetActive(true);
        }


    }


    //Buttons
    public void OnAttackButton()
    {
        if (state == BattleState.PLAYER_TURN)
        {
            StartCoroutine(PlayerAttackRoutine());
        }
    }

    public void OnAbilityButton()
    {
        if (state == BattleState.PLAYER_TURN && playerData.currentMana >= 5)
        {
            StartCoroutine(PlayerAbilityRoutine());
        }
    }

    public void OnRunButton()
    {
        if (state == BattleState.PLAYER_TURN)
        {
            RunFromBattle();
        }
    }

    public void OnWinButton()
    {
        SceneManager.LoadScene("HomeScene");

    }

    public void OnLoseButton()
    {
        
        SceneManager.LoadScene("NewGameScene");

    }

}