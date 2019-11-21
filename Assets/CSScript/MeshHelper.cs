using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CSScript
{
    class MeshHelper:Singleton<MeshHelper>
    {
        /* @param meshFilter 网格渲染器对象 */
        public bool CreateATriangleMesh(MeshFilter meshFilter)
        {
            //通过渲染器对象得到网格对象
            Mesh mesh = meshFilter.mesh;

            //官方文档mesh scriptapi用法有详细说明

            //设置顶点，这个属性非常重要
            //三个点确定一个面，所以Vector3数组的数量一定是3个倍数
            //遵循顺时针三点确定一面
            //这里的数量为6 也就是我创建了2个三角面
            //依次填写3D坐标点
            mesh.vertices = new Vector3[] { new Vector3(5, 0, 0), new Vector3(0, 5, 0), new Vector3(0, 0, 5), new Vector3(-5, 0, 0), new Vector3(0, -5, 0), new Vector3(0, 0, -5) };

            //设置贴图点，因为面确定出来以后就是就是2D 
            //所以贴图数量为Vector2 
            //第一个三角形设置5个贴图
            //第二个三角形设置一个贴图
            //数值数量依然要和顶点的数量一样(normal法线，uv2，tanent切线同理，否则会报out of bound错误)
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 5), new Vector2(5, 5), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };

            //设置三角形索引，通过索引值取vertices数组对应的顶点
            //每3个索引构成一组，对应的3个vertices[index]构成三角面
            //索引顺序确定方向性，如{3，4，5}和{5，4，3}构成的三角形互为反面(左手定则，拇指方向的面显示)
            //最后将两个三角形绘制在平面中
            //数值数量必须是3的倍数
            mesh.triangles = new int[] { 3, 4, 5, 2, 1, 4 };
            return true;
        }
    }

    
}
