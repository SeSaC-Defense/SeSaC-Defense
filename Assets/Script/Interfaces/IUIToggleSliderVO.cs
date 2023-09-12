using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIToggleSliderVO : IOptionSourceBool, IOptionSourceFloat, IOptionDestination<bool>, IOptionDestination<float>
{
    
}
