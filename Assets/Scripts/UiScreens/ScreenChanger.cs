using Controllers;
using Enums;
using UiScreens;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private ScreenType screenType;
    private ScreenManager screenManager;
    
    private void Awake()
    {
        screenManager = AppController.Instance.ScreenManager;
    }

    public void ScreenChange()
    {
        screenManager.ChangeScreen(screenType);
    }
}