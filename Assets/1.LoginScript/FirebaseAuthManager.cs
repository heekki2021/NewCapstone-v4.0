using UnityEngine;
using Firebase.Auth;
using System;
using Firebase.Analytics;
using System.Runtime.CompilerServices;
using UnityEngine.Analytics;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class FirebaseAuthManager
{

    private static FirebaseAuthManager instance = null;

    //싱글톤 처리 유저 정보 저장 instance

   

    private FirebaseAuth auth;
    //사용자 정보를 임시로 담을 수 있는 변수
    public FirebaseUser user;
    public bool firstRegister = false;
    public bool firstLogin = false;
    public bool userInit = false;

    

//    
    public static FirebaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            }
            return instance;
        }
    }


    public Action<bool> LoginState;
    public string UserId => user.UserId;
  

    public void Init(GameObject gameObject)
    {
        Debug.LogError("Init called from: " + gameObject.name);
        auth = FirebaseAuth.DefaultInstance;
        
      
     
        //임시처리
        if (auth.CurrentUser != null)
        {
            LogOut();
            Debug.LogError("최초 로그아웃 체킹");
        }
        auth.StateChanged += OnChanged;
        
        userInit = true;
        Debug.LogError("최초 초기화");
    }

    //로그인 상태가 변경될 때마다 호출됨
    private void OnChanged(object sender, EventArgs e)
    {
        Debug.Log("유저정보" + user); 
                Debug.LogError("chekcing 1");
        if (user == null && userInit == true) 
        {
            Debug.LogError("checking 1-2");
        
            if (firstRegister == true && firstLogin != true)
            {
                LoginState?.Invoke(false);

               
                
                //최초로 로그인을 시켜야 user에 정보가 저장되는 것으로 보임.
                //최초 로그인 시는 LoginState 발동 x
                
               
            }else
            
           
            
            if(firstRegister != true && firstLogin == true)
            {
                LoginState?.Invoke(true);
               
                
                Debug.LogError("첫 로그인");
            }
            else
            {
                LoginState.Invoke(false);
                Debug.LogError("첫 로그아웃");
            }
        }
        else
        if (user != null)
        {
            bool signed = auth.CurrentUser != user && auth.CurrentUser != null;
            Debug.Log("checking222");
            if (!signed && firstRegister == true)
            {
               
                LoginState?.Invoke(false);
                
                
                Debug.LogError("두번째 회원가입");
            } else
            if (!signed && firstLogin == true)
            {
                LoginState?.Invoke(true);
                Debug.LogError("두번째 로그인");
              
            } else
            {
                LoginState?.Invoke(false);
                Debug.LogError("두번째 로그아웃");
            }

            
        }


    }

    public void Register(string email, string password)
    {
        firstRegister = true;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("회원가입 취소");

                
                return;
            }
            if (task.IsFaulted)
            {
                //회원가입 실패 이유 => 이메일이 비정상 / 비밀번호가 너무 간단/ 이미 가입된 이메일 등등...
                Debug.LogError("회원가입 실패");
               
               
                return;

            }
           
            Debug.LogError("회원가입 완료");
            
                //LoginState?.Invoke(false);
                firstRegister =false;
                return;
            
        });

    }

   
    public void LogIn(string email, string password)
    {   
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {

           
            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                //로그인 실패 이유 => 이메일이 비정상 / 비밀번호가 너무 간단 / 이미 가입된 이메일 등등...
                Debug.LogError("로그인 실패");
                return;

            }
            AuthResult authResult = task.Result;
            FirebaseUser User = authResult.User;




            Debug.LogError("로그인 완료");




        });
    }
    public void LogOut()
    {
//        loginstatus = LoginStatus.Logout;
        auth.SignOut();
        Debug.LogError("로그아웃");
    }
}
