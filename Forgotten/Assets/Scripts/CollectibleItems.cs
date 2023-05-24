using Kino;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CollectibleItems : MonoBehaviour
{
    public int totalItems = 4;
    private int itemsCollected = 0;
    public Text itemsText;
    public AudioClip countSound;
    public AudioClip ListPartSound;
    public AudioClip NoiseSound;
    public AudioSource OtherSource;
    public AudioSource MusicSource;
    public AudioClip MusicClip;
    public Transform startPosition;
    public GameObject[] Lists;
    public GameObject BoxRepeater;
    public PlayableDirector CounterTimeline;
    public GameObject[] ForDestroy;

    public AudioClip justMonikaBg;
    public AudioClip ChooseButtonSound;

    public GameObject LeftButton;
    public GameObject RightButton;

    private int itemsforGlitch = 0;
    private int methodCounter = 0;

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;

        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();

        digitalGlitchComponent.enabled = false;
        analogGlitchComponent.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CollectibleItem"))
        {
            other.gameObject.SetActive(false);
            itemsCollected++;
            itemsforGlitch++;
            OtherSource.PlayOneShot(ListPartSound);
            UpdateText();
        }
        if (other.gameObject.CompareTag("BoxRepeat"))
        {
            MusicSource.Stop();
            RepeatBox();
            methodCounter++;
        }
    }
    private void UpdateText()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        itemsText.text = itemsCollected + "/" + totalItems;
        OtherSource.PlayOneShot(countSound);
        if (itemsCollected == totalItems)
        {
            MusicSource.Stop();
            OtherSource.Stop();
            OtherSource.PlayOneShot(NoiseSound);
            digitalGlitchComponent.enabled = true;
            analogGlitchComponent.enabled = true;
            Invoke("ResetGame", 1f);
        }
        if (itemsforGlitch == 5)
        {
            MusicSource.Stop();
            OtherSource.Stop();
            OtherSource.PlayOneShot(NoiseSound);
            digitalGlitchComponent.enabled = true;
            analogGlitchComponent.enabled = true;
            Invoke("ResetGame", 1f);
            RepeatBox();
        }
    }
    private void RepeatBox()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        MusicSource.Stop();
        OtherSource.Stop();
        OtherSource.PlayOneShot(NoiseSound);
        digitalGlitchComponent.enabled = true;
        analogGlitchComponent.enabled = true;
        Invoke("ResetGame", 1f);
        BoxRepeater.SetActive(true);
    }
    private void ResetGame()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        transform.position = startPosition.position;
        OtherSource.Stop();
        MusicSource.Stop();
        MusicSource.PlayOneShot(MusicClip);
        itemsCollected = 0;
        itemsText.text = itemsCollected + "/" + totalItems;
        digitalGlitchComponent.enabled = false;
        analogGlitchComponent.enabled = false;
        foreach (GameObject obj in Lists)
        {
            obj.SetActive(true);
        }
        if (methodCounter == 2)
        {
            CounterTimeline.Play();
            MusicSource.Stop();
            OtherSource.Stop();
            digitalGlitchComponent.enabled = false;
            analogGlitchComponent.enabled = false;
            BoxRepeater.SetActive(false);
            itemsText.gameObject.SetActive(false);
            MusicSource.PlayOneShot(justMonikaBg);
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene("HuntAwake");
    }
    public void LeftClick()
    {
        MusicSource.Stop();
        OtherSource.PlayOneShot(ChooseButtonSound);
        foreach (GameObject obj in ForDestroy)
        {
            obj.SetActive(false);
        }
        Invoke("LoadNextScene", 6f);
    }
    public void RightClick()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        RightButton.SetActive(false);
        LeftButton.SetActive(true);
        digitalGlitchComponent.enabled = true;
        analogGlitchComponent.enabled = true;
        OtherSource.PlayOneShot(NoiseSound);
        Invoke("CleanEffect", 0.5f);
    }
    private void CleanEffect()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        digitalGlitchComponent.enabled = false;
        analogGlitchComponent.enabled = false;
    }
    public void EffectFirst()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        digitalGlitchComponent.enabled = true;
        analogGlitchComponent.enabled = true;
        Invoke("CleanEffect", 0.8f);
    }
    public void EffectSecond()
    {
        DigitalGlitch digitalGlitchComponent = Camera.main.GetComponent<DigitalGlitch>();
        AnalogGlitch analogGlitchComponent = Camera.main.GetComponent<AnalogGlitch>();
        digitalGlitchComponent.enabled = true;
        analogGlitchComponent.enabled = true;
        Invoke("CleanEffect", 0.15f);
    }
}
