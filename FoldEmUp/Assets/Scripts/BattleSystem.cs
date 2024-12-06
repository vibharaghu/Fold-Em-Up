using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public GameObject enemy;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject attackButton;
    public GameObject shieldButton;

    public LogicScript logic;
    public GameObject shield;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        player.transform.position = playerBattleStation.position;
        playerUnit = player.GetComponent<Unit>();
        enemy.transform.position = enemyBattleStation.position;
        enemyUnit = enemy.GetComponent<Unit>();

        HideButtons();

        dialogueText.text = "A new " + enemyUnit.unitName + " approaches!";

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void PlayerTurn()
    {
        if (playerUnit.shields <= 0)
            ShowButtons(false);
        else
            ShowButtons(true);
        dialogueText.text = "Choose an action: ";
    }

    public IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        HideButtons();

        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            dialogueText.text = "You defeated the enemy!";

            yield return new WaitForSeconds(3f);
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You deal " + playerUnit.damage + " damage!";

            yield return new WaitForSeconds(3f);
            StartCoroutine(EnemyTurn());
        }
    }

    public IEnumerator PlayerShield()
    {
        playerUnit.UseShield();
        state = BattleState.ENEMYTURN;
        HideButtons();
        dialogueText.text = "You used your shield to block the next blow!";
        shield.transform.position = new Vector3(-1.63f, -1.5f, 2f);

        yield return new WaitForSeconds(3f);

        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);

        dialogueText.text = "You were dealt " + enemyUnit.damage + " damage!";
        yield return new WaitForSeconds(1f);

        if (playerUnit.shielded)
        {
            dialogueText.text = "Your shield blocked the attack!";
            shield.SetActive(false);
        }

        bool isDead = playerUnit.TakeDamage(playerUnit.damage);

        if (isDead)
        {
            state = BattleState.LOST;
            playerHUD.SetHP(playerUnit.currentHP = 0);
            dialogueText.text = "You were defeated!";

            yield return new WaitForSeconds(3f);
            StartCoroutine(EndBattle());
        }
        else
        {
            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnShieldButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerShield());
    }

    public IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Congratulations on your first victory!";
            yield return new WaitForSeconds(3f);
            //head back to the village
            logic.ReturnToVillage(new Vector3(-21.28f, -3.66f, 3.88f));
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle and fall unconcious...";
            yield return new WaitForSeconds(3f);
            //reload scene to try again
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    public void HideButtons()
    {
        attackButton.SetActive(false);
        shieldButton.SetActive(false);
    }

    public void ShowButtons(bool showShieldButton)
    {
        attackButton.SetActive(true);
        if (showShieldButton)
        {
            shieldButton.SetActive(true);
        }
    }
}
