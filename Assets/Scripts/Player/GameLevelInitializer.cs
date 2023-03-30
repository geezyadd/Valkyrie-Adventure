using Assets.Scripts.Player;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelInitializer : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private GameUiInputView _gameUiInputView;
    private ExternalDevicesInputReader _externalDevicesInputReader;
    private PlayerBrain _playerBrain;
    private void Awake()
    {
        _externalDevicesInputReader = new ExternalDevicesInputReader();
        _playerBrain = new PlayerBrain(_playerEntity, new List<IEtityInputSource> 
        {
            _gameUiInputView,
            _externalDevicesInputReader
        });
    }
    private void Update()
    {
        _externalDevicesInputReader.OnUpdate();
        
    }
    private void FixedUpdate()
    {
       _playerBrain.OnFixedUpdate();

    }

}
