using System.Collections;
using UnityEngine;

namespace Runtime.Infrastructure.Layer
{
    [ExecuteInEditMode]
    public class GridObject : MonoBehaviour
    {
        [SerializeField] private Vector2 objectSize;
        [SerializeField] private float padding;

        private Transform[] _children;

        private void OnTransformChildrenChanged()
        {
            var count = transform.childCount;
            _children = new Transform[count];

            for (int i = 0; i < count; i++)
                _children[i] = transform.GetChild(i);

            SetDirty();
        }

        private void OnValidate() =>
            SetDirty();


        private void OnDrawGizmos()
        {
            if (_children == null || _children.Length == 0)
                return;

            var total = _children.Length;

            Vector3 position = GetPosition();
            Gizmos.color = Color.white;
            for (int i = 0; i < total; i++)
            {
                Gizmos.DrawWireCube(position, objectSize);
                position.x += padding + objectSize.x;
            }
        }

        private void SetDirty()
        {
            if(_children == null || _children.Length == 0)
                return;
            
#if UNITY_EDITOR
            if (!Application.isPlaying)
                StartCoroutine(DelayedUpdate());
            else
#endif
                UpdateChildren();
        }

        private IEnumerator DelayedUpdate()
        {
            yield return null;
            UpdateChildren();
        }

        private void UpdateChildren()
        {
            var p = GetPosition();
            foreach (var child in _children)
            {
                child.localPosition = p;
                p += Offset();
            }
        }

        private Vector3 GetPosition() =>
            Vector3.left * HalfLength() + Vector3.right * HalfSize();

        private Vector3 Offset() =>
            Vector3.right * (objectSize.x + padding);

        private float Size() =>
            objectSize.x;

        private float HalfSize() =>
            HalfOf(Size());

        private float GetTotalLength() =>
            objectSize.x * _children.Length + padding * (_children.Length - 1);

        private float HalfLength() =>
            HalfOf(GetTotalLength());

        private static float HalfOf(float h) =>
            h * .5f;
    }
}