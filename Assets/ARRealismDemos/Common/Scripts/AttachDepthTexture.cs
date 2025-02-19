//-----------------------------------------------------------------------
// <copyright file="AttachDepthTexture.cs" company="Google LLC">
//
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Attaches and updates the depth texture each frame.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class AttachDepthTexture : MonoBehaviour
{
    private static readonly string _currentDepthTexturePropertyName = "_CurrentDepthTexture";
    private static readonly string _depthDisaplyMatrixName = "_DepthDisplayMatrix";
    //private static readonly string _topLeftRightPropertyName = "_UvTopLeftRight";
    //private static readonly string _bottomLeftRightPropertyName = "_UvBottomLeftRight";
    private Material _material;
    private ARCameraManager _cameraManager;
    private AROcclusionManager _occlusionManager;

    private void Start()
    {
        _occlusionManager = FindObjectOfType<AROcclusionManager>();
       // Debug.Assert(_occlusionManager);

        _cameraManager = FindObjectOfType<ARCameraManager>();
     //   Debug.Assert(_cameraManager);

        _cameraManager.frameReceived += OnCameraFrameReceived;

        // Assign the texture to the material.
        _material = GetComponent<Renderer>().sharedMaterial;
    }

    private void UpdateScreenOrientationOnMaterial()
    {
        #if DEPTH_LAB_DEV_SCREEN_ORIENTATION
        //var uvQuad = Frame.CameraImage.TextureDisplayUvs;
        //_material.SetVector(
        //    _topLeftRightPropertyName,
        //    new Vector4(
        //        uvQuad.TopLeft.x, uvQuad.TopLeft.y, uvQuad.TopRight.x, uvQuad.TopRight.y));
        //_material.SetVector(
        //    _bottomLeftRightPropertyName,
        //    new Vector4(uvQuad.BottomLeft.x, uvQuad.BottomLeft.y, uvQuad.BottomRight.x,
        //        uvQuad.BottomRight.y));
        #endif
    }

    private void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        _material.SetTexture(
            _currentDepthTexturePropertyName, _occlusionManager.environmentDepthTexture);
        _material.SetMatrix(
            _depthDisaplyMatrixName, eventArgs.displayMatrix.GetValueOrDefault());
    }
}
