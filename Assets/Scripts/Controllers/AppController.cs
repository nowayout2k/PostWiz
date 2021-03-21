using System;
using Facebook;
using Facebook.Unity;
using Screens;
using TwitterApi;
using TwitterKit.Unity;
using UiScreens;
using UnityEngine;
using UnityEngine.SceneManagement;
using Youtube;

namespace Controllers
{
    public class AppController : MonoBehaviour
    {
        public static AppController Instance;
        
        [SerializeField] private ScreenManager screenManager;
        public ScreenManager ScreenManager => screenManager;

        private GoogleService googleService;
        public GoogleService GoogleService => googleService;

        private FacebookService facebookService;
        public FacebookService FacebookService => facebookService;
        
        private TwitterService twitterService;
        public TwitterService TwitterService => twitterService;
        
        private void Awake()
        {
            if(Instance != null)
                Debug.LogError("AppController already has been created. Only 1 instance is allowed.");
            Instance = this;
            
            googleService = new GoogleService();
            facebookService = new FacebookService();
            twitterService = new TwitterService();

        }

        private void Start()
        {
            googleService.Initialize();
            facebookService.Initialize();
            TwitterService.Initialize();
        }
    }
}
