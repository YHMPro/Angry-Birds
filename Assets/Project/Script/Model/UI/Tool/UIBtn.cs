using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Farme.Extend;
using UnityEngine.Events;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// UI按钮
    /// </summary>
    public class UIBtn : MonoBehaviour
    {
        /// <summary>
        /// 默认颜色
        /// </summary>
        private Color32 m_DefaultColor = Color.white;
        /// <summary>
        /// 按下颜色
        /// </summary>
        private Color32 m_PressColor = new Color32(255, 255, 255, 150);
        private Image m_Img;
        /// <summary>
        /// 与指针的距离
        /// </summary>
        private float m_ToPointerDistance = 0;
        [SerializeField]
        private float m_ScaleValue = 0.2f;
        [SerializeField]
        private Image m_Icon = null;
        [SerializeField]
        private UnityEvent m_OnPointerEnterEvent = new UnityEvent();
        public UnityEvent OnPointerEnterEvent
        {
            get { return m_OnPointerEnterEvent; }
        }
        [SerializeField]
        private UnityEvent m_OnPointerExitEvent = new UnityEvent();
        public UnityEvent OnPointerExitEvent
        {
            get { return m_OnPointerExitEvent; }
        }
        [SerializeField]
        private UnityEvent m_OnPointerClickEvent = new UnityEvent();
        public UnityEvent OnPointerClickEvent
        {
            get { return m_OnPointerClickEvent; }
        }
        private void Awake()
        {
            m_Img = GetComponent<Image>();
        }

        private void Start()
        {
            m_Img.UIEventRegistered(EventTriggerType.PointerEnter, OnPointerEnter);
            m_Img.UIEventRegistered(EventTriggerType.PointerExit, OnPointerExit);
            m_Img.UIEventRegistered(EventTriggerType.PointerClick, OnPointerClick);
            m_Img.UIEventRegistered(EventTriggerType.PointerDown, OnPointerDown);


        }

        private void ScaleUpdate()
        {
            //计算指针的位置与自身的距离
            m_ToPointerDistance = Mathf.Clamp((transform.position - Input.mousePosition).magnitude, 0, m_Img.rectTransform.rect.width / 2f - 3f);
            //调控放缩量
            transform.localScale = Vector3.one - (1f - m_ToPointerDistance / (m_Img.rectTransform.rect.width / 2f - 3f)) * Vector3.one * m_ScaleValue;
        }

        private void OnDestroy()
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);

            m_OnPointerEnterEvent.RemoveAllListeners();
            m_OnPointerExitEvent.RemoveAllListeners();
            m_OnPointerClickEvent.RemoveAllListeners();

            m_Img.UIEventRemove(EventTriggerType.PointerEnter, OnPointerEnter);
            m_Img.UIEventRemove(EventTriggerType.PointerExit, OnPointerExit);
            m_Img.UIEventRemove(EventTriggerType.PointerClick, OnPointerClick);
            m_Img.UIEventRemove(EventTriggerType.PointerDown, OnPointerDown);
        }
        private void OnPointerEnter(BaseEventData bEData)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);
            m_OnPointerEnterEvent?.Invoke();
        }
        private void OnPointerExit(BaseEventData bEData)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);
            m_Img.color = m_DefaultColor;
            m_Icon.color = m_DefaultColor;
            m_OnPointerExitEvent?.Invoke();
        }
        private void OnPointerClick(BaseEventData bEData)
        {
            m_Img.color = m_DefaultColor;
            m_Icon.color = m_DefaultColor;
            GameAudio.PlayButtonAudio();
            m_OnPointerClickEvent?.Invoke();
        }
        private void OnPointerDown(BaseEventData bEData)
        {
            m_Img.color = m_PressColor;
            m_Icon.color = m_PressColor;
        }
    }
}
