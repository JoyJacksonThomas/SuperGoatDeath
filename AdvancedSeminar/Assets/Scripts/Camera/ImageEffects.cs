using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ImageEffects : MonoBehaviour {

   public Material mEffectMaterial;

   private void OnRenderImage(RenderTexture source, RenderTexture destination)
   {
      //[] pixels = new Color[];
      Graphics.Blit(source, destination, mEffectMaterial);
   }
}