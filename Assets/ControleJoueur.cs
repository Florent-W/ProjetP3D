// GENERATED AUTOMATICALLY FROM 'Assets/ControleJoueur.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ControleJoueur : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControleJoueur()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControleJoueur"",
    ""maps"": [
        {
            ""name"": ""Joueur"",
            ""id"": ""600c5545-9c7f-44c7-9974-79e08e2ba07d"",
            ""actions"": [
                {
                    ""name"": ""OuvrirMenu"",
                    ""type"": ""Button"",
                    ""id"": ""f3404ffb-844a-421b-a24e-20b9af8c2017"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OuvrirCarte"",
                    ""type"": ""Button"",
                    ""id"": ""098d3141-d128-4332-8662-c41bcccf8dd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lb"",
                    ""type"": ""Button"",
                    ""id"": ""a2ea2bb9-1caf-4f83-808a-edd6a8e5e3a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LaserSuperman"",
                    ""type"": ""Button"",
                    ""id"": ""fa12b38b-dbc8-4c43-a557-0bd6a18b863a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action1"",
                    ""type"": ""Button"",
                    ""id"": ""f3dd1731-d3e7-4d49-972a-aa450a56a77a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BougerVersHautBas"",
                    ""type"": ""Button"",
                    ""id"": ""68afd7d0-6353-4c23-a264-237d5261bae1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""592f379a-a845-4af7-891a-1e2bb38361de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Acceleration"",
                    ""type"": ""Button"",
                    ""id"": ""978bcb45-15bd-42c0-95bc-e8b7a832a0cc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ControleCamera"",
                    ""type"": ""Button"",
                    ""id"": ""879b14bc-0df7-4203-bf5c-f49795b109b7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ControleCameraSourisY"",
                    ""type"": ""Button"",
                    ""id"": ""a626e048-311f-4aaa-9896-f8c282f86833"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ControleCameraSourisX"",
                    ""type"": ""Button"",
                    ""id"": ""78624091-effe-4f32-a3bd-aace9ae069f2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Saut"",
                    ""type"": ""Button"",
                    ""id"": ""332e46ec-ab28-4460-937d-32846aa58bc6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""Button"",
                    ""id"": ""9484b26b-8f90-4286-85e7-eb50da13420a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action2"",
                    ""type"": ""Button"",
                    ""id"": ""0f9e46c4-b000-4acd-b6fa-557b840849ae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InformationsF3"",
                    ""type"": ""Button"",
                    ""id"": ""92614f56-8738-4f78-beea-dfb3677a66b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""L2"",
                    ""type"": ""Button"",
                    ""id"": ""3125e06a-83af-4603-a90a-d50ee086ec02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Voler"",
                    ""type"": ""Button"",
                    ""id"": ""32c4f369-0288-4ad2-a503-14bd16b0bacc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolerMonter"",
                    ""type"": ""Button"",
                    ""id"": ""d8c95e96-f052-4659-8fa7-7943a5cebb13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""K"",
                    ""type"": ""Button"",
                    ""id"": ""8ab5c832-46a6-4123-8938-0c2edec97b7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""1"",
                    ""type"": ""Button"",
                    ""id"": ""aa1149bb-a91f-4553-aa24-ce39918b4c8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""3"",
                    ""type"": ""Button"",
                    ""id"": ""51df8bca-7f5b-4c03-9032-d12c80bb9088"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""5"",
                    ""type"": ""Button"",
                    ""id"": ""32b34df6-6756-4101-8755-6780e99fe7ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dancer"",
                    ""type"": ""Button"",
                    ""id"": ""610bacfa-3a60-47c2-973a-90badaf80d73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dancer9"",
                    ""type"": ""Button"",
                    ""id"": ""73c79bc0-f55c-4182-9d0d-c246760274eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dancer0"",
                    ""type"": ""Button"",
                    ""id"": ""bb00f996-f8b2-4731-b42c-00d419426946"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OuvrirMenuSelectionPersonnage"",
                    ""type"": ""Button"",
                    ""id"": ""04603365-cf40-4516-9507-b92e692cca15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OuvrirMenuMontures"",
                    ""type"": ""Button"",
                    ""id"": ""c7f9affe-7b11-4da5-89fb-14265ced08a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OuvrirMenuCommandes"",
                    ""type"": ""Button"",
                    ""id"": ""b5bfe837-9964-4e1f-88e2-d96719c7a74a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangerModeCombat"",
                    ""type"": ""Button"",
                    ""id"": ""595a8910-a296-4e55-a2bf-ac51cfe72e9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78e02267-3c66-4c89-ba31-b28bbb68473c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48bad3f2-ba60-46b1-ac9b-1f8ffecf2eb7"",
                    ""path"": ""<Keyboard>/#(I)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee844e0d-7532-447c-9207-5fea34dd05ff"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2ea10ae-e9f6-4fb6-98e6-9826e7689178"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff0b4e29-a54f-443d-8884-8a410bbfed1d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""51b5fe2f-ee62-4660-ac20-04b833f65baa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1b786f30-8c7b-4b71-b5d3-8043ecc64186"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d1dfd3b8-be81-401c-97da-573ece0e2531"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""28d05052-3e07-486f-9cf6-c529e4518651"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""de7569a2-3331-4d29-bb01-407012478718"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""219e26ac-f644-4e4a-83b9-0c6be43e896e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""267b18ed-47a5-40eb-a468-0f06fa6c0269"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9404292f-62bf-4ab6-a1b1-91862bda56e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8d7ea425-c959-4408-8bea-af92c4cf36fb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97cbce29-e039-4454-a8dd-6f3257422eb4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""BougerVersHautBas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c1cd6e17-7910-48ec-b060-dd93ba184866"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Acceleration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cde3869d-973c-41e3-9532-abb7690ab2f6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Acceleration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3d64bda0-0b06-4899-ba31-b0110f622af5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControleCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b9f8ba09-39e6-41bf-8f7d-ec74eb4459a4"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.49,y=0.48)"",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1a83f682-2723-451b-8816-c65551892c2c"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.5,y=0.49)"",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""872cfb4a-a720-4e2c-b9d4-8dc32802dd54"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.49,y=0.51)"",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""789a1b1b-6b05-4a46-9adc-7d0f39e18f92"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.49,y=0.5)"",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""694cd69a-3a7b-455b-ac35-94d0cc73bf4f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Saut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6594f547-fb9f-4c94-abf4-db96c14ff28a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Saut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""595ceae5-c009-427c-af39-2def412d9bc4"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d2e6710-affd-4b16-b3b9-9c48411fc204"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b3c7e27-226d-4291-b759-3b0f07c628bc"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.08)"",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCameraSourisY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d876c531-edf1-4d6e-8a7b-cf763fffee7e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.08)"",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""ControleCameraSourisX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9631ee5-1fd3-4101-b92d-e1ae9e1dc2d6"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94b5af7c-b1bf-4ba7-8af5-dd1e8943bcba"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64be5ac1-8a66-4d0d-a8b6-df2b63adda1c"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirCarte"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""862a01ba-ffdf-46e7-9b1a-03a58f6aa9d3"",
                    ""path"": ""<Keyboard>/#(M)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirCarte"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""af95dd5d-ca11-4fa9-b792-0b57793f354b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e4075dc4-fe90-46c0-990e-a624ec785bce"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d6e7b61c-0675-4ad7-8526-6c3668fb114e"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""65b672e8-ea6e-4c84-a14a-d1e7ef4af641"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""56b86f8d-244c-466c-bebd-9a5dcd33e172"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cef961fc-fc12-4271-8aec-991bbf718cac"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""Lb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7b514d2-7f99-48ea-8809-ca01db23979f"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Lb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b05df6c-4c19-4b4d-9bf5-afbe1185a044"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""InformationsF3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d29db08-1481-4961-aa8c-8f83fa142767"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""L2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2fc2d48-ca67-43eb-9715-a02a6ec2026f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Gamepad 1"",
                    ""action"": ""L2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3fdb25f-1b43-44b1-a71a-67afaa0f3f25"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Voler"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e57373a7-5d6d-4468-8ea5-3a9e83ba7555"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""VolerMonter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12a0180a-17a6-4ec4-b121-eff06e3ebae3"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""K"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbcb8c47-1737-461b-a585-f381256f6aaf"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88fe3d40-c7cc-448c-b500-1d4dd91c8d74"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b12f4a5-6021-498f-986f-71b8da473269"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""785061b1-4b89-427c-bcf4-eda22d39308a"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Dancer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a270ded7-6d31-4cd8-adfd-d0fa70c3b83d"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Dancer9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cac07022-09a6-4c6b-b44a-2ced3a472863"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""Dancer0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21d28d63-f8bd-4d2d-98f6-67f46a67aa91"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenuSelectionPersonnage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c96f6ca-5686-4723-956f-9ed06a53a2fb"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""ChangerModeCombat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6295c66d-0cdc-4b00-b240-57861b536339"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""LaserSuperman"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49d08168-343f-4f8d-9ef4-e1e1904cf4fa"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenuMontures"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""457d41e9-572f-46ee-9279-4e6c785a9a0a"",
                    ""path"": ""<Keyboard>/rightAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Keyboard and Gamepad 1"",
                    ""action"": ""OuvrirMenuCommandes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7007f111-205c-4b80-b63d-ca2cc5765d8c"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OuvrirMenuCommandes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Gamepad 1"",
            ""bindingGroup"": ""Keyboard and Gamepad 1"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Joueur
        m_Joueur = asset.FindActionMap("Joueur", throwIfNotFound: true);
        m_Joueur_OuvrirMenu = m_Joueur.FindAction("OuvrirMenu", throwIfNotFound: true);
        m_Joueur_OuvrirCarte = m_Joueur.FindAction("OuvrirCarte", throwIfNotFound: true);
        m_Joueur_Lb = m_Joueur.FindAction("Lb", throwIfNotFound: true);
        m_Joueur_LaserSuperman = m_Joueur.FindAction("LaserSuperman", throwIfNotFound: true);
        m_Joueur_Action1 = m_Joueur.FindAction("Action1", throwIfNotFound: true);
        m_Joueur_BougerVersHautBas = m_Joueur.FindAction("BougerVersHautBas", throwIfNotFound: true);
        m_Joueur_Zoom = m_Joueur.FindAction("Zoom", throwIfNotFound: true);
        m_Joueur_Acceleration = m_Joueur.FindAction("Acceleration", throwIfNotFound: true);
        m_Joueur_ControleCamera = m_Joueur.FindAction("ControleCamera", throwIfNotFound: true);
        m_Joueur_ControleCameraSourisY = m_Joueur.FindAction("ControleCameraSourisY", throwIfNotFound: true);
        m_Joueur_ControleCameraSourisX = m_Joueur.FindAction("ControleCameraSourisX", throwIfNotFound: true);
        m_Joueur_Saut = m_Joueur.FindAction("Saut", throwIfNotFound: true);
        m_Joueur_Strafe = m_Joueur.FindAction("Strafe", throwIfNotFound: true);
        m_Joueur_Action2 = m_Joueur.FindAction("Action2", throwIfNotFound: true);
        m_Joueur_InformationsF3 = m_Joueur.FindAction("InformationsF3", throwIfNotFound: true);
        m_Joueur_L2 = m_Joueur.FindAction("L2", throwIfNotFound: true);
        m_Joueur_Voler = m_Joueur.FindAction("Voler", throwIfNotFound: true);
        m_Joueur_VolerMonter = m_Joueur.FindAction("VolerMonter", throwIfNotFound: true);
        m_Joueur_K = m_Joueur.FindAction("K", throwIfNotFound: true);
        m_Joueur__1 = m_Joueur.FindAction("1", throwIfNotFound: true);
        m_Joueur__3 = m_Joueur.FindAction("3", throwIfNotFound: true);
        m_Joueur__5 = m_Joueur.FindAction("5", throwIfNotFound: true);
        m_Joueur_Dancer = m_Joueur.FindAction("Dancer", throwIfNotFound: true);
        m_Joueur_Dancer9 = m_Joueur.FindAction("Dancer9", throwIfNotFound: true);
        m_Joueur_Dancer0 = m_Joueur.FindAction("Dancer0", throwIfNotFound: true);
        m_Joueur_OuvrirMenuSelectionPersonnage = m_Joueur.FindAction("OuvrirMenuSelectionPersonnage", throwIfNotFound: true);
        m_Joueur_OuvrirMenuMontures = m_Joueur.FindAction("OuvrirMenuMontures", throwIfNotFound: true);
        m_Joueur_OuvrirMenuCommandes = m_Joueur.FindAction("OuvrirMenuCommandes", throwIfNotFound: true);
        m_Joueur_ChangerModeCombat = m_Joueur.FindAction("ChangerModeCombat", throwIfNotFound: true);
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

    // Joueur
    private readonly InputActionMap m_Joueur;
    private IJoueurActions m_JoueurActionsCallbackInterface;
    private readonly InputAction m_Joueur_OuvrirMenu;
    private readonly InputAction m_Joueur_OuvrirCarte;
    private readonly InputAction m_Joueur_Lb;
    private readonly InputAction m_Joueur_LaserSuperman;
    private readonly InputAction m_Joueur_Action1;
    private readonly InputAction m_Joueur_BougerVersHautBas;
    private readonly InputAction m_Joueur_Zoom;
    private readonly InputAction m_Joueur_Acceleration;
    private readonly InputAction m_Joueur_ControleCamera;
    private readonly InputAction m_Joueur_ControleCameraSourisY;
    private readonly InputAction m_Joueur_ControleCameraSourisX;
    private readonly InputAction m_Joueur_Saut;
    private readonly InputAction m_Joueur_Strafe;
    private readonly InputAction m_Joueur_Action2;
    private readonly InputAction m_Joueur_InformationsF3;
    private readonly InputAction m_Joueur_L2;
    private readonly InputAction m_Joueur_Voler;
    private readonly InputAction m_Joueur_VolerMonter;
    private readonly InputAction m_Joueur_K;
    private readonly InputAction m_Joueur__1;
    private readonly InputAction m_Joueur__3;
    private readonly InputAction m_Joueur__5;
    private readonly InputAction m_Joueur_Dancer;
    private readonly InputAction m_Joueur_Dancer9;
    private readonly InputAction m_Joueur_Dancer0;
    private readonly InputAction m_Joueur_OuvrirMenuSelectionPersonnage;
    private readonly InputAction m_Joueur_OuvrirMenuMontures;
    private readonly InputAction m_Joueur_OuvrirMenuCommandes;
    private readonly InputAction m_Joueur_ChangerModeCombat;
    public struct JoueurActions
    {
        private @ControleJoueur m_Wrapper;
        public JoueurActions(@ControleJoueur wrapper) { m_Wrapper = wrapper; }
        public InputAction @OuvrirMenu => m_Wrapper.m_Joueur_OuvrirMenu;
        public InputAction @OuvrirCarte => m_Wrapper.m_Joueur_OuvrirCarte;
        public InputAction @Lb => m_Wrapper.m_Joueur_Lb;
        public InputAction @LaserSuperman => m_Wrapper.m_Joueur_LaserSuperman;
        public InputAction @Action1 => m_Wrapper.m_Joueur_Action1;
        public InputAction @BougerVersHautBas => m_Wrapper.m_Joueur_BougerVersHautBas;
        public InputAction @Zoom => m_Wrapper.m_Joueur_Zoom;
        public InputAction @Acceleration => m_Wrapper.m_Joueur_Acceleration;
        public InputAction @ControleCamera => m_Wrapper.m_Joueur_ControleCamera;
        public InputAction @ControleCameraSourisY => m_Wrapper.m_Joueur_ControleCameraSourisY;
        public InputAction @ControleCameraSourisX => m_Wrapper.m_Joueur_ControleCameraSourisX;
        public InputAction @Saut => m_Wrapper.m_Joueur_Saut;
        public InputAction @Strafe => m_Wrapper.m_Joueur_Strafe;
        public InputAction @Action2 => m_Wrapper.m_Joueur_Action2;
        public InputAction @InformationsF3 => m_Wrapper.m_Joueur_InformationsF3;
        public InputAction @L2 => m_Wrapper.m_Joueur_L2;
        public InputAction @Voler => m_Wrapper.m_Joueur_Voler;
        public InputAction @VolerMonter => m_Wrapper.m_Joueur_VolerMonter;
        public InputAction @K => m_Wrapper.m_Joueur_K;
        public InputAction @_1 => m_Wrapper.m_Joueur__1;
        public InputAction @_3 => m_Wrapper.m_Joueur__3;
        public InputAction @_5 => m_Wrapper.m_Joueur__5;
        public InputAction @Dancer => m_Wrapper.m_Joueur_Dancer;
        public InputAction @Dancer9 => m_Wrapper.m_Joueur_Dancer9;
        public InputAction @Dancer0 => m_Wrapper.m_Joueur_Dancer0;
        public InputAction @OuvrirMenuSelectionPersonnage => m_Wrapper.m_Joueur_OuvrirMenuSelectionPersonnage;
        public InputAction @OuvrirMenuMontures => m_Wrapper.m_Joueur_OuvrirMenuMontures;
        public InputAction @OuvrirMenuCommandes => m_Wrapper.m_Joueur_OuvrirMenuCommandes;
        public InputAction @ChangerModeCombat => m_Wrapper.m_Joueur_ChangerModeCombat;
        public InputActionMap Get() { return m_Wrapper.m_Joueur; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JoueurActions set) { return set.Get(); }
        public void SetCallbacks(IJoueurActions instance)
        {
            if (m_Wrapper.m_JoueurActionsCallbackInterface != null)
            {
                @OuvrirMenu.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenu;
                @OuvrirMenu.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenu;
                @OuvrirMenu.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenu;
                @OuvrirCarte.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirCarte;
                @OuvrirCarte.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirCarte;
                @OuvrirCarte.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirCarte;
                @Lb.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLb;
                @Lb.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLb;
                @Lb.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLb;
                @LaserSuperman.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLaserSuperman;
                @LaserSuperman.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLaserSuperman;
                @LaserSuperman.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnLaserSuperman;
                @Action1.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction1;
                @Action1.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction1;
                @Action1.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction1;
                @BougerVersHautBas.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnBougerVersHautBas;
                @BougerVersHautBas.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnBougerVersHautBas;
                @BougerVersHautBas.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnBougerVersHautBas;
                @Zoom.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnZoom;
                @Acceleration.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAcceleration;
                @Acceleration.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAcceleration;
                @Acceleration.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAcceleration;
                @ControleCamera.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCamera;
                @ControleCamera.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCamera;
                @ControleCamera.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCamera;
                @ControleCameraSourisY.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisY;
                @ControleCameraSourisY.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisY;
                @ControleCameraSourisY.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisY;
                @ControleCameraSourisX.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisX;
                @ControleCameraSourisX.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisX;
                @ControleCameraSourisX.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnControleCameraSourisX;
                @Saut.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnSaut;
                @Saut.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnSaut;
                @Saut.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnSaut;
                @Strafe.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnStrafe;
                @Strafe.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnStrafe;
                @Strafe.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnStrafe;
                @Action2.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction2;
                @Action2.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction2;
                @Action2.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnAction2;
                @InformationsF3.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnInformationsF3;
                @InformationsF3.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnInformationsF3;
                @InformationsF3.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnInformationsF3;
                @L2.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnL2;
                @L2.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnL2;
                @L2.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnL2;
                @Voler.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVoler;
                @Voler.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVoler;
                @Voler.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVoler;
                @VolerMonter.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVolerMonter;
                @VolerMonter.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVolerMonter;
                @VolerMonter.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnVolerMonter;
                @K.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnK;
                @K.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnK;
                @K.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnK;
                @_1.started -= m_Wrapper.m_JoueurActionsCallbackInterface.On_1;
                @_1.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.On_1;
                @_1.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.On_1;
                @_3.started -= m_Wrapper.m_JoueurActionsCallbackInterface.On_3;
                @_3.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.On_3;
                @_3.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.On_3;
                @_5.started -= m_Wrapper.m_JoueurActionsCallbackInterface.On_5;
                @_5.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.On_5;
                @_5.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.On_5;
                @Dancer.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer;
                @Dancer.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer;
                @Dancer.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer;
                @Dancer9.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer9;
                @Dancer9.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer9;
                @Dancer9.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer9;
                @Dancer0.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer0;
                @Dancer0.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer0;
                @Dancer0.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnDancer0;
                @OuvrirMenuSelectionPersonnage.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuSelectionPersonnage.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuSelectionPersonnage.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuMontures.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuMontures;
                @OuvrirMenuMontures.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuMontures;
                @OuvrirMenuMontures.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuMontures;
                @OuvrirMenuCommandes.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuCommandes;
                @OuvrirMenuCommandes.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuCommandes;
                @OuvrirMenuCommandes.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnOuvrirMenuCommandes;
                @ChangerModeCombat.started -= m_Wrapper.m_JoueurActionsCallbackInterface.OnChangerModeCombat;
                @ChangerModeCombat.performed -= m_Wrapper.m_JoueurActionsCallbackInterface.OnChangerModeCombat;
                @ChangerModeCombat.canceled -= m_Wrapper.m_JoueurActionsCallbackInterface.OnChangerModeCombat;
            }
            m_Wrapper.m_JoueurActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OuvrirMenu.started += instance.OnOuvrirMenu;
                @OuvrirMenu.performed += instance.OnOuvrirMenu;
                @OuvrirMenu.canceled += instance.OnOuvrirMenu;
                @OuvrirCarte.started += instance.OnOuvrirCarte;
                @OuvrirCarte.performed += instance.OnOuvrirCarte;
                @OuvrirCarte.canceled += instance.OnOuvrirCarte;
                @Lb.started += instance.OnLb;
                @Lb.performed += instance.OnLb;
                @Lb.canceled += instance.OnLb;
                @LaserSuperman.started += instance.OnLaserSuperman;
                @LaserSuperman.performed += instance.OnLaserSuperman;
                @LaserSuperman.canceled += instance.OnLaserSuperman;
                @Action1.started += instance.OnAction1;
                @Action1.performed += instance.OnAction1;
                @Action1.canceled += instance.OnAction1;
                @BougerVersHautBas.started += instance.OnBougerVersHautBas;
                @BougerVersHautBas.performed += instance.OnBougerVersHautBas;
                @BougerVersHautBas.canceled += instance.OnBougerVersHautBas;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Acceleration.started += instance.OnAcceleration;
                @Acceleration.performed += instance.OnAcceleration;
                @Acceleration.canceled += instance.OnAcceleration;
                @ControleCamera.started += instance.OnControleCamera;
                @ControleCamera.performed += instance.OnControleCamera;
                @ControleCamera.canceled += instance.OnControleCamera;
                @ControleCameraSourisY.started += instance.OnControleCameraSourisY;
                @ControleCameraSourisY.performed += instance.OnControleCameraSourisY;
                @ControleCameraSourisY.canceled += instance.OnControleCameraSourisY;
                @ControleCameraSourisX.started += instance.OnControleCameraSourisX;
                @ControleCameraSourisX.performed += instance.OnControleCameraSourisX;
                @ControleCameraSourisX.canceled += instance.OnControleCameraSourisX;
                @Saut.started += instance.OnSaut;
                @Saut.performed += instance.OnSaut;
                @Saut.canceled += instance.OnSaut;
                @Strafe.started += instance.OnStrafe;
                @Strafe.performed += instance.OnStrafe;
                @Strafe.canceled += instance.OnStrafe;
                @Action2.started += instance.OnAction2;
                @Action2.performed += instance.OnAction2;
                @Action2.canceled += instance.OnAction2;
                @InformationsF3.started += instance.OnInformationsF3;
                @InformationsF3.performed += instance.OnInformationsF3;
                @InformationsF3.canceled += instance.OnInformationsF3;
                @L2.started += instance.OnL2;
                @L2.performed += instance.OnL2;
                @L2.canceled += instance.OnL2;
                @Voler.started += instance.OnVoler;
                @Voler.performed += instance.OnVoler;
                @Voler.canceled += instance.OnVoler;
                @VolerMonter.started += instance.OnVolerMonter;
                @VolerMonter.performed += instance.OnVolerMonter;
                @VolerMonter.canceled += instance.OnVolerMonter;
                @K.started += instance.OnK;
                @K.performed += instance.OnK;
                @K.canceled += instance.OnK;
                @_1.started += instance.On_1;
                @_1.performed += instance.On_1;
                @_1.canceled += instance.On_1;
                @_3.started += instance.On_3;
                @_3.performed += instance.On_3;
                @_3.canceled += instance.On_3;
                @_5.started += instance.On_5;
                @_5.performed += instance.On_5;
                @_5.canceled += instance.On_5;
                @Dancer.started += instance.OnDancer;
                @Dancer.performed += instance.OnDancer;
                @Dancer.canceled += instance.OnDancer;
                @Dancer9.started += instance.OnDancer9;
                @Dancer9.performed += instance.OnDancer9;
                @Dancer9.canceled += instance.OnDancer9;
                @Dancer0.started += instance.OnDancer0;
                @Dancer0.performed += instance.OnDancer0;
                @Dancer0.canceled += instance.OnDancer0;
                @OuvrirMenuSelectionPersonnage.started += instance.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuSelectionPersonnage.performed += instance.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuSelectionPersonnage.canceled += instance.OnOuvrirMenuSelectionPersonnage;
                @OuvrirMenuMontures.started += instance.OnOuvrirMenuMontures;
                @OuvrirMenuMontures.performed += instance.OnOuvrirMenuMontures;
                @OuvrirMenuMontures.canceled += instance.OnOuvrirMenuMontures;
                @OuvrirMenuCommandes.started += instance.OnOuvrirMenuCommandes;
                @OuvrirMenuCommandes.performed += instance.OnOuvrirMenuCommandes;
                @OuvrirMenuCommandes.canceled += instance.OnOuvrirMenuCommandes;
                @ChangerModeCombat.started += instance.OnChangerModeCombat;
                @ChangerModeCombat.performed += instance.OnChangerModeCombat;
                @ChangerModeCombat.canceled += instance.OnChangerModeCombat;
            }
        }
    }
    public JoueurActions @Joueur => new JoueurActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandGamepad1SchemeIndex = -1;
    public InputControlScheme KeyboardandGamepad1Scheme
    {
        get
        {
            if (m_KeyboardandGamepad1SchemeIndex == -1) m_KeyboardandGamepad1SchemeIndex = asset.FindControlSchemeIndex("Keyboard and Gamepad 1");
            return asset.controlSchemes[m_KeyboardandGamepad1SchemeIndex];
        }
    }
    public interface IJoueurActions
    {
        void OnOuvrirMenu(InputAction.CallbackContext context);
        void OnOuvrirCarte(InputAction.CallbackContext context);
        void OnLb(InputAction.CallbackContext context);
        void OnLaserSuperman(InputAction.CallbackContext context);
        void OnAction1(InputAction.CallbackContext context);
        void OnBougerVersHautBas(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnAcceleration(InputAction.CallbackContext context);
        void OnControleCamera(InputAction.CallbackContext context);
        void OnControleCameraSourisY(InputAction.CallbackContext context);
        void OnControleCameraSourisX(InputAction.CallbackContext context);
        void OnSaut(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
        void OnAction2(InputAction.CallbackContext context);
        void OnInformationsF3(InputAction.CallbackContext context);
        void OnL2(InputAction.CallbackContext context);
        void OnVoler(InputAction.CallbackContext context);
        void OnVolerMonter(InputAction.CallbackContext context);
        void OnK(InputAction.CallbackContext context);
        void On_1(InputAction.CallbackContext context);
        void On_3(InputAction.CallbackContext context);
        void On_5(InputAction.CallbackContext context);
        void OnDancer(InputAction.CallbackContext context);
        void OnDancer9(InputAction.CallbackContext context);
        void OnDancer0(InputAction.CallbackContext context);
        void OnOuvrirMenuSelectionPersonnage(InputAction.CallbackContext context);
        void OnOuvrirMenuMontures(InputAction.CallbackContext context);
        void OnOuvrirMenuCommandes(InputAction.CallbackContext context);
        void OnChangerModeCombat(InputAction.CallbackContext context);
    }
}
