using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExportFormat
{

    public static float[] vertexXPos = {-.5f, -.5f, -.5f, -.5f, .5f, .5f, .5f, .5f };
    public static float[] vertexYPos = {-.5f, -.5f, .5f, .5f, -.5f, -.5f, .5f, .5f };
    public static float[] vertexZPos = {-.5f, .5f, -.5f, .5f, -.5f, .5f, -.5f, .5f };

    public static int[] face1 = { 1, 1, 1, 1, 3, 3, 5, 5, 1, 1, 2, 2 };
    public static int[] face2 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 1, 1 };
    public static int[] face3 = { 7, 3, 4, 2, 8, 4, 7, 8, 5, 6, 6, 8 };
    public static int[] face4 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 2, 2 };
    public static int[] face5 = { 5, 7, 3, 4, 7, 8, 8, 6, 6, 2, 8, 4 };
    public static int[] face6 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 1, 1 };

    

    /// <summary>
    ///  0 : 정점
    ///  1 : 면
    ///  2 : 머테리얼 이름
    /// </summary>
    public static string objFormat = @"

{0}

usemtl {2}

{1}

";
    public static string vertexFormat = @"
v {0} {1} {2}
";
    public static string faceFormat = @"
f {0}//{1} {2}//{3} {4}//{5}
";

    /// <summary>
    ///  0 : xPos
    ///  1 : yPos
    ///  2 : zPos
    ///  3 : surface name
    ///  4 : rgb r (0~1)
    ///  5 : rgb g (0~1)
    ///  6 : rgb b (0~1)
    /// </summary>
    public static string dbFormat = @"
position:{0},{1},{2}
texture:{3}
color:{4},{5},{6}
//
";
}