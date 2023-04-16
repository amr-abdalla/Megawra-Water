using UnityEngine;


public static class AccelerationUtility {
    
    public static float accelerate(float i_sign, float i_speed, float i_maxSpeed, float i_acceleration)
    {
        float deltaVel = i_acceleration * Time.fixedDeltaTime * i_sign;
        i_speed += deltaVel;

        i_speed = i_sign > 0 ?
            Mathf.Clamp(i_speed, 0, i_maxSpeed) :
            Mathf.Clamp(i_speed, -i_maxSpeed, 0);

        return i_speed;
    }

    public static float descelerate(float i_speed, float i_desceleration)
    {
        if (i_speed == 0f) return 0f;

        float deltaVel = i_desceleration * Time.fixedDeltaTime;

        if (i_speed < 0)
        {
            i_speed += deltaVel;
            if (i_speed > 0) i_speed = 0f;
        }
        else
        {
            i_speed -= deltaVel;
            if (i_speed < 0) i_speed = 0f;
        }

        return i_speed;
    }
}