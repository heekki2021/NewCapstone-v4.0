using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LoginSystem : MonoBehaviour
{


    public GameObject YesNoWindow;
    public TMP_InputField email;
    public TMP_InputField password;

    

    

    public Button yesButton;
    public Button noButton;

    public FirebaseAuthManager firebaseAuthManager;

    public TMP_Text outputText;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init(gameObject);

        YesNoWindow.SetActive(false);
        
        
        
    }




    private void OnChangedState(bool sign)
    {
        if (FirebaseAuthManager.Instance.user == null)
        {
            if (FirebaseAuthManager.Instance.firstRegister == true ||
                FirebaseAuthManager.Instance.firstLogin == true)
            {
                outputText.text = sign ? "로그인 오류 : " : "회원가입 오류 : ";
                outputText.text += sign ? FirebaseAuthManager.Instance.UserId : "";
            }else
            {
                outputText.text = "로그인을 해주세요";
            }
        }
        else
        if (FirebaseAuthManager.Instance.user != null)
        {
            if (FirebaseAuthManager.Instance.firstRegister == true ||
                FirebaseAuthManager.Instance.firstLogin == true)
            {

                outputText.text = sign ? "로그인 : " : "회원가입 성공!";
                outputText.text += sign ? FirebaseAuthManager.Instance.UserId : "";
            }
            else
            {
                outputText.text = "로그아웃";
            }
        }
    }

    public void Register()
    {

       
        FirebaseAuthManager.Instance.firstRegister = true;

        string e = email.text;
        string p = password.text;

        FirebaseAuthManager.Instance.Register(e, p);
        YNwindowpopup();




    }

   
    public void LogIn()
    {

        FirebaseAuthManager.Instance.firstLogin = true;
      
        string e = email.text;
        string p = password.text;
        FirebaseAuthManager.Instance.LogIn(e, p);

    }


    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();

    }
    public void YNwindowpopup()
    {
        YesNoWindow.SetActive(true);
    }
    public void Button_Yes()
    {

    }

    public void Button_No()
    {
        email.text = "";
        password.text = "";
        YesNoWindow.SetActive(false);
    }
    
    
    // Update is called once per frame
}
