using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentLevel = 1;
    
    public int completedBurgers = 0;
    public int targetBurgers = 3;
    public float levelTime = 30f;
    bool isLevelActive = false;

    private UIDocument _document;
    private Label TimeLabel;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _document = GetComponent<UIDocument>();
        TimeLabel = _document.rootVisualElement.Q<Label>("levelTime");
        TimeLabel.text = ((int)levelTime).ToString();
    }

    void Start()
    {
        completedBurgers = 0;
        isLevelActive = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (isLevelActive)
        {
            levelTime -= Time.deltaTime;
            TimeLabel.text = ((int)levelTime).ToString();

            if (levelTime <= 0)
            {
                Time.timeScale = 0;
                isLevelActive = false;
                if (completedBurgers >= targetBurgers)
                {
                    Debug.Log("Tebrikler");
                }
                else
                {
                    Debug.Log("Sure Bitti");
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
}
