using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

/// <summary>
/// Ǯ�� �Ŵ����� ���ҽ� �Ŵ����� �ͼӵǾ� �ִ� �Ŵ����� ����� ��ũ��Ʈ���� ���� ȣ���� �ʿ䰡 ����.
/// </summary>
public class PoolManager
{
    #region Pool
    class Pool
    {
        /// <summary>
        /// Ǯ�� ������Ʈ�� ������
        /// </summary>
        public GameObject Original { get; private set; }
        /// <summary>
        /// Ǯ�� ������Ʈ�� ��Ʈ
        /// </summary>
        public Transform Root { get; set; }
        /// <summary>
        /// Ǯ�� ������Ʈ�� ������ ����
        /// </summary>
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        /// <summary>
        /// ������ ������ ������Ʈ���� ��Ʈ�� ����� ���� �θ� �Ʒ��� ��� �д�.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="count"></param>
        public void init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        /// <summary>
        /// Ǯ�� ������Ʈ�� �����Ѵ�.
        /// </summary>
        /// <returns>Poolable ��ũ��Ʈ�� ���� �����Ѵ�</returns>
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        /// <summary>
        /// Ǯ�� ������Ʈ�� ���ÿ� �ִ´�.
        /// </summary>
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        /// <summary>
        /// ������ �θ� �ؿ��� Ǯ�� ������Ʈ�� ã�� �̾ƿ´�.
        /// </summary>
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            // DontDestroyOnLoad ���� �뵵
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    /// <summary>
    /// ��� Ǯ�� ��Ʈ�� ����� �ְ� ���� �ٲ� �������� �ʰ� ������ �Ѵ�.
    /// </summary>
    public void init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root"}.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    /// <summary>
    /// �������� �ָ� �ش��ϴ� ������ŭ Ǯ�� ������Ʈ�� ����� Ǯ�� �־� ���´�.
    /// </summary>
    /// <param name="original">Ǯ�� ������ ������</param>
    /// <param name="count">�� ���� ������Ʈ�� �̸� ������ ������ ���Ѵ�.</param>
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    /// <summary>
    /// Ǯ�� Push �Լ��� ������ ���� �Լ�
    /// </summary>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
        }

        _pool[name].Push(poolable);
    }

    /// <summary>
    /// Ǯ�� Pop �Լ��� ������ ���� �Լ�
    /// </summary>
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    /// <summary>
    /// Ǯ�� ����Ǿ� �ִ� �������� �ҷ��´�.
    /// </summary>
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    /// <summary>
    /// ���� �ٲ���� ��, �����س��� Ǯ�� �ʱ�ȭ���ִ� �ڵ�
    /// </summary>
    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
