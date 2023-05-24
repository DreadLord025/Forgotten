using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject Symbol;
    public Text targetText;
    public Text playbackText;
    public Text countText;
    public string[] targetStrings;
    public string[] playbackStrings;
    private bool isDisplaying = false;
    public float displayTime = 5f;
    public AudioSource audioSourceMUSIC;
    public AudioClip audioClip;
    public float fadeDuration = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeline.Play();
            Symbol.SetActive(false);
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("SearchLists");
    }
    public void Target()
    {
        StartCoroutine(DisplayTarget());
    }
    public void Playback()
    {
        StartCoroutine(DisplayPlayback());
    }

    private IEnumerator DisplayPlayback()
    {
        yield return new WaitForSeconds(1f);
        isDisplaying = true;
        Color originalColor = playbackText.color;
        string currentSentence = "";
        for (int i = 0; i < playbackStrings.Length; i++)
        {
            currentSentence = playbackStrings[i];
            playbackText.text = "";
            yield return StartCoroutine(TypeSentence(playbackText, currentSentence));
            yield return new WaitForSeconds(displayTime);
        }
        playbackText.text = "";
        isDisplaying = false;
        if (!isDisplaying)
        {
            playbackText.gameObject.SetActive(false);
        }
    }
    private IEnumerator DisplayTarget()
    {
        yield return new WaitForSeconds(1f);
        isDisplaying = true;
        Color originalColor = targetText.color;
        string currentSentence = "";
        for (int i = 0; i < targetStrings.Length; i++)
        {
            currentSentence = targetStrings[i];
            targetText.text = "";
            yield return StartCoroutine(TypeSentence(targetText, currentSentence));
            yield return new WaitForSeconds(displayTime);
        }
        targetText.text = "";
        isDisplaying = false;
        if (!isDisplaying)
        {
            countText.gameObject.SetActive(true);
            targetText.gameObject.SetActive(false);
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
    public void StartSoundEp()
    {
        audioSourceMUSIC.volume = 0f;
        audioSourceMUSIC.PlayOneShot(audioClip);
        StartCoroutine(FadeInAudio(audioSourceMUSIC, fadeDuration));
    }
    private IEnumerator FadeInAudio(AudioSource audioSource, float duration = 15f)
    {
        float elapsedTime = 0f;
        float startVolume = 0f;
        float targetVolume = 0.8f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

}
