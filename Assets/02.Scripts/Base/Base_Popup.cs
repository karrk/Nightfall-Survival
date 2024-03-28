using DG.Tweening;
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
        /// [상태] 현재 팝업창이 활성화되어있는지를 확인합니다. 
        /// <br> true : 활성화 </br><br> false : 비활성화</br>
        /// </summary>
        private bool isActivePopup = false;

        /// <summary>
        /// [기능] 암막처리의 페이드가 적용되는 연출시간입니다.
        /// </summary>
        private const float duration_fadeDim = 0.1f;

        /// <summary>
        /// [데이터] 모든 활성화된 팝업창 중에서 우선도가 얼마나 높은지 나타내는 수치입니다. 
        /// <br> 기본값 : -1</br>
        /// </summary>
        public int activeIndex = -1;

        /// <summary>
        /// [상태] 현재 팝업창이 활성화되어있는지를 확인합니다. 
        /// </summary>
        /// <returns>true : 활성화 <br>false : 비활성화</br></returns>
        protected bool CheckIsActivePopup() => isActivePopup;

        /// <summary>
        /// [콜백함수] 팝업창이 성공적으로 확인버튼이 눌린 경우 호출됩니다. 
        /// </summary>
        protected Action _cb_complete;
        #endregion


        #region 초기 베이스 기능
        private void Start()
        {
            Logic_Init();
        }
        #endregion

        #region 기본 기능

        /// <summary>
        /// [초기화] Start 타이밍에 초기화가 진행됩니다. 
        /// <br> Scene이 초기화되고 1회만 실행되는것을 권장합니다. </br>
        /// </summary>
        private void Logic_Init()
        {
            // - 기본 설정은 닫혀있는 상태로 실행 될 수 있도록 합니다. - //
            Logic_Close_Base();

            Logic_Init_Setting();

            Logic_Init_Base();
        }

        /// <summary>
        /// [초기화] 초기화 과정에서 기반설정을 지정합니다.
        /// </summary>
        private void Logic_Init_Setting()
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
                        _component.b_blockingInput.onClick.AddListener(Logic_Close_Base);
                }
                // ------------------ //
            }
            // ----------- //
        }

        /// <summary>
        /// [초기화] Start 타이밍에 진행되는 초기화입니다. 
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        protected virtual void Logic_Init_Base() { }

        /// <summary>
        /// [기능] 팝업창을 엽니다.
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        protected virtual void Logic_Open_Base()
        {
            if (!CheckExistGraphicObject()) return;

            Logic_ActivePopup();
        }

        /// <summary>
        /// [기능] 팝업창을 닫습니다. 콜백 없이 진행됩니다.
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        protected virtual void Logic_Close_Base()
        {
            if (!CheckExistGraphicObject()) return;

            Logic_DeActivePopup();
        }

        /// <summary>
        /// [기능] 팝업창을 닫습니다. 
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        protected virtual void Logic_Close_Base(Action m_callback)
        {
            if (!CheckExistGraphicObject()) return;

            Logic_DeActivePopup();

            m_callback?.Invoke();
        }

        /// <summary>
        /// [기능] 팝업창을 열려있다면 닫고, 닫혀있다면 엽니다.
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        protected virtual void Logic_Toggle_Base()
        {
            if (!CheckExistGraphicObject()) return;

            if (isActivePopup)
                Logic_Close_Base();
            else
                Logic_Open_Base();
        }

        /// <summary>
        /// [기능] 팝업을 비활성화 시킵니다.
        /// </summary>
        protected void Logic_DeActivePopup()
        {
            _component.obj_graphic.gameObject.SetActive(false);
            isActivePopup = false;

            if (_component.i_dim != null && _component.used_dim)
            {
                if (_component.used_fadeDim) FadeOutDim();
                else _component.i_dim.gameObject.SetActive(false);
            }

            if (_component.b_blockingInput != null && _component.used_blockingInput)
            {
                _component.b_blockingInput.gameObject.SetActive(false);
            }

            transform.SetAsFirstSibling();
        }

        /// <summary>
        /// [기능] 팝업을 활성화 시킵니다. 
        /// </summary>
        protected void Logic_ActivePopup()
        {
            _component.obj_graphic.gameObject.SetActive(true);
            isActivePopup = true;

            if (_component.i_dim != null && _component.used_dim)
            {
                if (_component.used_fadeDim) FadeInDim();
                else _component.i_dim.gameObject.SetActive(true);
            }

            if (_component.b_blockingInput != null && _component.used_blockingInput)
            {
                _component.b_blockingInput.gameObject.SetActive(true);
            }

            transform.SetAsLastSibling();
            activeIndex = -1;
        }
        #endregion

        /// <summary>
        /// [검사] 팝업의 본체에 해당하는 graphic 오브젝트가 존재하는지를 확인합니다. 
        /// <br> 만약 없을 경우, 에러를 송출합니다. </br>
        /// </summary>
        /// <returns> true : 해당 오브젝트가 존재함 <br> false : 해당 오브젝트가 없음</br></returns>
        private bool CheckExistGraphicObject()
        {
            if (_component.obj_graphic == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// [기능] 암막 오브젝트를 페이드인으로 활성화 시킵니다.
        /// </summary>
        private void FadeInDim()
        {
            _component.i_dim.gameObject.SetActive(true);
            _component.i_dim.DOKill();
            _component.i_dim.DOFade(0.5f, duration_fadeDim);
        }

        /// <summary>
        /// [기능] 암막 오브젝트를 페이드아웃으로 비활성화 시킵니다.
        /// </summary>
        private void FadeOutDim()
        {
            _component.i_dim.DOKill();
            _component.i_dim.DOFade(0, duration_fadeDim).OnComplete(() => { _component.i_dim.gameObject.SetActive(false); });
        }

        #region 버튼 콜백
        /// <summary>
        /// [버튼콜백] 확인 버튼이 눌렸을때 팝업창을 닫고 콜백 함수를 실행시킵니다.
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        public virtual void OnClickOkay_Base()
        {
            _cb_complete?.Invoke();
            Logic_Close_Base();
        }

        /// <summary>
        /// [버튼콜백] 취소 버튼이 눌렸을때 팝업창을 닫고 콜백 함수를 실행시킵니다.
        /// <br> base을 통해서 상위 함수를 실행해야합니다. </br>
        /// </summary>
        public virtual void OnClickCancel_Base()
        {
            Logic_Close_Base();
        }
        #endregion
    }
}