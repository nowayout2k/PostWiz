using System.Collections.Generic;
using System.Linq;
using Controllers;
using Enums;
using TMPro;
using UnityEngine;
using Utility;
using Youtube;

namespace UiScreens
{
    public class YoutubeScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.Youtube;
 
        [SerializeField] private TMP_InputField commentIF; 
        [SerializeField] private TMP_InputField hashTagIF;
        [SerializeField] private TMP_Dropdown pageDropdown;

        private GoogleService googleService;
        private List<GoogleService.YoutubeVideoData> youtubeVideosData;
 
         
        protected override void Awake()
        {
            base.Awake();
            googleService = AppController.Instance.GoogleService;
        }

        protected void Start()
        {
            //googleService.OnLogin += OnLogin;
        }
        
        private void PopulateDropdown(List<GoogleService.YoutubeVideoData> youtubeVideos)
        {
            pageDropdown.ClearOptions();
            youtubeVideosData.Clear();
            
            youtubeVideosData = youtubeVideos;
            var videoNames = youtubeVideosData.Select(yvd => yvd.ResourceIdVideoId).ToList();
            
            pageDropdown.AddOptions(videoNames);
        }
        
        public void CommentOnVideo()
        {
            var hashtags = StringParser.ParseHashtags(hashTagIF.text);
            var hashtagString = string.Join(" ", hashtags);
            var concatMessage = commentIF.text + "  " + hashtagString;
            googleService.CommentOnVideo(youtubeVideosData[pageDropdown.value].ResourceIdVideoId, concatMessage, OnCommentOnVideo);
        }

        private void OnCommentOnVideo(bool success)
        {
            Debug.Log(success ? "Successfully commented on video!" : "Failed to comment on video!");
        }

        public void GetUploadedVideos()
        {
            googleService.GetLatestVideos(ReceivedUploadedVideos);
        }

        private void ReceivedUploadedVideos(List<GoogleService.YoutubeVideoData> youtubeVideos)
        {
            var success = youtubeVideos.Count > 0;
            PopulateDropdown(youtubeVideos);
            Debug.Log(success ? "Successfully commented on video!" : "Failed to comment on video!");
        }
    }
}
