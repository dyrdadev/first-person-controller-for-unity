using UnityEngine;

public static class RotationTools
{
    public static Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle)
    {
        // You can find an alternative implementation in the MouseLook script of the unity standard assets.
        // Or check out https://ornithoptergames.com/reactivex-and-unity3d-part-3/

        var euler = q.eulerAngles;

        if (euler.x > 180)
        {
            euler.x -= 360;
        }

        euler.x = Mathf.Clamp(euler.x, minAngle, maxAngle);

        return Quaternion.Euler(euler);
    }
}