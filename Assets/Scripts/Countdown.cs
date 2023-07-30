using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Countdown : MonoBehaviour
{
    public HourGlass hourglass;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (hourglass.isSelfDestructing)
        {
            text.text = "";
        }
        else if (hourglass.isCountingDown)
            switch(hourglass.timeUntilSelfDestruct)
            {
                case 3:
                    text.text = "3";
                    break;
                case 2:
                    text.text = "2";
                    break;
                case 1:
                    text.text = "1";
                    break;
                default:
                    text.text = "";
                    break;
            }
        else
        {
            text.text = "";
        }
    }
}
