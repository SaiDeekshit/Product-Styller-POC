using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotDetails 
{
    public string productName;
    public string shotType;
    public string shotName;
    public int renderWidth;
    public int renderHeight;
    public int frameCount;
    public int degrees;
    public Direction.RotationDirection direction;
    public Vector3 cameraPosition;
    public float cameraRotationX;
    public float cameraRotationY;
    public float fov;
    public bool isCaptured;

    //For Silo Shot
    public ShotDetails(string _productName,string _shotName,int _renderWidth,int _renderHeight)
    {
        this.productName = _productName;
        this.shotType = "Silo";
        this.shotName = _shotName;
        this.renderWidth = _renderWidth;
        this.renderHeight = _renderHeight;
        this.frameCount = 0;
        this.degrees = 0;
    }

    //For 360 Shot
    public ShotDetails(string _productName,string _shotName,int _renderWidth,int _renderHeight,int _degrees,int _frameCount,Direction.RotationDirection _rotationDirection)
    {
        this.productName = _productName;
        this.shotType = "New360";
        this.shotName = _shotName;
        this.renderWidth = _renderWidth;
        this.renderHeight = _renderHeight;
        this.frameCount = _frameCount;
        this.degrees = _degrees;
        this.direction = _rotationDirection;
    }
}
[System.Serializable]
public class ListOfShotDetails
{
    public List<ShotDetails> Items;
}
