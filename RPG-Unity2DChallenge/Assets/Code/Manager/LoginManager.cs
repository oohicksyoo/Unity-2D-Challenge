using DG.Tweening;
using Project.Data;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Manager {
    public class LoginManager : MonoBehaviour {

        public enum LoginState {
            Login,
            Register
        }

        public enum TabState {
            None = 0,
            Username,
            Password,
            Email
        }

        [Header("Referenced Items")]
        [SerializeField]
        private GameObject username;
        [SerializeField]
        private GameObject password;
        [SerializeField]
        private GameObject email;
        [SerializeField]
        private GameObject register;
        [SerializeField]
        private GameObject login;
        [SerializeField]
        private GameObject back;
        [SerializeField]
        private GameObject confirm;

        [Header("Buttons")]
        [SerializeField]
        private Button loginButton;
        [SerializeField]
        private Button confirmButton;

        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI errorText;

        [Header("Input Fields")]
        [SerializeField]
        private TMP_InputField usernameInputField;
        [SerializeField]
        private TMP_InputField passwordInputField;
        [SerializeField]
        private TMP_InputField emailInputField;

        private LoginState loginState;
        private TabState tabState;
        private string usernameText;
        private string passwordText;
        private string emailText;

        public void Start () {
            changeState(LoginState.Login);
            changeTabState(TabState.None);
            loginButton.interactable = false;
            confirmButton.interactable = false;
            errorText.text = "";
        }

        public void Update() {
            #region Temp bypass of logging in
            if(Input.GetKeyDown(KeyCode.Minus)) {
                PlayerInformation.Instance.PlayerName = "Admin";
                PlayerInformation.Instance.IsAdmin = true;

                //Load after our login/register and admin check
                LoaderManager.Instance.LoadLevel(SceneList.MAIN_MENU_SCREEN, delegate (string E) {
                    LoaderManager.Instance.UnLoadLevel(SceneList.LOGIN); //Unload after new level is in
                });
            }

            if (Input.GetKeyDown(KeyCode.Alpha0)) {
                PlayerInformation.Instance.PlayerName = "Admin-2";
                PlayerInformation.Instance.IsAdmin = true;

                //Load after our login/register and admin check
                LoaderManager.Instance.LoadLevel(SceneList.MAIN_MENU_SCREEN, delegate (string E) {
                    LoaderManager.Instance.UnLoadLevel(SceneList.LOGIN); //Unload after new level is in
                });
            }
            #endregion

            //ENTER FOR LOGGING IN AND REGISTERING
            if (Input.GetKeyDown(KeyCode.Return)) {
                if(loginState == LoginState.Login && loginButton.interactable) {
                    OnAttemptToLogin();
                } else if(loginState == LoginState.Register && confirmButton.interactable) {
                    OnAttemptToRegister();
                }
            }

            //Handle Tabbing For Blackbelt and Crew
            if(Input.GetKeyDown(KeyCode.Tab)) {
                //Based on loginstate loop state back around
                if(loginState == LoginState.Login) {
                    tabState = ((int)tabState + 1 < 3) ? tabState + 1 : TabState.Username;
                } else {
                    tabState = ((int)tabState + 1 < 4) ? tabState + 1 : TabState.Username;
                }
                //Debug.LogFormat("{0}", tabState);

                //Deactivate
                usernameInputField.DeactivateInputField();
                passwordInputField.DeactivateInputField();
                emailInputField.DeactivateInputField();

                switch (tabState) {
                    case TabState.Username:
                        usernameInputField.ActivateInputField();
                        usernameInputField.MoveToEndOfLine(false, false);
                        break;
                    case TabState.Password:
                        passwordInputField.ActivateInputField();
                        break;
                    case TabState.Email:
                        emailInputField.ActivateInputField();
                        break;
                    default:
                        break;
                }
            }
        }

        #region Text Input
        public void OnUsernameChange(string Value) {
            usernameText = Value;
            checkButtons();
        }

        public void OnPasswordChange(string Value) {
            passwordText = Value;
            checkButtons();
        }

        public void OnEmailChange(string Value) {
            emailText = Value;
            checkButtons();
        }
        #endregion

        #region Button States
        public void ChangeToLogin() {
            changeState(LoginState.Login);
        }

        public void ChangeToRegister() {
            changeState(LoginState.Register);
        }

        public void OnAttemptToLogin() {
            if (validateUsername(usernameText) && validatePassword(passwordText)) {
                string username = usernameText;
                QueryValues qv = Database.Instance.GenerateQueryValues("LoginUser", new Dictionary<string, string> {
                    {"username", usernameText},
                    {"password", passwordText}
                });

                Database.Instance.QueryDatabase(qv, delegate (string Value) {
                    //Debug.Log(Value);
                    GeneralReturn generalReturn = JsonUtility.FromJson<GeneralReturn>(Value);
                    if (generalReturn.Result) {
                        PlayerInformation.Instance.PlayerName = generalReturn.Details;
                        onCheckAdminStatus();                        
                    } else {
                        //If Fail present fail message given by server
                        errorText.DOKill();
                        errorText.color = errorText.color.SetAlpha(1);
                        errorText.text = "Error: " + generalReturn.Details;
                        errorText.DOFade(0, 0.5f).SetDelay(5).SetEase(Ease.InOutSine).OnComplete(() => {
                            errorText.text = "";
                        });
                    }
                });
            }
        }

        public void OnAttemptToRegister() {
            if (validateUsername(usernameText) && validatePassword(passwordText) && validateEmail(emailText)) {
                string username = usernameText;
                QueryValues qv = Database.Instance.GenerateQueryValues("RegisterUser", new Dictionary<string, string> {
                    {"username", usernameText},
                    {"password", passwordText},
                    {"email", emailText}
                });

                Database.Instance.QueryDatabase(qv, delegate (string Value) {
                    //Debug.Log(Value);
                    GeneralReturn generalReturn = JsonUtility.FromJson<GeneralReturn>(Value);
                    if (generalReturn.Result) {
                        PlayerInformation.Instance.PlayerName = generalReturn.Details;
                        onCheckAdminStatus();
                    } else {
                        //If Fail present fail message given by server
                        errorText.DOKill();
                        errorText.color = errorText.color.SetAlpha(1);
                        errorText.text = "Error: " + generalReturn.Details;
                        errorText.DOFade(0, 0.5f).SetDelay(5).SetEase(Ease.InOutSine).OnComplete(() => {
                            errorText.text = "";
                        });
                    }
                });
            }
        }

        private void onCheckAdminStatus() {
            QueryValues qv = Database.Instance.GenerateQueryValues("CheckAdminStatus", new Dictionary<string, string> {
                {"username", usernameText},
                {"password", passwordText}
            });

            Database.Instance.QueryDatabase(qv, delegate (string Value) {
                //Debug.Log(Value);
                GeneralReturn generalReturn = JsonUtility.FromJson<GeneralReturn>(Value);
                if (generalReturn.Result) {
                    PlayerInformation.Instance.IsAdmin = true;
                }

                //Load after our login/register and admin check
                LoaderManager.Instance.LoadLevel(SceneList.MAIN_MENU_SCREEN, delegate (string E) {
                    LoaderManager.Instance.UnLoadLevel(SceneList.LOGIN); //Unload after new level is in
                });
            });

            
        }
        #endregion

        #region Validation Checks
        private bool validateUsername(string Value) {
            if (Value == null) {
                return false;
            }

            if (Value.Length >= 4) {
                return true;
            }
            return false;
        }

        private bool validatePassword(string Value) {
            if (Value == null) {
                return false;
            }

            if (Value.Length >= 8) {
                return true;
            }
            return false;
        }

        private bool validateEmail(string Value) {
            if (Value == null) {
                return false;
            }

            try {
                var addr = new System.Net.Mail.MailAddress(Value);
                return addr.Address == Value;
            } catch {
                return false;
            }
        }
        #endregion

        #region Tab State
        public void OnTabState(int StateID) {
            tabState = (TabState)StateID;
        }

        public void OnDeselectInputField() {
            tabState = TabState.None;
        }
        #endregion

        private void changeState(LoginState State) {
            loginState = State;

            switch (loginState) {
                case LoginState.Login:
                    username.SetActive(true);
                    password.SetActive(true);
                    email.SetActive(false);
                    register.SetActive(true);
                    login.SetActive(true);
                    back.SetActive(false);
                    confirm.SetActive(false);
                    break;
                case LoginState.Register:
                    username.SetActive(true);
                    password.SetActive(true);
                    email.SetActive(true);
                    register.SetActive(false);
                    login.SetActive(false);
                    back.SetActive(true);
                    confirm.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void changeTabState(TabState State) {
            tabState = State;
            //TODO: YO finish this
        }

        private void checkButtons() {
            //Validate and enable/disable login and create
            if (validateUsername(usernameText) && validatePassword(passwordText)) {
                loginButton.interactable = true;
                if (validateEmail(emailText)) {
                    confirmButton.interactable = true;
                } else {
                    confirmButton.interactable = false;
                }
            } else {
                loginButton.interactable = false;
                confirmButton.interactable = false;
            }
        }
    }
}
