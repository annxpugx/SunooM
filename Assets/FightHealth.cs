using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightHealth : MonoBehaviour
{
    public const float MAX_HEALTH = 24;

    public GameObject playerHealthIndicator;
    public GameObject enemyHealthIndicator;

    [HideInInspector] public float playerHealth;
    [HideInInspector] public float enemyHealth;

    private Image[] _playerHearts;
    private Image[] _enemyHearts;

    void Start()
    {
        playerHealth = enemyHealth = MAX_HEALTH;
        _playerHearts = playerHealthIndicator.GetComponentsInChildren<Image>();
        _enemyHearts = enemyHealthIndicator.GetComponentsInChildren<Image>();
    }

    public float InflictDamageOnPlayer(float damage)
    {
        playerHealth -= damage;
        UpdateHearts(_playerHearts, Math.Round(playerHealth));

        if (RemainingHearts(_playerHearts) == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        return playerHealth + damage;
    }

    public float InflictDamageOnEnemy(float damage)
    {
        enemyHealth -= damage;
        UpdateHearts(_enemyHearts, Math.Round(enemyHealth));

        if (RemainingHearts(_enemyHearts) == 0) SceneManager.LoadScene("nightScene");
        
        return enemyHealth + damage;
    }

    private int RemainingHearts(Image[] hearts)
    {
        int found = 0;
        foreach (var heart in hearts)
        {
            if (heart.enabled) found++;
        }

        return found;
    }

    private void UpdateHearts(Image[] hearts, double remainingHearts)
    {
        int currentHearts = hearts.Length;
        while (currentHearts > remainingHearts)
        {
            if (currentHearts <= 0) return;
            hearts[--currentHearts].enabled = false;
        }
    }
}