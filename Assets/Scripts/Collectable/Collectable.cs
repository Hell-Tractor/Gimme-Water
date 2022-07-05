using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Runtime.Serialization;


public abstract class Collectable : NetworkBehaviour
{
    abstract public void collectByCharacter(CharacterStatus status);
}
