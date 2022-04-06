using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Farme.Extend;
using UnityEngine.Events;
using Farme;
using Farme.UI;
namespace Bird_VS_Boar
{
    /// <summary>
    /// UI按钮
    /// </summary>
    public class UIBtn : MonoBehaviour
    {
        /// <summary>
        /// 是否可交互
        /// </summary>
        private bool m_Interactable = true;
        /// <summary>
        /// 是否可交互
        /// </summary>
        public bool Interactable
        {
            get
            {
                return m_Interactable;
            }
            set
            {
                m_Interactable = value;
                if (!value)
                {
                    Img.color = m_DisabledColor;
                    m_Icon.color = m_DisabledColor;
                }
                else
                {
                    Img.color = m_DefaultColor;
                    m_Icon.color = m_DefaultColor;
                }
            }
        }
        /// <summary>
        /// 默认颜色
        /// </summary>
        private Color32 m_DefaultColor = Color.white;
        /// <summary>
        /// 按下颜色
        /// </summary>
        private Color32 m_PressColor = new Color32(255, 255, 255, 150);
        /// <summary>
        /// 禁用颜色
        /// </summary>
        private Color32 m_DisabledColor = new Color32(100, 100, 100, 255);
        /// <summary>
        /// 
        /// </summary>
        private Image m_Img = null;
        private Image Img
        {
            get
            {
                if(m_Img==null)
                {
                    m_Img = GetComponent<Image>();
                }
                return m_Img;
            }
        }
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
        }
        private void OnEnable()
        {
            ScaleUpdate();
        }
        private void Start()
        {
            Img.UIEventRegistered(EventTriggerType.PointerEnter, OnPointerEnter);
            Img.UIEventRegistered(EventTriggerType.PointerExit, OnPointerExit);
            Img.UIEventRegistered(EventTriggerType.PointerClick, OnPointerClick);
            Img.UIEventRegistered(EventTriggerType.PointerDown, OnPointerDown);
        }

        private void ScaleUpdate()
        {
            if (!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                return;
            }     
            //计算指针的位置与自身的距离
            m_ToPointerDistance = Mathf.Clamp(((Vector3)RectTransformUtility.WorldToScreenPoint(MonoSingletonFactory<WindowRoot>.GetSingleton().Camera, transform.position) - Input.mousePosition).magnitude, 0, (Img.rectTransform.rect.width* Img.transform.localScale.x) / 2f - 3f);
            //调控放缩量
            transform.localScale = Vector3.one - (1f - m_ToPointerDistance / ((Img.rectTransform.rect.width* Img.transform.localScale.x) / 2f - 3f)) * Vector3.one * m_ScaleValue;
        }

       
        private void OnDestroy()
        {
            if (MonoSingletonFactory<ShareMono>.SingletonExist)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);
            }

            m_OnPointerEnterEvent.RemoveAllListeners();
            m_OnPointerExitEvent.RemoveAllListeners();
            m_OnPointerClickEvent.RemoveAllListeners();

            Img.UIEventRemove(EventTriggerType.PointerEnter, OnPointerEnter);
            Img.UIEventRemove(EventTriggerType.PointerExit, OnPointerExit);
            Img.UIEventRemove(EventTriggerType.PointerClick, OnPointerClick);
            Img.UIEventRemove(EventTriggerType.PointerDown, OnPointerDown);
        }
        private void OnPointerEnter(BaseEventData bEData)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);
            if (!m_Interactable)
            {
                return;
            }
            if (!IsPointerMouseLeftKey(bEData as PointerEventData))
            {
                return;
            }
            m_OnPointerEnterEvent?.Invoke();
        }
        private void OnPointerExit(BaseEventData bEData)
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ScaleUpdate);
            transform.localScale = Vector3.one;
            if (!m_Interactable)
            {
                return;
            }
            if (!IsPointerMouseLeftKey(bEData as PointerEventData))
            {
                return;
            }
            Img.color = m_DefaultColor;
            m_Icon.color = m_DefaultColor;
            m_OnPointerExitEvent?.Invoke();
        }
        private void OnPointerClick(BaseEventData bEData)
        {
            if (!m_Interactable)
            {
                return;
            }
            if (!IsPointerMouseLeftKey(bEData as PointerEventData))
            {
                return;
            }
            Img.color = m_DefaultColor;
            m_Icon.color = m_DefaultColor;
            GameAudio.PlayButtonAudio();           
            m_OnPointerClickEvent?.Invoke();
        }
        private void OnPointerDown(BaseEventData bEData)
        {
            if (!m_Interactable)
            {
                return;
            }
            if (!IsPointerMouseLeftKey(bEData as PointerEventData))
            {
                return;
            }
            Img.color = m_PressColor;
            m_Icon.color = m_PressColor;
        }


        private bool IsPointerMouseLeftKey(PointerEventData pEData)
        {
            return pEData.button == PointerEventData.InputButton.Left;
        }
    }
}
