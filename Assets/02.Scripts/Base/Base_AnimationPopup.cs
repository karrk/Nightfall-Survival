using DG.Tweening;
using System;
using UnityEngine;

namespace VS.Base.Popup
{
    public class Base_AnimationPopup : Base_Popup
    {

        /// <summary>
        /// [종류] 팝업의 애니메이션 종류중, 등장하는 연출입니다.
        /// </summary>
        private enum eAnimationType_In
        {
            None = 0,
            /// <summary> [종류] 투명한 상태에서 서서히 등장하는 연출입니다. </summary>
            FadeIn,
            /// <summary> [종류] 왼쪽에서 등장해오는 연출입니다. </summary>
            LeftIn,
            /// <summary> [종류] 오른쪽에서 등장해오는 연출입니다. </summary>
            RightIn,
            /// <summary> [종류] 위에서 내려오는 연출입니다. </summary>
            UpIn,
            /// <summary> [종류] 아래에서 올라오는 연출입니다. </summary>
            DownIn,
            /// <summary> [종류] 중앙에서 빠르게 확대되면서 등장하는 연출입니다.</summary>
            BounceIn,
            /// <summary> [종류] 위에서 떨어트린것과 같은 연출입니다. </summary>
            DropIn,
        }

        /// <summary>
        /// [종류] 팝업의 애니메이션 종류중, 퇴장하는 연출입니다.
        /// </summary>
        private enum eAnimationType_Out
        {
            None = 0,
            /// <summary> [종류] 서서히 투명해져서 사라지는 연출입니다. </summary>
            FadeOut,
            /// <summary> [종류] 왼쪽으로 나가는 연출입니다. </summary>
            Leftout,
            /// <summary> [종류] 오른쪽으로 나가는 연출입니다. </summary>
            RightOut,
            /// <summary> [종류] 위로 올라가는 연출입니다. </summary>
            UpOut,
            /// <summary> [종류] 아래로 내려가는 연출입니다.. </summary>
            DownOut,
            /// <summary> [종류] 빠르게 작아지면서 사라지는 연출입니다.</summary>
            BounceOut,
        }


        /// <summary>
        /// [기능] 팝업이 열릴때 작동되는 애니메이션 연출 타입입니다.
        /// </summary>
        [Header("AnimationBase")]
        [SerializeField] private eAnimationType_In aniType_in = eAnimationType_In.None;

        /// <summary>
        /// [기능] 팝업이 닫힐때 작동되는 애니메이션 연출 타입입니다.
        /// </summary>
        [SerializeField] private eAnimationType_Out aniType_out = eAnimationType_Out.None;

        /// <summary>
        /// [기능] 팝업창이 열릴때까지 애니메이션이 소요되는 시간입니다.
        /// </summary>
        [SerializeField][Range(0.01f, 1.0f)] private const float duration_appearance = 0.2f;

        /// <summary>
        /// [기능] 팝업창이 닫힐때까지 애니메이션이 소요되는 시간입니다.
        /// </summary>
        [SerializeField][Range(0.01f, 1.0f)] private const float duration_extinction = 0.2f;


        private RectTransform pv_target = null;
        /// <summary>
        /// [캐시] 팝업 애니메이션의 대상입니다.
        /// </summary>
        private RectTransform obj_target
        {
            get
            {
                if (pv_target == null)
                {
                    if (!transform.GetChild(0).TryGetComponent(out pv_target)) return null;
                }
                return pv_target;
            }
        }

        /// <summary>
        /// [상태] 현재 애니메이션중인지를 체크합니다. 중복 실행을 방지합니다. 
        /// </summary>
        private bool isAnimating = false;

        /// <summary>
        /// [상태] 현재 팝업 애니메이션이 작동중인지를 확인합니다.. 
        /// </summary>
        /// <returns>true : 작동중 <br>false : 대기상태</br></returns>
        protected bool CheckIsAnimating() => isAnimating;


        #region 기본 팝업 로직
        protected sealed override void Logic_Open_Base()
        {
            if (isAnimating == false && CheckIsActivePopup() == false)
            {
                isAnimating = true;
                base.Logic_Open_Base();

                Logic_OpenAnimation();
            }
        }

        /// <summary>
        /// [기능] 팝입이 열린 이후 애니메이션 연출까지 끝나고나서 호출되는 함수입니다. 
        /// <br>애니메이션이 끝난직후 특정 로직을 실행시키고자 한다면 해당 함수를 상속하십시오.</br>
        /// </summary>
        protected virtual void Logic_Open_CompleteCallback_Custom() { }

        protected sealed override void Logic_Close_Base()
        {
            if (isAnimating == false && CheckIsActivePopup() == true)
            {
                isAnimating = true;
                Logic_CloseAnimation();
            }
        }

        protected sealed override void Logic_Close_Base(Action m_callback)
        {
            if (isAnimating == false && CheckIsActivePopup() == true)
            {
                isAnimating = true;
                Logic_CloseAnimation(m_callback);
            }
        }


        /// <summary>
        /// [기능] 팝업이 닫힌 이후 애니메이션 연출까지 끝나고나서 호출되는 함수입니다. 
        /// <br>애니메이션이 끝난직후 특정 로직을 실행시키고자 한다면 해당 함수를 상속하십시오.</br>
        /// </summary>
        protected virtual void Logic_Close_CompleteCallback_Custom() { }


