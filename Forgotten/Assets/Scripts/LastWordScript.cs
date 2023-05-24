using Kino;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LastWordScript : MonoBehaviour
{
    public string[] laterStrings;
    public Text laterText;
    public float displayTime = 6f;
    public float intervalTime = 1f;
    public AudioSource NoiseSource;
    public AudioClip NoiseSound;
    public float delayforclean = 5f;
    public PlayableDirector FinishTimeline;
    public GameObject[] ForDeactive;
    public MoveObject script;

    public void StartStrings()
    {
        StartCoroutine(DisplaySentences());
    }
    public AudioSource KeyboardsSound;
    private IEnumerator DisplaySentences()
    {
        KeyboardsSound.loop = true;

        string currentSentence = "";
        for (int i = 0; i < laterStrings.Length; i++)
        {
            currentSentence = laterStrings[i];
            laterText.text = "";
            if (i == 9 || i == 10)
            {
                EffectNoise();
            }
            KeyboardsSound.Play();
            yield return StartCoroutine(TypeSentence(laterText, currentSentence));
            KeyboardsSound.Stop();
            yield return new WaitForSeconds(displayTime);
        }
        laterText.text = "";
        startFinish();
    }
    private IEnumerator TypeSentence(Text text, string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void EffectNoise()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        digitalGlitchComponent.enabled = true;
        analogGlitchComponent.enabled = true;
        NoiseSource.PlayOneShot(NoiseSound);
        Invoke("CleanEffect", delayforclean);
        digitalGlitchComponent.intensity = 0.243f;
        analogGlitchComponent.colorDrift = 0.3f;
        analogGlitchComponent.horizontalShake = 0.06f;
        analogGlitchComponent.scanLineJitter = 0.5f;
    }
    private void CleanEffect()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        digitalGlitchComponent.enabled = false;
        analogGlitchComponent.enabled = false;
    }
    private void startFinish()
    {
        FinishTimeline.Play();
    }
    public void DeactivateAll()
    {
        foreach(GameObject obj in ForDeactive)
        {
            obj.SetActive(false);
        }
        foreach (GameObject copy in script.clonesList)
        {
            copy.SetActive(false);
        }
    }
}
