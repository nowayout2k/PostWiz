using System;
using UnityEngine;

namespace Utility
{
    public static class StringParser
    {
        public static string[] ParseHashtags(string unparsedString)
        {
            var hashTags = unparsedString.Split(new []{' ', ',', '#'}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < hashTags.Length; i++)
            {
                hashTags[i] = hashTags[i].Insert(0, "#");
            }
            Debug.Log("Hashtags: " + hashTags);
            return hashTags;
        }
    }
}
