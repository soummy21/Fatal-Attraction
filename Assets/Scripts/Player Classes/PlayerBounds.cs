using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    [SerializeField] float min_pos, max_pos;
    // Update is called once per frame
    void Update()
    {
        var temp = transform.position;
        
        if(temp.x<=min_pos)
        {
            temp.x = min_pos;
            transform.position = temp;
        }
        else if (temp.x >= max_pos)
        {
            temp.x = max_pos;
            transform.position = temp;
        }



    }
}
