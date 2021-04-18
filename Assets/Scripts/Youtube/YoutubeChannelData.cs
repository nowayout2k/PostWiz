
using System.Collections.Generic;

namespace Youtube
{
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

    public YoutubeChannelData(IReadOnlyDictionary<string, object> dict)
    {
      if (dict.ContainsKey("id"))
      {
        Id = dict["id"] as string;
      }

      if (dict.ContainsKey("kind"))
      {
        Kind = dict["kind"] as string;
      }

      if (dict.ContainsKey("etag"))
      {
        Etag = dict["etag"] as string;
      }

      if (dict.ContainsKey("likesChannelId"))
      {
        LikesChannelId = dict["likesChannelId"] as string;
      }

      if (dict.ContainsKey("favoritesChannelId"))
      {
        FavoritesChannelId = dict["favoritesChannelId"] as string;
      }

      if (dict.ContainsKey("uploadsChannelId"))
      {
        UploadsChannelId = dict["uploadsChannelId"] as string;
      }

      if (dict.ContainsKey("watchHistoryChannelId"))
      {
        WatchHistoryChannelId = dict["watchHistoryChannelId"] as string;
      }

      if (dict.ContainsKey("watchLaterChannelId"))
      {
        WatchLaterChannelId = dict["watchLaterChannelId"] as string;
      }
    }
  }
}
