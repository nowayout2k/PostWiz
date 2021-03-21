 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Youtube
{
    public class GoogleService
    {
      public string WebClientId = String.Empty;
    
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

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
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
    }
}
