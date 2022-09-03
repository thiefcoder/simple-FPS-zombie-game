using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float MaxHealth=100f;
    public bool isDead = false;
    public Slider healthBar;
    public Text healthcounter;
    [Header ("Damage Screen")]
    public Color damageScreenColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    bool isTakingDamage = false;
    [Header("Player Respawn")]
    public Transform playerRespawn;
    Animator animator;

    private void Awake()
    {
        singleton = this;
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.value = currentHealth;
        healthcounter.text = currentHealth.ToString();
        animator.enabled = false;
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            damageImage.gameObject.SetActive(true);
            damageImage.color = damageScreenColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        isTakingDamage = false;
        updateHealthUI();
    }

    public void DamagePlayer(float damage)
    {
        if(currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                isTakingDamage = true;
                Dead();
            }
            else
            {
                isTakingDamage = true;
                currentHealth -= damage;
            }
            updateHealthUI();
        }
    }

    public void AddHealth(float healthAmmount)
    {
        currentHealth += healthAmmount;
        updateHealthUI();
    }

    void Dead()
    {
        isDead = true;
        Debug.Log("player is Dead");
        healthBar.value = 0;
        currentHealth =0;
        StartCoroutine(PlayerRespawn());
    }

    void revive()
    {
        isDead = false;
        healthBar.value = MaxHealth;
        currentHealth = MaxHealth;
        updateHealthUI();
        animator.enabled = false;
    }

    public void updateHealthUI()
    {
        healthcounter.text = currentHealth.ToString();
        healthBar.value = currentHealth;
    }

    #region PLAYER RESPAWN
    IEnumerator PlayerRespawn()
    {
        animator.enabled = true;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(2);
        transform.position = playerRespawn.position;
        animator.SetTrigger("revive");
        animator.enabled = false;
        revive();
    }
    #endregion
}
