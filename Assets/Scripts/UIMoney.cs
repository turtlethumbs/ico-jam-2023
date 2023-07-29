using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMoney : MonoBehaviour
{
    private GameManager gameManager;
    private TextMeshProUGUI text;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = Mathf.Floor(gameManager.money).ToString();
    }
}
