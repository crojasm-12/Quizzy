using UnityEngine;


public class LoginPopupManager : MonoBehaviour
{

    private LoginPanel loginPanel;
    private ForgotPasswordPanel forgotPasswordPanel;
    private InitialBackgroundPanel initialBackgroundPanel;

    void Start()
    {
        initialBackgroundPanel.ShowPanel();
        loginPanel.ShowPanel();
        forgotPasswordPanel.HidePanel();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Awake() // It's better to use Awake to ensure that references are set before any Start methods call them
    {
        loginPanel = FindFirstObjectByType<LoginPanel>();
        forgotPasswordPanel = FindFirstObjectByType<ForgotPasswordPanel>();
        initialBackgroundPanel = FindFirstObjectByType<InitialBackgroundPanel>();

        if (loginPanel == null || forgotPasswordPanel == null || initialBackgroundPanel == null)
        {
            Debug.LogError("PanelController instances not found in the scene!");
        }
    }

}
