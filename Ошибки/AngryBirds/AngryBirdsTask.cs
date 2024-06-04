using System;

namespace AngryBirds
{
    public static class AngryBirdsTask
    {
        // ���� � ��� XML ������������, � ���������� ���� ����� ����������, 
        // ����� ���������� ��������� �� ������������� �������. 
        // �� ������ � ����������� �� �����������.
        /// <param name="v">��������� ��������</param>
        /// <param name="distance">���������� �� ����</param>
        /// <returns>���� ������������ � �������� �� 0 �� Pi/2</returns>
        public static double FindSightAngle(double v, double distance)
        {

            return Math.Asin((9.8 * distance) / (v * v)) / 2;
        }
    }
}