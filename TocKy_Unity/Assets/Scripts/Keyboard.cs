using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public delegate void CallbackParameter(string str1, string str2);
public class Keyboard : MonoBehaviour
{
    [SerializeField] private bool m_isKeyBoardGame;
    [SerializeField] private Text m_myText;
    [SerializeField] private Text m_myHistory;
    [SerializeField] private List<Key> m_keyboardsRoot;
    [SerializeField] private List<Key> m_keyboardsTocKyRoot; 
    private Dictionary<string, Key> m_keyboards;
    private Dictionary<string, Key> m_keyboardsTocKy;
    private Dictionary<string, int> m_dicCodeCharacterAmDau;
    private Dictionary<string, int> m_dicCodeCharacterAmChinh;
    private Dictionary<string, int> m_dicCodeCharacterAmCuoi;
    private Dictionary<string, string> m_dicRuleAmTiet;
    public Dictionary<string, string> DicRuleAmTiet {
        get {
            return m_dicRuleAmTiet;
        }
    }
    private static Keyboard m_instance;
    public static Keyboard Instance {
        get {
            return m_instance;
        }
    }
    private void Awake() {
        if (m_instance != null && m_instance != this) {
            Destroy(this.gameObject);
            return;
        }
        m_instance = this;
    }
    void Start()
    {
        
        m_keyboards = new Dictionary<string, Key>();
        m_keyboardsTocKy = new Dictionary<string, Key>();
        m_dicCodeCharacterAmDau = new Dictionary<string, int>() {
            //âm đầu
            {"#", 0},
            {"S-", 1}, // Phím Q
            {"T-", 2}, // Phím A
            {"K-", 3}, // phím W
            {"P", 4}, // Phím S
            {"R", 5}, // Phím E
            {"H-", 6}, // Phím D
            //âm chính
            {"N-", 7}, // Phím R or F
            {"-H", 8}, // Phím T :TODO check tiep thang H
            {"-S", 9}, // Phím G
            {"*", 10}, // Phím U
            {"I", 11}, // Phím J
            {"U", 12}, // Phím C
            {"O", 13}, // Phím V
            {"E", 14}, // Phím N
            {"A", 15}, // Phím M
            {"W", 16}, // Phím I
            {"Y", 17}, // Phím K
            //âm cuối
            {"J-", 18}, // Phím O or L 
            {"-N", 19}, // Phím P
            {"G", 20}, // Phím ;
            {"-T", 21}, // Phím [
            {"-K", 22}, // Phím '
        };
        m_dicCodeCharacterAmChinh = new Dictionary<string, int>() {
            //âm chính
            {"N-", 7}, // Phím R or F
            {"-H", 8}, // Phím T :TODO check tiep thang H
            {"-S", 9}, // Phím G
            {"*", 10}, // Phím U
            {"I", 11}, // Phím J
            {"U", 12}, // Phím C
            {"O", 13}, // Phím V
            {"E", 14}, // Phím N
            {"A", 15}, // Phím M
            {"W", 16}, // Phím I
            {"Y", 17}, // Phím K
            
            
        };
        m_dicCodeCharacterAmCuoi = new Dictionary<string, int>() {
            //âm cuối
            {"J-", 18}, // Phím O or L 
            {"-N", 19}, // Phím P
            {"G", 20}, // Phím ;
            {"-T", 21}, // Phím [
            {"-K", 22}, // Phím '
            
        };
        this.SetRules();
        foreach (var key in m_keyboardsRoot)
        {
            key.Color = key.Icon.color;
            m_keyboards.Add(key.Label, key);
        }
        foreach (var key in m_keyboardsTocKyRoot)
        {
            key.Color = key.Icon.color;
            m_keyboardsTocKy.Add(key.Label, key);
        }
    }
    private void SetRules() {
        m_dicRuleAmTiet = new Dictionary<string, string>();
        // thêm từ điển âm đầu
        TextAsset ruleAmDauFile = Resources.Load("AmDauTocKy") as TextAsset;
        var rulesAmDau = ruleAmDauFile.text.Split('\r','\n');
        for (int i = 0; i < rulesAmDau.Length; i++)
        {
            if (!rulesAmDau[i].Equals("")) {
                var tmps = rulesAmDau[i].Split('\t');
                if (!m_dicRuleAmTiet.ContainsKey(tmps[1])) m_dicRuleAmTiet.Add(tmps[1], tmps[0]);
            }
        }
        // thêm từ điển âm chính
        TextAsset ruleAmChinhFile = Resources.Load("AmChinhTocKy") as TextAsset;
        var rulesAmChinh = ruleAmChinhFile.text.Split('\r','\n');
        for (int i = 0; i < rulesAmChinh.Length; i++)
        {
            if (!rulesAmChinh[i].Equals("")) {
                var tmps = rulesAmChinh[i].Split('\t');
                if (!m_dicRuleAmTiet.ContainsKey(tmps[1])) m_dicRuleAmTiet.Add(tmps[1], tmps[0]);
            }
            
        }
        // thêm từ điển âm cuối
        TextAsset ruleAmCuoiFile = Resources.Load("AmCuoiTocKy") as TextAsset;
        var rulesAmCuoi = ruleAmCuoiFile.text.Split('\r','\n');
        for (int i = 0; i < rulesAmCuoi.Length; i++)
        {
            if (!rulesAmCuoi[i].Equals("")) {
                var tmps = rulesAmCuoi[i].Split('\t');
                if (!m_dicRuleAmTiet.ContainsKey(tmps[1])) m_dicRuleAmTiet.Add(tmps[1], tmps[0]);
            }
            
        }
        // thêm từ điển số
        TextAsset ruleSoFile = Resources.Load("So") as TextAsset;
        var rulesSo = ruleSoFile.text.Split('\r','\n');
        for (int i = 0; i < rulesSo.Length; i++)
        {
            if (!rulesSo[i].Equals("")) {
                var tmps = rulesSo[i].Split('\t');
                if (!m_dicRuleAmTiet.ContainsKey(tmps[1])) m_dicRuleAmTiet.Add(tmps[1], tmps[0]);
            }
        }
        // thêm từ Ký tự đặc biệt
        TextAsset ruleKyTuDacBietFile = Resources.Load("KiTuDacBiet") as TextAsset;
        var rulesKyTuDacBiet = ruleKyTuDacBietFile.text.Split('\r','\n');
        for (int i = 0; i < rulesKyTuDacBiet.Length; i++)
        {
            if (!rulesKyTuDacBiet[i].Equals("")) {
                var tmps = rulesKyTuDacBiet[i].Split('\t');
                if (!m_dicRuleAmTiet.ContainsKey(tmps[1])) m_dicRuleAmTiet.Add(tmps[1], tmps[0]);
            }
        }
        //Test
        // foreach (var item in m_dicRuleAmTiet)
        // {
        //     Debug.Log(item.Key + "  " + item.Value);
        // }
    }
    // Update is called once per frame
    void Update()
    {
        this.GoAmDau();
        
    }
    private string m_codeKeyPressedAmDau = "";
    private bool m_keyPressed = false;
    private void GoAmDau() {
        // if (Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.A ) && Input.GetKeyDown(KeyCode.W)) {
        //     m_keyboards["Q"].Icon.color = Color.red;
        //     m_keyboards["A"].Icon.color = Color.red;
        //     m_keyboards["W"].Icon.color = Color.red;
        //     this.ShowCharacter("thuong", new char[] {'Q', 'A', 'W'});
        // }
        // if (Input.GetKeyUp(KeyCode.Q)) {
        //     m_keyboards["Q"].Icon.color = m_keyboards["Q"].Color;
        //     m_keyboards["A"].Icon.color = m_keyboards["A"].Color;
        //     m_keyboards["W"].Icon.color = m_keyboards["W"].Color;
        // }
        m_keyPressed = Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)
        || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9);
        if (m_keyPressed) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "# ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "S- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "K- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "T- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "P ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "R ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "H- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        //
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.F)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "N- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "-H ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "-S ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.U)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "* ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "I ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "U ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "O ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "E ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "A ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "W ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "Y ";
            Invoke("SortStringAmDau", 0.2f);
        }
        //
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.L)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "J- ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "-N ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "G ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "-T ";
            Invoke("SortStringAmDau", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Quote)) {
            CancelInvoke("SortStringAmDau");
            m_codeKeyPressedAmDau += "-K ";
            Invoke("SortStringAmDau", 0.2f);
        }
        this.FeedbackKeyboard();
    }
    private Color m_tocKyKeyboardFeedbackColor = new Color(0.003960493f, 0.5139203f, 0.8396226f, 1);
    public void FeedbackKeyboard() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            m_keyboards["`"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            m_keyboards["1"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            m_keyboards["2"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            m_keyboards["3"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            m_keyboards["4"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            m_keyboards["5"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            m_keyboards["6"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            m_keyboards["7"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            m_keyboards["8"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            m_keyboards["9"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            m_keyboards["0"].Icon.color = Color.red;
            m_keyboardsTocKy["#"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Minus)) {
            m_keyboards["-"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Equals)) {
            m_keyboards["="].Icon.color = Color.red;
        }
        // if (Input.GetKeyDown(KeyCode.Backspace)) {
        //     m_keyboards["Backspace"].Icon.color = Color.red;
        // }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            m_keyboards["Tab"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            m_keyboards["Q"].Icon.color = Color.red;
            m_keyboardsTocKy["S-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            m_keyboards["W"].Icon.color = Color.red;
            m_keyboardsTocKy["K-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            m_keyboards["E"].Icon.color = Color.red;
            m_keyboardsTocKy["R"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            m_keyboards["R"].Icon.color = Color.red;
            m_keyboardsTocKy["N-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            m_keyboards["T"].Icon.color = Color.red;
            m_keyboardsTocKy["-H"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            m_keyboards["Y"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.U)) {
            m_keyboards["U"].Icon.color = Color.red;
            m_keyboardsTocKy["*"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            m_keyboards["I"].Icon.color = Color.red;
            m_keyboardsTocKy["W"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            m_keyboards["O"].Icon.color = Color.red;
            m_keyboardsTocKy["J-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            m_keyboards["P"].Icon.color = Color.red;
            m_keyboardsTocKy["-N"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            m_keyboards["["].Icon.color = Color.red;
            m_keyboardsTocKy["-T"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.RightBracket)) {
            m_keyboards["]"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Backslash)) {
            m_keyboards["Backslash"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.CapsLock)) {
            m_keyboards["CapsLock"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            m_keyboards["A"].Icon.color = Color.red;
            m_keyboardsTocKy["T-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            m_keyboards["S"].Icon.color = Color.red;
            m_keyboardsTocKy["P"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            m_keyboards["D"].Icon.color = Color.red;
            m_keyboardsTocKy["H-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            m_keyboards["F"].Icon.color = Color.red;
            m_keyboardsTocKy["N-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            m_keyboards["G"].Icon.color = Color.red;
            m_keyboardsTocKy["-S"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            m_keyboards["H"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            m_keyboards["J"].Icon.color = Color.red;
            m_keyboardsTocKy["I"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            m_keyboards["K"].Icon.color = Color.red;
            m_keyboardsTocKy["Y"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            m_keyboards["L"].Icon.color = Color.red;
            m_keyboardsTocKy["J-"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Semicolon)) {
            m_keyboards["Semicolon"].Icon.color = Color.red;
            m_keyboardsTocKy["G"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Quote)) {
            m_keyboards["Quote"].Icon.color = Color.red;
            m_keyboardsTocKy["-K"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            m_keyboards["Enter"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            m_keyboards["ShiftLeft"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            m_keyboards["Z"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            m_keyboards["X"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            m_keyboards["C"].Icon.color = Color.red;
            m_keyboardsTocKy["U"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            m_keyboards["V"].Icon.color = Color.red;
            m_keyboardsTocKy["O"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            m_keyboards["B"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            m_keyboards["N"].Icon.color = Color.red;
            m_keyboardsTocKy["E"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            m_keyboards["M"].Icon.color = Color.red;
            m_keyboardsTocKy["A"].Icon.color = m_tocKyKeyboardFeedbackColor;
        }
        if (Input.GetKeyDown(KeyCode.Comma)) {
            m_keyboards["Comma"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Period)) {
            m_keyboards["Period"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Slash)) {
            m_keyboards["Slash"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            m_keyboards["ShiftRight"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            m_keyboards["CtrLeft"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.LeftWindows)) {
            m_keyboards["WinLeft"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            m_keyboards["AltLeft"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            m_keyboards["Space"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.RightAlt)) {
            m_keyboards["AltRight"].Icon.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.RightControl)) {
            m_keyboards["CtrRight"].Icon.color = Color.red;
        }

    }
    private void RefreshFeedbackKeyboard() {
        foreach (var key in m_keyboards)
        {
            key.Value.Icon.color = key.Value.Color;
        }
        foreach (var key in m_keyboardsTocKy)
        {
            key.Value.Icon.color = key.Value.Color;
        }
    }
    private string[] m_codeTocKyAmDau;

    private string last;
    private int j;
    private void SortStringAmDau() {
        //Insertion Sort link tham khảo https://viblo.asia/p/cac-thuat-toan-sap-xep-co-ban-Eb85ooNO52G
        m_codeTocKyAmDau =  m_codeKeyPressedAmDau.Trim().Split(' ');
        
        for (int i = 1; i < m_codeTocKyAmDau.Length; i++)
        {
            last = m_codeTocKyAmDau[i];
            j = i;
            while ((j>0) && m_dicCodeCharacterAmDau[m_codeTocKyAmDau[j-1]] > m_dicCodeCharacterAmDau[last]) {
                m_codeTocKyAmDau[j] = m_codeTocKyAmDau[j-1];
                j = j-1;
            }
            m_codeTocKyAmDau[j] = last;
        }
        Debug.Log(string.Join("", m_codeTocKyAmDau));
        if (m_isKeyBoardGame) {
            // this.ShowCharacter(string.Join("", m_codeTocKyAmDau), CheckResultTyingInGame);
            this.ShowCharacter(string.Join("", m_codeTocKyAmDau), null); // test
        } 
        else {
            this.ShowCharacter(string.Join("", m_codeTocKyAmDau), null);
        }
        m_codeKeyPressedAmDau = "";
        this.RefreshFeedbackKeyboard();
    }
    
    private void ShowCharacter(string codeTocKy, CallbackParameter callback) {
        if (m_myText != null && m_dicRuleAmTiet.ContainsKey(codeTocKy)) m_myText.text += m_dicRuleAmTiet[codeTocKy];
        m_myHistory.text += "\n" + DateTime.Now.ToString() + ": ";
        m_myHistory.text += codeTocKy;
        if (!StopPoint.CodeTocKy.Equals("") && callback != null) callback(StopPoint.CodeTocKy, codeTocKy);
    }
    private void CheckResultTyingInGame(string codeTemplate, string codePressed) {
        if (codePressed.Equals(codeTemplate)) {
            // right typing
            GameData.Instance.AlertRightTyping();
            StartCoroutine(GameControler.Instance.ChangePlayerStateHandleDelay(1.0f, PlayerState.Jump));
            
        }
        else {
            // wrong typing
            GameData.Instance.AlertWrongTyping();
            StartCoroutine(GameControler.Instance.ChangePlayerStateHandleDelay(1.0f, PlayerState.Die));
        }

    }
    //test
    public IEnumerator NonPlayer() {
        yield return new WaitForSeconds(2.0f);
        this.CheckResultTyingInGame("","");
    }
    
}
