using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class OldPig : Pig
    {
        private IEquipBinding equipBinding;
        protected override void Awake()
        {
            m_PigType = EnumPigType.OldPig;
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();


        }

        protected override void OnEnable()
        {
            base.OnEnable();
            //加载法杖
            if(!GoReusePool.Take("Staff",out GameObject staff))
            {
                if(!GoLoad.Take(OtherConfigInfo.GetSingleton().GetStaffPrefabPath(),out staff))
                {

                }
            }
            equipBinding = staff.GetComponent<IEquipBinding>();
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.EquipBindingUpdate);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            if (ShareMono.Exists)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.EquipBindingUpdate);
            }
        }

        private void EquipBindingUpdate()
        {
            equipBinding.Binding(transform);         
        }

        public override void Died(bool isDestroy = false)
        {
            equipBinding.Binding(null);
            base.Died(isDestroy);
        }
    }
}
