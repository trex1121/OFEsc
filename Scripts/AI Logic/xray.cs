using UnityEngine;
using System.Collections;

public class xray : MonoBehaviour
{
  void Update()
    {
      if(Physics2d.Raycast(this.transform, Vector3.forward, Physics2d.maxLinearCorrection)

