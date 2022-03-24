using LanKuDot.UnityToolBox;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;
using UniRx;
using System.Collections;
using System.Collections.Generic;

namespace MYgame.Scripts.Scenes.Building
{
    /// <summary>
    /// The progress of the constructed building
    /// </summary>
    public class BuildingProgress : MonoBehaviour
    {
        [SerializeField]
        private BuildingProgressUnit[] _bulidingPieces;
        [SerializeField]
        private BuildingProgressUnit _completedPiece;
        [SerializeField]
        private TweenHDRColorEaseCurve _showUpEmissionCurve;
        [SerializeField]
        private float _InGameEndScaleSize = 1.0f;
        [SerializeField]
        private BuildingRecipeData _buildings;
        [SerializeField]
        private StaticGlobalDel.ECompleteBuilding _HistoryCompleteBuilding;

        public BuildingRecipeData buildings => _buildings;
        public float InGameEndScaleSize => _InGameEndScaleSize;
        public int Score => _buildings.Score;
        public StaticGlobalDel.ECompleteBuilding HistoryCompleteBuilding => _HistoryCompleteBuilding;
        public BrickAmount[] NeedBricks => _NeedBricks;
        public BuildingProgressUnit completedPiece => _completedPiece;


  
        protected Transform _BuildingPosition ;
        protected int _BuildingPositionIndex ;
        protected BrickAmount[] _NeedBricks = null;
        protected int _MaxBricks;
        protected int _CurBricks;
        protected int _ParentIndex = -1;

        public Transform BuildingPosition
        {
            set
            {
                this.transform.parent = value;
                this.transform.localPosition = Vector3.zero;
                _BuildingPosition = value;
            }
            get => _BuildingPosition;
        }

        public int BuildingPositionIndex
        {
            set => _BuildingPositionIndex = value;
            get => _BuildingPositionIndex;
        }

        public int ParentIndex
        {
            set => _ParentIndex = value;
            get => _ParentIndex;
        }

        private const float _progressStep = 0.25f;
        private const float _progressComplete = 1f;

        private int _currentStep = 0;

        private void Awake()
        {
            foreach (var piece in _bulidingPieces)
                piece.Inactivate();

            _completedPiece.Inactivate();

            int lTempMaxBricks = 0;
            _NeedBricks = new BrickAmount[buildings.brickAmounts.Length];
            for (int i = 0; i < buildings.brickAmounts.Length; i++)
            {
                _NeedBricks[i] = new BrickAmount();
                _NeedBricks[i].color = buildings.brickAmounts[i].color;
                _NeedBricks[i].amount = 0;
                lTempMaxBricks += buildings.brickAmounts[i].amount;
            }

            _MaxBricks = lTempMaxBricks;
            _CurBricks = 0;
        }

        /// <summary>
        /// Update the building progress
        /// </summary>
        /// <param name="progress">The progress of the building</param>
        public void UpdateProgress(float progress)
        {
            progress = Mathf.Min(progress, _progressComplete);

            var step = (int)(progress / _progressStep);

            if (step <= _currentStep)
                return;

            if (progress >= _progressComplete) {
                _currentStep = 100;
                ShowCompletedPiece();
                return;
            }

            for (; _currentStep < step; ++_currentStep)
                ShowPiece(_bulidingPieces[_currentStep]);
        }

        /// <summary>
        /// Show the pieces
        /// </summary>
        /// <param name="targetPiece">The target piece to be shown</param>
        private void ShowPiece(BuildingProgressUnit targetPiece)
        {
            targetPiece.Activate(_showUpEmissionCurve);
        }

        /// <summary>
        /// Show the completed piece
        /// </summary>
        private void ShowCompletedPiece()
        {
            foreach (var piece in _bulidingPieces)
                piece.Inactivate();

            _completedPiece.Activate(_showUpEmissionCurve);
            _ShowPieceCallBack.OnNext(ParentIndex);
        }

        public void ClearNeedBricks()
        {
            for (int i = 0; i < buildings.brickAmounts.Length; i++)
                _NeedBricks[i].amount = 0;
        }

        public int AddBricks(StaticGlobalDel.EBrickColor color, int AddBricksCount)
        {
            int i = 0;
            int lTempColorMaxCount = 0;
            for (i = 0; i < buildings.brickAmounts.Length; i++)
            {
                if (color == buildings.brickAmounts[i].color)
                {
                    lTempColorMaxCount = buildings.brickAmounts[i].amount;
                    break;
                }
            }

            if (i == buildings.brickAmounts.Length)
                return -1;

            int lTempCount = _NeedBricks[i].amount + AddBricksCount;
            if (lTempCount > lTempColorMaxCount)
                return -1;

            _NeedBricks[i].amount = lTempCount;
            _CurBricks += AddBricksCount;

            if (_CurBricks >= _MaxBricks)
            {
                UpdateProgress(1.0f);
                return -1;
            }
            else
            {
                UpdateProgress((float)_CurBricks / (float)_MaxBricks);
                return lTempCount;
            }
        }
        
        // ===================== UniRx ======================
        public Subject<int> _ShowPieceCallBack = new Subject<int>();

        public Subject<int> ObserveShowPieceCallBack() { return _ShowPieceCallBack ?? (_ShowPieceCallBack = new Subject<int>()); }

        // ===================== UniRx ======================
    }
}
