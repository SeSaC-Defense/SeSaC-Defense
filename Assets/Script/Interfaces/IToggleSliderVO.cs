using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToggleSliderVO : IOptionSourceBool, IOptionSourceFloat, IOptionDestination<bool>, IOptionDestination<float>
{
    
}
