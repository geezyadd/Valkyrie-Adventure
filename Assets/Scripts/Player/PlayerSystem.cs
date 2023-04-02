using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem 
{
    private readonly PlayerEntity _playerEntity;
    private readonly PlayerBrain _playerBrain;

    public PlayerSystem(PlayerEntity playerEntity, List<IEtityInputSource> inputSources)
    {
        _playerEntity = playerEntity;
        _playerBrain = new PlayerBrain(_playerEntity, inputSources);
    }
}
