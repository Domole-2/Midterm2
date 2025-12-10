using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GMScript : MonoBehaviour
{
    public int lastPlayerAttackDamage;
    public int playerHP;
    public int enemyHP;
    public TextMeshProUGUI informationText;
    public bool hasGameEnded;
    public bool isPlayerTurn;
    public bool playerDefended;
    public bool skillAvailable;
    public Button attackButton;
    public Button defendButton;
    public Button skillButton;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = 1000;
        enemyHP = 1000;
        hasGameEnded = false;
        isPlayerTurn = true;
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

        if(hasGameEnded == false)
        {
            informationText.text = "Player HP: " + playerHP + "\nEnemy HP: " + enemyHP;

            if(playerHP <= 0)
            {
                Destroy(player);
                informationText.text = "You lose...";
                hasGameEnded = true;
            }
            else if(enemyHP <= 0)
            {
                Destroy(enemy);
                informationText.text = "You win!";
                hasGameEnded = true;
            }

            if(isPlayerTurn == true)
            {
                Debug.Log("Player's Turn!");

                attackButton.interactable = true;
                defendButton.interactable = true;

                if(skillAvailable == true)
                {
                    skillButton.interactable = true;
                }

                else if(skillAvailable == false)
                {
                    skillButton.interactable = false;
                }
            }
            else if (isPlayerTurn == false)
            {
                Debug.Log("Enemy's Turn!");
                attackButton.interactable = false;
                defendButton.interactable = false;
                skillButton.interactable = false;
                EnemyAttack(Random.Range(150, 301), Random.Range(1,5));
            }
        }
        
    }

    public void PlayerAttack()
    {
        Debug.Log("Player Attacks!");
        lastPlayerAttackDamage = Random.Range(200, 301);
        enemyHP -= lastPlayerAttackDamage;

        isPlayerTurn = false;
    }
   
    public void PlayerDefend()
    {
        Debug.Log("Player Defended!");
        playerDefended = true;
        skillAvailable = true;
        isPlayerTurn = false;
    }

    public void PlayerSkill()
    {
        Debug.Log("Player uses a skill!");
        lastPlayerAttackDamage = Random.Range(300, 501);
        enemyHP -= lastPlayerAttackDamage;
        skillAvailable = false;
        isPlayerTurn = false;
    }

    public void EnemyAttack(int damage, int criticalCheck)
    {
        if (playerDefended == false)
        {
            playerHP -= Random.Range(150, 301);
        }
        else if(playerDefended == true)
        {
            playerHP -= Random.Range(50, 101);
        }
        isPlayerTurn = true;
        
        if (criticalCheck == 4)
        {
            damage *= 2;
            Debug.Log("The enemy swings a powerful blow!");
        }

        if (playerDefended == true)
        {
            playerHP -= damage / 2;
        }

        else if (playerDefended == false)
        {
            playerHP -= damage;
        }
    }
}
