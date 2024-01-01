using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;

        uint rabbitBufferID;
        uint xyzAxesBufferID;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;

        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX = 0,
                     translationY = 0,
                     translationZ = 0;

        public float scaleX = 1,
                     scaleY = 1,
                     scaleZ = 1;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 rabbitCenter;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");

            Gl.glClearColor(0, 0, 0.4f, 1);

            float[] rabbitVertices = {

                //ears
                //leftear
                -0.937f * 30, 0.931f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.556f * 30, 0.777f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.886f * 30, 0.544f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                -0.886f * 30, 0.544f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.721f * 30, 0.21f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                -0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.721f * 30, 0.21f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                -0.556f * 30, 0.777f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.288f * 30, 0.533f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //15
 
 
                //rightear
                0.937f * 30, 0.931f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.556f * 30, 0.777f * 30, 0.0f,
                0.71f ,0.65f ,0.60f,
                0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.886f * 30, 0.544f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                0.886f * 30, 0.544f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.721f * 30, 0.21f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.721f * 30, 0.21f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                0.556f * 30, 0.777f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.407f * 30, 0.469f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.288f * 30, 0.533f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //30
 
 
                //face
                0.0f * 30, 0.19f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.527f * 30, -0.374f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.521f * 30, -0.751f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.236f * 30, -0.915f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.117f * 30, -0.958f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.117f * 30, -0.958f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.236f * 30, -0.915f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.521f * 30, -0.751f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.527f * 30, -0.374f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //43
 
 
                //triangle at the top of the head 
                0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.0f * 30, 0.19f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.105f * 30, 0.241f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.0f * 30, 0.04f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //47
 
 
                //left part next to the eye
                -0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.527f * 30, -0.374f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.345f * 30, -0.325f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.39f * 30, -0.257f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.39f * 30, -0.194f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.345f * 30, -0.114f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //right part next to the eye
                0.43f * 30, 0.034f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.527f * 30, -0.374f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.345f * 30, -0.325f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.39f * 30, -0.257f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.39f * 30, -0.194f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.345f * 30, -0.114f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //59
 
 
                //the middle of the face
                0.0f * 30, 0.04f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.345f * 30, -0.114f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.322f * 30, -0.204f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.345f * 30, -0.325f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.111f * 30, -0.48f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.111f * 30, -0.48f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.345f * 30, -0.325f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.322f * 30, -0.204f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.345f * 30, -0.114f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
 
                //68

                //Part between the nose and the mouth
                -0.521f * 30, -0.751f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.134f * 30, -0.613f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.134f * 30, -0.613f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.521f * 30, -0.751f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.0f * 30, -0.804f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                
 
                //73
 
                //nose
                -0.111f * 30, -0.48f * 30, 0.0f,
                0f, 0f, 0f,
                -0.134f * 30, -0.613f * 30, 0.0f,
                0f, 0f, 0f,
                0.0f * 30, -0.724f * 30, 0.0f,
                0f, 0f, 0f,
                0.134f * 30, -0.613f * 30, 0.0f,
                0f, 0f, 0f,
                0.111f * 30, -0.48f * 30, 0.0f,
                0f, 0f, 0f,
                
 
                //78
 
 
                //mouth
                -0.236f * 30, -0.915f * 30, 0.0f,
                1f, 1f, 1f,
                0.0f * 30, -0.804f * 30, 0.0f,
                1f, 1f, 1f,
                0.236f * 30, -0.915f * 30, 0.0f,
                1f, 1f, 1f,
                0.117f * 30, -0.958f * 30, 0.0f,
                1f, 1f, 1f,
                0.0f * 30, -0.878f * 30, 0.0f,
                1f, 1f, 1f,
                -0.117f * 30, -0.958f * 30, 0.0f,
                1f, 1f, 1f,
 
                //84
                //teeth
 
                0.0f * 30, -0.804f * 30, 0.0f,
                1f, 1f, 1f,
                -0.088f * 30, -0.846f * 30, 0.0f,
                1f, 1f, 1f,
                0.0f * 30, -0.878f * 30, 0.0f,
                1f, 1f, 1f,
                0.088f * 30, -0.846f * 30, 0.0f,
                1f, 1f, 1f,
 
                //88
 
                //some lines in the face
                0.0f * 30, -0.878f * 30, 0.0f,
                1f, 1f, 1f,
                0.0f * 30, -0.958f * 30, 0.0f,
                1f, 1f, 1f,

                0.322f * 30, -0.204f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                -0.322f * 30, -0.204f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                0.0f * 30, -0.724f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.0f * 30, -0.804f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                0.0f * 30, 0.19f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,
                0.0f * 30, -0.48f * 30, 0.0f,
                0.71f, 0.65f, 0.60f,

                //96

                //eyes
                //left eye
                -0.345f * 30, -0.114f * 30, 0.0f,
                0f, 0f, 0f,
                -0.39f * 30, -0.194f * 30, 0.0f,
                0f, 0f, 0f,
                -0.39f * 30, -0.257f * 30, 0.0f,
                0f, 0f, 0f,
                -0.345f * 30, -0.325f * 30, 0.0f,
                0f, 0f, 0f,
                -0.322f * 30, -0.204f * 30, 0.0f,
                0f, 0f, 0f,

                //101


                //right eye
                0.345f * 30, -0.114f * 30, 0.0f,
                0f, 0f, 0f,
                0.39f * 30, -0.194f * 30, 0.0f,
                0f, 0f, 0f,
                0.39f * 30, -0.257f * 30, 0.0f,
                0f, 0f, 0f,
                0.345f * 30, -0.325f * 30, 0.0f,
                0f, 0f, 0f,
                0.322f * 30, -0.204f * 30, 0.0f,
                0f, 0f, 0f,


            };



            float[] xyzAxesVertices = {
		        //x
		        0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f,
                100.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, 
		        //y
	            0.0f, 0.0f, 0.0f,
                0.0f,1.0f, 0.0f,
                0.0f, 100.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 
		        //z
	            0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f,
                0.0f, 0.0f, -100.0f,
                0.0f, 0.0f, 1.0f,
            };


            rabbitBufferID = GPU.GenerateBuffer(rabbitVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(50, 50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 1, 0)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);

            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region Animated Triangle
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, rabbitBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);

            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            //ears
            Gl.glDrawArrays(Gl.GL_QUADS, 0, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 4, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 10, 5);

            Gl.glDrawArrays(Gl.GL_QUADS, 15, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 19, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 25, 5);


            //face
            Gl.glDrawArrays(Gl.GL_POLYGON, 30, 13);

            Gl.glDrawArrays(Gl.GL_POLYGON, 43, 4);

            Gl.glDrawArrays(Gl.GL_POLYGON, 47, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 53, 6);

            Gl.glDrawArrays(Gl.GL_POLYGON, 59, 9);

            Gl.glDrawArrays(Gl.GL_POLYGON, 68, 5);

            //nose
            Gl.glDrawArrays(Gl.GL_POLYGON, 73, 5);

            //mouth
            Gl.glDrawArrays(Gl.GL_POLYGON, 78, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 84, 4);

            Gl.glDrawArrays(Gl.GL_LINES, 88, 2);
            Gl.glDrawArrays(Gl.GL_LINES, 90, 2);
            Gl.glDrawArrays(Gl.GL_LINES, 92, 2);
            Gl.glDrawArrays(Gl.GL_LINES, 94, 2);

            //eyes
            Gl.glDrawArrays(Gl.GL_POLYGON, 96, 5);
            Gl.glDrawArrays(Gl.GL_POLYGON, 101, 5);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion
        }


        public void Update()
        {

            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds / 1000.0f;

            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1 * rabbitCenter));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 0, 1)));
            transformations.Add(glm.translate(new mat4(1), rabbitCenter));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            transformations.Add(glm.scale(new mat4(1), new vec3(scaleX, scaleY, scaleZ)));
            ModelMatrix = MathHelper.MultiplyMatrices(transformations);

            timer.Reset();
            timer.Start();
        }

        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}