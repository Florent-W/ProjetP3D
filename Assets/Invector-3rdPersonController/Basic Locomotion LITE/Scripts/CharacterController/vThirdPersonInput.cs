using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Invector.CharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region variables

        [Header("Default Inputs")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Settings")]
        public string rotateCameraXInput ="Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        public GameObject canvasGameObject;

        public ControleJoueur controls;

        [SerializeField]
        private ProjetP3DScene1.main Main;
        [SerializeField]
        private AudioSource musiqueGameObject;

        [SerializeField]
        private MiseAJourInformationsF3 miseAJourInformationsF3Script;

        protected vThirdPersonCamera tpCamera;                // acess camera info        
        [HideInInspector]
        public string customCameraState;                    // generic string to change the CameraState        
        [HideInInspector]
        public string customlookAtPoint;                    // generic string to change the CameraPoint of the Fixed Point Mode        
        [HideInInspector]
        public bool changeCameraState;                      // generic bool to change the CameraState        
        [HideInInspector]
        public bool smoothCameraState;                      // generic bool to know if the state will change with or without lerp  
        [HideInInspector]
        public bool keepDirection;                          // keep the current direction in case you change the cameraState

        protected vThirdPersonController cc;                // access the ThirdPersonController component                

        #endregion

        private void Awake()
        {
            controls = new ControleJoueur();
           // controls.devices = new InputDevice[] { Keyboard.current, Mouse.current };

          //  controls.Joueur.BougerVersHautBas.performed += ctx => b();
        }

        protected virtual void Start()
        {
            CharacterInit();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        protected virtual void CharacterInit()
        {
            cc = GetComponent<vThirdPersonController>();
            if (cc != null)
                cc.Init();

            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            // if (tpCamera) tpCamera.SetMainTarget(this.transform);

            if (Main != null)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        protected virtual void LateUpdate()
        {
            if (cc == null) return;             // returns if didn't find the controller		    
            InputHandle();                      // update input methods
            UpdateCameraStates();               // update camera states
        }

        protected virtual void FixedUpdate()
        {
            cc.AirControl();
            cc.FlyAirControl();
            CameraInput();
        }

        protected virtual void Update()
        {
            cc.UpdateMotor();                   // call ThirdPersonMotor methods               
            cc.UpdateAnimator();                // call ThirdPersonAnimator methods		             
        }

        void b ()
        {
            //  float moveAxis = controls.Joueur.BougerVersHautBas.ReadValue<float>();
        }

        protected virtual void InputHandle()
        {
            ExitGameInput();
            CameraInput();

            if (Main != null)
            {
                if (!cc.lockMovement && Main.mapGameObject.activeSelf == false)
                {
                    MoveCharacter();
                    SprintInput();
                    StrafeInput();
                    JumpInput();
                    FlyInput();
                }
            }
            else
            {
                if (!cc.lockMovement)
                {
                    MoveCharacter();
                    SprintInput();
                    StrafeInput();
                    JumpInput();
                }
            }
        }

        #region Basic Locomotion Inputs      

        private void OnOuvrirMenu()
        {
            if (this.GetComponent<PlayerInput>().actions["OuvrirMenu"].ReadValue<float>() == 1)
            {
                if (this.gameObject.name == "vThirdPersonCamera")
                {
                    canvasGameObject.transform.GetChild(0).gameObject.GetComponent<MenuStartScript>().OuvrirMenu(0);
                }
                else if (this.gameObject.name == "vThirdPersonCameraJoueur2")
                {
                    canvasGameObject.transform.GetChild(0).gameObject.GetComponent<MenuStartScript>().OuvrirMenu(1);
                }
            }
        }

        private void OnOuvrirCarte()
        {
            if (this.GetComponent<PlayerInput>().actions["OuvrirCarte"].ReadValue<float>() == 1)
            {
                if (this.gameObject.name == "vThirdPersonCamera")
                {
                    Main.btn_carte(0);
                }
                else if (this.gameObject.name == "vThirdPersonCameraJoueur2")
                {
                    Main.btn_carte(1);
                }
            }
        }

        private void OnOuvrirMenuMontures()
        {
            if (this.GetComponent<PlayerInput>().actions["OuvrirMenuMontures"].ReadValue<float>() == 1)
            {
                if (this.gameObject.name == "vThirdPersonCamera")
                {
                    GameObject menuMontures = canvasGameObject.transform.GetChild(13).gameObject;
                    if (menuMontures.activeSelf == false)
                    {
                        menuMontures.SetActive(true);
                    }
                    else
                    {
                        menuMontures.SetActive(false);
                    }
                }
                /*
                else if (this.gameObject.name == "vThirdPersonCameraJoueur2")
                {
                    canvasGameObject.transform.GetChild(0).gameObject.GetComponent<MenuStartScript>().OuvrirMenu(1);
                } */
            }
        }

        private void OnOuvrirMenuCommandes()
        {
            if (this.GetComponent<PlayerInput>().actions["OuvrirMenuCommandes"].ReadValue<float>() == 1)
            {
                Main.btn_ouvrir_menu_commandes();
            }
        }

        // Ouvre les informations F3 (Coordonnnés)
        private void OnInformationsF3()
        {
            if (this.GetComponent<PlayerInput>().actions["InformationsF3"].ReadValue<float>() == 1)
            {
                if (this.gameObject.name == "vThirdPersonCamera")
                {
                    if (canvasGameObject.transform.GetChild(11).gameObject.activeSelf == false) // On active si le menu n'est pas activé
                    {
                        canvasGameObject.transform.GetChild(11).gameObject.SetActive(true);
                    }
                    else if (canvasGameObject.transform.GetChild(11).gameObject.activeSelf == true) // Sinon on l'enlève
                    {
                        canvasGameObject.transform.GetChild(11).gameObject.SetActive(false);
                    }
                }
                else if (this.gameObject.name == "vThirdPersonCameraJoueur2")
                {
                    if (canvasGameObject.transform.GetChild(11).gameObject.activeSelf == false) // On active si le menu n'est pas activé
                    {
                        canvasGameObject.transform.GetChild(11).gameObject.SetActive(true);
                    }
                    else if (canvasGameObject.transform.GetChild(11).gameObject.activeSelf == true) // Sinon on l'enlève
                    {
                        canvasGameObject.transform.GetChild(11).gameObject.SetActive(false);
                    }
                }
            }
        }

        // Pour Flash, va ralentir le jeu pour dire qu'il va plus vite
        private void OnL2()
        {
            if (DonneesChargement.nomGameObjectModel == "Flash")
            {
                if (this.GetComponent<PlayerInput>().actions["L2"].ReadValue<float>() == 1 && Time.timeScale == 1)
                {
                    if (this.gameObject.name == "vThirdPersonCamera" || this.gameObject.name == "vThirdPersonCameraJoueur2")
                    {
                        Time.timeScale = 0.3f;
                      //  musiqueGameObject.pitch = 0.62f;
                        musiqueGameObject.clip = Resources.Load<AudioClip>("Models/Personnages/Flash/At the Speed of Force"); // On charge la musique
                        musiqueGameObject.Play();
                    }
                }
                else if (this.GetComponent<PlayerInput>().actions["L2"].ReadValue<float>() == 1 && Time.timeScale == 0.3f) // On remet le temps normal ainsi que la musique
                {
                    if (this.gameObject.name == "vThirdPersonCamera" || this.gameObject.name == "vThirdPersonCameraJoueur2")
                    {
                        Time.timeScale = 1f;
                        // musiqueGameObject.pitch = 1f;
                        musiqueGameObject.clip = Resources.Load<AudioClip>("Musics/Ville/the-flash-theme-epic-version-dc-fandome-tribute");
                        musiqueGameObject.Play();
                    }
                }
            }
        }

        // On change le mode de combat
        private void OnChangerModeCombat()
        {
            if (this.GetComponent<PlayerInput>().actions["ChangerModeCombat"].ReadValue<float>() == 1)
            {
                string modeCombat = Main.modeCombat;

                if (modeCombat == "Tour par tour")
                { // On vérifie le mode
                    Main.modeCombat = "Temps réel";
                    miseAJourInformationsF3Script.miseAJourModeCombat();
                }
                else if (modeCombat == "Temps réel")
                {
                    Main.modeCombat = "Tour par tour";
                    miseAJourInformationsF3Script.miseAJourModeCombat();
                }
            }
        }

        // On ouvre le menu de sélection des personnages
        void OnOuvrirMenuSelectionPersonnage()
        {
            if (this.GetComponent<PlayerInput>().actions["OuvrirMenuSelectionPersonnage"].ReadValue<float>() == 1)
            {
                Main.btn_ouvrir_menu_personnages_selection();
            }
        }

        public void MoveCharacter()
        {
            cc.input = this.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
            //  cc.input.x = Input.GetAxis(horizontalInput);
            //  cc.input.y = Input.GetAxis(verticallInput);
        }

        protected virtual void StrafeInput()
        {
            if (this.GetComponent<PlayerInput>().actions["Strafe"].ReadValue<float>() > 0)
          //  if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if(this.GetComponent<PlayerInput>().actions["Acceleration"].ReadValue<float>() > 0)
           // if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else
           // else if(Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        protected virtual void JumpInput()
        {
            if(this.GetComponent<PlayerInput>().actions["Saut"].ReadValue<float>() > 0)
           // if (Input.GetKeyDown(jumpInput))
                cc.Jump();
        }

        protected virtual void FlyInput()
        {
            float boutonVolerMonter = this.GetComponent<PlayerInput>().actions["K"].ReadValue<float>(); // Appuie sur la touche pour s'envoler plus haut
            float boutonChangerDirection1 = this.GetComponent<PlayerInput>().actions["1"].ReadValue<float>();
            float boutonChangerDirection2 = this.GetComponent<PlayerInput>().actions["3"].ReadValue<float>();
            float boutonChangerDirection3 = this.GetComponent<PlayerInput>().actions["5"].ReadValue<float>();

            if (boutonVolerMonter == 1)
            {
                cc.Fly();
            }
            if (boutonChangerDirection1 == 1)
            {
                cc.ChangeFlyDirection(1);
            }
            else if (boutonChangerDirection2 == 1)
            {
                cc.ChangeFlyDirection(-1);
            }
            else
            {
                cc.ChangeFlyDirection(0);
            }  
        }
       
        protected virtual void ExitGameInput()
        {
            // just a example to quit the application 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Cursor.visible)
                    Cursor.visible = true;
                else { }
                  // Application.Quit();
            }
        }

        #endregion

        #region Camera Methods

        protected virtual void CameraInput()
        {
            if (tpCamera == null)
                return;

            Vector2 MouvementCamera = this.GetComponent<PlayerInput>().actions["ControleCamera"].ReadValue<Vector2>();     

            // Mouvement Camera manette
            float y = MouvementCamera.y;
            float x = MouvementCamera.x;

            // Mouvement Camera souris
            float MouvementCameraSourisY = this.GetComponent<PlayerInput>().actions["ControleCameraSourisY"].ReadValue<float>();
            float MouvementCameraSourisX = this.GetComponent<PlayerInput>().actions["ControleCameraSourisX"].ReadValue<float>();

            //  var Y = Input.GetAxis(rotateCameraYInput);
            //  var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(x, y);
            tpCamera.RotateCamera(MouvementCameraSourisX, MouvementCameraSourisY);

            // tranform Character direction from camera if not KeepDirection
            if (!keepDirection)
                cc.UpdateTargetDirection(tpCamera != null ? tpCamera.transform : null);
            // rotate the character with the camera while strafing        
            RotateWithCamera(tpCamera != null ? tpCamera.transform : null);            
        }

        protected virtual void UpdateCameraStates()
        {
            // CAMERA STATE - you can change the CameraState here, the bool means if you want lerp of not, make sure to use the same CameraState String that you named on TPCameraListData
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                   // tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }            
        }

        protected virtual void RotateWithCamera(Transform cameraTransform)
        {
            if (cc.isStrafing && !cc.lockMovement && !cc.lockMovement)
            {                
                cc.RotateWithAnotherTransform(cameraTransform);                
            }
        }

        #endregion     
    }
}