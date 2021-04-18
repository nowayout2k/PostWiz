using System.Collections.Generic;

namespace Youtube
{
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

    public YoutubeVideoData(IReadOnlyDictionary<string, object> dict)
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

      if (dict.ContainsKey("publishedAt"))
      {
        PublishedAt = dict["publishedAt"] as string;
      }

      if (dict.ContainsKey("channelId"))
      {
        ChannelId = dict["channelId"] as string;
      }

      if (dict.ContainsKey("title"))
      {
        Title = dict["title"] as string;
      }

      if (dict.ContainsKey("DefaultThumbUrl"))
      {
        DefaultThumbUrl = dict["DefaultThumbUrl"] as string;
      }

      if (dict.ContainsKey("mediumThumbUrl"))
      {
        MediumThumbUrl = dict["mediumThumbUrl"] as string;
      }

      if (dict.ContainsKey("highThumbUrl"))
      {
        HighThumbUrl = dict["highThumbUrl"] as string;
      }

      if (dict.ContainsKey("playlistId"))
      {
        PlaylistId = dict["playlistId"] as string;
      }

      if (dict.ContainsKey("position"))
      {
        Position = dict["position"] as string;
      }

      if (dict.ContainsKey("resourceIdKind"))
      {
        ResourceIdKind = dict["resourceIdKind"] as string;
      }

      if (dict.ContainsKey("resourceIdVideoId"))
      {
        Id = dict["resourceIdVideoId"] as string;
      }
    }
  }
}
