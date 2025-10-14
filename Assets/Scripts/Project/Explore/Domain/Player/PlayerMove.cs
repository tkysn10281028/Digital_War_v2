using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Player.Rotate;
using DigitalWar.Project.Explore.Domain.Player.IF;
using DigitalWar.Project.Common.Enums;
namespace DigitalWar.Project.Explore.Domain.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private MonoBehaviour _inputHandlerComponent;
        [SerializeField] private MonoBehaviour _obstacleHandlerComponent;
        [SerializeField] private MonoBehaviour _playerRotatorComponent;
        private IPlayerMoveInputHandler inputHandler;
        private IPlayerObstacleHandler obstacleHandler;
        private PlayerRotator playerRotator;

        void Awake()
        {
            inputHandler = _inputHandlerComponent as IPlayerMoveInputHandler;
            obstacleHandler = _obstacleHandlerComponent as IPlayerObstacleHandler;
            playerRotator = _playerRotatorComponent as PlayerRotator;
            GameManager.Instance.PlayerCurrentState = new PlayerCurrentState(-1, -1, PlayerColors.Yellow);
        }

        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => !GameManager.Instance.IsPlayerLocked)
                .Subscribe(_ =>
                {
                    var origin = transform.position;
                    Vector3 moveDirection = inputHandler.GetPlayerMoveVector();

                    if (moveDirection == Vector3.zero)
                        return;

                    var target = origin + _moveSpeed * Time.deltaTime * moveDirection;
                    var nextPosition = obstacleHandler.JudgeIsObstacle(origin, target);

                    if (nextPosition != origin)
                    {
                        transform.SetPositionAndRotation(nextPosition, playerRotator.Rotate(moveDirection));
                    }
                    else
                    {
                        transform.position = origin;
                    }
                })
                .AddTo(this);
        }
    }
}