using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class NavigationBar : MonoBehaviour
    {
        [SerializeField] private Button[] navButtons;
        
        private void Awake()
        {
            var buttonIndex = 0;
            foreach (var b in navButtons)
            {
                int i = buttonIndex++;
                b.onClick.AddListener(()=>RefreshButtons(i));
            }
            RefreshButtons(0);
        }

        private void RefreshButtons(int activeButtonIndex)
        {
            for (var i = 0; i < navButtons.Length; i++)
            {
                navButtons[i].interactable = i != activeButtonIndex;
            }
        }
    }
}