        /// <summary>
        /// [기능] 현재 타입에 알맞는 분기의 애니메이션 함수를 실행시킵니다. 
        /// </summary>
        private void Logic_CloseAnimation(Action m_callback = null)
        {
            switch (aniType_out)
            {
                case eAnimationType_Out.FadeOut:
                    FadeOut(m_callback);
                    break;
                case eAnimationType_Out.RightOut:
                    RightOutMove(m_callback);
                    break;
                case eAnimationType_Out.Leftout:
                    LeftOutMove(m_callback);
                    break;
                case eAnimationType_Out.UpOut:
                    UpOutMove(m_callback);
                    break;
                case eAnimationType_Out.DownOut:
                    DownOutMove(m_callback);
                    break;
                case eAnimationType_Out.BounceOut:
                    LeavingScale(m_callback);
                    break;
                case eAnimationType_Out.None:
                default:
                    isAnimating = false;
                    base.Logic_Close_Base(m_callback);
                    Logic_Close_CompleteCallback_Custom();
                    break;
            }
        }

        /// <summary>
        /// [기능] 현재 타입에 알맞는 분기의 애니메이션 함수를 실행시킵니다. 
        /// </summary>
        private void Logic_OpenAnimation()
        {
            switch (aniType_in)
            {
                case eAnimationType_In.FadeIn:
                    FadeIn();
                    break;
                case eAnimationType_In.LeftIn:
                    LeftInMove();
                    break;
                case eAnimationType_In.RightIn:
                    RightInMove();
                    break;
                case eAnimationType_In.UpIn:
                    UpInMove();
                    break;
                case eAnimationType_In.DownIn:
                    DownInMove();
                    break;
                case eAnimationType_In.DropIn:
                    DropInDown();
                    break;
                case eAnimationType_In.BounceIn:
                    AppearScale();
                    break;
                case eAnimationType_In.None:
                default:
                    isAnimating = false;
                    Logic_Open_CompleteCallback_Custom();
                    break;
            }
        }
        #endregion


        #region 애니메이션 연출 부분
        // 가시적인 부분이고 수정할것같지않아 해당내용에대한 주석을 생략합니다. 

        #region FadeInFadeOut
        private void FadeIn()
        {
            obj_target.DOKill();

            if (obj_target.TryGetComponent<CanvasGroup>(out CanvasGroup canvas) == false)
            {
                canvas = obj_target.gameObject.AddComponent<CanvasGroup>();
            }

            canvas.alpha = 0.0f;
            canvas.DOFade(1, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void FadeOut(Action m_callback)
        {
            obj_target.DOKill();

            if (obj_target.TryGetComponent<CanvasGroup>(out CanvasGroup canvas) == false)
            {
                canvas = obj_target.gameObject.AddComponent<CanvasGroup>();
            }

            canvas.alpha = 1.0f;
            canvas.DOFade(0, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Close_CompleteCallback_Custom();
            });
        }
        #endregion

        #region CenterScale
        private void AppearScale()
        {
            obj_target.DOKill();

            obj_target.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            obj_target.DOScale(1, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void LeavingScale(Action m_callback)
        {
            obj_target.localScale = Vector3.one;
            obj_target.DOScale(0.1f, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Close_CompleteCallback_Custom();
            });
        }
        #endregion

        #region LeftInRightOut
        private void LeftInMove()
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = new Vector3(-obj_target.rect.width, 0);
            obj_target.DOAnchorPosX(0, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void RightOutMove(Action m_callback)
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = Vector3.zero;
            obj_target.DOAnchorPosX(-obj_target.rect.width, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Open_CompleteCallback_Custom();
            });
        }
        #endregion

        #region RightInLeftOut
        private void RightInMove()
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = new Vector3(obj_target.rect.width, 0);
            obj_target.DOAnchorPosX(0, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void LeftOutMove(Action m_callback)
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = Vector3.zero;
            obj_target.DOAnchorPosX(obj_target.rect.width, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Open_CompleteCallback_Custom();
            });
        }
        #endregion

        #region UpInUpOut
        private void UpInMove()
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = new Vector3(0, obj_target.rect.height);
            obj_target.DOAnchorPosY(0, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void UpOutMove(Action m_callback)
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = Vector3.zero;
            obj_target.DOAnchorPosY(obj_target.rect.height, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Open_CompleteCallback_Custom();
            });
        }
        #endregion

        #region DownInDownOut
        private void DownInMove()
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = new Vector3(0, -obj_target.rect.height);
            obj_target.DOAnchorPosY(0, duration_appearance).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            });
        }

        private void DownOutMove(Action m_callback)
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = Vector3.zero;
            obj_target.DOAnchorPosY(-obj_target.rect.height, duration_extinction).OnComplete(() =>
            {
                isAnimating = false;
                base.Logic_Close_Base();
                m_callback?.Invoke();
                Logic_Open_CompleteCallback_Custom();
            });
        }
        #endregion

        #region
        private void DropInDown()
        {
            obj_target.DOKill();

            obj_target.anchoredPosition = new Vector3(0, obj_target.rect.height);
            obj_target.DOAnchorPosY(0, duration_appearance * 2f).OnComplete(() =>
            {
                isAnimating = false;
                Logic_Open_CompleteCallback_Custom();
            }).SetEase(Ease.OutBounce);
        }
        #endregion

        #endregion

    }
}