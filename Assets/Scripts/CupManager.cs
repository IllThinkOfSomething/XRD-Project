using UnityEngine;

namespace DefaultNamespace
{
    public class CupManager : MonoBehaviour
    {
        public GameObject Cap;
        public GameObject fireWorks;
        

        public void ActivateCap()
        {
            Cap.SetActive(true);
            fireWorks.SetActive(true);
        }
    }
}