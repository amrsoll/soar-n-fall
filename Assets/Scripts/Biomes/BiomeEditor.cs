using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BiomeEditor : MonoBehaviour {
    public BiomeManager biomeManager;
    public ItemManager itemManager;
    public InputField inputField;
    public Transform preview;

    public Text currentText;

    Vector3 previewOffset;

    List<BlockType> types;
    List<BlockShape> shapes;
    List<Item> items;

    bool editBlocks;
    int currentType;
    int currentShape;
    int currentRotation;

	// Use this for initialization
	void Start () {
        editBlocks = true;
        currentType = 0;
        currentShape = 0;
        currentRotation = 0;

        types = new List<BlockType>(biomeManager.Blocks.Keys);
        shapes = new List<BlockShape>(biomeManager.Shapes.Keys);
        items = new List<Item>(itemManager.Items.Keys);

        RefreshPreview();
    }

    void PreparePreview(Transform t)
    {
        foreach (Collider c in t.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        foreach (Rigidbody rb in t.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = false;
        }
    }

    void RefreshPreview()
    {
        if (preview != null)
        {
            Destroy(preview.gameObject);
        }

        if (editBlocks) {
            Transform t = Instantiate(biomeManager.GetBlockShape(shapes[currentShape]), transform);
            previewOffset = t.localPosition;
            t.position = t.localPosition * Biome.BlockSize;
            t.localScale *= Biome.BlockSize;
            BlockController newBlock = t.gameObject.AddComponent<BlockController>().SetRotation(Vector3.up * currentRotation);
            if (currentType != -1)
            {
                Material mat = biomeManager.GetBlockMaterial(types[currentType]);
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
            PreparePreview(newBlock.transform);
            preview = newBlock.transform;
        } else
        {
            ItemController newItem = Instantiate(itemManager.Items[items[currentType]], transform);
            if (newItem.GetComponent<Renderer>() != null)
            {
                previewOffset = newItem.GetComponent<Renderer>().bounds.size.y / 2 * Vector3.up;
            } else
            {
                previewOffset = Vector3.zero;
            }
            Destroy(newItem.GetComponent<Rigidbody>());
            PreparePreview(newItem.transform);
            preview = newItem.transform;
        }
        MovePreview();
    }

    void UpdateCurrentText()
    {
        string s = "";
        if (editBlocks)
        {
            s += "Block: ";
            if (currentType > -1)
                s += types[currentType].ToString() + " on ";
            s += shapes[currentShape].ToString();
        } else
        {
            s += "Item: ";
            s += items[currentType].ToString();
        }
        s += " (" + currentRotation + "°)";
        currentText.text = s;
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
                if (editBlocks)
                {
                    preview.localPosition = (previewOffset + GetNewBlockPos(hit)) * Biome.BlockSize;
                    preview.GetComponent<BlockController>().SetRotation(Vector3.up * currentRotation);
                } else
                {
                    preview.position = hit.point + previewOffset;
                    preview.localRotation = Quaternion.Euler(0, currentRotation, 0);
                }
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
                + Vector3.up * Input.GetAxis("Vertical");
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                Camera.main.transform.localPosition += Camera.main.transform.forward * Input.mouseScrollDelta.y;
            }
            Camera.main.transform.LookAt(new Vector3(Biome.XSize, Biome.YSize, Biome.ZSize) * Biome.BlockSize * 0.5f);

            if (Input.GetKeyUp(KeyCode.F))
            {
                if (editBlocks)
                {
                    editBlocks = false;
                    currentType = 0;
                } else
                {
                    editBlocks = true;
                    currentType = -1;
                    currentShape = 0;
                }
                RefreshPreview();
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                currentRotation = editBlocks ? (currentRotation - currentRotation % 90 + 90) : (currentRotation - currentRotation % 45 + 45);
                if (currentRotation == 360)
                {
                    currentRotation = 0;
                }
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                currentRotation += (int)Input.mouseScrollDelta.y * 2;
                if (currentRotation < 0)
                    currentRotation = 359;
                if (currentRotation >= 360)
                    currentRotation = 0;
            }

            if (editBlocks)
            {
                if (Input.GetKeyUp(KeyCode.T))
                {
                    currentType--;
                    if (currentType == -2)
                    {
                        currentType = types.Count - 1;
                    }
                    RefreshPreview();
                }

                if (Input.GetKeyUp(KeyCode.Y))
                {
                    currentType++;
                    if (currentType == types.Count)
                    {
                        currentType = -1;
                    }
                    RefreshPreview();
                }

                if (Input.GetKeyUp(KeyCode.G))
                {
                    currentShape--;
                    if (currentShape == -1)
                    {
                        currentShape = shapes.Count - 1;
                    }
                    if (currentShape != 0)
                        currentType = -1;
                    RefreshPreview();
                }

                if (Input.GetKeyUp(KeyCode.H))
                {
                    currentShape++;
                    if (currentShape == shapes.Count)
                    {
                        currentShape = 0;
                    }
                    if (currentShape != 0)
                        currentType = -1;
                    RefreshPreview();
                }
            } else
            {
                if (Input.GetKeyUp(KeyCode.T))
                {
                    currentType--;
                    if (currentType == -1)
                    {
                        currentType = items.Count - 1;
                    }
                    RefreshPreview();
                }

                if (Input.GetKeyUp(KeyCode.Y))
                {
                    currentType++;
                    if (currentType == items.Count)
                    {
                        currentType = 0;
                    }
                    RefreshPreview();
                }
            }

            MovePreview();
            UpdateCurrentText();
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
                    if (editBlocks)
                    {
                        Vector3Int newPos = GetNewBlockPos(hit);
                        BlockController newBlock;
                        if (currentType == -1)
                        {
                            newBlock = biome.SetBlock(newPos, shapes[currentShape]);
                        }
                        else
                        {
                            newBlock = biome.SetBlock(newPos, shapes[currentShape], types[currentType]);
                        }

                        if (newBlock != null)
                        {
                            newBlock.SetRotation(Vector3.up * currentRotation);
                        }
                    } else
                    {
                        Instantiate(preview, biome.transform);
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
                if (editBlocks)
                {
                    BlockController block = hit.transform.GetComponent<BlockController>();
                    if (block != null)
                    {
                        BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
                        biome.RemoveBlock(hit.transform.name);
                    }
                } else
                {
                    ItemController item = hit.transform.GetComponent<ItemController>();
                    if (item != null)
                    {
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    public void New()
    {
        biomeManager.RemoveBiome(Vector3Int.zero);
        biomeManager.CreateBiome(Vector3Int.zero, BiomeType.BaseEditor);
    }

    public void LoadBiome()
    {
        biomeManager.RemoveBiome(Vector3Int.zero);
        CreatePremadeBiomeByName(inputField.text);
    }

    public BiomeController CreatePremadeBiomeByName(string name)
    {
        GameObject biome = new GameObject("0-0-0");
        biome.transform.parent = biomeManager.transform;
        BiomeController b = biome.AddComponent<BiomeController>();
        b.Manager = biomeManager;
        b.transform.localPosition = Vector3Int.zero;

        b.BiomeInstance = new BiomePremade(name);
        b.BiomeInstance.Generate(b);

        return b;
    }

    public void SaveBiome()
    {
        BiomeController bc = biomeManager.GetBiome(Vector3Int.zero);
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
