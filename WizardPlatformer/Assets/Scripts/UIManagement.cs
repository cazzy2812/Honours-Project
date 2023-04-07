using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManagement : MonoBehaviour
{
    public GameObject hpTextPrefab;
    public GameObject damageTextPrefab;
    public GameObject infoTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        
    }

    private void OnEnable()
    {
        CharacterEvents.damageTaken += DamageTaken;
        CharacterEvents.hpHealed += Healing;
        CharacterEvents.provideInfo += Info;
    }

    private void OnDisable()
    {
        CharacterEvents.damageTaken -= DamageTaken;
        CharacterEvents.hpHealed -= Healing;
        CharacterEvents.provideInfo -= Info;
    }

    public void DamageTaken(GameObject character, int damageTaken)
    {
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text text = Instantiate(damageTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        text.text = damageTaken.ToString();
    }

    public void Healing(GameObject character, int hpHealed)
    {
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text text = Instantiate(hpTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        text.text = hpHealed.ToString();
    }

    public void Info(GameObject character, string information)
    {
        Vector3 spawnPoint = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text text = Instantiate(infoTextPrefab, spawnPoint, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        text.text = information;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif

            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene");
            #endif

        }
    }
}
