using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private UIDocument _document;
    private Button btn, btn_menu;

    public AudioSource buttonSFX;
    void Awake()
    {
        _document = GetComponent<UIDocument>();
    
        btn_menu = _document.rootVisualElement.Q<Button>("btn_menu");
        btn_menu.RegisterCallback<ClickEvent>(OnBackClick);

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 1; i < 31; i++)
        {
            btn = _document.rootVisualElement.Q<Button>("btn_level_" + i);

            if (i <= unlockedLevel)
            {
                btn.SetEnabled(true);
                int levelNum = i;
                btn.RegisterCallback<ClickEvent>(evt => OnLevelClick(levelNum));
                
            }
            else
            {
                btn.SetEnabled(false);
                btn.style.opacity = 0.4f;
            }
        }
    }

    void OnBackClick(ClickEvent evt)
    {
        buttonSFX.Play();
        SceneManager.LoadScene(0);
    }

    void OnLevelClick(int levelNum)
    {
        buttonSFX.Play();
        GameManager.currentLevel = levelNum;
        SceneManager.LoadScene(2);
    }
}
