using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  interface IInteractable
{
  void OnInteract();
  string GetInteractText { get; }
  Highlight GetHighlight { get; }
  Collider GetCollider { get; }
  //Image GetImage { get; }
  
}
