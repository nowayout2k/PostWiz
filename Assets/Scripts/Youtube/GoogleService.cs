 
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using Controllers;
using Google;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;
 

namespace Youtube
{
    public class GoogleService
    {
      public string WebClientId = String.Empty;
    
      private const string GOOGLE_API_KEY = "";
      private string accessToken;
      private GoogleSignInConfiguration configuration;
      
      public void Initialize()
      {
        if (WebClientId == String.Empty)
        {
          Debug.LogError("webClient Id must be set before calling GoogleService.Initialize()");
          return;
        }
        configuration = new GoogleSignInConfiguration
        {
          WebClientId = WebClientId,
          RequestIdToken = true
        };
      }
      
      public void SignIn() 
      {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        Debug.Log("Calling SignIn");
        var instance = GoogleSignIn.DefaultInstance;
        var task = instance.SignIn();
        task.ContinueWith(OnAuthenticationFinished);
      }

      public void SignOut()
      {
        Debug.Log("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
      }

      public void Disconnect() 
      {
        Debug.Log("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
      }

      private void OnAuthenticationFinished(Task<GoogleSignInUser> task) 
      {
        if (task.IsFaulted) 
        {
          using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator()) 
          {
            if (enumerator.MoveNext()) 
            {
              GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
              Debug.Log("Got Error: " + error.Status + " " + error.Message);
            }
            else 
            {
              Debug.Log("Got Unexpected Exception?!?" + task.Exception);
            }
          }
        }
        else if(task.IsCanceled)
        {
          Debug.Log("Canceled");
        }
        else
        {
          accessToken = task.Result.IdToken;
          Debug.Log("Welcome: " + task.Result.DisplayName + "!");
        }
      }

      public void SignInSilently() 
      { 
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false; 
        GoogleSignIn.Configuration.RequestIdToken = true;
        Debug.Log("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
      }


      public void GamesSignIn() 
      {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        Debug.Log("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
      }

      public class YoutubeVideoData
      {
        public string Id;
        public string Kind;
        public string Etag;
        public string PublishedAt;
        public string ChannelId;
        public string Title;
        public string DefaultThumbUrl;
        public string MediumThumbUrl;
        public string HighThumbUrl;
        public string PlaylistId;
        public string Position;
        public string ResourceIdKind;
        public string ResourceIdVideoId;
      }

      public class YoutubeChannelData
      {
        public string Id;
        public string Kind;
        public string Etag;
        public string LikesChannelId;
        public string FavoritesChannelId;
        public string UploadsChannelId;
        public string WatchHistoryChannelId;
        public string WatchLaterChannelId;
      }
      
      public void GetLatestVideos(Action<List<YoutubeVideoData>> callback)
      {
        AppController.Instance.StartCoroutine(GetChannelUploadedVideos(callback));
      }

      private IEnumerator GetChannelUploadedVideos(Action<List<YoutubeVideoData>> callback)
      {
        var playlistId = String.Empty;
        yield return GetChannelData((ycd)=> { playlistId = ycd.Id; });
        if (playlistId == String.Empty)
        {
          callback(null);
        }
        yield return GetUploadedVideosData(playlistId,callback);
 
      }
      
      private IEnumerator GetChannelData(Action<YoutubeChannelData> callback)
      {
        UnityWebRequest channelDataRequest = UnityWebRequest.Get($"https://www.googleapis.com/youtube/v3/channels?part=contentDetails&mine=true&key={GOOGLE_API_KEY}");
        channelDataRequest.SetRequestHeader("Authorization", $"Bearer [{accessToken}]");

        yield return channelDataRequest.SendWebRequest();


        if (channelDataRequest.isNetworkError || channelDataRequest.isHttpError)
        {
          callback(null);
          Debug.Log(channelDataRequest.error);
        }
        else
        {
          callback(new YoutubeChannelData());
          Debug.Log(" complete!");
        }
      }

      private IEnumerator GetUploadedVideosData(string playlistId, Action<List<YoutubeVideoData>> callback)
      {
        UnityWebRequest playListItemsRequest = UnityWebRequest.Get($"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={playlistId}&key={GOOGLE_API_KEY}");
        playListItemsRequest.SetRequestHeader("Authorization", $"Bearer [{accessToken}]");

        yield return playListItemsRequest.SendWebRequest();
          
        if (playListItemsRequest.isNetworkError || playListItemsRequest.isHttpError)
        {
          callback(null);
          Debug.Log(playListItemsRequest.error);
        }
        else
        {
          callback(new List<YoutubeVideoData>());
          Debug.Log(" complete!");
        }
      }
      
      public void CommentOnVideo(string videoId, string commentText, Action<bool> callback)
      {
        AppController.Instance.StartCoroutine(Upload(videoId, commentText, callback));
      }

      private IEnumerator Upload(string videoId, string commentText, Action<bool> callback)
      {
        var formData = new List<IMultipartFormSection>
        {
          new MultipartFormFileSection("snippet.videoId", videoId),
          new MultipartFormFileSection("snippet.topLevelComment.snippet.textOriginal", commentText)
        };
 
        UnityWebRequest request = UnityWebRequest.Post($"https://youtube.googleapis.com/youtube/v3/commentThreads?part=snippet&key={GOOGLE_API_KEY}", formData);
        request.SetRequestHeader("Authorization", $"Bearer [{accessToken}]");
        request.SetRequestHeader("Accept", $"application/json");
        request.SetRequestHeader("Content-Type", $"application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
          callback(false);
          Debug.Log(request.error);
        }
        else
        {
          callback(true);
          Debug.Log("Form upload complete!");
        }
        
      }
 
    }
}
 

