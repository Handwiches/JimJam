using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

    [ExecuteInEditMode]
    [RequireComponent (typeof(Camera))]
    [AddComponentMenu("Metkis/VHSShader")]
    public class VHSShader : ImageEffectBase
    {
        private Vector2 radius = new Vector2(-0.95F,0.03F);
        private float angle = 28;
        private float dilation = 0;
        [Range(-10,10)]
        public float OverlayRepeat = 1;
        [Range(-30,30)]
        public float OverlaySpeed = 10f;
        [Range(-2,2)]
        public float DistortionSpeed = 1f;
        private Vector2 center = new Vector2(0.5F, 0F);

		
        // Called by camera to apply image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            ImageEffects.RenderDistortion (material, source, destination, angle, center, radius);
        }

        void Update(){

            Mathf.Clamp(OverlayRepeat, 0.1f, 10.0f);
            Mathf.Clamp(OverlaySpeed, -10.0f, 10.0f);
            Mathf.Clamp(DistortionSpeed, -5.0f, 5.0f);

        	dilation -= ((0.1f+ UnityEngine.Random.Range(0.2f,0.6f)) * Time.deltaTime * DistortionSpeed);
        	if(dilation < 0){dilation = 1; }
        	center = new Vector3(0.5f,dilation);
            material.SetFloat ("_OverlayRepeat", OverlayRepeat);
            material.SetFloat ("_OverlaySpeed", -OverlaySpeed);
            material.SetFloat ("_DistortionSpeed", DistortionSpeed);


        }

    }

