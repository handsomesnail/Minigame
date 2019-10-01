using ZCore;
using UnityEngine;
using UnityEngine.UI;

namespace Biz.Account {
    public class AccountView : View {
        public GameObject IndexRoot;
        public Button ToRegister;
        public Button ToLogin;
        public Button Guest;

        public GameObject RegisterRoot;
        public InputField RegisterUsername;
        public InputField RegisterPassword;
        public InputField RegisterRepeatPassword;
        public Button RegisterButton;
        public Button RegisterBack;

        public GameObject LoginRoot;
        public InputField LoginUsername;
        public InputField LoginPassword;
        public Button LoginButton;
        public Button LoginBack;

        public GameObject TipPanel;
        public Text Tip;
    }
}
