using UnityEngine;

public static class LerpUtility
{
    public enum LerpFunction { EaseOutExpo, EaseOutBack }

    public static float Lerp(float x, LerpFunction i_functionType)
    {
        switch (i_functionType)
        {
            case LerpFunction.EaseOutExpo:
                return EaseOutExpo(x);
            case LerpFunction.EaseOutBack:
                return EaseOutBack(x);
            default:
                return EaseOutExpo(x);
        }
    }

    public static float EaseOutExpo(float x)
    {
        return 1 - Mathf.Pow(2, -10 * x);
    }

    public static float EaseOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }
}

