using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using Google;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Youtube
{
    public class GoogleService
    {
      public string WebClientId = String.Empty;
      private string accessToken;
      private GoogleSignInConfiguration configuration;

      public delegate void OnGoogleLogin();
      public event OnGoogleLogin onGoogleLogin;
      
      private const string BASE_URL = "https://www.googleapis.com/youtube/v3/";
      private const string COMMENT_THREADS_ENDPOINT = "commentThreads";
      private const string CHANNEL_ENDPOINT = "channels";
      private const string PLAYLIST_ITEMS_ENDPOINT = "playlistItems";
      private const string COMMENT_THREAD_ENDPOINT = "commentThreads";
      
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
        var headers = new Dictionary<string, string>
        {
          {"Authorization", $"Bearer [{accessToken}]"}
        };
        yield return GetRequest( BASE_URL + CHANNEL_ENDPOINT + $"?part=contentDetails&mine=true&key={configuration.WebClientId}", headers,
          (b, objects) =>
          {
            callback(b ? null : new YoutubeChannelData(objects));
          });
      }

      private IEnumerator GetUploadedVideosData(string playlistId, Action<List<YoutubeVideoData>> callback)
      {
        var headers = new Dictionary<string, string>
        {
          {"Authorization", $"Bearer [{accessToken}]"}
        };
        yield return GetRequest(BASE_URL+PLAYLIST_ITEMS_ENDPOINT+$"?part=snippet&maxResults=50&playlistId={playlistId}&key={configuration.WebClientId}", headers,
          (b, dict) =>
          {
            if (b)
            {
              callback(null);
            }
            else
            {
              var videos = new List<YoutubeVideoData>();
              foreach (var obj in dict)
              {
                if (obj.Value is Dictionary<string, object> subDict)
                {
                  videos.Add(new YoutubeVideoData(subDict));
                }
              }
              callback(videos);
            }
          });
      }

      public void CommentOnVideo(string videoId, string commentText, Action<bool> callback)
      {
        var headers = new Dictionary<string, string>
        {
          {"Authorization", $"Bearer [{accessToken}]"},
          {"Accept", $"application/json"},
          {"Content-Type", $"application/json"}
        };
        var formData = new List<IMultipartFormSection>
        {
          new MultipartFormFileSection("snippet.videoId", videoId),
          new MultipartFormFileSection("snippet.topLevelComment.snippet.textOriginal", commentText)
        };
        AppController.Instance.StartCoroutine(Upload(BASE_URL+COMMENT_THREADS_ENDPOINT+$"?part=snippet&key={configuration.WebClientId}",formData, headers,(b) =>
        {
          callback(b);
          Debug.Log($"commented on video {(b ? "successfully":"unsuccessfully")}");
        }));
      }

      private IEnumerator Upload(string uri, List<IMultipartFormSection> formData, Dictionary<string, string> headers, Action<bool> callback)
      {
        UnityWebRequest request = UnityWebRequest.Post(BASE_URL+COMMENT_THREAD_ENDPOINT+$"?part=snippet&key={configuration.WebClientId}", formData);

        foreach (var header in headers)
        {
          request.SetRequestHeader(header.Key, header.Value);
        }
        
        using (UnityWebRequest www = UnityWebRequest.Post(uri, formData))
        {
          yield return www.SendWebRequest();

          if (www.isNetworkError || www.isHttpError)
          {
            Debug.Log(www.error);
            callback(false);
          }
          else
          {
            Debug.Log("Form upload complete!");
            callback(true);
          }
        }
      }
      
      private IEnumerator GetRequest(string uri, Dictionary<string, string> headers, Action<bool, Dictionary<string, object>> callback)
      {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
          foreach (var header in headers)
          {
            webRequest.SetRequestHeader(header.Key, header.Value);
          }
          
          // Request and wait for the desired page.
          yield return webRequest.SendWebRequest();

          if (webRequest.isNetworkError)
          {
            callback(false, null);
            Debug.Log(": Error: " + webRequest.error);
          }
          else
          {
            Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(webRequest.downloadHandler.text);
            callback(true, dict);
          }
        }
      }
    }
}
 

