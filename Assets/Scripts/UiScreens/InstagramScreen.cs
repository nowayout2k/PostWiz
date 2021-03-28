using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using Facebook;
using Facebook.Unity;
using TMPro;
using Unity.Notifications.iOS;
using UnityEngine;
using Utility;
using Screen = UiScreens.Screen;

namespace Screens
{
    public class InstagramScreen : Screen
    {
         public override ScreenType ScreenType =>  ScreenType.Instagram;

         [SerializeField] private TMP_InputField captionIF; 
         [SerializeField] private TMP_InputField hashTagIF;
         [SerializeField] private TMP_Dropdown pageDropdown;
         [SerializeField] private TextMeshProUGUI igUserIdText;
         
         private List<FbPage> cachedFbPages;
         private FacebookService facebookService;
         private string selectedPageId;
         private string igUserId;
         
         protected override void Awake()
         {
             base.Awake();
             cachedFbPages = new List<FbPage>();
             facebookService = AppController.Instance.FacebookService;
         }

         protected void Start()
         {
             facebookService.FbLogin += OnLogin;
             facebookService.ReceivedUserPages += PopulateDropdown;
         }

         private void OnLogin(bool success, IEnumerable<string> permissions)
         {
             
         }

         private void OnDestroy()
         {
             facebookService.FbLogin -= OnLogin;
             facebookService.ReceivedUserPages -= PopulateDropdown;
         }

         public void PostImageToInstagram()
         {
             var hashtags = StringParser.ParseHashtags(hashTagIF.text);
             var hashtagString = string.Join(" ", hashtags);
             var concatMessage = captionIF.text + "  " + hashtagString;
             
             facebookService.CreateIgImageContainer(igUserId, new IgImageContainerData()
             {
                 //TODO: use user selected image url
                 ImageUrl = "https://memoryhollow.files.wordpress.com/2013/10/bronze_fonz.jpg",
                 Caption = concatMessage,
                 UserTags = new List<IgImageContainerData.UserTag>()
             }, CreateImageContainerCallback);
         }
         
         public void GetUserPages()
         {
             facebookService.GetUserPages(GetPagesCallback);
         }
         
        
         private void GetPagesCallback(List<FbPage> pageIdentities)
         {
             PopulateDropdown(pageIdentities);
             Debug.Log(pageIdentities.Count > 0 ? "Find Groups Successful!" : "Find Groups Failure!");
         }

         private void PopulateDropdown(List<FbPage> pageIdentities)
         {
             pageDropdown.ClearOptions();
             cachedFbPages.Clear();
            
             cachedFbPages = pageIdentities;
             var pageNames = cachedFbPages.Select(fbPage => fbPage.Name).ToList();
            
             pageDropdown.AddOptions(pageNames);
             facebookService.GetIgUser(cachedFbPages[0].ID, ReceivedIgUser);
         }

         public void OnDropdownSelect()
         {
             facebookService.GetIgUser(cachedFbPages[pageDropdown.value].ID, ReceivedIgUser);
         }

         private void ReceivedIgUser(string id)
         {
             igUserId = id;
             igUserIdText.text = igUserId;
         }

         private void CreateImageContainerCallback(string id)
         {
             if(id != null)
                facebookService.PostToInstagram(igUserId, id, PostToInstagramCallback);
         }

         private void PostToInstagramCallback(bool success)
         {
             Debug.Log($"Instagram post {(success? "success" :"Failure")}");
         }
 
    }
}
