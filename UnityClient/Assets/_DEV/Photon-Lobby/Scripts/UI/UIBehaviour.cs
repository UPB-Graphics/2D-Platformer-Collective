using System;
using System.Collections.Generic;
using System.Linq;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIBehaviour : MonoBehaviour
    {
        [SerializeField] private UISystem uiSystem;

        [SerializeField] private List<UIState> activeStates;
        [SerializeField] private bool _isOpen;

        public UIState currentActiveState;

        private List<UIBehaviour_Component> comps = new List<UIBehaviour_Component>();
        

        private GameObject _content;
        private Vector3 _activePoint;
        private Vector3 _inactivePoint;
        

        [HideInInspector] public Action onOpen;
        [HideInInspector] public Action onClose;

        private Camera _thisCamera;
        private Camera _ThisCamera => (_thisCamera == null) ? _thisCamera = Camera.main : _thisCamera;

        private RectTransform _canvas;
        private RectTransform _Canvas => (_canvas == null) ? _canvas = GetComponent<RectTransform>() : _canvas;

       

    #if UNITY_EDITOR
        private void OnValidate() { AssignReferences(); }
    #endif
        private void Start()
        {
            AssignReferences();
            if(!_isOpen)
                InstantHide();
        }

        private void AssignReferences()
        {
            _content = transform.Find("Content").gameObject;
            _activePoint = Vector3.zero;
            _inactivePoint = GetInactivePoint();

            if (!_isOpen) InstantHide();
        }

        private void OnEnable()
        {
            comps = GetComponentsInChildren<UIBehaviour_Component>(true).ToList();
            uiSystem.onStateChanged += UISystem_OnStateChanged;
        }

        private void OnDisable()
        {
            uiSystem.onStateChanged -= UISystem_OnStateChanged;
        }

        private void UISystem_OnStateChanged(UIState state)
        {
                
            var shouldOpen = activeStates.Contains(state);
            if (shouldOpen)
            {
                if (!_isOpen)
                    ActivateContent();
            }
            else if (_isOpen) DeactivateContent();

            if (!_isOpen && shouldOpen)
            {
                OnOpened();
                currentActiveState = state;
            }
            else if (_isOpen && !shouldOpen)
            {
                OnClosed();
                currentActiveState = null;
            }
        }

        private void ActivateContent()
        {
            _content.transform.localPosition = _activePoint;
            comps.ForEach(x => {x.Activated();});
        }

        private void DeactivateContent()
        {
            _content.transform.localPosition = _inactivePoint;
            foreach (var comp in comps) comp.Deactivated();
        }

        private void OnOpened()
        {
            _isOpen = true;
            onOpen.Fire();
        }

        private void OnClosed()
        {
            _isOpen = false;
            onClose.Fire();
        }

        Vector3 GetInactivePoint()
        {
            return new Vector3(-ViewportSize().x * 1.5f, _activePoint.y, _activePoint.z);
        }

        Vector2 ViewportSize()
        {
            return new Vector2(_Canvas.rect.width + 1, _Canvas.rect.height + 1);
        }

        private void InstantShow() { _content.transform.localPosition = _activePoint; ; }
        private void InstantHide() { _content.transform.localPosition = _inactivePoint; ; }
    }
}