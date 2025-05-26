using System.Collections;
using UnityEngine;
using TMPro;

public class IntroText : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI continueText;
    public GameObject backgroundPanel;

    [Header("Settings")]
    [TextArea(3, 10)]
    public string fullText;
    public float typingSpeed = 0.05f;
    public float autoHideTime = 30f;

    private bool textFinished = false;
    private bool skipped = false;

    void Start()
    {
        // Panel ve yazýlarý baþlangýçta hazýrla
        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);

        mainText.text = "";
        continueText.gameObject.SetActive(false);

        StartCoroutine(TypeText());
        Invoke("AutoHide", autoHideTime);
    }

    void Update()
    {
        if (textFinished && !skipped && Input.anyKeyDown)
        {
            HideTexts();
        }
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            mainText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        textFinished = true;
        continueText.gameObject.SetActive(true);
    }

    void AutoHide()
    {
        if (!skipped)
        {
            HideTexts();
        }
    }

    void HideTexts()
    {
        skipped = true;

        // UI elemanlarýný kapat
        mainText.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);

        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);
    }
}
