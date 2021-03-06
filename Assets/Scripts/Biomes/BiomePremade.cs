﻿using System;
using System.IO;
using UnityEngine;

public class BiomePremade : Biome
{
    string filename;

    public BiomePremade(string file)
    {
        this.filename = file;
    }

    public override void Generate(BiomeController biome)
    {
        TextAsset binFile = Resources.Load<TextAsset>("Biomes/" + filename);
        if (binFile == null)
        {
            Debug.LogError("Unable to load premade biome <" + filename + ">");
            return;
        }

        using (
            BinaryReader reader = new BinaryReader(new MemoryStream(binFile.bytes))
        )
        {
            try
            {
                biome.Read(reader);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading biome " + filename);
            }
        }
    }

    public override void Update(BiomeController biome)
    {

    }
}