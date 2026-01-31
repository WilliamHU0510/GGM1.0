using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float leftLim;
    public float rightLim;

    public float widthHeightRatio;
    float width;
    float heightDiff;
    GameObject Character;
    void YAxisChange()
    {
        Vector3 newPosition;
        newPosition = this.transform.position;
        newPosition.y = Character.transform.position.y + heightDiff;
        this.transform.position = newPosition;
    }
    void LeftXAxisChange()
    {
        if ((Character.transform.position.x - this.transform.position.x) < -(0.5 - leftLim) * width)
        {
            Vector3 newPosition;
            newPosition = this.transform.position;
            newPosition.x = Character.transform.position.x + (float)((0.5 - leftLim) * width);
            this.transform.position = newPosition;
        }
        
    }
    void RightXAxisChange()
    {
        if ((Character.transform.position.x - this.transform.position.x) > (rightLim - 0.5) * width) 
        {
            Vector3 newPosition;
            newPosition = this.transform.position;
            newPosition.x = Character.transform.position.x - (float)((rightLim - 0.5) * width);
            this.transform.position = newPosition;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character");
        //print("Width" + Screen.width + "Height" + Screen.height);
        widthHeightRatio = (float)Screen.width / Screen.height;
        width = this.GetComponent<Camera>().orthographicSize * widthHeightRatio;
        heightDiff = this.transform.position.y - Character.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        YAxisChange();
        LeftXAxisChange();
        RightXAxisChange();
    }
}
