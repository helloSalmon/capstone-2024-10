using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����Ƽ�� �����ϴ� ���Ŵ����� ������ �Ŵ���
/// </summary>
public class SceneManagerEx
{
    /// <summary>
    /// ���� ��ġ�� ���� ������� �˾Ƴ���.
    /// ������ ���̽� ���� ����� ��ũ��Ʈ�� ������ @Scene ��ü�� ������ �ִ�.
    /// </summary>
    public BaseScene CurrentScene
    {
        get { return GameObject.FindObjectOfType<BaseScene>(); }
    }

    public Define.Scene PreviousScene { get; set; }

    /// <summary>
    /// ���� �ҷ��´�. �ٸ� ������ ��Ʈ���� �ƴ� enum Ÿ������ ������ �� �ִ�.
    /// ���������� ���� �Ŵ����� ���� �ʱ�ȭ��Ű�� �ڵ带 ������ ���� �ٲ� ��,
    /// �ڵ����� �ʱ�ȭ �۾��� �̷�������� �Ѵ�.
    /// </summary>
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        PreviousScene = CurrentScene.SceneType;
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void ReturnScene()
    {
        SceneManager.LoadScene(GetSceneName(PreviousScene));
        PreviousScene = CurrentScene.SceneType;
    }

    /// <summary>
    /// ���� �̸��� �����´�. ���� �̸��� Ư�� ��Ģ�� ���� ��� ���⿡ �ݿ��Ѵ�.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    /// <summary>
    /// ���̽� ���� ����� ������ ���� ���ǵǾ� �ִ� Ŭ���� �Լ��� �ҷ��´�.
    /// </summary>
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
