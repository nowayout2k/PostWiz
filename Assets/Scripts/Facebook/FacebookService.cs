using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Facebook.Unity;
using UnityEditor;
using UnityEngine;

namespace Facebook
{
    public class FacebookService
    {
        public string AccessTokenString => AccessToken.CurrentAccessToken.TokenString;
        public bool IsLoggedIn => FB.IsLoggedIn;
        public string UserId => AccessToken.CurrentAccessToken.UserId;
        
        public void Initialize()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(InitCallback);
            }
        }
        
        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else 
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        public void FacebookLogin()
        {
            var permissions = new List<string>
            {
                FbPermissions.EMAIL,
                FbPermissions.PAGES_MANAGE_POSTS,
                FbPermissions.PAGES_READ_ENGAGEMENT,
                FbPermissions.PAGES_SHOW_LIST
            };
            FB.LogInWithReadPermissions(permissions, FbLoginComplete);
            Debug.Log($"Logging into facebook with Permissions: {string.Join(",", permissions )}");
        }

        public void InstagramLogin()
        {
            var permissions =new List<string>
            {
                FbPermissions.PAGES_SHOW_LIST,
                FbPermissions.INSTAGRAM_BASIC
            };
            FB.LogInWithReadPermissions(permissions, FbLoginComplete);
            Debug.Log($"Logging into instagram with Permissions: {string.Join("," , permissions)}");
        }

        private void FbLoginComplete(ILoginResult result)
        {
            if (FB.IsLoggedIn) 
            {
                Debug.Log($"Access Token: {UserId}, Granted permissions: {string.Join(",", AccessToken.CurrentAccessToken.Permissions)}");
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

        public void GetUserPages(Action<List<FbPage>> callback)
        {
            FB.API($"/{UserId}/accounts", HttpMethod.GET, (r) =>
            {
                var fbPages = new List<FbPage>();
                if (r.ResultDictionary.ContainsKey("data"))
                {
                    if (r.ResultDictionary["data"] is List<object> dataList)
                    {

                        foreach (var data in dataList)
                        {
                            if (!(data is Dictionary<string, object> dataDict)) 
                                continue;
                            
                            var fbPage = new FbPage();
                        
                            if (dataDict.ContainsKey("id"))
                            {
                                fbPage.ID = Convert.ToString(dataDict["id"]);
                            }
                            if(dataDict.ContainsKey("name"))
                            {
                                fbPage.Name = Convert.ToString(dataDict["name"]);
                            }
                            if(dataDict.ContainsKey("access_token"))
                            {
                                fbPage.AccessToken = Convert.ToString(dataDict["access_token"]);
                            }
                            if(dataDict.ContainsKey("category"))
                            {
                                fbPage.Category = Convert.ToString(dataDict["category"]);
                            }
                            if(dataDict.ContainsKey("category_list"))
                            {
                                fbPage.CategoryList = dataDict["category_list"] as List<object>;
                            }
                            if(dataDict.ContainsKey("tasks"))
                            {
                                fbPage.Tasks = dataDict["tasks"] as List<string>;
                            }
                            fbPages.Add(fbPage);
                        }
                    }
                }
                callback?.Invoke(fbPages);
            });
            Debug.Log($"Getting groups for {UserId}.");
        }

        public void PostToFbPage(FbPage fbPage, string message, Action<bool> callback)
        {
            var formData = new Dictionary<string, string>()
            {
                {"message", message},
                {"access_token", fbPage.AccessToken }
            };
            
            FB.API($"/{fbPage.ID}/feed", HttpMethod.POST, (r)=>
            {
                var success = r.ResultDictionary.ContainsKey("id");
                callback?.Invoke(success);

                if (success)
                {
                    Debug.Log($"Page post id: {r.ResultDictionary["id"]}");
                }
                else
                {
                    Debug.Log($"{r.Error}");
                }
            },formData);
            
            
            Debug.Log($"Posting message: \"{message}\", to page {fbPage.Name}...");
        }

        /// <summary>
        /// Maximum file size: 8MiB
        ///Aspect ratio: Must be within a 4:5 to 1.91:1 range
        ///Minimum width: 320 (will be scaled up to the minimum if necessary)
        ///Maximum width: 1440 (will be scaled down to the maximum if necessary)
        ///Height: Varies, depending on width and aspect ratio
        ///Formats: JPEG
        /// </summary>
        /// <param name="igUserId"></param>
        /// <param name="igImageContainerData"></param>
        /// <param name="callback"></param>
        public void CreateIgImageContainer(IgImageContainerData igImageContainerData, Action<string> callback)
        {
            var formData = new Dictionary<string, string>()
            {
                {"image_url", igImageContainerData.ImageUrl },
                {"caption", igImageContainerData.Caption },
                {"user_tags", igImageContainerData.UserTagsToString() },
                {"access_token", AccessTokenString }
            };
            
            FB.API($"/{UserId}/media", HttpMethod.POST, (r)=>
            {
                var success = r.ResultDictionary.ContainsKey("id");

                if (success)
                {
                    callback?.Invoke(Convert.ToString(r.ResultDictionary["id"]));
                    Debug.Log($"Container id: {r.ResultDictionary["id"]}");
                }
                else
                {
                    callback?.Invoke(null);
                    Debug.Log($"Did not create container. {r.Error}");
                }
            },formData);
            
            
            Debug.Log($"Posting Image: \"{igImageContainerData.ImageUrl}\", to page {UserId}...");
        }

        public void PostToInstagram(string creationId, Action<bool> callback)
        {
            var formData = new Dictionary<string, string>()
            {
                {"creation_id", creationId },
                {"access_token", AccessTokenString }
            };
            
            FB.API($"/{UserId}/media_publish", HttpMethod.POST, (r)=>
            {
                var success = r.ResultDictionary.ContainsKey("id");
                callback?.Invoke(success);

                if (success)
                {
                    Debug.Log($"Page post id: {r.ResultDictionary["id"]}");
                }
                else
                {
                    Debug.Log($"{r.Error}");
                }
            },formData);
            
            
            Debug.Log($"Posting message: \"{creationId}\", to page {UserId}...");
        }
        
        
        public void GetUserGroups(Action<List<FbGroupIdentity>> callback)
        {
            FB.API($"/{UserId}/groups", HttpMethod.GET, (r) =>
            {
                var groupIdentities = new List<FbGroupIdentity>();
                if (r.ResultDictionary.ContainsKey("data"))
                {
                    if (r.ResultDictionary["data"] is List<object> groupList)
                    {
                        foreach (var groupObj in groupList)
                        {
                            if (!(groupObj is Dictionary<string, object> groupDict)) 
                                continue;
                            
                            FbGroupIdentity fbGroupIdentity = new FbGroupIdentity();
                        
                            if (groupDict.ContainsKey("id"))
                            {
                                fbGroupIdentity.Id = Convert.ToString(groupDict["id"]);
                            }
                            if(groupDict.ContainsKey("name"))
                            {
                                fbGroupIdentity.Name = Convert.ToString(groupDict["name"]);
                            }
                            groupIdentities.Add(fbGroupIdentity);
                        }
                    }
                }
                callback?.Invoke(groupIdentities);
            });
            Debug.Log($"Getting groups for {UserId}.");
        }

        /// <summary>
        /// Post to a group user group (must be an Admin and Group must have app installed)
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        public void PostToUserGroup(string groupId, string message, Action<bool> callback)
        {
            var formData = new Dictionary<string, string>()
            {
                {"message", message},
                {"access_token", AccessTokenString }
            };
            FB.API($"/{groupId}/feed", HttpMethod.POST, (r)=>
            {
                var success = r.ResultDictionary.ContainsKey("id");
                if (!success)
                {
                    Debug.Log($"{r.Error}");
                }

                callback?.Invoke(success);
                Debug.Log($"Post to group success = {success}");
            },formData);
            Debug.Log($"Posting message: \"{message}\", to group {groupId}.");
        }
    }
    
    public class FbPage
    {
        public string AccessToken;
        public string ID;
        public string Name;
        public string Category;
        public List<object> CategoryList;
        public List<string> Tasks;

    }
    public struct FbGroupIdentity
    {
        public string Name;
        public string Id;
    }
    
    public struct IgImageContainerData
    {
        public struct UserTag
        {
            public string Username;
            public float X;
            public float Y;
        }
        public string ImageUrl;
        public string Caption;
        public List<UserTag> UserTags;

        public string UserTagsToString()
        {
            string s = "[";
                
            foreach (var userTag in UserTags)
            {
                s += $"{{ username:{userTag.Username}, x: {userTag.X}, y: {userTag.Y} }},";
            }

            s += "]";
                
            return s;
        }
    }
}
