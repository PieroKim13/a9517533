using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private bool initialized = false;

    /// <summary>
    /// �̹� ����ó�� �ߴ��� Ȯ���ϴ� ����
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// �̱��� ��ü
    /// </summary>
    private static T instance;

    /// <summary>
    /// �̱����� ��ü�� �б� ���� ������Ƽ
    /// </summary>
    public static T Inst
    {
        get
        {
            //���� ó�� ���� ��
            if(isShutDown)
            {
                //��� ���
                Debug.LogWarning($"{typeof(T).Name} �̱����� �̹� ���� ���̴�.");
                //null ��ȯ
                return null;
            }
            //instance�� ���� ��
            if(instance == null)
            {
                T singletone = FindObjectOfType<T>();
                if( singletone == null )
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = $"{typeof(T).Name} Singletone";
                    singletone = gameObj.AddComponent<T>();
                }
                instance = singletone;

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!initialized)
        {
            OnPreInitialize();
        }
        if (mode != LoadSceneMode.Additive)
        {
            OnIntialize();
        }
    }

    private void OnApplicationQuit()
    {
        isShutDown = true;
    }

    /// <summary>
    /// �̱����� ������� �� �� �ѹ��� ȣ��� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        initialized = true;
    }

    /// <summary>
    /// �̱����� ��������� ���� Single�� �ε�� ������ ȣ�� �� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnIntialize()
    {

    }
}
