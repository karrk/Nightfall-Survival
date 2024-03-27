using System;
using UnityEngine;
using UnityEngine.UI;

namespace VS.Base.Popup
{
    /// <summary>
    /// [팝업] 팝업의 가장 기본적인 열고 닫는 기능을 가지고 있습니다. 
    /// </summary>
    public class Base_Popup : MonoBehaviour
    {
        /// <summary>
        /// [데이터] 팝업에 대한 구성과, 설정들을 담고 있는 데이터입니다.
        /// </summary>
        [System.Serializable]
        protected struct PopupComponent
        {
            /// <summary>
            /// [캐시] 팝업 이외의 영역을 입력하지 못하게 막는 오브젝트입니다.
            /// </summary>
            [SerializeField] public Button b_blockingInput;

            /// <summary>
            /// [기능] 팝업 이외의 영역을 클릭하지 못하게 할지 정합니다. 
            /// </summary>
            [SerializeField] public bool used_blockingInput;

            /// <summary>
            /// [기능] 입력 방지 영역을 클릭했을때 팝업창이 닫히게 할지를 정합니다.
            /// </summary>
            [SerializeField] public bool used_closeBlockButton;

            [SerializeField] public RectTransform obj_graphic;

            /// <summary>
            /// [캐시] 배경의 암막처리를 적용하는 이미지 오브젝트입니다.
            /// </summary>
            [SerializeField] public Image i_dim;

            /// <summary>
            /// [기능] 배경의 암막처리의 기능을 사용할지를 정합니다.
            /// </summary>
            [SerializeField] public bool used_dim;

            /// <summary>
            /// [기능] 암막처리가 전환되었을때 페이드 연출이 적용될지를 정합니다.
            /// </summary>
            [SerializeField] public bool used_fadeDim;
        }


        #region 기본 구성
        [Header("기본 구성")]
        [SerializeField] protected PopupComponent _component;

        /// <summary>
        /// [콜백함수] 팝업창이 성공적으로 확인버튼이 눌린 경우 호출됩니다. 
        /// </summary>
        protected Action _cb_complete;
        #endregion


        #region 초기 베이스 기능
        private void Start()
        {
            Init();
        }

        #region 기본 기능

        /// <summary>
        /// [초기화] Start 타이밍에 초기화가 진행됩니다. 
        /// <br> Scene이 초기화되고 1회만 실행되는것을 권장합니다. </br>
        /// </summary>
        private void Init()
        {
            // - 기본 설정은 닫혀있는 상태로 실행 될 수 있도록 합니다. - //
            ClosePopup();

            Init_Setting();

            Init_Custom();
        }

        /// <summary>
        /// [초기화] 초기화 과정에서 기반설정을 지정합니다.
        /// <br> 미설정된 부분들은 초기값으로 지정해주고 잘못된 설정들을 바로잡습니다. </br>
        /// </summary>
        private void Init_Setting()
        {
            // 그래픽 오브젝트 설정이 없는 경우 대응 //
            if (!CheckExistGraphicObject())
            {
                if (!transform.GetChild(0).TryGetComponent<RectTransform>(out _component.obj_graphic))
                    // 팝업의 그래픽 구성이 없는 경우
                    return;
            }
            // ----------- //

            // 장막 오브젝트가 설정이 없는 경우 대응 //
            if (_component.i_dim == null)
            {
                _component.used_dim = false;
                _component.used_fadeDim = false;
            }
            else
            {
                _component.i_dim.gameObject.SetActive(false);
                if (!_component.used_dim) _component.used_fadeDim = false;
            }
            // ----------- //

            // 입력방지 오브젝트가 설정이 없는 경우 대응 //
            if (_component.b_blockingInput == null)
            {
                _component.used_blockingInput = false;
                _component.used_closeBlockButton = false;
            }
            else
            {
                _component.b_blockingInput.gameObject.SetActive(false);

                // 입력방지 오브젝트의 버튼기능의 사용 유무 설정 // 
                _component.b_blockingInput.interactable = _component.used_closeBlockButton && _component.used_blockingInput;

                if (_component.used_closeBlockButton)
                {
                    if (_component.b_blockingInput.onClick.GetPersistentEventCount() == 0)
                        _component.b_blockingInput.onClick.AddListener(ClosePopup);
                }
                // ------------------ //
            }
            // ----------- //
        }

        /// <summary>
        /// [조건] 팝업의 본체에 해당하는 graphic 오브젝트가 존재하는지를 확인합니다. 
        /// <br> 만약 없을 경우, 에러를 송출합니다. </br>
        /// </summary>
        /// <returns> true : 해당 오브젝트가 존재함 <br> false : 해당 오브젝트가 없음</br></returns>
        private bool CheckExistGraphicObject()
        {
            if (_component.obj_graphic == null)
            {
                // TODO :: 에러 송출 구문
                return false;
            }
            return true;
        }

        /// <summary>
        /// [초기화] Start 발생 타이밍에 기본 설정 이후 호출되는 함수입니다.
        /// <br> Awake 나 Start를 별도로 사용되는것보다는 해당 함수를 통해서 초기화 구문을 진행하는것을 권장합니다. </br>
        /// </summary>
        public virtual void Init_Custom() { }
        #endregion


        #region 팝업 기본 기능
        /// <summary>
        /// [기능] 팝업을 닫습니다. 
        /// </summary>
        protected virtual void ClosePopup()
        {
            CompleteClose();
        }

        /// <summary>
        /// [콜백함수] 팝업이 닫히는 모든 절차가 성공적으로 완료되면 호출됩니다.
        /// </summary>
        protected virtual void CompleteClose() { }

        /// <summary>
        /// [기능] 팝업을 엽니다.
        /// </summary>
        protected virtual void OpenPopup()
        {
            CompleteOpen();
        }

        /// <summary>
        /// [콜백함수] 팝업이 열리는 모든 절차가 성공적으로 완료되면 호출됩니다.
        /// </summary>
        protected virtual void CompleteOpen() { }
        #endregion


        #region 콜백 함수
        // 주의 :: 해당 함수들은 콜백 함수입니다. 참조가 없더라도 애니메이션 및 오브젝트에서 직접 할당되어 사용될 수 있습니다.

        /// <summary>
        /// [버튼콜백] 팝업창의 확인 버튼이 눌린 경우 호출됩니다. 
        /// </summary>
        public void OnClickCancel()
        {
            _cb_complete?.Invoke();

            ClosePopup();
        }

        /// <summary>
        /// [버튼콜백] 팝업창의 취소 버튼이 눌린 경우 호출됩니다. 
        /// </summary>
        public void OnClickOkay()
        {
            ClosePopup();
        }
        #endregion
    }
}