using System;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEditor;
using UnityEngine;

namespace Facebook
{
    public class FacebookService
    {
        private AccessToken accessToken;
        
        public void Initialize()
        {
            FB.Init();
        }

        public void GetUserGroups(string userId, Action<List<string>> callback)
        {
            FB.API($"/{userId}/groups", HttpMethod.GET, (r) =>
            {
                var groupIds = new List<string>();
                foreach (var result in r.ResultList)
                {
                    
                }
                callback(groupIds);
                Debug.Log($"User groups: {string.Join(",", groupIds)}");
            });
            Debug.Log($"Getting groups for {userId}.");
        }

        public void PostToUserGroup(string groupId, string message, Action<bool> callback)
        {
            var formData = new Dictionary<string, string>()
            {
                {"message", message},
                {"access_token", accessToken.TokenString }
            };
            FB.API($"/{groupId}/feed", HttpMethod.POST, (r)=>
            {
                callback(r.ResultDictionary.ContainsKey("id"));
                Debug.Log($"Post to group success = {false}");
            },formData);
            Debug.Log($"Posting message: \"{message}\", to group {groupId}.");
        }
        
        public void FacebookLogin()
        {
            var permissions = new List<string> {FbPermissions.EMAIL, FbPermissions.PUBLISH_TO_GROUPS, FbPermissions.GROUP_ACCESS_MEMBER_INFO};
            FB.LogInWithReadPermissions(permissions, FbLoginComplete);
            Debug.Log($"Logging into facebook with Permissions: {string.Join(",", permissions )}");
        }

        public void InstagramLogin()
        {
            var permissions =new List<string>{FbPermissions.PAGES_SHOW_LIST, FbPermissions.INSTAGRAM_BASIC};
            FB.LogInWithReadPermissions(permissions, FbLoginComplete);
            Debug.Log($"Logging into instagram with Permissions: {string.Join("," , permissions)}");
        }

        private void FbLoginComplete(ILoginResult result)
        {
            if (FB.IsLoggedIn) 
            {
                accessToken = AccessToken.CurrentAccessToken;
                Debug.Log($"Access Token: {accessToken.UserId}, Granted permissions: {string.Join(",", accessToken.Permissions)}");
            } 
            else 
            {
                Debug.Log("User cancelled login");
            }
        }

        public void Logout()
        {
            FB.LogOut();
        }
    }
}
