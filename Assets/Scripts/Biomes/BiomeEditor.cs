using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BiomeEditor : MonoBehaviour {
    public BiomeManager manager;
    public InputField inputField;
    public BlockController preview;

    Vector3 previewOffset;

    List<BlockType> types;
    List<BlockShape> shapes;

    int currentType;
    int currentShape;
    int currentRotation;

	// Use this for initialization
	void Start () {
        currentType = 0;
        currentShape = 0;
        currentRotation = 0;

        types = new List<BlockType>(manager.Blocks.Keys);
        shapes = new List<BlockShape>(manager.Shapes.Keys);

        RefreshPreview();
    }

    void RefreshPreview()
    {
        Vector3Int oldPos = preview.biomeCoords;
        Destroy(preview.gameObject);

        Transform t = Instantiate(manager.GetBlockShape(shapes[currentShape]), transform);
        previewOffset = t.localPosition;
        t.position = (t.localPosition + oldPos) * Biome.BlockSize;
        t.localScale *= Biome.BlockSize;
        BlockController newBlock = t.gameObject.AddComponent<BlockController>().SetBiomeCoords(oldPos).SetRotation(Vector3.up * currentRotation);
        if (currentType != -1)
        {
            Material mat = manager.GetBlockMaterial(types[currentType]);
            MeshRenderer mr = newBlock.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                foreach (MeshRenderer mrchild in newBlock.GetComponentsInChildren<MeshRenderer>())
                {
                    if (mrchild != null) mrchild.material = mat;
                }
            }
            else
            {
                mr.material = mat;
            }
        }
        Destroy(newBlock.GetComponent<Collider>());
        preview = newBlock;
    }

    void MovePreview()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            BlockController block = hit.transform.GetComponent<BlockController>();
            if (block != null)
            {
                preview.transform.localPosition = (previewOffset + GetNewBlockPos(hit)) * Biome.BlockSize;
                preview.SetRotation(Vector3.up * currentRotation);
                preview.gameObject.SetActive(true);
            } else
            {
                preview.gameObject.SetActive(false);
            }
        } else
        {
            preview.gameObject.SetActive(false);
        }
    }
	
    Vector3Int GetNewBlockPos(RaycastHit hit)
    {
        BlockController block = hit.transform.GetComponent<BlockController>();
        BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
        Vector3 dist = hit.point - block.transform.position;
        Vector3Int dir = Vector3Int.zero;

        if (Mathf.Abs(dist.x) == 1f)
        {
            dir = new Vector3Int((int)dist.x, 0, 0);
        }
        if (Mathf.Abs(dist.y) == 1f)
        {
            dir = new Vector3Int(0, (int)dist.y, 0);
        }
        if (Mathf.Abs(dist.z) == 1f)
        {
            dir = new Vector3Int(0, 0, (int)dist.z);
        }

        return block.biomeCoords + dir;
    }

	// Update is called once per frame
	void Update () {
        if (!inputField.isFocused)
        {
            Camera.main.transform.localPosition += Camera.main.transform.right * Input.GetAxis("Horizontal")
                + Camera.main.transform.forward * Input.mouseScrollDelta.y
                + Vector3.up * Input.GetAxis("Vertical");
            Camera.main.transform.LookAt(new Vector3(Biome.XSize, Biome.YSize, Biome.ZSize) * Biome.BlockSize * 0.5f);

            if (Input.GetKeyUp(KeyCode.P))
            {
                currentType++;
                if (currentType == types.Count)
                {
                    currentType = -1;
                }
                RefreshPreview();
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                currentShape++;
                if (currentShape == shapes.Count)
                {
                    currentShape = 0;
                }
                RefreshPreview();
            }

            if (Input.GetKeyUp(KeyCode.O))
            {
                currentRotation += 90;
                if (currentRotation == 360)
                {
                    currentRotation = 0;
                }
            }

            MovePreview();
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                BlockController block = hit.transform.GetComponent<BlockController>();
                if (block != null)
                {
                    BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
                    Vector3Int newPos = GetNewBlockPos(hit);
                    if (currentType == -1)
                    {
                        biome.SetBlock(newPos, shapes[currentShape]).SetRotation(Vector3.up * currentRotation);
                    }
                    else
                    {
                        biome.SetBlock(newPos, shapes[currentShape], types[currentType]).SetRotation(Vector3.up * currentRotation);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                BlockController block = hit.transform.GetComponent<BlockController>();
                if (block != null)
                {
                    BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
                    biome.RemoveBlock(hit.transform.name);
                }
            }
        }
    }

    public void New()
    {
        manager.RemoveBiome(Vector3Int.zero);
        manager.CreateBiome(Vector3Int.zero, BiomeType.BaseEditor);
    }

    public void LoadBiome()
    {
        manager.RemoveBiome(Vector3Int.zero);
        CreatePremadeBiomeByName(inputField.text);
    }

    public BiomeController CreatePremadeBiomeByName(string name)
    {
        GameObject biome = new GameObject("0-0-0");
        biome.transform.parent = manager.transform;
        BiomeController b = biome.AddComponent<BiomeController>();
        b.Manager = manager;
        b.transform.localPosition = Vector3Int.zero;

        b.BiomeInstance = new BiomePremade(name);
        b.BiomeInstance.Generate(b);

        return b;
    }

    public void SaveBiome()
    {
        BiomeController bc = manager.GetBiome(Vector3Int.zero);
        if (bc == null) return;

        string path = Path.Combine(Application.dataPath, "Resources/Biomes/" + inputField.text + ".bytes");
        using (
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))
        )
        {
            bc.Save(writer, true);
        }
    }
}
