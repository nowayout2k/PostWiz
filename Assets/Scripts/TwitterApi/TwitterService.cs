﻿using System;
using TwitterKit.Unity;
using UnityEngine;

namespace TwitterApi
{
    public class TwitterService
    {

        public void Initialize()
        {
            Twitter.Init();
        }
        
        public void StartLogin()
        {
            var session = Twitter.Session;

            if (session == null)
            {
                Twitter.LogIn(TwitterLoginComplete, TwitterLoginFailure);
            }
            else
            {
                TwitterLoginComplete(session);
            }
        }

        public void StartComposer(TwitterSession session, String imageUri, string text, string[] hashtags, Action<string> successCallback = null, Action<ApiError> failureCallback = null, Action cancelCallback = null)
        {
            Twitter.Compose(session, imageUri, 
                text, 
                hashtags,
                successCallback,
                failureCallback,
                cancelCallback
            );
        }
        private void TwitterLoginComplete(TwitterSession session) 
        {
            Debug.Log ("Twitter Login Success!");
        }

        private void TwitterLoginFailure(ApiError error) 
        {
            Debug.Log ("code=" + error.code + " msg=" + error.message);
        }
    }
}