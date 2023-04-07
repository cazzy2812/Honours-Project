using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Damage playerDamage;

    public TMP_Text hpText;
    public Slider hpSlider;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamage = player.GetComponent<Damage>();
    }

    private void Start()
    {
        
        hpSlider.value = HPPercentFloat(playerDamage.CurrentHP, playerDamage.MaxHP);
        hpText.text = "HP: " + playerDamage.CurrentHP + "/" + playerDamage.MaxHP;
    }

    private float HPPercentFloat(float currentHP, float maxHP)
    {
        return currentHP / maxHP;
    }

    private void OnEnable()
    {
        playerDamage.hpUpdate.AddListener(OnPlayerHPUpdate);
    }

    private void OnDisable()
    {
        playerDamage.hpUpdate.RemoveListener(OnPlayerHPUpdate);
    }

    private void OnPlayerHPUpdate(int currentHP, int maxHP)
    {
        hpSlider.value = HPPercentFloat(currentHP, maxHP);
        hpText.text = "HP: " + currentHP + "/" + maxHP;
    }
}
