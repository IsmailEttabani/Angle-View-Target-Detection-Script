using UnityEngine;
using UnityEditor;

public class AngleViewDetection : MonoBehaviour
{
    [Range(0,90)] public float LineAngle = 45f;
    public float reduis = 1;
    public float UpStep = 1;
    public Transform Target;
    void OnDrawGizmos(){

        if(Target == null)
        return;

        Vector3 up = transform.up;
        Vector3 origin = transform.position;

        float Rad2Deg = LineAngle * Mathf.Deg2Rad;

       
        
        Vector3 Right = new Vector3(Mathf.Sin(Rad2Deg), 0, Mathf.Cos(Rad2Deg));
        Vector3 Left = new Vector3(Mathf.Sin(-Rad2Deg), 0, Mathf.Cos(Rad2Deg));

        Handles.color = Inside(Target.position) ? Color.red : Color.white;
        
        Handles.DrawLine(origin, origin+Right * reduis);
        Handles.DrawLine(origin, origin+Left * reduis);


        Vector3 UpOffset = origin + up * UpStep;

       
        
        Vector3 UpRight = new Vector3(Mathf.Sin(Rad2Deg), 0, Mathf.Cos(Rad2Deg));
        Vector3 UpLeft = new Vector3(Mathf.Sin(-Rad2Deg), 0, Mathf.Cos(Rad2Deg));

        
        Handles.DrawLine(UpOffset, UpOffset+UpRight * reduis);
        Handles.DrawLine(UpOffset, UpOffset+UpLeft * reduis);



        Handles.DrawLine(origin, UpOffset);
        Handles.DrawLine(origin+Right * reduis, UpOffset+UpRight * reduis);
        Handles.DrawLine(origin+Left * reduis, UpOffset+UpLeft * reduis);

        Handles.DrawWireArc(origin,up,Left,LineAngle * 2,reduis);
        Handles.DrawWireArc(UpOffset,up,Left,LineAngle * 2,reduis);
        

        if(Inside(Target.position))
        Debug.Log("Is Inside!");
        else
        Debug.Log("Out!");

    }

    public bool Inside(Vector3 targetPos){

        float distance = Vector3.Distance(targetPos, transform.position);
        Vector3 MagnitudeDistance = Target.position - transform.position;
        Vector3 LocalMagniDis = transform.InverseTransformPoint(MagnitudeDistance);
        Vector3 MangitudeNorm = LocalMagniDis.normalized;

        if(targetPos.y < 0 || targetPos.y > UpStep)
        return false;

        if(MangitudeNorm.x > Mathf.Sin(LineAngle * Mathf.Deg2Rad) || MangitudeNorm.z < Mathf.Cos(LineAngle * Mathf.Deg2Rad))
        return false;


        if(distance > reduis)
        return false;

        return true;
    }
}
