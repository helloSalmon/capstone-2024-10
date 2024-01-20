using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �Ŵ������� �����ϰ� �ִ� ���� �Ŵ���
/// ���� �Ŵ����� ���� @Mangers�� ������Ʈ�� �پ� �ִ�.
/// </summary>
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { init(); return s_instance; } }

    // �ٸ� �Ŵ����� �����ؼ� ���� �� �ʱ�ȭ �۾��� Ŭ���� �۾��� �����ؼ� Init�� Clear���� ȣ���Ѵ�.
    // �Ϻ� �Ŵ������� Monobehaviour�� ���� ������� �ʴ´�.
    // ������ �κ��� �� �̻� �Ŵ����� �Ҵ����� �ʰ� ���ӿ�����Ʈ�� �Ҵ��Ѵ�.
    #region Contents
    GameManagerEX _game = new GameManagerEX();

    public static GameManagerEX Game { get { return Instance._game; } }
    #endregion

    // �ھ� �κ��� ���ӿ��� �ʿ� ����� �Ŵ������� �Ҵ�ȴ�.
    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    #endregion

    void Start()
    {
        init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    /// <summary>
    /// ó���� ȭ���� �ε�� �� �Ŵ������� �ʱ�ȭ�� ���Ǵ� �Լ�, 
    /// ���� ���� �Ŵ��� ������ �������� ������ �ڵ����� �����Ѵ�.
    /// </summary>
    static void init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._game.init();
            s_instance._pool.init();
            s_instance._sound.init();
            s_instance._data.init();
        }
    }

    /// <summary>
    /// �� �Ŵ������� �ʱ�ȭ���ִ� �Լ�
    /// ������ ���Ƿ� �ٲٸ� ������ �߻��� �� �ִ�.
    /// </summary>
    public static void Clear()
    {
        Game.Clear();
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
