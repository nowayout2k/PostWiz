using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using Facebook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UiScreens
{
    public class FacebookScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.Facebook;
        [SerializeField] private TMP_InputField messageTextIF;
        [SerializeField] private TMP_InputField hashTagIF;
        [SerializeField] private TMP_Dropdown pageDropdown;
        private List<FbPage> cachedFbPages;
        private FacebookService facebookService;
        private string selectedPageId;
        
        
        protected override void Awake()
        {
            base.Awake();
            facebookService = AppController.Instance.FacebookService;
            cachedFbPages = new List<FbPage>();
        }
        public void PostToPage()
        {
            var hashtags = StringParser.ParseHashtags(hashTagIF.text);
            var hashtagString = string.Join(" ", hashtags);
            var concatMessage = messageTextIF.text + "  " + hashtagString;
            facebookService.PostToFbPage(cachedFbPages[pageDropdown.value], concatMessage, PostCallback);
        }

        public void GetUserPages()
        {
            facebookService.GetUserPages(GetPagesCallback);
        }
        
        private void GetPagesCallback(List<FbPage> pageIdentities)
        {
            pageDropdown.ClearOptions();
            cachedFbPages.Clear();
            
            cachedFbPages = pageIdentities;
            var pageNames = cachedFbPages.Select(fbPage => fbPage.Name).ToList();
            
            pageDropdown.AddOptions(pageNames);
            
            Debug.Log(pageIdentities.Count > 0 ? "Find Groups Successful!" : "Find Groups Failure!");
        }
        
        private void PostCallback(bool success)
        {
            Debug.Log(success ? "Post Successful!" : "Post Failure!");
        }
 
    }
}
