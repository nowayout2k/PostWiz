using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using TMPro;
using TwitterApi;
using TwitterKit.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;
using Screen = UiScreens.Screen;

namespace Screens
{
    public class TwitterScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.Twitter;

        [FormerlySerializedAs("tweetText")] [SerializeField] private TMP_InputField tweetIF;
        [FormerlySerializedAs("hashTagText")] [SerializeField] private TMP_InputField hashTagIF;

        private TwitterService twitterService;
        protected override void Awake()
        {
            base.Awake();
            twitterService = AppController.Instance.TwitterService;
        }
        public void Post()
        {
            twitterService.StartComposer(null, tweetIF.text, StringParser.ParseHashtags(hashTagIF.text), PostSuccessCallback, PostFailureCallback);
            Debug.Log("Tweet: " + tweetIF.text);
        }
 
        private void PostSuccessCallback(string s)
        {
            Debug.Log("Post Successful!");
        }

        private void PostFailureCallback(ApiError error)
        {
            Debug.Log("Post Failure!");
        }

    }
}
