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

    [SerializeField] private UserData _userData;
    // Start is called before the first frame update
    private Firebase.Auth.FirebaseAuth auth;
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SignInButtonPress()
    {
        string email = EmailInputField.text;
        string password = PasswordInputField.text;
        FirebaseUser user = TrySingIn(email, password);
            UserData userData = _userData;
            DontDestroyOnLoad(userData);
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
