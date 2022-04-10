using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public void OnClick()
    {
        SocketProxy.getInstance().login(usernameInput.text, passwordInput.text);

    }
}
