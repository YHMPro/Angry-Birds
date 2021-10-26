using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class BirdNest : MonoBehaviour
    {
        private FixedJoint2D m_Fix2D = null;
        private List<GameObject> m_PigLi = null;
        private static List<GameObject> m_BirdLi = null;
        private List<GameObject> PigLi
        {
            get
            {
                if(m_PigLi==null)
                {
                    m_PigLi = new List<GameObject>();
                }
                return m_PigLi;
            }
        }
        private static List<GameObject> BirdLi
        {
            get
            {
                if(m_BirdLi==null)
                {
                    m_BirdLi = new List<GameObject>();
                }
                return m_BirdLi;
            }
        }
        private void Awake()
        {
            m_Fix2D = GetComponent<FixedJoint2D>();


        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                if (!PigLi.Contains(collision.gameObject))
                {
                    PigLi.Add(collision.gameObject);
                }
            }
            if (collision.gameObject.layer == 8)
            {
                if (!BirdLi.Contains(collision.gameObject))
                {
                    Bird bird = collision.gameObject.GetComponent<Bird>();
                    //if (bird!=null&& bird.IsAbleBindBirdNets)
                    //{

                    //    BirdLi.Add(collision.gameObject);
                    //}                
                }
            }
            //if(m_Fix2D)


        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                if (BirdLi.Contains(collision.gameObject))
                {
                    BirdLi.Remove(collision.gameObject);
                }
            }
            if (collision.gameObject.layer == 8)
            {
                if (BirdLi.Contains(collision.gameObject))
                {
                    BirdLi.Remove(collision.gameObject);
                }
            }
        }
        private void CheckPig()
        {
            
        }

        private void CheckBird()
        {

        }
    }
}
