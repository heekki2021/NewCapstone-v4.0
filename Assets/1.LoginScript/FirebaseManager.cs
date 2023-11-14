using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

using System.Threading.Tasks;
using System;
using System.Threading;

public class FirebaseManager : MonoBehaviour
{
    //Firebase 변수들
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //로그인 변수들
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //회원가입 변수들
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField xpField;
    public TMP_InputField killsField;
    public TMP_InputField deathsField;
    public GameObject scoreElement;
    public Transform scoreboardContent;


    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("파이어 베이스 관련 의존성 해결을 하지 못하였습니다" +
                    "(버전 호환성, 라이브러리간 충돌, 잘못된 구성, 네트워크 문제 etc");
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //인증 객체 생성
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    
    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }
    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text)); 
    }
    
    public void SignOutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterFields();
        ClearLoginFields();
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateXp(int.Parse(xpField.text)));
        StartCoroutine(UpdateKills(int.Parse(killsField.text)));
        StartCoroutine(UpdateDeaths(int.Parse(deathsField.text)));
    }
    private IEnumerator Login(string _email, string _password)
    {
        //이메일과 비밀번호를 훑고 가는 FirebaseAuth를 호출
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);


        if (LoginTask.Exception != null)
        {
            //오류가 있다면 처리하기
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist"; break;
            }

            warningLoginText.text = message;

        }
        else
        {
            AuthResult authResult = LoginTask.Result;
            User = authResult.User;
            Debug.LogFormat("성공적으로 로그인 하였습니다: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "로그인";

            yield return new WaitForSeconds(1);

            usernameField.text = User.DisplayName;

            UIManager.instance.UserDataScreen();
            confirmLoginText.text = "";
            ClearLoginFields();
            ClearRegisterFields();

        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //유저 닉네임 필드가 빈칸이라면 경고메세지
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //이메일과 비밀번호를 넘겨주는 파이어베이스 로그인 함수를 호출
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //업무가 끝날때까지 기다리기
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            
            if(RegisterTask.Exception != null)
            {
                //에러가 있다면 캐치
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Aready In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                AuthResult authResult = RegisterTask.Result;
                User = authResult.User;

                if (User != null)
                {
                    //유저 프로파일을 생성하고 닉네임 설정
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //프로필과 닉네임을 넘겨줘서 현재 유저의 프로필을 업데이트하는 파이어베이스 auth
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //업무가 끝날때까지 기다리기.
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //에러가 있다면 처리하기
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                        
                    }
                    else
                    {
                        //유저의 이름이 설정되었다면
                        //로그인 페이지로 리턴
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }

            }
        }
    }
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //유저 프로파일을 만들고 유저이름을 설정
        UserProfile profile = new UserProfile { DisplayName = _username };

        //유저 이름이 담긴 프로필을 넘겨 유저 프로필 함수를 업데이트하는
        //firebase auth를 호출한다.
        var ProfileTask = User.UpdateUserProfileAsync(profile);

        //일 처리가 끝날 때까지 기다린다.
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated;
        }

    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated;
        }
    }

    private IEnumerator UpdateXp(int _xp)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").SetValueAsync(_xp);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }

    }


    private IEnumerator UpdateKills(int _kills)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("kills").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateDeaths(int _deaths)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(_deaths);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now Updated
        }
    }
}
