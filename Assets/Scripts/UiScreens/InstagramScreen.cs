using System;
using System.Collections.Generic;
using Controllers;
using Enums;
using Facebook;
using Facebook.Unity;
using TMPro;
using UnityEngine;
using Utility;
using Screen = UiScreens.Screen;

namespace Screens
{
    public class InstagramScreen : Screen
    {
         public override ScreenType ScreenType =>  ScreenType.Instagram;
         private FacebookService facebookService; 
         [SerializeField] private TMP_InputField captionIF; 
         [SerializeField] private TMP_InputField hashTagIF;
         
         protected override void Awake()
         {
             base.Awake();
             facebookService = AppController.Instance.FacebookService;
         }

         public void PostImageToInstagram()
         {
             var hashtags = StringParser.ParseHashtags(hashTagIF.text);
             var hashtagString = string.Join(" ", hashtags);
             var concatMessage = captionIF.text + "  " + hashtagString;
             
             facebookService.CreateIgImageContainer(new IgImageContainerData()
             {
                 //TODO: use user selected image url
                 ImageUrl = "https://en.wikipedia.org/wiki/Dance#/media/File:Two_dancers.jpg",
                 Caption = concatMessage,
                 UserTags = new List<IgImageContainerData.UserTag>()
             }, CreateImageContainerCallback);
         }

         private void CreateImageContainerCallback(string id)
         {
             if(id != null)
                facebookService.PostToInstagram(id, PostToInstagramCallback);
         }

         private void PostToInstagramCallback(bool success)
         {
             Debug.Log($"Instagram post {(success? "success" :"Failure")}");
         }
 
    }
}
