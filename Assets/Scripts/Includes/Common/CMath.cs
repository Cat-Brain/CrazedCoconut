using UnityEngine;

namespace ClownLib
{
    public static class CMath
    {
        #region int
        public static int Mod(int original, int value)
        {
            return (original % value + value) % value;
        }
        #endregion
        #region float
        public static float Mod(float original, float value)
        {
            return (original % value + value) % value;
        }

        public static float NPClamp(float original, float minMax)
        {
            return Mathf.Clamp(original, -minMax, minMax);
        }

        public static float Rot0To360IntoN180To180(float rotation)
        {
            return rotation < 180 ? rotation : rotation - 360;
        }

        public static float TryAdd(float original, float additional, float maximum)
        {
            additional += original;
            if (Mathf.Abs(additional) > maximum && Mathf.Abs(additional) > Mathf.Abs(original))
                return Mathf.Abs(original) > maximum ? original : Mathf.Sign(original) * maximum;
            return additional;
        }

        public static float TrySub(float original, float subtraction)
        {
            subtraction = original - Mathf.Sign(original) * subtraction;
            if (Mathf.Sign(subtraction) != Mathf.Sign(original))
                return 0;
            return subtraction;
        }

        public static float TrySubTo(float original, float desired, float subtraction)
        {
            return desired + TrySub(original - desired, subtraction);
        }
        #endregion
        #region Vector2

        public static Vector2Int FloorToVector2Int(Vector2 original)
        {
            return new Vector2Int(Mathf.FloorToInt(original.x), Mathf.FloorToInt(original.y));
        }

        public static Vector2Int CeilToVector2Int(Vector2 original)
        {
            return new Vector2Int(Mathf.CeilToInt(original.x), Mathf.CeilToInt(original.y));
        }

        public static Vector2Int RoundToVector2Int(Vector2 original)
        {
            return new Vector2Int(Mathf.RoundToInt(original.x), Mathf.RoundToInt(original.y));
        }

        public static Vector2 TryAdd2(Vector2 original, Vector2 additional, float maximum)
        {
            additional += original;
            float newMagnitude = additional.magnitude, oldMagnitude = original.magnitude;
            if (newMagnitude > maximum && newMagnitude > oldMagnitude)
                return Vector2.ClampMagnitude(additional, Mathf.Max(oldMagnitude, maximum));
            return additional;
        }

        public static Vector2 TrySub2(Vector2 original, float subtraction)
        {
            Vector2 newValue = original - original.normalized * subtraction;
            if (Mathf.Sign(newValue.x) != Mathf.Sign(original.x) || Mathf.Sign(newValue.y) != Mathf.Sign(original.y))
                return Vector2.zero;
            return newValue;
        }

        public static Vector2 Rotate(Vector2 original, float radians)
        {
            float sTheta = Mathf.Sin(radians), cTheta = Mathf.Cos(radians);
            return new Vector2(original.x * cTheta - original.y * sTheta, original.x * sTheta + original.y * cTheta);
        }

        public static Vector2 Rotate90Clock(Vector2 original)
        {
            return new Vector2(original.y, -original.x);
        }

        public static Vector2Int Rotate90Clock(Vector2Int original)
        {
            return new Vector2Int(original.y, -original.x);
        }

        public static Vector2 Rotate90Counter(Vector2 original)
        {
            return new Vector2(-original.y, original.x);
        }

        public static Vector2Int Rotate90Counter(Vector2Int original)
        {
            return new Vector2Int(-original.y, original.x);
        }

