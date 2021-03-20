using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

namespace Facebook
{
    public class FacebookService
    {
        public void Initialize()
        {
            FB.Init();
        }
        public void FacebookLogin()
        { 
            FB.LogInWithReadPermissions(new List<string>(){FbPermissions.EMAIL}, FbLoginComplete);
        }

        public void InstagramLogin()
        {
            FB.LogInWithReadPermissions(new List<string>(){FbPermissions.PAGES_SHOW_LIST, FbPermissions.INSTAGRAM_BASIC}, FbLoginComplete);
        }

        private void FbLoginComplete(ILoginResult result)
        {
            if (FB.IsLoggedIn) 
            {
                // AccessToken class will have session details
                var aToken = AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log(aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions) 
                {
                    Debug.Log(perm);
                }
            } 
            else 
            {
                Debug.Log("User cancelled login");
            }
        }
    }
}
