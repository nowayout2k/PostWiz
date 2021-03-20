using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Google;
using UnityEngine;
using Youtube;

public class GoogleLoginInit : MonoBehaviour
{
    [SerializeField] private string webClientId = "<your client id here>";
    private void Awake()
    {
        AppController.Instance.GoogleService.WebClientId = webClientId;
    }
}
