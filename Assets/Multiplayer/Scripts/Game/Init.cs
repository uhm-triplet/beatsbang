using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{


    public class Init : MonoBehaviour
    {
        // Start is called before the first frame update
        async void Start()
        {
            await UnityServices.InitializeAsync();

            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                AuthenticationService.Instance.SignedIn += OnSignedIn;
                await AuthenticationService.Instance.SignInAnonymouslyAsync();


                if (AuthenticationService.Instance.IsSignedIn)
                {
                    string username = PlayerPrefs.GetString("Username");
                    if (username == "")
                    {
                        username = "Player";
                        PlayerPrefs.SetString("Username", username);
                    }

                    SceneManager.LoadSceneAsync("Main");
                }
            }
        }

        private void OnSignedIn()
        {
            Debug.Log($"Token: {AuthenticationService.Instance.AccessToken}");
            Debug.Log($"PlayerId: {AuthenticationService.Instance.PlayerId}");
        }

    }
}