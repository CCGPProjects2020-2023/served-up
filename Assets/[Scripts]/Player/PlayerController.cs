using FMOD.Studio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    PlayerInputActions input;
    RecipeSystem recipeSystem;
    Vector2 move = Vector2.zero;
    public float speed = 10;
    public float rotationSpeed = 600;
    Rigidbody rb;
    public LayerMask placeableLayer;
    public GameObject hitObject;
    public GameObject heldItem;
    public GameObject heldItemPos;
    PlayerLook playerLook;
    public Transform orientation;
    private Vector3 moveDir;

    private EventInstance playerFootsteps;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = GetComponent<PlayerLook>().camera;
        playerLook = GetComponent<PlayerLook>();
        rb = GetComponent<Rigidbody>();
        input = new PlayerInputActions();
        input.Player.Enable();
        input.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => move = Vector2.zero;
        input.Player.Interact.performed += ctx => Interact();
        input.Player.Pickup.performed += ctx => Pickup();
    }

    void Start()
    {
        recipeSystem = RecipeSystem.Instance;

        playerFootsteps = AudioManager.Instance.CreateInstance(FMODEvents.Instance.playerFootsteps);
    }

    // Update is called once per frame
    void Update()
    {
        ShootRaycast();
        playerLook.Look(input.Player.Look.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        Move();
        SpeedControl();
        UpdateSound();
    }

    private void ShootRaycast()
    {
        RaycastHit objectHit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        //Vector3 rayPos = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        Debug.DrawRay(ray.origin, ray.direction * 2, Color.green);
        if (Physics.Raycast(ray, out objectHit, 2, placeableLayer))
        {
            if (objectHit.collider.gameObject != hitObject)
            {
                if (objectHit.collider.gameObject.GetComponent<ItemSOHolder>())
                {
                    hitObject = objectHit.collider.gameObject.GetComponentInParent<Placeable>().gameObject;
                }
                else if (objectHit.collider.gameObject.GetComponent<ModificationSOHolder>())
                {
                    hitObject = objectHit.collider.gameObject.GetComponentInParent<Placeable>().gameObject;
                }
                else
                {
                    hitObject = objectHit.collider.gameObject;
                }
                SetSelectedObject(hitObject);
            }
        }
        else
        {
            hitObject = null;
            SetSelectedObject(hitObject);
        }
    }

    private void Pickup()
    {
        if (hitObject)
        {
            Placeable placeable;
            if (hitObject.GetComponent<CupHolder>())
            {
                CupHolder cupHolder = hitObject.GetComponent<CupHolder>();

                if (heldItem && cupHolder.maxCups > cupHolder.currentCups)
                {

                    if (heldItem.GetComponent<ItemSOHolder>().itemSO == cupHolder.emptyCup.GetComponent<ItemSOHolder>().itemSO)
                    {

                        cupHolder.currentCups++;
                        cupHolder.UpdateCups();
                        Destroy(heldItem);
                        heldItem = null;
                    }

                }
                else if (0 < cupHolder.currentCups && !heldItem)
                {
                    GameObject emptyCup = Instantiate(cupHolder.emptyCup, heldItemPos.transform);
                    emptyCup.layer = 0;
                    heldItem = emptyCup;

                    cupHolder.currentCups--;
                    cupHolder.UpdateCups();

                }
            }
            //WASHING CUPS
            else if (hitObject.GetComponent<ItemConverter>())
            {
                ItemConverter itemConverter = hitObject.GetComponent<ItemConverter>();

                if (heldItem && heldItem.GetComponent<ItemSOHolder>().itemSO == itemConverter.inputItem.GetComponent<ItemSOHolder>().itemSO)
                {
                    Destroy(heldItem);
                    heldItem = null;
                    GameObject outputItem = Instantiate(itemConverter.outputItem, heldItemPos.transform);
                    outputItem.layer = 0;
                    heldItem = outputItem;



                }
            }

            else if (hitObject.GetComponent<ItemConverterAdvanced>() && heldItem)
            {
                ItemConverterAdvanced itemConverterAdvanced = hitObject.GetComponent<ItemConverterAdvanced>();
                ItemSO heldItemSO = heldItem.GetComponent<ItemSOHolder>().itemSO;
                ItemSO converterItemSO = itemConverterAdvanced.itemSO;
                ItemSO outputItem = recipeSystem.GetRecipeOutput(converterItemSO, heldItemSO);

                Debug.Log(outputItem.name);
                if (heldItem)
                {
                    Debug.Log(outputItem.name);
                    Destroy(heldItem);
                    heldItem = null;
                    GameObject outputItem1 = Instantiate(outputItem.prefab, heldItemPos.transform);
                    outputItem1.layer = 0;
                    heldItem = outputItem1;
                }
            }

            else if (hitObject.GetComponent<PlaceableButton>())
            {
                ModificationManager.Instance.LockInModifications();
            }
            else
            {
                if (hitObject.GetComponent<ItemSOHolder>())
                {
                    placeable = hitObject.GetComponentInParent<Placeable>();
                }
                else if (hitObject.GetComponent<ModificationSOHolder>())
                {
                    placeable = hitObject.GetComponentInParent<Placeable>();
                }
                else
                {
                    placeable = hitObject.GetComponent<Placeable>();
                }

                //item placed on placeable
                if (!placeable.item && heldItem)
                {
                    placeable.item = heldItem;
                    placeable.item.transform.SetParent(placeable.itemPos.transform, true);
                    placeable.item.transform.localPosition = Vector3.zero;
                    placeable.item.transform.localRotation = Quaternion.identity;
                    placeable.item.layer = 3;
                    heldItem = null;
                }
                //item picked up from placeable
                else if (placeable.item && !heldItem)
                {
                    placeable.item.transform.SetParent(heldItemPos.transform);
                    heldItem = placeable.item;
                    heldItem.transform.localPosition = Vector3.zero;
                    if (heldItem.transform.childCount > 0 && heldItem.layer == 3)
                    {
                        heldItem.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    heldItem.layer = 0;
                    placeable.item = null;
                }
                else if (placeable.item && heldItem) // item is on placeable and player has item already (checks if they can be combined)
                {
                    ItemSO placeableItemSO = placeable.item.GetComponent<ItemSOHolder>().itemSO;
                    ItemSO heldItemSO = heldItem.GetComponent<ItemSOHolder>().itemSO;
                    ItemSO outputItem = recipeSystem.GetRecipeOutput(placeableItemSO, heldItemSO);
                    if (outputItem != null)
                    {
                        if (placeableItemSO.isDestructable && !heldItemSO.isDestructable)
                        {
                            Destroy(placeable.item);
                            GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0, 0, 0), outputItem.prefab.transform.rotation);
                            newItem.transform.SetParent(placeable.itemPos.transform);
                            placeable.item = newItem;
                            placeable.item.layer = 3;
                            placeable.item.transform.localPosition = Vector3.zero;
                        }
                        else if (!placeableItemSO.isDestructable && heldItemSO.isDestructable)
                        {
                            Destroy(heldItem);
                            heldItem = null;

                            GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0, 0, 0), outputItem.prefab.transform.rotation);
                            newItem.transform.SetParent(heldItemPos.transform);
                            heldItem = newItem;
                            heldItem.layer = 0;
                            heldItem.transform.localPosition = Vector3.zero;
                        }
                        else
                        {
                            Destroy(placeable.item);
                            GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0, 0, 0), outputItem.prefab.transform.rotation);
                            newItem.transform.SetParent(placeable.itemPos.transform);
                            placeable.item = newItem;
                            placeable.item.layer = 3;
                            placeable.item.transform.localPosition = Vector3.zero;
                            Destroy(heldItem);
                            heldItem = null;
                        }
                    }
                }
            }
        }
    }

    private void Interact()
    {
        if (hitObject)
        {
            if (hitObject.TryGetComponent(out Table table))
            {
                if (table.order == null)
                    table.TakeOrder();
            }
            Placeable placeable = hitObject.GetComponent<Placeable>();

            if (hitObject.TryGetComponent(out ContainerCounter container))
            {
                if (!heldItem)
                {
                    Debug.Log("is this not working?");

                    container.item.transform.position = placeable.item.transform.position;

                    Instantiate(placeable.item, heldItemPos.transform);
                    placeable.item = heldItem;


                }
            }
        }
    }

    public void Move()
    {

        moveDir = Vector3.zero;
        moveDir = orientation.forward * move.y + orientation.right * move.x;

        rb.AddForce(moveDir.normalized * speed * 100f * Time.fixedDeltaTime, ForceMode.Force);


    }

    private void SpeedControl()
    {
        Vector3 vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (vel.magnitude > speed)
        {
            Vector3 controlVel = vel.normalized * speed;

            rb.velocity = new Vector3(controlVel.x, 0f, controlVel.z);
        }
    }

    private void SetSelectedObject(GameObject obj)
    {
        Events.onObjectSelectedChanged.Invoke(obj);
    }

    private void UpdateSound()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.IMMEDIATE);
        }
    }
}
