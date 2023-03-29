using Player;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class PlayerBrain 
{
    private readonly PlayerEntity _playerEntity;
    private readonly List<IEtityInputSource> _inputSources;
    private bool IsJump => _inputSources.Any(source => source.Jump);
    private bool IsAttack => _inputSources.Any(source => source.Attack);
    public PlayerBrain(PlayerEntity playerEntity, List<IEtityInputSource> inputSources) 
    {
        _playerEntity = playerEntity;
        _inputSources = inputSources;
    }
    public void OnFixedUpdate() 
    {
        _playerEntity.MoveHorizontally(GetHorizontalDirection());
        if (IsJump) 
        {
            _playerEntity.Jump();
            _playerEntity.WallClimb();
        }
        if (IsAttack)
        {
            _playerEntity.Attack();
           
        }

        foreach (var inputSource in _inputSources) 
        {
            inputSource.ResetOneTimeActions();
        }
    }

    private float GetHorizontalDirection() 
    {
        foreach(var inputSource in _inputSources) 
        {
            if(inputSource._direction == 0) 
            {
                continue;
            }
            return inputSource._direction;
        }
        return 0;
    }
}
