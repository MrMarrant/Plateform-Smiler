using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthText;
    public GameObject damageText;

    private Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>(); // Find the first object of type Canvas
    }

    private void OnEnable()
    {
        CharacterEvent.CharacterOnDamage += UICharacterOnDamage;
        CharacterEvent.CharacterOnHeal += UICharacterOnHeal;
    }

    private void OnDisable()
    {
        CharacterEvent.CharacterOnDamage += UICharacterOnDamage;
        CharacterEvent.CharacterOnHeal += UICharacterOnHeal;
    }

    public void UICharacterOnDamage(GameObject character, float damage)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damage.ToString();
    }

    public void UICharacterOnHeal(GameObject character, float heal)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthText, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = heal.ToString();
    }
}
