using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StoryDialog : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timer = 0;
    private bool isShowingDialog = false;
    public float dialogTimeOut = 3f;
    public float fadeTime = 5f;
    private float lerpTimer = 0f;
    private Color32 colorA = new Color32(255, 255, 255, 0);
    private Color32 colorB = new Color32(255, 255, 255, 255);

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = colorA;
    }

    private void Update()
    {
        if (isShowingDialog) timer += Time.deltaTime;
        if (timer >= dialogTimeOut)
        {
            isShowingDialog = false;
            timer = 0f;
            lerpTimer = fadeTime;
        }
        if (isShowingDialog)
        {
            lerpTimer += Time.deltaTime;
            lerpTimer = Mathf.Clamp(lerpTimer, 0, fadeTime);
            text.color = Color.Lerp(colorA, colorB, lerpTimer / fadeTime);
        } else {
            lerpTimer -= Time.deltaTime;
            lerpTimer = Mathf.Clamp(lerpTimer, 0, fadeTime);
            text.color = Color.Lerp(colorA, colorB, lerpTimer / fadeTime);
        }
    }

    public void ShowDialog1()
    {
        text.text = "Delay the inevitable..";
        isShowingDialog = true;
        lerpTimer = timer = 0f;
    }

    public void ShowDialog2()
    {
        text.text = "Keep it up..";
        isShowingDialog = true;
        lerpTimer = timer = 0f;
    }

    public void ShowDialog3()
    {
        text.text = "You're almost out..";
        isShowingDialog = true;
        lerpTimer = timer = 0f;
    }

    public void ShowDialog4()
    {
        text.text = "You've escaped!";
        isShowingDialog = true;
        lerpTimer = timer = 0f;
    }

    public void ShowDialogLose()
    {
        text.text = "You'll never escape!";
        isShowingDialog = true;
        lerpTimer = timer = 0f;
    }
}
