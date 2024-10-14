using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Boss_Button : MonoBehaviour
{
    // Поля для кнопок и панели паузы
    public GameObject pausePanel;   // Панель паузы
    public Button pauseButton;      // Кнопка паузы
    public Button continueButton;   // Кнопка продолжения
    public Button exitButton;       // Кнопка выхода в меню

    private bool isPaused = false;  // Состояние паузы

    void Start()
    {
        // Назначаем действия кнопкам
        if (pauseButton != null) pauseButton.onClick.AddListener(TogglePause);
        if (continueButton != null) continueButton.onClick.AddListener(ContinueGame);
        if (exitButton != null) exitButton.onClick.AddListener(ExitToMenu);

        // Делаем панель паузы невидимой при старте
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    // Логика для загрузки сцены
    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }

    // Логика выхода из игры
    public void ExitGame()
    {
        Application.Quit();
    }

    // Логика для включения и выключения паузы
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }

    // Останавливаем игру (ставим на паузу)
    private void PauseGame()
    {
        Time.timeScale = 0f;          // Останавливаем время
        if (pausePanel != null) pausePanel.SetActive(true);   // Показываем панель паузы
    }

    // Возобновляем игру
    public void ContinueGame()
    {
        StartCoroutine(ResumeGame());  // Используем корутину для возобновления
    }

    private IEnumerator ResumeGame()
    {
        Time.timeScale = 1f;          // Возвращаем нормальное время
        if (pausePanel != null) pausePanel.SetActive(false);  // Скрываем панель паузы

        // Принудительное обновление Canvas, чтобы кнопки сразу активировались
        Canvas.ForceUpdateCanvases();

        // Ждем 0.1 секунды перед тем, как сделать кнопку паузы активной (защита от случайных нажатий)
        yield return new WaitForSecondsRealtime(0.1f);

        pauseButton.interactable = true;  // Активируем кнопку паузы снова
    }

    // Логика для выхода в меню
    public void ExitToMenu()
    {
        Time.timeScale = 1f;          // Возвращаем нормальное время, если игра на паузе
        SceneManager.LoadScene("SC_GAME"); // Переход в главное меню
    }
}
