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

    //�̱��� ó�� ���� ���� ���� instance

   

    private FirebaseAuth auth;
    //����� ������ �ӽ÷� ���� �� �ִ� ����
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
        
      
     
        //�ӽ�ó��
        if (auth.CurrentUser != null)
        {
            LogOut();
            Debug.LogError("���� �α׾ƿ� üŷ");
        }
        auth.StateChanged += OnChanged;
        
        userInit = true;
        Debug.LogError("���� �ʱ�ȭ");
    }

    //�α��� ���°� ����� ������ ȣ���
    private void OnChanged(object sender, EventArgs e)
    {
        Debug.Log("��������" + user); 
                Debug.LogError("chekcing 1");
        if (user == null && userInit == true) 
        {
            Debug.LogError("checking 1-2");
        
            if (firstRegister == true && firstLogin != true)
            {
                LoginState?.Invoke(false);

               
                
                //���ʷ� �α����� ���Ѿ� user�� ������ ����Ǵ� ������ ����.
                //���� �α��� �ô� LoginState �ߵ� x
                
               
            }else
            
           
            
            if(firstRegister != true && firstLogin == true)
            {
                LoginState?.Invoke(true);
               
                
                Debug.LogError("ù �α���");
            }
            else
            {
                LoginState.Invoke(false);
                Debug.LogError("ù �α׾ƿ�");
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
                
                
                Debug.LogError("�ι�° ȸ������");
            } else
            if (!signed && firstLogin == true)
            {
                LoginState?.Invoke(true);
                Debug.LogError("�ι�° �α���");
              
            } else
            {
                LoginState?.Invoke(false);
                Debug.LogError("�ι�° �α׾ƿ�");
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
                Debug.LogError("ȸ������ ���");

                
                return;
            }
            if (task.IsFaulted)
            {
                //ȸ������ ���� ���� => �̸����� ������ / ��й�ȣ�� �ʹ� ����/ �̹� ���Ե� �̸��� ���...
                Debug.LogError("ȸ������ ����");
               
               
                return;

            }
           
            Debug.LogError("ȸ������ �Ϸ�");
            
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
                Debug.LogError("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                //�α��� ���� ���� => �̸����� ������ / ��й�ȣ�� �ʹ� ���� / �̹� ���Ե� �̸��� ���...
                Debug.LogError("�α��� ����");
                return;

            }
            AuthResult authResult = task.Result;
            FirebaseUser User = authResult.User;




            Debug.LogError("�α��� �Ϸ�");




        });
    }
    public void LogOut()
    {
//        loginstatus = LoginStatus.Logout;
        auth.SignOut();
        Debug.LogError("�α׾ƿ�");
    }
}
