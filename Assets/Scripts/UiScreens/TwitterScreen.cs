using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using TMPro;
using TwitterApi;
using TwitterKit.Unity;
using UnityEngine;
using UnityEngine.UI;
using Screen = UiScreens.Screen;

namespace Screens
{
    public class TwitterScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.Twitter;

        [SerializeField] private TMP_InputField tweetText;
        [SerializeField] private TMP_InputField hashTagText;

        private TwitterService twitterService;
        private void Awake()
        {
            twitterService = AppController.Instance.TwitterService;
        }
        public void Post()
        {
            Debug.Log("Tweet: " + tweetText.text);
            twitterService.StartComposer(null, tweetText.text, ParseHashtags(), PostSuccessCallback, PostFailureCallback);
        }

        private string[] ParseHashtags()
        {
            var text = hashTagText.text;
 
            var textArray = text.Split(new []{' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            Debug.Log("Hashtags: " + textArray);
            return textArray;
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
