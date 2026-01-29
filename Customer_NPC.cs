using Pathfinding;
using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class Customer_NPC : NetworkBehaviour
{
    public float moveSpeed = 2f;
    public NPC npcData;
    public AIPath aiPath;
    public Seeker seeker;
    public AIDestinationSetter destinationSetter;

    private Queue<Shelf_Items> shoppingRoute = new Queue<Shelf_Items>();
    private Transform cashierArea;
    private bool isPausing = false;
    private bool inQueue = false;
    private Transform queueTarget;

    private Coroutine pathRefreshCoroutine;

    public void Initialize(List<Shelf_Items> route, Transform cashier)
    {
        shoppingRoute = new Queue<Shelf_Items>(route);
        cashierArea = cashier;
        MoveToNextTarget();
    }

    void Update()
    {
        if (!IsServer) return;
        if (isPausing) return;

        if (!aiPath.pathPending && aiPath.reachedDestination && shoppingRoute.Count > 0)
        {
            StartCoroutine(TakeItemAndPause());
        }

        if (inQueue)
        {
            int desiredIndex = CashierQueueManager.Instance.GetQueueIndex(this);
            if (desiredIndex >= 0 && desiredIndex < CashierQueueManager.Instance.queueSpots.Count)
            {
                Transform queueSpot = CashierQueueManager.Instance.queueSpots[desiredIndex];
                if (queueSpot != null && destinationSetter.target != queueSpot)
                {
                    destinationSetter.target = queueSpot;
                    if (aiPath != null && aiPath.enabled)
                        aiPath.SearchPath();
                }
            }
        }
    }

    private System.Collections.IEnumerator TakeItemAndPause()
    {
        isPausing = true;
        var shelf = shoppingRoute.Dequeue();

        // Call the RPC to handle item pickup on all clients
        TakeItemRpc(shelf.GetComponent<NetworkObject>());

        yield return new WaitForSeconds(2f);

        if (shoppingRoute.Count > 0)
        {
            MoveToNextTarget();
        }
        else
        {
            JoinCashierQueue();
        }

        isPausing = false;
    }

    private void JoinCashierQueue()
    {
        try
        {
            int myIndex = CashierQueueManager.Instance.JoinQueue(this);
            inQueue = true;

            if (myIndex >= 0 && myIndex < CashierQueueManager.Instance.queueSpots.Count)
            {
                Transform queueSpot = CashierQueueManager.Instance.queueSpots[myIndex];
                destinationSetter.target = queueSpot;
                if (aiPath != null && aiPath.enabled)
                    aiPath.SearchPath();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Customer {gameObject.name} failed to join queue: {ex}");
        }
    }

    // This RPC will be executed on all clients
    [Rpc(SendTo.Everyone)]
    private void TakeItemRpc(NetworkObjectReference shelfRef)
    {
        if (shelfRef.TryGet(out NetworkObject shelfNetObj))
        {
            var shelf = shelfNetObj.GetComponent<Shelf_Items>();
            if (shelf.spawnPoint.childCount > 0)
            {
                var item = shelf.spawnPoint.GetChild(0).gameObject;
                var netObj = item.GetComponent<NetworkObject>();
                if (netObj != null && netObj.IsSpawned)
                {
                    Debug.Log($"Customer {gameObject.name} despawning networked item {item.name} from shelf {shelf.name}");
                    netObj.Despawn();
                    shelf.ItemOnShelf = "";
                    shelf.IsItemOnShelf = false;
                }
                else
                {
                    Debug.Log($"Customer {gameObject.name} destroying local item {item.name} from shelf {shelf.name}");
                    Destroy(item);
                }
            }
        }
    }

    private void MoveToNextTarget()
    {
        if (shoppingRoute.Count > 0)
        {
            var nextShelf = shoppingRoute.Peek();
            destinationSetter.target = nextShelf.customerTargetPoint != null
                ? nextShelf.customerTargetPoint
                : nextShelf.transform;
        }
        else
        {
            // Optionally, set to cashier area if not queueing yet
            destinationSetter.target = cashierArea;
        }
    }

    public void LeaveQueueAndExit()
    {
        if (inQueue)
        {
            int oldIndex = CashierQueueManager.Instance.GetQueueIndex(this);
            CashierQueueManager.Instance.LeaveQueue(this);
            inQueue = false;

            // Wait 1 second before moving up the queue
            CashierQueueManager.Instance.UpdateAllQueueTargetsDelayed(1f);
        }
    }

    public void LeaveStore(Transform exit)
    {
        if (destinationSetter != null && exit != null)
        {
            // Use the provided exit transform directly
            destinationSetter.target = exit;
            StartCoroutine(CheckIfExited(exit));
        }
    }

    private System.Collections.IEnumerator CheckIfExited(Transform exit)
    {
        while (Vector3.Distance(transform.position, exit.position) > 1f)
        {
            yield return null;
        }
        // Despawn or destroy NPC
        if (IsServer)
        {
            GetComponent<NetworkObject>().Despawn();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetQueueTarget(Transform target)
    {
        if (destinationSetter != null && target != null)
        {
            destinationSetter.target = target;
            if (aiPath != null && aiPath.enabled)
                aiPath.SearchPath();
        }
    }

    private void OnEnable()
    {
        if (IsServer)
            pathRefreshCoroutine = StartCoroutine(PeriodicPathRefresh());
    }

    private void OnDisable()
    {
        if (pathRefreshCoroutine != null)
            StopCoroutine(pathRefreshCoroutine);
    }

    private System.Collections.IEnumerator PeriodicPathRefresh()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f)); // Randomize to avoid all NPCs updating at once
            if (aiPath != null && aiPath.enabled)
                aiPath.SearchPath();
        }
    }
}