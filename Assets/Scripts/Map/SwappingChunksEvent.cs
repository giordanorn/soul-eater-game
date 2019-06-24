using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwappingChunksEvent : UnityEvent<IEnumerable<RectInt>, IEnumerable<RectInt>>
{
}
