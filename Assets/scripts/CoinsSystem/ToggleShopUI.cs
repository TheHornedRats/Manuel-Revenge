using UnityEngine;
using System.Collections;

public class ToggleShopUI : MonoBehaviour
{
    public GameObject shopPanel;
    public float slideDuration = 0.3f;

    private RectTransform shopRectTransform;
    private Vector2 hiddenPosition = new Vector2(-800f, 0);
    private Vector2 visiblePosition = Vector2.zero;

    private bool isShopOpen = true;

    void Start()
    {
        shopRectTransform = shopPanel.GetComponent<RectTransform>();
        shopPanel.SetActive(true);
        shopRectTransform.anchoredPosition = visiblePosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isShopOpen)
                StartCoroutine(SlideOut());
            else
                StartCoroutine(SlideIn());
        }
    }

    IEnumerator SlideIn()
    {
        shopPanel.SetActive(true);
        float elapsed = 0f;
        Vector2 start = hiddenPosition;
        Vector2 end = visiblePosition;

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            shopRectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        shopRectTransform.anchoredPosition = end;
        isShopOpen = true;
    }

    IEnumerator SlideOut()
    {
        float elapsed = 0f;
        Vector2 start = shopRectTransform.anchoredPosition;
        Vector2 end = hiddenPosition;

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            shopRectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        shopRectTransform.anchoredPosition = end;
        shopPanel.SetActive(false);
        isShopOpen = false;
    }
}
