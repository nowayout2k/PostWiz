using System;
using DeadMosquito.AndroidGoodies;
using UnityEngine;

namespace Utility
{
    public static class NativeOperationsHelper
    {
        public static void OpenImageGallery(ImageResultSize imageResultSize, Action<Texture2D> callback)
        {
            AGGallery.PickImageFromGallery(
                selectedImage =>
                {
                    var imageTexture2D = selectedImage.LoadTexture2D();

                    Debug.Log(string.Format("{0} was loaded from gallery with size {1}x{2}", selectedImage.OriginalPath, imageTexture2D.width, imageTexture2D.height));
                    callback?.Invoke(imageTexture2D);
                     
                    // Clean up
                    Resources.UnloadUnusedAssets();
                },
                errorMessage => AGUIMisc.ShowToast("Cancelled picking image from gallery: " + errorMessage), imageResultSize);
        }
    }
}
