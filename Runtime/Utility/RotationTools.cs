using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public static class RotationTools
    {
        // Ripped straight out of the Standard Assets MouseLook script. (This should really be a standard function...)
		public static Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle) {
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

			angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}
    }
}