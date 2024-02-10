using UnityEngine;

public class BonesHandler : MonoBehaviour
{
	[SerializeField] Transform bone2;
	[SerializeField] Transform bone3;
	[SerializeField] Transform bone4;

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
		float bone2y = Mathf.Lerp(-0.157f, 0.157f, t);
		float bone3y = Mathf.Lerp(0.037f, -0.037f, t);
		float bone4y = Mathf.Lerp(0.1f, -0.1f, t);

		bone2.localPosition = new(0.543f, bone2y);
		bone3.localPosition = new(0.462f, bone3y);
		bone4.localPosition = new(0.355f, bone4y);
	}

}
