using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;
public class ChangeTextDelayed : MonoBehaviour
{
    // Later Text settings
    public string[] laterStrings;
    public Text laterText;
    public float displayTime = 3f;
    public float intervalTime = 1f;
    private int sentenceIndex = 0;
    private bool isDisplaying = false;

    // Intro Text settings
    public Text introText;
    public Text chooseText;
    public string[] introStrings;
    public string[] chooseStrings;
    public GameObject UITextBlock;
    public float delay = 3f;
    public float fadeDuration = 1f;
    private int currentStringIndex;
    private bool isPlaying = true;
    private bool isChoose = false;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject SymbolUI;
    public GameObject[] Buttons;
    public GameObject ChestSkipStory;
    public PlayableDirector timeline;
    public PlayableDirector ChooseTimeline;
    public GameObject skipButton;

    public GameObject[] ChooseButtons;

    private IEnumerator Start()
    {
        yield return StartCoroutine(FadeTextOut(introText, fadeDuration));
        while (isPlaying == true)
        {
            introText.text = introStrings[currentStringIndex];
            yield return StartCoroutine(FadeTextIn(introText, fadeDuration));
            yield return new WaitForSecondsRealtime(delay);
            yield return StartCoroutine(FadeTextOut(introText, fadeDuration));
            currentStringIndex = (currentStringIndex + 1) % introStrings.Length;
            if (currentStringIndex == introStrings.Length - 1) StopPlaying();
        }
        introText.gameObject.SetActive(false);
    }
    public void ChooseText()
    {
        ChooseTimeline.Pause();
        if (isChoose == true) return;
        isChoose = true;
        chooseText.gameObject.SetActive(true);
        StartCoroutine(DisplayChooseText());
    }
    private IEnumerator DisplayChooseText()
    {
        yield return StartCoroutine(FadeTextOut(chooseText, fadeDuration));
        while (currentStringIndex < chooseStrings.Length)
        {
            chooseText.text = chooseStrings[currentStringIndex];
            yield return StartCoroutine(FadeTextIn(chooseText, fadeDuration));
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(FadeTextOut(chooseText, fadeDuration));
            currentStringIndex++;
        }
        chooseText.gameObject.SetActive(false);
        if (currentStringIndex == introStrings.Length - 1)
        {
            StopPlaying();
        }
        ChooseTimeline.Resume();
    }
    public void ViewButtons()
    {
        chooseText.gameObject.SetActive(true);
        chooseText.color = new Color(255, 255, 255, 255);
        chooseText.text = "Выбирай";
        foreach (GameObject go in ChooseButtons)
        {
            go.SetActive(true);
        }
    }
    private IEnumerator FadeTextOut(Text text, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / duration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (elapsedTime >= duration)
        {
            yield return new WaitForSeconds(0.1f);
            text.color = targetColor;
            text.text = "";
            if (sentenceIndex == 0)
            {
                text.color = targetColor;
                yield return new WaitForSeconds(intervalTime);
            }
            else if (sentenceIndex == introStrings.Length - 1) StopPlaying();
        }
    }
    private IEnumerator FadeTextIn(Text text, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        while (elapsedTime < duration)
        {
            float alpha = elapsedTime / duration;
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            text.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.color = targetColor;
    }
    public void StopPlaying()
    {
        isPlaying = false;
        isChoose = false;
    }
    public void OnTimelineFinished()
    {
        UITextBlock.SetActive(true);
        StartCoroutine(DisplaySentences());
    }
    public void StartSoundEp()
    {
        audioSource.volume = 0f;
        audioSource.PlayOneShot(audioClip);
        StartCoroutine(FadeInAudio(audioSource, fadeDuration));
        foreach (GameObject go in Buttons)
        {
            go.SetActive(true);
        }
    }
    private IEnumerator FadeInAudio(AudioSource audioSource, float duration = 10f)
    {
        float elapsedTime = 0f;
        float startVolume = 0f;
        float targetVolume = 0.5f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
    private IEnumerator DisplaySentences()
    {
        yield return new WaitForSeconds(1f);
        isDisplaying = true;
        Color originalColor = laterText.color;
        string currentSentence = "";
        for (int i = 0; i < laterStrings.Length; i++)
        {
            currentSentence = laterStrings[i];
            laterText.text = "";
            yield return StartCoroutine(TypeSentence(laterText, currentSentence));
            yield return new WaitForSeconds(displayTime);
        }

        laterText.text = "";
        isDisplaying = false;
        if (!isDisplaying)
        {
            UITextBlock.SetActive(false);
            laterText.gameObject.SetActive(false);
            SymbolUI.SetActive(true);
            ChestSkipStory.SetActive(true);
        }
    }
    private IEnumerator TypeSentence(Text text, string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void PauseTimeline()
    {
        timeline.Pause();
        skipButton.SetActive(true);
    }
    public void ResumeTimeline()
    {
        timeline.Play();
        skipButton.SetActive(false);
    }
}