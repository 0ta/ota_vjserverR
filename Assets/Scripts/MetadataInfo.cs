using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ota.ndi
{
    public class MetadataInfo
    {
        public string arcameraPosition { get; set; }
        public string arcameraRotation { get; set; }
        public string projectionMatrix { get; set; }
        public float minDepth { get; set; }
        public float maxDepth { get; set; }
        public bool[] toggles { get; set; }
        public float[] sliders { get; set; }


        public MetadataInfo()
        {
        }

        public MetadataInfo(Vector3 arcameraPosition, Quaternion arcameraRotation, Matrix4x4 projectionMatrix, float minDepth, float maxDepth, bool[] toggles, float[] sliders)
        {
            this.arcameraPosition = arcameraPosition.ToString("F2");
            this.arcameraRotation = arcameraRotation.ToString("F5");
            this.projectionMatrix = ToStringFromMat(projectionMatrix);
            this.minDepth = minDepth;
            this.maxDepth = maxDepth;

            this.toggles = toggles;
            this.sliders = sliders;
        }

        public MetadataInfo(string arcameraPosition, string arcameraRotation, string projectionMatrix, float minDepth, float maxDepth)
        {
            SetMetadataInfo(arcameraPosition, arcameraRotation, projectionMatrix, minDepth, maxDepth);
        }

        public void SetMetadataInfo(string arcameraPosition, string arcameraRotation, string projectionMatrix, float minDepth, float maxDepth)
        {
            this.arcameraPosition = arcameraPosition;
            this.arcameraRotation = arcameraRotation;
            this.projectionMatrix = projectionMatrix;
            this.minDepth = minDepth;
            this.maxDepth = maxDepth;
        }

        public bool GetToggle(int i)
        {
            return toggles[i];
        }

        public float GetSlider(int i)
        {
            return sliders[i];
        }

        public Vector3 getArcameraPosition()
        {
            if (arcameraPosition == null) throw new Exception("AR Camera position is null.");
            return createVector3(this.arcameraPosition);
        }

        public Quaternion getArcameraRotation()
        {
            if (arcameraPosition == null) throw new Exception("AR Camera rotaion is null.");
            return createRotation(this.arcameraRotation).normalized;
        }

        public Matrix4x4 getProjectionMatrix()
        {
            if (arcameraPosition == null) throw new Exception("Projection Matrix is null.");
            return createMatrix4x4ForSentFromRMBasedString(this.projectionMatrix);
        }

        public Vector2 getDepthRange()
        {
            return new Vector2(this.minDepth, this.maxDepth);
        }

        Vector3 createVector3(string str)
        {
            var farray = convertStr2FloatArray(str);
            return new Vector3(farray[0], farray[1], farray[2]);
        }

        Quaternion createRotation(string str)
        {
            var farray = convertStr2FloatArray(str);
            return new Quaternion(farray[0], farray[1], farray[2], farray[3]);
        }

        Matrix4x4 createMatrix4x4ForSentFromCMBasedString(string str)
        {
            var farray = convertStr2FloatArray(str);
            var mat = Matrix4x4.identity;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mat[i, j] = farray[i * 4 + j];
                }
            }
            return mat;
        }

        Matrix4x4 createMatrix4x4ForSentFromRMBasedString(string str)
        {
            var farray = convertStr2FloatArray(str);
            var mat = Matrix4x4.identity;
            for (int i = 0; i < 16; i++)
            {
                mat[i] = farray[i];
            }
            return mat;
        }

        string ToStringFromMat(Matrix4x4 mat)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                sb.Append(mat[i].ToString("F5"));
                if (i != 15)
                {
                    sb.Append(" : ");
                }
            }
            return sb.ToString();
        }

        public float[] convertStr2FloatArray(string str)
        {
            var matchs = Regex.Matches(str, "-?[0-9]+\\.[0-9]+");
            var ret = new float[matchs.Count + 1];
            for (int i = 0; i < matchs.Count; i++)
            {
                ret[i] = float.Parse(matchs[i].Value);
            }
            return ret;
        }
    }
}