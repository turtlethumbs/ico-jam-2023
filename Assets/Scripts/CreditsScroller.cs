using System.Collections;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 30f;

    private RectTransform creditsRectTransform;
    private float screenHeight;
    private bool isScrolling = false;

    private void Start()
    {
        creditsRectTransform = GetComponent<RectTransform>();
        screenHeight = creditsRectTransform.rect.height;
        StartScrolling();
    }

    private void Update()
    {
        // Enable the player to navigate out of Credits Screen
        if (Input.GetButtonDown("Fire1"))
        {
            SceneNavigator.GoToMainMenu();
        }
    }

    public void StartScrolling()
    {
        if (!isScrolling)
        {
            StartCoroutine(ScrollCoroutine());
        }
    }

    private IEnumerator ScrollCoroutine()
    {
        isScrolling = true;

        while (creditsRectTransform.anchoredPosition.y < screenHeight)
        {
            creditsRectTransform.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);
            yield return null;
        }

        isScrolling = false;
    }
}
