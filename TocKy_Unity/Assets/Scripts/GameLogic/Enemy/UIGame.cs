using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject m_gameStartPanel;
    [SerializeField] private GameObject m_mapLevelPanel;
    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private GameObject m_gameWinPanel;

    [SerializeField] private GameObject m_gameIntro;
    public Text m_templateVB;
    public Text m_templateTocKy;
    public Image m_popupTemPlateTyping;
    public Animator m_popupAnimator;
    private static UIGame s_instance;
    public static UIGame Instance {
        get {
            return s_instance;
        }
    }
    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;
    }
    public void OnStart() {
        m_gameStartPanel.SetActive(false);
        m_mapLevelPanel.SetActive(true);
    }
    public void GameOver() {
        m_gameOverPanel.SetActive(true);
    }
    public void GameWin() {
        this.m_gameWinPanel.SetActive(true);
    }
    public void OnRestart() {
        m_gameOverPanel.SetActive(false);

    }
    public void OnLevel1() {
        this.SelectLevel(1);
    }
    public void SelectLevel(int level) {
        m_gameIntro.SetActive(false);
        m_mapLevelPanel.SetActive(false);
        if (GameData.SceneDataCurrent != null) Destroy(GameData.SceneDataCurrent);
        GameData.SceneDataCurrent = Instantiate(Resources.Load("SceneData/SceneData_" + level), new Vector2(-3.85f, 1.6f), Quaternion.identity) as GameObject;
        CameraFollow.Instance.Begin();
    }
 }
