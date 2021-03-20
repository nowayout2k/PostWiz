using System;
using Controllers;
using Facebook;
using TwitterApi;
using TwitterKit.Unity;
using UnityEngine;
using Youtube;
using Button = UnityEngine.UI.Button;

namespace Login
{
    [RequireComponent(typeof(Button))]
    public class SocialLoginButton : MonoBehaviour
    {
        [Serializable]
        enum LoginProvider
        {
            None,
            Youtube,
            Twitter,
            Facebook,
            Instagram
        }

        [SerializeField] private LoginProvider loginProvider = LoginProvider.None;
        
        private Button loginButton;
        private TwitterService twitterService;
        private GoogleService googleService;
        private FacebookService facebookService;
 
        private void Awake()
        {
            loginButton = GetComponent<Button>();
            
            var ac = AppController.Instance;
            twitterService = ac.TwitterService;
            googleService = ac.GoogleService;
            facebookService = ac.FacebookService;
        }

        private void Start()
        {
            loginButton.onClick.AddListener(OnClickEvent);
        }

        private void OnDestroy()
        {
            loginButton.onClick.RemoveAllListeners();
        }

        private void OnClickEvent()
        {
            switch (loginProvider)
            {
                case LoginProvider.Facebook:
                    facebookService.FacebookLogin();
                    break;
                case LoginProvider.Instagram:
                    facebookService.InstagramLogin();
                    break;
                case LoginProvider.Twitter:
                    twitterService.StartLogin();
                    break;
                case LoginProvider.Youtube:
                    googleService.SignIn();
                    break;
                default:
                    Debug.LogError("Login provider has not been set.");
                    break;
            }
        }
    }
}
