 
    public static class Screens_Names
    {
        public const string HOME_SCREEN = "Home";
        public const string TWITTER_SCREEN = "Twitter";
        public const string YOUTUBE_SCREEN = "Youtube";
        public const string INSTAGRAM_SCREEN = "Instagram";
        public const string FACEBOOK_SCREEN = "Facebook";
    }

    public static class FbPermissions
    {
        public const string ADS_MANAGEMENT = "ads_management"; //the ads_management permission allows your app to both read and manage the Ads account it owns, or has been granted access to, by the Ad account owner.
        public const string ADS_READ = "ads_read"; //The ads_read permission allows your app to access the Ads Insights API to pull Ads report information for Ad accounts you own or have been granted access to by the owner or owners of other ad accounts through this permission. This permissions also grants your app access to the Server-Side API to allow advertisers to send web events from their servers directly to Facebook.
        public const string ATTRIBUTION_READ = "attribution_read"; //The attribution_read permission grants your app access to the Attribution API to pull attribution report data for lines of business you own or have been granted access to by the owner or owners of other lines of business.
        public const string BUSINESS_MANAGEMENT = "business_management"; //The business_management permission allows your app to read and write with the Business Manager API.
        public const string CATALOG_MANAGEMENT = "catalog_management"; //	The catalog_management permission allows your app to create, read, update and delete business-owned product catalogs that the user is an admin of.
        public const string EMAIL = "email"; //	The email permission allows your app to read a person's primary email address.
        public const string GROUP_ACCESS_MEMBER_INFO = "groups_access_member_info"; // The groups_access_member_info permission allows your app to read publicly available group member information like name and ID if the post author has granted your app access.
        public const string INSTAGRAM_BASIC = "instagram_basic"; //The instagram_basic permission allows your app to read an Instagram account profile's info and media.
        public const string INSTAGRAM_CONTENT_PUBLISH = "instagram_content_publish"; //The instagram_content_publish permission allows your app to create organic feed photo and video posts on behalf of a business user.
        public const string INSTAGRAM_MANAGE_COMMENTS = "instagram_manage_comments"; //The instagram_manage_comments permission allows your app to create, delete and hide comments on behalf of the Instagram account linked to a Page. Your app can also read and respond to public media and comments that a business has been photo tagged or @mentioned in.
        public const string INSTAGRAM_MANAGE_INSIGHTS = "instagram_manage_insights"; //The instagram_manage_insights permission allows your app to get access to insights for the Instagram account linked to a Facebook Page. Your app can also discover and read the profile info and media of other business profiles.
        public const string LEADS_RETRIEVAL = "leads_retrieval"; //	The leads_retrieval permission allows your app to retrieve and read all information captured by a lead ads form associated with an ad created in Ads Manager or the Marketing API.
        public const string PAGES_MANAGE_ADS = "pages_manage_ads"; //The pages_manage_ads permission allows your app to manage ads associated with the Page.
        public const string PAGES_MANAGE_CTA = "pages_manage_cta"; //The pages_manage_cta permission allows your app to carry out POST and DELETE functions on endpoints used to manage call-to-action buttons on a Facebook Page.
        public const string PAGES_MANAGE_INSTANT_ARTICLES = "pages_manage_instant_articles"; //The pages_manage_instant_articles permission allows your app to manage Instant Articles on behalf of Facebook Pages administered by people using your app.
        public const string PAGES_MANAGE_ENGAGEMENT = "pages_manage_engagement"; //The pages_manage_engagement permission allows your app to create, edit and delete comments posted on the Page.
        public const string PAGES_MANAGE_METADATA = "pages_manage_metadata"; //The pages_manage_metadata permission allows your app to subscribe and receive webhooks about activity on the Page, and to update settings on the Page.
        public const string PAGES_MANAGE_POSTS = "pages_manage_posts"; //The pages_manage_posts permission allows your app to create, edit and delete your Page posts.
        public const string PAGES_MESSAGING = "pages_messaging"; //The pages_messaging permission allows your app to manage and access Page conversations in Messenger.
        public const string PAGES_READ_ENGAGEMENT = "pages_read_engagement"; //The pages_read_engagement permission allows your app to read content (posts, photos, videos, events) posted by the Page, read followers data (including name, PSID), and profile picture, and read metadata and other insights about the Page.
        public const string PAGES_READ_USER_CONTENT = "pages_read_user_content"; //The pages_read_user_content permission allows your app to read user generated content on the Page, such as posts, comments, and ratings by users or other Pages, and to delete user comments on Page posts.
        public const string PAGES_SHOW_LIST = "pages_show_list"; //The pages_show_list permission allows your app to access the list of Pages a person manages.
        public const string PAGES_USER_GENDER = "pages_user_gender"; //The pages_user_gender permission allows your app to access a user's gender through the Page your app is connected to.
        public const string PAGES_USER_LOCALE = "pages_user_locale"; //The pages_user_locale permission allows your to app to a user's locale through the Page your app is connected to.
        public const string PAGES_USER_TIMEZONE = "pages_user_timezone"; //The pages_user_timezone permission grants your app access to a user's time zone through the Page your app is connected to.
        public const string PUBLIC_PROFILE = "public_profile"; //Allows apps to read the Default Public Profile Fields on the User node. This permission is automatically granted to all apps.
        public const string PUBLISH_TO_GROUPS = "publish_to_groups"; //The publish_to_groups permission allows your app to post content into a Group on behalf of a person if they've granted your app access.
        public const string PUBLISH_VIDEO = "publish_video"; //The publish_video permission allows your app to publish live videos to an app user's timeline, group, event or Page.
        public const string READ_INSIGHTS = "read_insights"; //The read_insights permission allows your app to read the Insights data for Pages, apps and web domains the person owns.
        public const string USER_AGE_RANGE = "user_age_range"; //The user_age_range permission allows your app to access a person's age range as listed in their Facebook profile.
        public const string USER_BIRTHDAY = "user_birthday"; //The user_birthday permission allows your app to read a person's birthday as listed in their Facebook profile.
        public const string USER_FRIENDS = "user_friends"; //The user_friends permission allows your app to get a list of a person's friends using that app.
        public const string USER_GENDER = "user_gender"; //The user_gender permission allows your app to read a person's gender as listed in their Facebook profile.
        public const string USER_HOMETOWN = "user_hometown"; //The user_hometown permission allows your app to read a person's hometown location from their Facebook profile.
        public const string USER_LIKES = "user_likes"; //The user_likes permission allows your app to read a list of all Facebook Pages that a user has liked.
        public const string USER_LOCATION = "user_location"; //The user_location permission allows your app to read the city name as listed in the location field of a person's Facebook profile.
        public const string USER_PHOTOS = "user_photos"; //The user_photos permission allows your app to read the photos a person has uploaded to Facebook.
        public const string USER_POSTS = "user_posts"; //The user_posts permission allows your app to access the posts that a user has made on their timeline.
        public const string USER_VIDEOS = "user_videos"; //The user_videos permission allows your app to read a list of videos uploaded by a person.
        public const string INSTAGRAM_GRAPH_USER_MEDIA = "instagram_graph_user_media"; //	The instagram_graph_user_media permission allows your app to read the Media node, which represents an image, video, or album and the node’s edges.
        public const string INSTAGRAM_GRAPH_USER_PROFILE = "instagram_graph_user_profile"; //The instagram_graph_user_profile permission allows your app to read the app user's profile.

    }
