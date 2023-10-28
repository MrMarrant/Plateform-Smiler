﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Events
{
    public class CharacterEvent
    {
        public static UnityAction<GameObject, float> CharacterOnDamage;

        public static UnityAction<GameObject, float> CharacterOnHeal;
    }
}
