using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;
    private VisualElement _visualElement;

    private Slider slider_music, slider_volume;
    private VisualElement _root;

    public AudioSource buttonSFX;
    public AudioMixer mainAudioMixer;
    public GameObject music;
    private static float valMusic = 40f, valSes = 40f;
    
    void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = GetComponent<UIDocument>().rootVisualElement;

        _button = _document.rootVisualElement.Q("btn_play") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayClick);

        _button = _document.rootVisualElement.Q("btn_setting") as Button;
        _button.RegisterCallback<ClickEvent>(OnSettingClick);

        _button = _document.rootVisualElement.Q<Button>("btn_close");
        _button.RegisterCallback<ClickEvent>(OnCloseClick);

        
        slider_music = _root.Q<Slider>("slider_music");
        slider_music.RegisterValueChangedCallback(ChangeMusicVolume);

        slider_volume = _root.Q<Slider>("slider_volume");
        slider_volume.RegisterValueChangedCallback(ChangeSesVolume);

        _visualElement = _document.rootVisualElement.Q<VisualElement>("Setting");

        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        if (musicObjects.Length == 1) DontDestroyOnLoad(music);
        else Destroy(music);
    }

    private void Start()
    {
        _visualElement.style.display = DisplayStyle.None;
        slider_music.value = valMusic;
        mainAudioMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(valMusic / 100f, 0.0001f)) * 20);
        slider_volume.value = valSes;
        mainAudioMixer.SetFloat("SesVol", Mathf.Log10(Mathf.Max(valSes / 100f, 0.0001f)) * 20);
    }

    private void OnPlayClick(ClickEvent evt)
    {
        buttonSFX.Play();
        SceneManager.LoadScene(1);
    }

    private void OnSettingClick(ClickEvent evt)
    {
        buttonSFX.Play();
        _visualElement.style.display = DisplayStyle.Flex;
    }

    public void OnCloseClick(ClickEvent evt)
    {
        buttonSFX.Play();
        _visualElement.style.display = DisplayStyle.None;
    }

    public void ChangeMusicVolume(ChangeEvent<float> evt)
    {
        valMusic = evt.newValue;
        float val = Mathf.Max(valMusic, 0.0001f) / 100f;
        val = Mathf.Log10(val) * 20;
        mainAudioMixer.SetFloat("MusicVol", val);
    }

    public void ChangeSesVolume(ChangeEvent<float> evt)
    {
        valSes = evt.newValue;
        float val = Mathf.Max(valSes, 0.0001f) / 100f;
        val = Mathf.Log10(val) * 20;
        mainAudioMixer.SetFloat("SesVol", val);
    }
}