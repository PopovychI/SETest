using UnityEngine.EventSystems;

public class PlayerMainState : State
{
    private InputActionsInterpreter _input;
    private PlayerCharacterBehaviour _player;
    private Movement _playerMovement;
    private PlayerAttackState _attackState;
    public void InitializeState(PlayerCharacterBehaviour player,
                                PlayerAttackState attackState,
                                InputActionsInterpreter input)
    {
        _input = input;
        _player = player;
        _attackState = attackState;
        _playerMovement =_player.CharMovement;
    }
    public override State RunCurrentState()
    {
        if (_player.Health <= 0) return this;
        if (_player.PowerfulAttack) //for powerful attack
        {
            _playerMovement.FullStop();
            return _attackState;
        }
        if (_input.AttackAction.IsPressed() && !EventSystem.current.IsPointerOverGameObject())
        {
            _playerMovement.FullStop();
            return _attackState;
        }
        if (_input.BackwardAction.IsPressed())
        {
            _playerMovement.SetDirection(Direction.Backward);
        }
        if (_input.LeftAction.IsPressed())
        {
            _playerMovement.SetDirection(Direction.Left);
        }
        if (_input.RightAction.IsPressed())
        {
            _playerMovement.SetDirection(Direction.Right);
        }
        if (_input.ForwardAction.IsPressed())
        {
            _playerMovement.SetDirection(Direction.Forward);
        }
        _playerMovement.Move();


        return this;
    }

}
