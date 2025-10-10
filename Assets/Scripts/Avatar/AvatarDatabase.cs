using UnityEngine;

public class AvatarDatabase : MonoBehaviour
{
    public static AvatarDatabase Instance { get; private set; }

    private const string AVATARS_PATH = "Avatars";

    public Sprite[] Avatars { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadAvatars();
    }

    private void LoadAvatars()
    {
        Avatars = Resources.LoadAll<Sprite>(AVATARS_PATH);
        if (Avatars == null || Avatars.Length == 0)
        {
            Debug.LogWarning("No avatars found in Resources/Avatars.");
        }
    }

    public Sprite GetAvatarByName(string name)
    {
        return Resources.Load<Sprite>($"{AVATARS_PATH}/{name}");
    }
}
