using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    [SerializeField] private InputField EmailInputField;
    [SerializeField] private InputField PasswordInputField;
    [SerializeField] private Button SignInButton;
    [SerializeField] private Text ErrorText;

    private Firebase.Auth.FirebaseAuth auth;
    private FirebaseUser user;

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SignInButtonPress()
    {
        string email = EmailInputField.text;
        string password = PasswordInputField.text;
        user = TrySingIn(email, password);
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("SampleScene");
    }

    private FirebaseUser TrySingIn(string email, string password)
    {
        FirebaseUser newUser = null;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                string message = "SignInWithEmailAndPasswordAsync was canceled.";
                Debug.LogError(message);
                ErrorText.text = message;
                return;
            }

            if (task.IsFaulted)
            {
                string message = "SignInWithEmailAndPasswordAsync encountered an error";
                Debug.LogError(message);
                ErrorText.text = message;
                return;
            }

            newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

        return newUser;
    }
}