using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManagerment : MonoBehaviour
{
    [System.Serializable]
    public class Fx {
        public string Name;
        public GameObject Prefab;
    }
    [SerializeField] private Transform m_transform;
    [SerializeField] private List<Fx> m_Fxes;
    private Dictionary<string, GameObject> m_FxesPool;
    private static FxManagerment m_instance;
    public static FxManagerment Instance {
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
    private void Start() {
        this.m_FxesPool = new Dictionary<string, GameObject>();
        for (int i = 0; i < m_Fxes.Count; i++)
        {
            this.m_FxesPool.Add(m_Fxes[i].Name, m_Fxes[i].Prefab);
        }
    }
    public void GetFx(string name, Vector2 pos) {
        if (m_FxesPool.ContainsKey(name)) {
            var fx = Instantiate(m_FxesPool[name], pos, Quaternion.identity);
            fx.transform.SetParent(this.m_transform);
            fx.transform.localScale = Vector2.one;
        }
    }
}
