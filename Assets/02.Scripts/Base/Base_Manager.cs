using UnityEngine;

namespace VS.Base.Manager
{
    /// <summary>
    /// [매니저] 게임 매니저에서 각 매니저들을 통합관리하기 위한 형태의 매니저 기반입니다. 
    /// <br> 일반적으로 초기화 타이밍과 회수 절차등 일괄적인 처리를 위해서 해당 베이스가 사용됩니다. </br>
    /// </summary>
    public abstract class Base_Manager : MonoBehaviour
    {
        /// <summary>
        /// [초기화] 외부에서 매니저를 초기화시키기위해서 진행되는 부분입니다.
        /// </summary>
        public void Logic_Init()
        {
            Logic_Init_Custom();
        }

        protected abstract void Logic_Init_Custom();
    }
}