        public static Vector2 Polar(float radius, float angle)
        {
            return radius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public static Vector2 YX_2(Vector2 original)
        {
            return new Vector2(original.y, original.x);
        }

        public static Vector3 YZ_X(Vector2 yz, float x = 0)
        {
            return new Vector3(x, yz.x, yz.y);
        }

        public static Vector3 XZ_Y(Vector2 xz, float y = 0)
        {
            return new Vector3(xz.x, y, xz.y);
        }

        public static Vector3 XY_Z(Vector2 xy, float z = 0)
        {
            return new Vector3(xy.x, xy.y, z);
        }

        #endregion
        #region Vector3

        public static Vector3Int FloorToVector3Int(Vector3 original)
        {
            return new Vector3Int(Mathf.FloorToInt(original.x), Mathf.FloorToInt(original.y), Mathf.FloorToInt(original.z));
        }

        public static Vector3Int CeilToVector3Int(Vector3 original)
        {
            return new Vector3Int(Mathf.CeilToInt(original.x), Mathf.CeilToInt(original.y), Mathf.CeilToInt(original.z));
        }

        public static Vector3Int RoundToVector3Int(Vector3 original)
        {
            return new Vector3Int(Mathf.RoundToInt(original.x), Mathf.RoundToInt(original.y), Mathf.RoundToInt(original.z));
        }

        public static Vector2 XY_3(Vector3 original)
        {
            return original;
        }

        public static Vector2 YX_3(Vector3 original)
        {
            return new Vector2(original.y, original.x);
        }

        public static Vector2 XZ_3(Vector3 original)
        {
            return new Vector2(original.x, original.z);
        }

        public static Vector2 ZX_3(Vector3 original)
        {
            return new Vector2(original.z, original.x);
        }

        public static Vector2 YZ_3(Vector3 original)
        {
            return new Vector2(original.y, original.z);
        }

        public static Vector2 ZY_3(Vector3 original)
        {
            return new Vector2(original.z, original.y);
        }

        public static Vector3 SetX_3(Vector3 original, float x = 0)
        {
            return new Vector3(x, original.y, original.z);
        }

        public static Vector3 SetY_3(Vector3 original, float y = 0)
        {
            return new Vector3(original.x, y, original.z);
        }

        public static Vector3 SetZ_3(Vector3 original, float z = 0)
        {
            return new Vector3(original.x, original.y, z);
        }

        public static Vector3 Div3(Vector3 numerator, Vector3 denominator)
        {
            return new Vector3(numerator.x / denominator.x, numerator.y / denominator.y, numerator.z / denominator.z);
        }

        public static Vector3 Min3(Vector3 first, Vector3 second)
        {
            return new Vector3(Mathf.Min(first.x, second.x), Mathf.Min(first.y, second.y), Mathf.Min(first.z, second.z));
        }

        public static Vector3 Max3(Vector3 first, Vector3 second)
        {
            return new Vector3(Mathf.Max(first.x, second.x), Mathf.Max(first.y, second.y), Mathf.Max(first.z, second.z));
        }

        public static Vector3 Clamp3(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y), Mathf.Clamp(value.z, min.z, max.z));
        }

        public static Vector3 Abs3(Vector3 original)
        {
            return new Vector3(Mathf.Abs(original.x), Mathf.Abs(original.y), Mathf.Abs(original.z));
        }

        public static Vector3 Mod3(Vector3 original, Vector3 value)
        {
            return new Vector3(Mod(original.x, value.x), Mod(original.y, value.y), Mod(original.z, value.z));
        }

        public static Vector3 Mod3(Vector3 original, float value)
        {
            return new Vector3(Mod(original.x, value), Mod(original.y, value), Mod(original.z, value));
        }

        public static Vector3 TryAdd3(Vector3 original, Vector3 additional, float maximum)
        {
            additional += original;
            float newMagnitude = additional.magnitude, oldMagnitude = original.magnitude;
            if (newMagnitude > maximum && newMagnitude > oldMagnitude)
                return Vector3.ClampMagnitude(additional, Mathf.Max(oldMagnitude, maximum));
            return additional;
        }

