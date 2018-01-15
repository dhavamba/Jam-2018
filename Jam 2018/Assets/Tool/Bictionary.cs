﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bictionary<T1, T2> : Dictionary<T1, T2>
{
    public T1 this[T2 index]
    {
        get
        {
            if (!this.Any(x => x.Value.Equals(index)))
            {
                throw new System.Collections.Generic.KeyNotFoundException();
            }
            return this.First(x => x.Value.Equals(index)).Key;
        }
    }
}
