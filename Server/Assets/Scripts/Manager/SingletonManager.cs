using UnityEngine;

public static class Managers
{
    public static SceneManager Scene { get { return SceneManager.Instance; } }
    public static NetworkManager Network { get { return NetworkManager.Instance; } }
    public static GPHManager GPH { get { return GPHManager.Instance; } }
}

public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null && Time.timeScale != 0)
            {
                instance = FindObjectOfType<T>() ?? new GameObject(typeof(T).Name).AddComponent<T>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        Time.timeScale = 0;
    }
}