using OpenYandere.Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OpenYandere.Managers.Traits;

internal class InventoryUI : Singleton<InventoryUI>
{
    public int currentItemIndex = 0;
    public TextMeshProUGUI itemNameText, itemDescriptionText;
    public Transform circleCenter; // O ponto central do círculo
    public float circleRadius = 5.0f; // O raio do círculo
    public GameObject itemSlotPrefab; // Prefab para representar um item no inventário
    private List<GameObject> itemSlots = new(); // Lista para manter os gameobjects do item slot
    [SerializeField] protected Button btnUse, btnDrop;
    // Supondo que você tenha um GameObject para o inventário UI.
    public GameObject inventoryUI;
    protected CanvasGroup _canvasGroup;
    private bool isInventoryOpen = false;

    protected void Awake()
    {
        if (!TryGetComponent(out _canvasGroup))
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        _canvasGroup.alpha = 0;
    }
    private void OnEnable()
    {
        btnUse.onClick.AddListener(UseOrEquipItem);
        btnDrop.onClick.AddListener(DropItem);
        InventorySystem.Instance.OnItemAdded += UpdateUI;
        InventorySystem.Instance.OnItemRemoved += UpdateUI;
    }

    private void OnDisable()
    {
        btnUse.onClick.RemoveAllListeners();
        btnDrop.onClick.RemoveAllListeners();
        InventorySystem.Instance.OnItemAdded -= UpdateUI;
        InventorySystem.Instance.OnItemRemoved -= UpdateUI;
    }

    private void Start()
    {
        CreateItemSlots();
    }

    private void FixedUpdate()
    {
        NavigateItems();
        DisplayItem();
        UseOrEquipItem();
        DropItem();
        UpdateCircularView();
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    private void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }

    }

    private void OpenInventory()
    {
        // Usando DOTween para fazer o inventário aparecer (por exemplo, fadeIn). Ajuste conforme necessário.
        _canvasGroup.DOFade(1, 1.5f).OnComplete(() =>
        {
            //inventoryUI.SetActive(true);
            GameManager.Instance.Pause();
            isInventoryOpen = true;
        });

        
    }

    private void CloseInventory()
    { //inventoryUI.SetActive(false);
       GameManager.Instance.Resume();
        // Usando DOTween para fazer o inventário desaparecer (por exemplo, fadeOut).
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            isInventoryOpen = false;
        });

        
    }

    private void CreateItemSlots()
    {
        int itemCount = InventorySystem.Instance.GetItems().Count;
        for (int i = 0; i < itemCount; i++)
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryUI.transform);
            itemSlots.Add(slot);
        }
    }
    private void UpdateUI()
    {
        // Atualize a representação visual dos itens.
        UpdateCircularView();

        // Atualize os textos com base no item atual.
        DisplayItem();
    }
    private void NavigateItems()
    {
        int itemCount = InventorySystem.Instance.GetItems().Count;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentItemIndex = (currentItemIndex + 1) % itemCount;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
                currentItemIndex = itemCount - 1;
        }
        DisplayItem();
    }

    private void DisplayItem()
    {
        if (InventorySystem.Instance.GetItems().Count > 0)
        {
            itemNameText.text = InventorySystem.Instance.GetItems()[currentItemIndex].ItemName;
            itemDescriptionText.text = InventorySystem.Instance.GetItems()[currentItemIndex].ItemDescription;
        }
        else
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }
    }

    private void UseOrEquipItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && InventorySystem.Instance.GetItems().Count > 0)
        {
            ItemBase currentItem = InventorySystem.Instance.GetItem(currentItemIndex);
            if (!currentItem.IsConsumable)
            {
                EquipmentManager.Instance.Equip(currentItem);
            }
            
        }
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && InventorySystem.Instance.GetItems().Count > 0)
        {
            InventorySystem.Instance.RemoveAt(currentItemIndex);
            if (currentItemIndex >= InventorySystem.Instance.GetItems().Count)
                currentItemIndex = 0;
        }
    }

    private void UpdateCircularView()
    {
        int itemCount = InventorySystem.Instance.GetItems().Count;
        float angleStep = 360.0f / itemCount;

        for (int i = 0; i < itemCount; i++)
        {
            GameObject slot = itemSlots[i];
            float angle = angleStep * i + (currentItemIndex * angleStep);

            // Modifique o raio com base no item selecionado
            float modifiedRadius = (i == currentItemIndex) ? circleRadius - 20 : circleRadius;

            Vector3 slotPosition = new(circleCenter.position.x + modifiedRadius * Mathf.Sin(angle * Mathf.Deg2Rad), circleCenter.position.y, circleCenter.position.z + modifiedRadius * Mathf.Cos(angle * Mathf.Deg2Rad));
            slot.transform.position = slotPosition;

            slot.transform.LookAt(circleCenter);

            if (i == currentItemIndex)
            {
                slot.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                slot.transform.localScale = Vector3.one;
            }
        }
    }

}
