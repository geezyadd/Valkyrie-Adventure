//using Assets.Scripts.Player;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player 
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUiInputView _gameUiInputView;
        private ExternalDevicesInputReader _externalDevicesInputReader;
        private ProjectUpdater _projectUpdater;
        private List<IDisposable> _disposables;
        private PlayerSystem _playerSystem;
        private void Awake()
        {
            _disposables= new List<IDisposable>();
            if(ProjectUpdater.Instance == null) 
            {
                _projectUpdater= new GameObject().AddComponent<ProjectUpdater>();
            }
            else 
            {
                _projectUpdater =ProjectUpdater.Instance as ProjectUpdater;
            }
            _externalDevicesInputReader = new ExternalDevicesInputReader();
            _disposables.Add(_externalDevicesInputReader);
            _playerSystem = new PlayerSystem(_playerEntity, new List<IEtityInputSource> 
            {
                _gameUiInputView,
                _externalDevicesInputReader
            });
        }
        private void OnDestroy()
        {
                foreach(var disposable in _disposables) { disposable.Dispose(); }
        }

    }
}

