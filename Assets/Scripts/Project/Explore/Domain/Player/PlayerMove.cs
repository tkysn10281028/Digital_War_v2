using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Player.Rotate;
using DigitalWar.Project.Explore.Domain.Player.IF;
namespace DigitalWar.Project.Explore.Domain.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 30f;
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
        }

        void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => !GameManager.Instance.IsPlayerLocked)
                .Subscribe(_ =>
                {
                    var origin = transform.position; // 移動元

                    Vector3 moveDirection = inputHandler.GetPlayerMoveVector();
                    var target = origin + moveDirection * _moveSpeed * Time.deltaTime; // 移動先

                    // プレイヤーの移動・回転
                    transform.SetPositionAndRotation(obstacleHandler.JudgeIsObstacle(origin, target), playerRotator.Rotate(moveDirection));
                })
                .AddTo(this);
        }
    }
}