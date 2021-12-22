using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Buoyancy : MonoBehaviour
{
    public Transform[] floaters; 
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float waterHeight = 0f;
    public float floatingPower = 100f;
    
    Rigidbody obj;
    
    int floatersUnderwater;
    
    bool underwater;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderwater = 0;
        for(int i=0; i<floaters.Length; i++)
        {
            float difference = floaters[i].position.y - waterHeight;

            if (difference<0)
            {
                obj.AddForceAtPosition(Vector3.up * floatingPower*Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderwater +=1;
                if(!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }
        
        if(underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(false);
        }

        void SwitchState(bool isUnderwater)
        {
            if(isUnderwater)
            {
                obj.drag = underwaterDrag;
                obj.angularDrag = underwaterAngularDrag;
            }
            else
            {
                obj.drag = airDrag;
                obj.angularDrag = airAngularDrag;
            }
        }
    }
}
