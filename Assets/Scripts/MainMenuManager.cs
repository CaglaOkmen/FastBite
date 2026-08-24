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

    private Slider _slider;
    private VisualElement _root;

    public AudioSource buttonSFX;
    public AudioMixer mainAudioMixer;
    public GameObject music;
    
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

        
        _slider = _root.Q<Slider>("slider_music");
        _slider.RegisterValueChangedCallback(ChangeMusicVolume);

        _slider = _root.Q<Slider>("slider_volume");
        _slider.RegisterValueChangedCallback(ChangeSesVolume);

        _visualElement = _document.rootVisualElement.Q<VisualElement>("Setting");

        DontDestroyOnLoad(music);
    }

    private void Start()
    {
        _visualElement.style.display = DisplayStyle.None;
        mainAudioMixer.SetFloat("MusicVol", Mathf.Log10(0.4f) * 20);
        mainAudioMixer.SetFloat("SesVol", Mathf.Log10(0.4f) * 20);
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
        float val = evt.newValue / 100;
        val = Mathf.Max(val, 0.0001f);
        val = Mathf.Log10(val) * 20;
        mainAudioMixer.SetFloat("MusicVol", val);
    }

    public void ChangeSesVolume(ChangeEvent<float> evt)
    {
        float val = evt.newValue / 100;
        val = Mathf.Max(val, 0.0001f);
        val = Mathf.Log10(val) * 20;
        mainAudioMixer.SetFloat("SesVol", val);
    }
}
