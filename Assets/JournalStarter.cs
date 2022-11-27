using System;
using System.Collections;
using System.Collections.Generic;
using LostOnTenebris;
using UnityEngine;

public class JournalStarter : MonoBehaviour
{
    public Journal journal;

    private void Start()
    {
        journal.Clear();
    }
}