        public static Vector3 TrySub3(Vector3 original, float subtraction)
        {
            Vector3 newValue = original - original.normalized * subtraction;
            if (Mathf.Sign(newValue.x) != Mathf.Sign(original.x) || Mathf.Sign(newValue.y) != Mathf.Sign(original.y) || Mathf.Sign(newValue.z) != Mathf.Sign(original.z))
                return Vector3.zero;
            return newValue;
        }

        public static readonly Vector3
            V3110 = new(1, 1, 0),
            V3101 = new(1, 0, 1),
            V3100 = new(1, 0, 0),
            V3011 = new(0, 1, 1),
            V3010 = new(0, 1, 0),
            V3001 = new(0, 0, 1);

        public static readonly Vector3Int
            V3I110 = new(1, 1, 0),
            V3I101 = new(1, 0, 1),
            V3I100 = new(1, 0, 0),
            V3I011 = new(0, 1, 1),
            V3I010 = new(0, 1, 0),
            V3I001 = new(0, 0, 1);

        #region Sign Floppers
        public static Vector3 V3PPN(Vector3 value)
        {
            return new Vector3(value.x, value.y, -value.z);
        }

        public static Vector3 V3PNP(Vector3 value)
        {
            return new Vector3(value.x, -value.y, value.z);
        }

        public static Vector3 V3PNN(Vector3 value)
        {
            return new Vector3(value.x, -value.y, -value.z);
        }

        public static Vector3 V3NPP(Vector3 value)
        {
            return new Vector3(-value.x, value.y, value.z);
        }

        public static Vector3 V3NPN(Vector3 value)
        {
            return new Vector3(-value.x, value.y, -value.z);
        }

