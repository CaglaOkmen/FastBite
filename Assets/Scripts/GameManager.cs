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
    private VisualElement game_over;
    private Label lbl_time, lbl_result;
    private Button btn_next, btn_retry;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _document = GetComponent<UIDocument>();
        lbl_time = _document.rootVisualElement.Q<Label>("lbl_time");
        lbl_time.text = ((int)levelTime).ToString();
        
        game_over = _document.rootVisualElement.Q<VisualElement>("game_over");
        lbl_result = _document.rootVisualElement.Q<Label>("lbl_result");

        btn_retry = _document.rootVisualElement.Q<Button>("btn_retry");
        btn_retry.RegisterCallback<ClickEvent>(OnRetryClick);

        btn_next = _document.rootVisualElement.Q<Button>("btn_next");
        btn_next.RegisterCallback<ClickEvent>(OnNextClick);
    }

    void Start()
    {
        completedBurgers = 0;
        isLevelActive = true;
        Time.timeScale = 1;

        game_over.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (isLevelActive)
        {
            levelTime -= Time.deltaTime;
            lbl_time.text = ((int)levelTime).ToString();

            if (levelTime <= 0)
            {
                Time.timeScale = 0;
                isLevelActive = false;
                if (completedBurgers >= targetBurgers)
                {
                    lbl_result.text = "Tebrikler!";
                    game_over.style.display = DisplayStyle.Flex;
                    btn_next.style.display = DisplayStyle.Flex;
                    btn_retry.style.display = DisplayStyle.None;
                }
                else
                {
                    lbl_result.text = "Sure Bitti!";
                    game_over.style.display = DisplayStyle.Flex;
                    btn_next.style.display = DisplayStyle.None;
                    btn_retry.style.display = DisplayStyle.Flex;
                }
            }
        }
    }

    public void BurgerCompleted()
    {
        completedBurgers++;
        if (isLevelActive)
        {
        RecipeCard recipeCard = FindObjectOfType<RecipeCard>();
        recipeCard.GenerateNewRecipe();
        }
    }

    void OnNextClick(ClickEvent evt)
    {
        currentLevel++;
        SceneManager.LoadScene(2);
    }

    void OnRetryClick(ClickEvent evt)
    {
        SceneManager.LoadScene(2);
    }
}
