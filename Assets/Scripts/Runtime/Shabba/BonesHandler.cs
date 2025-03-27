using UnityEngine;

public class BonesHandler : MonoBehaviour
{
    [SerializeField] Transform bone2;
    [SerializeField] Transform bone3;
    [SerializeField] Transform bone4;


    [SerializeField]
    private AnimationCurve rotationCurve;

    public struct bonesPositionPreset
    {
        public Vector2 bone2Pos;
        public Vector2 bone3Pos;
        public Vector2 bone4Pos;



        public bonesPositionPreset(Vector2 bone2Pos, Vector2 bone3Pos, Vector2 bone4Pos)
        {
            this.bone2Pos = bone2Pos;
            this.bone3Pos = bone3Pos;
            this.bone4Pos = bone4Pos;
        }
    }


    public void SetBones(float t)
    {
        // float bone2y = Mathf.Lerp(4.872665e-08f, 0.01686376f, t);
        // float bone3y = Mathf.Lerp(4.624603e-08f, 0.06601496f, t);
        // float bone4y = Mathf.Lerp(3.957644e-09f, 0.09823086f, t);

        float bone2RotZ = rotationCurve.Evaluate(t) * -8.837f;
        float bone3RotZ = rotationCurve.Evaluate(t) * -3.64f;
        float bone4RotZ = rotationCurve.Evaluate(t) * -8.208f;

        // bone2.localPosition = new(0.364f, bone2y);
        // bone3.localPosition = new(0.363f, bone3y);
        // bone4.localPosition = new(0.379f, bone4y);
        bone2.localRotation = Quaternion.Euler(0, 0, bone2RotZ);
        bone3.localRotation = Quaternion.Euler(0, 0, bone3RotZ);
        bone4.localRotation = Quaternion.Euler(0, 0, bone4RotZ);
    }

}
