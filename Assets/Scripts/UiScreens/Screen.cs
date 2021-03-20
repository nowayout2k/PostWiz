using Enums;
using UnityEngine;

namespace UiScreens
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Screen : MonoBehaviour
    {
        protected CanvasGroup CanvasGroup;
    
        public abstract ScreenType ScreenType { get; }

        public virtual bool IsScreenActive => CanvasGroup.alpha > 0;
        
        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }
        
        public virtual void ActivateScreen(bool isActive)
        {
            CanvasGroup.alpha = isActive ? 1 : 0;
            gameObject.SetActive(isActive);
        }
    }
}
