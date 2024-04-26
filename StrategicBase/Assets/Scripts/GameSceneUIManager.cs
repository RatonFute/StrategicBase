using UnityEngine;


public class GameSceneUIManager : MonoBehaviour
{
    public GameObject SpawnMenu;
    [HideInInspector] public TacticCenter CurrentTacticCenter;

    private static GameSceneUIManager instance = null;
    public static GameSceneUIManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void OpenMenu(TacticCenter tacticCenter)
    {
        CurrentTacticCenter = tacticCenter;
        SpawnMenu.SetActive(true);
    }
    public void CloseMenu(TacticCenter tacticCenter)
    {
        CurrentTacticCenter = null;
        SpawnMenu.SetActive(false);
    }

}
