using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class Bag : MonoBehaviour
    {
        [HideInInspector] public List<Items> items = new List<Items>();
        public Image image;

        private void Start()
        {
            image.gameObject.SetActive(false);
        }

    }
}