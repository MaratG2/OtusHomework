using Homeworks.SaveLoad;
using UnityEngine;
using Zenject;

public class LoadGameData : MonoBehaviour
{
    private SaveLoadManager _saveLoadManager;

    [Inject]
    public void Construct(SaveLoadManager saveLoadManager)
    {
        this._saveLoadManager = saveLoadManager;
    }

    private void Start()
    {
        _saveLoadManager.Load();
    }
}
