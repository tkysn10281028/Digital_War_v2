using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Player.Rotate;
using DigitalWar.Project.Explore.Domain.Player.IF;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
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
            // Playerに紐づいている各インスタンスの紐付け
            inputHandler = _inputHandlerComponent as IPlayerMoveInputHandler;
            obstacleHandler = _obstacleHandlerComponent as IPlayerObstacleHandler;
            playerRotator = _playerRotatorComponent as PlayerRotator;

            // プレイヤーの現在地関連の情報を設定
            // TODO: ここかエントランスでサーバーから現在地を受け取って設定する
            GameManager.Instance.PlayerCurrentState = new PlayerCurrentState(0, 0, PlayerColors.Yellow);
            GameManager.Instance.ExploreObject.SetPlayerCurPosOnMap();
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