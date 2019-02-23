using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Items : MonoBehaviour
{
    public Image m_img;
    public Sprite m_sprite;
    public string m_name;
    public string m_description;
    public float m_sellingPrice;
    public float m_buyingPrice;


    abstract public void Use();
    abstract public void Drop();
    abstract public void Sell();
    abstract public void Buy();

}
