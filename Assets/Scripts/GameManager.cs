using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int currentLevel = 1;
    
    public int completedBurgers = 0;
    public int targetBurgers = 3;
    public float levelTime = 30f;
    bool isLevelActive = false;

    private UIDocument _document;
    private VisualElement game_over, new_item;
    private Label lbl_time, lbl_result, lbl_level, lbl_count;
    private Button btn_next, btn_retry, btn_back, btn_close, btn_close2, btn_pause;
    private Image img_item;

    RecipeCard recipeCard;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _document = GetComponent<UIDocument>();
        lbl_time = _document.rootVisualElement.Q<Label>("lbl_time");
        lbl_count = _document.rootVisualElement.Q<Label>("lbl_count");
        lbl_level = _document.rootVisualElement.Q<Label>("lbl_level");
        lbl_time.text = ((int)levelTime).ToString();
        
        game_over = _document.rootVisualElement.Q<VisualElement>("game_over");
        new_item = _document.rootVisualElement.Q<VisualElement>("new_item");
        lbl_result = _document.rootVisualElement.Q<Label>("lbl_result");
        img_item = _document.rootVisualElement.Q<Image>("img_item");

        btn_retry = _document.rootVisualElement.Q<Button>("btn_retry");
        btn_retry.RegisterCallback<ClickEvent>(OnRetryClick);

        btn_next = _document.rootVisualElement.Q<Button>("btn_next");
        btn_next.RegisterCallback<ClickEvent>(OnNextClick);

        btn_back = _document.rootVisualElement.Q<Button>("btn_back");
        btn_back.RegisterCallback<ClickEvent>(OnBackClick);

        btn_close = _document.rootVisualElement.Q<Button>("btn_close");
        btn_close.RegisterCallback<ClickEvent>(OnCloseClick);

        btn_close2 = _document.rootVisualElement.Q<Button>("btn_close2");
        btn_close2.RegisterCallback<ClickEvent>(OnClose2Click);
        
        btn_pause = _document.rootVisualElement.Q<Button>("btn_pause");
        btn_pause.RegisterCallback<ClickEvent>(OnPauseClick);
    }

    void Start()
    {
        recipeCard = FindObjectOfType<RecipeCard>();

        completedBurgers = 0;
        isLevelActive = true;
        Time.timeScale = 1;
        targetBurgers = 3 + currentLevel / 3 + currentLevel / 10;
        levelTime = (currentLevel / 10) * 10f + (currentLevel / 3) * 3f + targetBurgers * 7f;
        game_over.style.display = DisplayStyle.None;
        new_item.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (isLevelActive)
        {
            levelTime -= Time.deltaTime;
            lbl_time.text = ((int)levelTime).ToString();
            lbl_count.text =  (completedBurgers + "/" + targetBurgers).ToString();
            lbl_level.text = "Level " + currentLevel.ToString();
            if (levelTime <= 0)
            {
                Time.timeScale = 0;
                isLevelActive = false;
                if (completedBurgers >= targetBurgers)
                {
                    currentLevel++;
                    lbl_result.text = "Tebrikler!";
                    btn_next.style.display = DisplayStyle.Flex;
                    btn_retry.style.display = DisplayStyle.None;
                    btn_close.style.display = DisplayStyle.None;
                    btn_pause.SetEnabled(false);
                    
                    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
                    if (currentLevel % 2 == 0 && currentLevel > unlockedLevel && ((currentLevel / 2) + 1) < recipeCard.gameObjects.Length)
                    {
                        AudioManager.Instance.PlayNewItemSound();
                        new_item.style.display = DisplayStyle.Flex;
                        Sprite newSprite = recipeCard.gameObjects[(currentLevel / 2) + 1].GetComponent<SpriteRenderer>().sprite;
                        img_item.sprite = newSprite;
                    }
                    else game_over.style.display = DisplayStyle.Flex;
                }
                else
                {
                    lbl_result.text = "Sure Bitti!";
                    game_over.style.display = DisplayStyle.Flex;
                    btn_next.style.display = DisplayStyle.None;
                    btn_retry.style.display = DisplayStyle.Flex;
                    btn_close.style.display = DisplayStyle.None;
                    btn_pause.SetEnabled(false);
                }
            }
        }
    }

    public void BurgerCompleted()
    {
        completedBurgers++;
        if (isLevelActive)
        {
        recipeCard.GenerateNewRecipe();
        }
    }

    void OnNextClick(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel > unlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
        }

        SceneManager.LoadScene(2);
    }

    void OnRetryClick(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene(2);
    }

    void OnBackClick(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel > unlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
        }
        SceneManager.LoadScene(1);
    }

    void OnCloseClick(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        game_over.style.display = DisplayStyle.None;
        btn_pause.SetEnabled(true);
        if (levelTime > 0) Time.timeScale = 1;
    }

    void OnClose2Click(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        new_item.style.display = DisplayStyle.None;
        game_over.style.display = DisplayStyle.Flex;
    }

    void OnPauseClick(ClickEvent evt)
    {
        AudioManager.Instance.PlayClickSound();
        Time.timeScale = 0;
        game_over.style.display = DisplayStyle.Flex;
        btn_next.style.display = DisplayStyle.None;
        btn_retry.style.display = DisplayStyle.Flex;
        btn_close.style.display = DisplayStyle.Flex;
        btn_pause.SetEnabled(false);
    }
    
}