        public static Vector3 V3NNP(Vector3 value)
        {
            return new Vector3(-value.x, -value.y, value.z);
        }
        #endregion
        #endregion
        #region Quaternion
        public static Quaternion LookDir(Vector2 dir)
        {
            return Quaternion.Euler(0, 0, Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg);
        }
        public static Quaternion LookDir(Vector2 dir, Vector2 baseDir)
        {
            return Quaternion.Euler(0, 0, Vector2.Angle(baseDir, dir));
        }
        #endregion
        #region Miscellaneous
        public static bool LayerOverlapsMask(int layer, LayerMask mask)
        {
            return (mask & (1 << layer)) != 0;
        }
        #endregion
    }

    public static class CIntExtensions
    {
        public static int Mod(this int original, int value)
        {
            return CMath.Mod(original, value);
        }

        public static int ModEq(ref this int original, int value)
        {
            return original = original.Mod(value);
        }
    }

    public static class CFLoatExtensions
    {
        public static float Lerp(this float original, float other, float amount)
        {
            return Mathf.Lerp(original, other, amount);
        }

        public static float Mod(this float original, float value)
        {
            return CMath.Mod(original, value);
        }

        public static float NPClamp(this float original, float minMax)
        {
            return CMath.NPClamp(original, minMax);
        }

        public static float Rot0To360IntoN180To180(this float rotation)
        {
            return Rot0To360IntoN180To180(rotation);
        }

        public static float TryAdd(this float original, float additional, float maximum)
        {
            return CMath.TryAdd(original, additional, maximum);
        }

        public static float TrySub(this float original, float subtraction)
        {
            return CMath.TrySub(original, subtraction);
        }

        public static float TrySubTo(this float original, float desired, float subtraction)
        {
            return CMath.TrySubTo(original, desired, subtraction);
        }
    }

    public static class CVector2Extensions
    {
        public static Vector2Int FloorToVector2Int(this Vector2 original)
        {
            return CMath.FloorToVector2Int(original);
        }

        public static Vector2Int CeilToVector2Int(this Vector2 original)
        {

            return CMath.CeilToVector2Int(original);
        }

        public static Vector2Int RoundToVector2Int(this Vector2 original)
        {
            return CMath.RoundToVector2Int(original);
        }

        public static Vector2 TryAdd2(this Vector2 original, Vector2 additional, float maximum)
        {
            return CMath.TryAdd2(original, additional, maximum);
        }

        public static Vector2 TrySub2(this Vector2 original, float subtraction)
        {
            return CMath.TrySub2(original, subtraction);
        }

        public static Vector2 Rotate(this Vector2 original, float radians)
        {
            return CMath.Rotate(original, radians);
        }

        public static Vector2 Rotate90Clock(this Vector2 original)
        {
            return CMath.Rotate90Clock(original);
        }

        public static Vector2Int Rotate90Clock(this Vector2Int original)
        {
            return CMath.Rotate90Clock(original);
        }

        public static Vector2 Rotate90Counter(this Vector2 original)
        {
            return CMath.Rotate90Counter(original);
        }

        public static Vector2Int Rotate90Counter(this Vector2Int original)
        {
            return CMath.Rotate90Counter(original);
        }

        public static Vector2 YX(this Vector2 original)
        {
            return CMath.YX_2(original);
        }

        public static Vector3 YZ_X(this Vector2 yz, float x = 0)
        {
            return CMath.YZ_X(yz, x);
        }

        public static Vector3 XZ_Y(this Vector2 xz, float y = 0)
        {
            return CMath.XZ_Y(xz, y);
        }

        public static Vector3 XY_Z(this Vector2 xy, float z = 0)
        {
            return CMath.XY_Z(xy, z);
        }
    }

    public static class CVector3Extensions
    {
        public static Vector3Int FloorToVector3Int(this Vector3 original)
        {
            return CMath.FloorToVector3Int(original);
        }

        public static Vector3Int CeilToVector3Int(this Vector3 original)
        {

            return CMath.CeilToVector3Int(original);
        }

        public static Vector3Int RoundToVector3Int(this Vector3 original)
        {
            return CMath.RoundToVector3Int(original);
        }

        public static Vector2 XY(this Vector3 original)
        {
            return CMath.XY_3(original);
        }

        public static Vector2 YX(this Vector3 original)
        {
            return CMath.YX_3(original);
        }

        public static Vector2 XZ(this Vector3 original)
        {
            return CMath.XZ_3(original);
        }

        public static Vector2 ZX(this Vector3 original)
        {
            return CMath.ZX_3(original);
        }

        public static Vector2 YZ(this Vector3 original)
        {
            return CMath.YZ_3(original);
        }

        public static Vector2 ZY(this Vector3 original)
        {
            return CMath.ZY_3(original);
        }

        public static Vector3 SetX(this Vector3 original, float x = 0)
        {
            return CMath.SetX_3(original, x);
        }

        public static Vector3 SetY(this Vector3 original, float y = 0)
        {
            return CMath.SetY_3(original, y);
        }

        public static Vector3 SetZ(this Vector3 original, float z = 0)
        {
            return CMath.SetZ_3(original, z);
        }

        public static Vector3 Div(this Vector3 numerator, Vector3 denominator)
        {
            return CMath.Div3(numerator, denominator);
        }

        public static Vector3 Min(this Vector3 first, Vector3 second)
        {
            return CMath.Min3(first, second);
        }

        public static Vector3 Max(this Vector3 first, Vector3 second)
        {
            return CMath.Max3(first, second);
        }

        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
        {
            return CMath.Clamp3(value, min, max);
        }

        public static Vector3 Abs(this Vector3 original)
        {
            return CMath.Abs3(original);
        }

        public static Vector3 Mod(this Vector3 original, Vector3 value)
        {
            return CMath.Mod3(original, value);
        }

        public static Vector3 Mod(this Vector3 original, float value)
        {
            return CMath.Mod3(original, value);
        }

        public static Vector3 TryAdd(this Vector3 original, Vector3 additional, float maximum)
        {
            return CMath.TryAdd3(original, additional, maximum);
        }

        public static Vector3 TrySub(this Vector3 original, float subtraction)
        {
            return CMath.TrySub3(original, subtraction);
        }
    }
}