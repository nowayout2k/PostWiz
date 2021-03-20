using Enums;
using UnityEngine;

namespace UiScreens
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Screen[] screens;

        private void Start()
        {
            ChangeScreen(ScreenType.Home);
        }

        public void ChangeScreen(ScreenType screenType)
        {
            bool foundScreen = false;
            foreach (var screen in screens)
            {
                if (screen.ScreenType != screenType)
                {
                    screen.ActivateScreen(false);
                }
                else
                {
                    foundScreen = true;
                    screen.ActivateScreen(true);
                }
            }

            if (!foundScreen)
            {
                Debug.LogWarning("Could not find screen!");
            }
        }
    }
}
