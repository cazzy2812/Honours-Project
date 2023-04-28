using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleUI : MonoBehaviour
{
    Damage playerDamage;
    PlayerController playerController;

    public TMP_Text collectibleText;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        collectibleText.text = playerController.CurrentCollectibles + "/" + playerController.maxCollectibles;
    }

    private void OnEnable()
    {
        playerController.collectibleUpdate.AddListener(OnPlayerCollectibleUpdate);
    }

    private void OnDisable()
    {
        playerController.collectibleUpdate.RemoveListener(OnPlayerCollectibleUpdate);
    }

    private void OnPlayerCollectibleUpdate(int currentCollectibles, int maxCollectibles)
    {
        collectibleText.text = currentCollectibles + "/" + maxCollectibles;
    }
}
