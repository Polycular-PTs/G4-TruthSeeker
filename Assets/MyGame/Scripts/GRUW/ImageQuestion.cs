using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageQuestion", menuName = "ScriptableObjects/ImageQuestion", order = 1)]
public class ImageQuestion : ScriptableObject
{
    public int correctImage;
    public Sprite images;
}
