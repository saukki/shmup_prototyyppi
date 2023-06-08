using Aya.Events;
using UnityEngine;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class DamageEffect : MonoListener
    {
        [Range(0f, 2f)] public float DamageEffectDuration = 0.1f;
        public Color DamageColor = Color.white;
        private readonly int _shaderPropertyColor = Shader.PropertyToID("_BaseColor");
        private MeshRenderer _cachedMeshRenderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        [Space(10)] private Color _originalColor;

        private void Start() => ApplyDamageColor();

        private void ApplyDamageColor()
        {
            SaveOriginalColor();
            ChangeToDamgeColor();
        }

        private void SaveOriginalColor()
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            _cachedMeshRenderer ??= GetComponent<MeshRenderer>();
            _originalColor = _cachedMeshRenderer.material.GetColor(_shaderPropertyColor);
        }

        private void ChangeToDamgeColor()
        {
            _materialPropertyBlock.SetColor(_shaderPropertyColor, DamageColor);
            _cachedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
            this.AttachTimer(DamageEffectDuration, ChangeBackToOriginalColor);
        }

        private void ChangeBackToOriginalColor()
        {
            _materialPropertyBlock.SetColor(_shaderPropertyColor, _originalColor);
            _cachedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}