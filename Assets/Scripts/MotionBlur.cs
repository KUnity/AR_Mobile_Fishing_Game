using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotionBlur : MonoBehaviour
{
    private static float throwForce = 4.5f;
    private static float chamjilForce = 2.5f;

    /// <summary>
    /// 낚시대 던지는 모션을 감지하는 함수
    /// </summary>
    /// <returns></returns>
    public static bool CheckThrow()
    {
        return Input.acceleration.magnitude > throwForce;
    }

    /// <summary>
    /// Hooking을 감지하는 함수
    /// </summary>
    /// <returns></returns>
    public static bool CheckChamjil()
    {
        return Input.acceleration.magnitude > chamjilForce;
    }
}
