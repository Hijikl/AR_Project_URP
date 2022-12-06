using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDissolver : MonoBehaviour
{
    [SerializeField]
    List<Material> _materials;


    [Range(0f, 1f)]
    public float _dissolveRate;

    int _dissolveRateID;

    // Start is called before the first frame update
    void Start()
    {
       var renderers= GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            var material = renderer.material;
            _materials.Add(material);

        }

        _dissolveRateID= Shader.PropertyToID("_DissolveRate");
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var mat in _materials)
        {
          
            mat.SetFloat(_dissolveRateID,_dissolveRate);
        }
    }

    private void OnDestroy()
    {
        foreach (var mat in _materials)
        {
            Destroy(mat);
        }
    }
}
