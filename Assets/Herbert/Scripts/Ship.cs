using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    [SerializeField]
    List<Component> components;
    List<PowerUp> powerUps;
    [SerializeField]
    bool hasObjective;

    public int Id { get; set; }
    public GameObject objectivePrefab;
    private Vector3 finalCenter = Vector3.zero;

    public static List<Ship> ships = new List<Ship>();

    public void OnEnable(){
        ships.Add(this);
    }

    public void OnDisable()
    {
        ships.Remove(this);
    }

    public bool HasObjective
    {
        get
        {
            return hasObjective;
        }

        set
        {
            hasObjective = value;
            foreach (var c in components)
                c.ObjectiveBoost(hasObjective);
            if (hasObjective) {
                GetComponentInChildren<SpriteRenderer>().sprite = objectivePrefab.GetComponent<SpriteRenderer>().sprite;
            } else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = null;
                Instantiate(objectivePrefab, this.transform.position, new Quaternion());
            }
        }
    }

    public bool HasEnergy()
    {
        foreach (var c in components)
            if (c.Health > 0)
                return true;
        return false;
    }

    void CheckObjective()
    {
        if (!HasObjective) return;
        if (!HasEnergy()) HasObjective = false;
    }

    // Use this for initialization
    void Start () {
        powerUps = new List<PowerUp>();
        hasObjective = false;
        FindObjectOfType<GameManager>().AddShip(this);
        
	}

    void Update () {
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        CheckIfShipReady();
        CheckObjective();

    }

    // TODO: do this when ship is ready to go (phase 2)
    bool shipReady = false;
    private void CheckIfShipReady() {
        if (shipReady)
            return;

        if (components.Count >= 2) { // if ship has min 3 components
            foreach (Component item in components) {
                if (item.Attachment != null) // check if all has allready chosen their attachment
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // set body constraints to none (allow rotation)
            }
        }
    }

    public void AddComponent(Component component)
    {
        Rigidbody2D rb = component.GetComponent<Rigidbody2D>();
        FixedJoint2D newJoint = gameObject.AddComponent<FixedJoint2D>();
        //RelativeJoint2D newJoint = gameObject.AddComponent<RelativeJoint2D>();
        newJoint.connectedBody = rb;

        if (components == null)
            components = new List<Component>();

        components.Add(component);
        component.Ship = this;
        transform.position = GetCenter();
        //print("woop " + component.GetInstanceID() + " #:" + components.Count);
    }

    private Vector3 GetCenter()
    {
        if (shipReady) return finalCenter;

        Vector3 center = Vector3.zero;
        foreach (Component c in components)
        {
            center += c.transform.position;
        }
        finalCenter = center /= components.Count;
        return finalCenter;
    }
}