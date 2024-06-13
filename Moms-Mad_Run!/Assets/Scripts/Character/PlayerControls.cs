//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Character/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MomMovementControl"",
            ""id"": ""90b174aa-c8a2-4182-bcd1-13f427e27137"",
            ""actions"": [
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""22aee334-5614-43f8-abdf-31b4fcf6dc69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""acb811b1-fdb3-4fe7-9aa3-ab4d7c6f12b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a610e7dd-7057-43d6-ad02-de4ee5168ea7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""54bde3b4-63cb-4b51-a6d9-4358dce7b985"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""18fbea93-3ec3-4181-95b3-0abbc4528c67"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""48b38e99-fa6d-43e9-9aa0-34de2e4f3583"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b7d7168-1b9c-4366-9644-93d85765189b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ac27f0d3-925d-46a4-a9fe-4b2af11ba406"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""58af44e3-e166-42d8-8583-56a7e7988c96"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bece9a20-7557-4b3c-aed4-ad57b5ac2e89"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""04c3f6b5-20a0-4e1d-8755-bbbbebd86fc0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""be8ddaf9-a86e-481f-91dc-8bddf3db18fc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7720976f-9c12-44ca-bd84-5884399de3b6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ChildMovementControl"",
            ""id"": ""660c2d8c-bc81-4a11-9207-bba66d8ff74b"",
            ""actions"": [
                {
                    ""name"": ""TakeItem"",
                    ""type"": ""Button"",
                    ""id"": ""35936789-9f05-4a74-812d-7c06d6ef34f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""07e5af29-5091-44a5-aa5e-9c349fd8cc2d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""75e8c8f1-c2ef-456e-962a-b0de1f473247"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""be6236c5-4aa8-4a8e-8e8e-8558f82c3ee2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e4b6640f-4693-4821-9e75-e22321cde1ae"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00a76793-6e03-40fd-9ec4-b82d0b1e73a6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b82551e8-507b-4213-b67b-efa1290dbeca"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b346492-a1d7-4579-b005-eb85ab9f1ebb"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""keyboard"",
                    ""id"": ""03d09262-528f-4b7e-9e1d-796845230e8a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""32bcf365-02c1-42a2-82f9-60001462f32c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4d3e6fa9-afbd-467c-a469-705bc2267dc6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e23c5180-e533-411c-ad43-32b436f3f6c8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fc8531e8-2683-48fd-81b5-7a63726520c8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""aca8a818-e823-4167-ac5d-9b4e6a400858"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""d44e9fa9-7b10-4f14-a1ef-1db25ff26d08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5466315c-d6ed-4fcd-91b6-7b9f998a8ad9"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MomMovementControl
        m_MomMovementControl = asset.FindActionMap("MomMovementControl", throwIfNotFound: true);
        m_MomMovementControl_Throw = m_MomMovementControl.FindAction("Throw", throwIfNotFound: true);
        m_MomMovementControl_Move = m_MomMovementControl.FindAction("Move", throwIfNotFound: true);
        // ChildMovementControl
        m_ChildMovementControl = asset.FindActionMap("ChildMovementControl", throwIfNotFound: true);
        m_ChildMovementControl_TakeItem = m_ChildMovementControl.FindAction("TakeItem", throwIfNotFound: true);
        m_ChildMovementControl_Move = m_ChildMovementControl.FindAction("Move", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MomMovementControl
    private readonly InputActionMap m_MomMovementControl;
    private List<IMomMovementControlActions> m_MomMovementControlActionsCallbackInterfaces = new List<IMomMovementControlActions>();
    private readonly InputAction m_MomMovementControl_Throw;
    private readonly InputAction m_MomMovementControl_Move;
    public struct MomMovementControlActions
    {
        private @PlayerControls m_Wrapper;
        public MomMovementControlActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Throw => m_Wrapper.m_MomMovementControl_Throw;
        public InputAction @Move => m_Wrapper.m_MomMovementControl_Move;
        public InputActionMap Get() { return m_Wrapper.m_MomMovementControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MomMovementControlActions set) { return set.Get(); }
        public void AddCallbacks(IMomMovementControlActions instance)
        {
            if (instance == null || m_Wrapper.m_MomMovementControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MomMovementControlActionsCallbackInterfaces.Add(instance);
            @Throw.started += instance.OnThrow;
            @Throw.performed += instance.OnThrow;
            @Throw.canceled += instance.OnThrow;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IMomMovementControlActions instance)
        {
            @Throw.started -= instance.OnThrow;
            @Throw.performed -= instance.OnThrow;
            @Throw.canceled -= instance.OnThrow;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IMomMovementControlActions instance)
        {
            if (m_Wrapper.m_MomMovementControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMomMovementControlActions instance)
        {
            foreach (var item in m_Wrapper.m_MomMovementControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MomMovementControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MomMovementControlActions @MomMovementControl => new MomMovementControlActions(this);

    // ChildMovementControl
    private readonly InputActionMap m_ChildMovementControl;
    private List<IChildMovementControlActions> m_ChildMovementControlActionsCallbackInterfaces = new List<IChildMovementControlActions>();
    private readonly InputAction m_ChildMovementControl_TakeItem;
    private readonly InputAction m_ChildMovementControl_Move;
    public struct ChildMovementControlActions
    {
        private @PlayerControls m_Wrapper;
        public ChildMovementControlActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TakeItem => m_Wrapper.m_ChildMovementControl_TakeItem;
        public InputAction @Move => m_Wrapper.m_ChildMovementControl_Move;
        public InputActionMap Get() { return m_Wrapper.m_ChildMovementControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChildMovementControlActions set) { return set.Get(); }
        public void AddCallbacks(IChildMovementControlActions instance)
        {
            if (instance == null || m_Wrapper.m_ChildMovementControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ChildMovementControlActionsCallbackInterfaces.Add(instance);
            @TakeItem.started += instance.OnTakeItem;
            @TakeItem.performed += instance.OnTakeItem;
            @TakeItem.canceled += instance.OnTakeItem;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IChildMovementControlActions instance)
        {
            @TakeItem.started -= instance.OnTakeItem;
            @TakeItem.performed -= instance.OnTakeItem;
            @TakeItem.canceled -= instance.OnTakeItem;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IChildMovementControlActions instance)
        {
            if (m_Wrapper.m_ChildMovementControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IChildMovementControlActions instance)
        {
            foreach (var item in m_Wrapper.m_ChildMovementControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ChildMovementControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ChildMovementControlActions @ChildMovementControl => new ChildMovementControlActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Newaction;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IMomMovementControlActions
    {
        void OnThrow(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IChildMovementControlActions
    {
        void OnTakeItem(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
