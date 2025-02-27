using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExportFormat
{

    public static float[] VertexXPos = {-.5f, -.5f, -.5f, -.5f, .5f, .5f, .5f, .5f };
    public static float[] VertexYPos = {-.5f, -.5f, .5f, .5f, -.5f, -.5f, .5f, .5f };
    public static float[] VertexZPos = {-.5f, .5f, -.5f, .5f, -.5f, .5f, -.5f, .5f };

    public static int[] Face1 = { 1, 1, 1, 1, 3, 3, 5, 5, 1, 1, 2, 2 };
    public static int[] Face2 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 1, 1 };
    public static int[] Face3 = { 7, 3, 4, 2, 8, 4, 7, 8, 5, 6, 6, 8 };
    public static int[] Face4 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 1, 1 };
    public static int[] Face5 = { 5, 7, 3, 4, 7, 8, 8, 6, 6, 2, 8, 4 };
    public static int[] Face6 = { 2, 2, 6, 6, 3, 3, 5, 5, 4, 4, 1, 1 };

    public static int[] Vt1 =   { 3, 3, 1, 1, 3, 3, 1, 1, 3, 3, 3, 3 };
    public static int[] Vt2 =   { 1, 4, 3, 4, 1, 4, 2, 3, 2, 1, 2, 1 };
    public static int[] Vt3 =   { 2, 1, 2, 3, 2, 1, 3, 4, 1, 4, 1, 4 };


    public static string UseMtl = "mtllib output.mtl";

    /// <summary>
    ///  0 : 정점
    ///  1 : 면
    ///  2 : 머테리얼 이름
    /// </summary>
    /// 
    public static string ObjFormat = @"{0}
vt 0 0
vt 0 1
vt 1 1
vt 1 0

vn  0.0  0.0 -1.0  # 1 앞면
vn  0.0  0.0  1.0  # 2 뒷면
vn -1.0  0.0  0.0  # 3 왼쪽면
vn  1.0  0.0  0.0  # 4 오른쪽면
vn  0.0 -1.0  0.0  # 5 바닥면
vn  0.0  1.0  0.0  # 6 윗면

usemtl {2}
{1}

";
    public static string VertexFormat = @"
v {0} {1} {2}";
    public static string FaceFormat = @"
f {0}/{6}/{1} {2}/{7}/{3} {4}/{8}/{5}";

    /// <summary>
    ///  0 : xPos
    ///  1 : yPos
    ///  2 : zPos
    ///  3 : surface name
    ///  4 : rgb r (0~1)
    ///  5 : rgb g (0~1)
    ///  6 : rgb b (0~1)
    /// </summary>
    public static string BdFormat = @"
position:{0},{1},{2}
texture:{3}
color:{4},{5},{6}
//
";
    /// <summary>
    /// 0 : Material Name
    /// 1 : texture name
    /// 2 : r
    /// 3 : g
    /// 4 : b
    /// </summary>
    public static string MtlFormat = @"
newmtl {0}

Kd {2} {3} {4}
map_Kd {1}.png
";
    /// <summary>
    /// 0 : r
    /// 1 : g
    /// 2 : b
    /// 3 : texture name
    /// </summary>
    public static string MtlName = @"{0}_{1}_{2}_{3}";
}