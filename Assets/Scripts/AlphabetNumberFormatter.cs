using System;
using System.Numerics;

public static class AlphabetNumberFormatter
{
    public static string FormatNumber(BigInteger number)
    {
        if (number < 1000)
            return number.ToString();

        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int unitIndex = 0;
        BigInteger divisor = 1000;

        // 몇 번째 단위인지 계산
        while (number >= divisor * 1000 && unitIndex + 1 < letters.Length)
        {
            divisor *= 1000;
            unitIndex++;
        }

        decimal value = (decimal)number / (decimal)divisor;
        int integerPart = (int)value;

        string valueStr = integerPart >= 10
            ? integerPart.ToString()                       
            : value.ToString("0.##");                      

        return $"{valueStr}{letters[unitIndex]}";
    }
}

