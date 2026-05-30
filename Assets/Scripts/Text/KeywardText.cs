using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardKeyward
{
    Exhaust
}
public static class KeywardText 
{
    public static string[] Deffense = {"방어도","한 턴 동안 피해를 받을 시 체력 대신 줄어듭니다."};
    public static string[] Strength = { "힘", "공격 시 피해를 수치만큼 증가시킵니다." };
    public static string[] Vulnerable = { "취약", "피해를 50% 더 받습니다." };
    public static string[] Exhaust = { "일회성", "카드 사용시 소멸됩니다." };


    public static Dictionary<string, string[]> keywardDict = new Dictionary<string, string[]>()
    {
        {"Deffense",Deffense},
        {"Strength",Strength},
        {"Vulnerable",Vulnerable},
        {"Exhaust",Exhaust}
    };
}
