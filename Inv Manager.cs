Dictionary<string, int> ComponentTargets = new Dictionary<string, int>
{
    {"SteelPlate", 250000},
    {"InteriorPlate", 50000},
    {"Construction", 100000},
    {"Girder", 50000},
    {"SmallTube", 50000},
    {"LargeTube", 50000},
    {"Motor", 50000},
    {"Computer", 50000},
    {"Display", 10000},
    {"BulletproofGlass", 50000},
    {"MetalGrid", 100000},
    {"Reactor", 20000},
    {"PowerCell", 20000},
    {"Medical", 5000},
    {"Detector", 10000},
    {"RadioCommunication", 2000},
    {"Thrust", 20000},
    {"GravityGenerator", 5000},
    {"Superconductor", 20000}
};

Dictionary<string, int> TopToolTargets = new Dictionary<string, int>
{
    {"Welder4Item", 100},
    {"AngleGrinder4Item", 100},
    {"HandDrill4Item", 100}
};

string OreTag = "[ORE]";
string IngotTag = "[INGOT]";
string ComponentTag = "[COMP]";
string MiscTag = "[MISC]";

int SortMovesPerRun = 2;
int PullMovesPerRun = 4;
int CraftCheckInterval = 18;
int PullCheckInterval = 6;
int AutoAssignInterval = 30;
int RefreshInterval = 180;
int MaxQueuePerComponentPerCheck = 500;
double FillLimit = 0.98;

class ToolTier
{
    public string ItemSubtype;
    public string[] BlueprintSubtypes;

    public ToolTier(string itemSubtype, params string[] blueprintSubtypes)
    {
        ItemSubtype = itemSubtype;
        BlueprintSubtypes = blueprintSubtypes;
    }
}

class SourceInventoryRef
{
    public IMyTerminalBlock Block;
    public int InventoryIndex;

    public SourceInventoryRef(IMyTerminalBlock block, int inventoryIndex)
    {
        Block = block;
        InventoryIndex = inventoryIndex;
    }
}

class CountInventoryRef
{
    public IMyTerminalBlock Block;
    public int InventoryIndex;

    public CountInventoryRef(IMyTerminalBlock block, int inventoryIndex)
    {
        Block = block;
        InventoryIndex = inventoryIndex;
    }
}

List<ToolTier> WelderChain = new List<ToolTier>
{
    new ToolTier("WelderItem", "Welder", "WelderItem"),
    new ToolTier("Welder2Item", "Welder2", "Welder2Item"),
    new ToolTier("Welder3Item", "Welder3", "Welder3Item"),
    new ToolTier("Welder4Item", "Welder4", "Welder4Item")
};

List<ToolTier> GrinderChain = new List<ToolTier>
{
    new ToolTier("AngleGrinderItem", "AngleGrinder", "AngleGrinderItem"),
    new ToolTier("AngleGrinder2Item", "AngleGrinder2", "AngleGrinder2Item"),
    new ToolTier("AngleGrinder3Item", "AngleGrinder3", "AngleGrinder3Item"),
    new ToolTier("AngleGrinder4Item", "AngleGrinder4", "AngleGrinder4Item")
};

List<ToolTier> DrillChain = new List<ToolTier>
{
    new ToolTier("HandDrillItem", "HandDrill", "HandDrillItem"),
    new ToolTier("HandDrill2Item", "HandDrill2", "HandDrill2Item"),
    new ToolTier("HandDrill3Item", "HandDrill3", "HandDrill3Item"),
    new ToolTier("HandDrill4Item", "HandDrill4", "HandDrill4Item")
};

List<IMyCargoContainer> oreContainers = new List<IMyCargoContainer>();
List<IMyCargoContainer> ingotContainers = new List<IMyCargoContainer>();
List<IMyCargoContainer> componentContainers = new List<IMyCargoContainer>();
List<IMyCargoContainer> miscContainers = new List<IMyCargoContainer>();
List<IMyCargoContainer> allManagedContainers = new List<IMyCargoContainer>();
List<IMyCargoContainer> unassignedContainers = new List<IMyCargoContainer>();
List<IMyAssembler> assemblers = new List<IMyAssembler>();
List<IMyRefinery> refineries = new List<IMyRefinery>();
List<SourceInventoryRef> pullSources = new List<SourceInventoryRef>();
List<CountInventoryRef> countInventories = new List<CountInventoryRef>();

Dictionary<MyDefinitionId, double> itemTotals = new Dictionary<MyDefinitionId, double>();
Dictionary<MyDefinitionId, double> queuedTotals = new Dictionary<MyDefinitionId, double>();
Dictionary<MyDefinitionId, double> plannedToolTotals = new Dictionary<MyDefinitionId, double>();

List<IMyCargoContainer> tempCargoList = new List<IMyCargoContainer>();
List<IMyTerminalBlock> tempBlockList = new List<IMyTerminalBlock>();
List<MyInventoryItem> tempInventoryItems = new List<MyInventoryItem>();
List<MyProductionItem> tempQueueItems = new List<MyProductionItem>();
HashSet<long> countedInventoryKeys = new HashSet<long>();

int sourceContainerIndex = 0;
int sourceItemIndex = 0;
int pullSourceIndex = 0;
int pullItemIndex = 0;
int runCounter = 0;
bool initialToolPlanBuilt = false;
bool runtimeBootstrapped = false;

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
    RefreshBlocks();
    UpdateItemTotals();
    UpdateQueuedTotals();
    BuildInitialToolPlan();
    runtimeBootstrapped = true;
}

public void Main(string argument, UpdateType updateSource)
{
    EnsureRuntimeStarted();

    if (argument == "refresh")
    {
        RefreshBlocks();
        UpdateItemTotals();
        UpdateQueuedTotals();
        BuildInitialToolPlan();
    }

    if (runCounter % RefreshInterval == 0)
    {
        RefreshBlocks();
    }

    if (runCounter % AutoAssignInterval == 0)
    {
        AutoAssignContainers();
    }

    if (runCounter % PullCheckInterval == 0)
    {
        PullFromConnectedInventories();
    }

    SortManagedCargo();

    if (runCounter % CraftCheckInterval == 0)
    {
        UpdateItemTotals();
        UpdateQueuedTotals();
        RunAutocrafting();
    }

    EchoStatus();
    runCounter++;
}

void EnsureRuntimeStarted()
{
    if ((Runtime.UpdateFrequency & UpdateFrequency.Update100) == 0)
    {
        Runtime.UpdateFrequency |= UpdateFrequency.Update100;
    }

    if (runtimeBootstrapped)
    {
        return;
    }

    RefreshBlocks();
    UpdateItemTotals();
    UpdateQueuedTotals();
    BuildInitialToolPlan();
    runtimeBootstrapped = true;
}

void RefreshBlocks()
{
    oreContainers.Clear();
    ingotContainers.Clear();
    componentContainers.Clear();
    miscContainers.Clear();
    allManagedContainers.Clear();
    unassignedContainers.Clear();
    assemblers.Clear();
    refineries.Clear();
    pullSources.Clear();
    countInventories.Clear();
    countedInventoryKeys.Clear();

    tempCargoList.Clear();
    GridTerminalSystem.GetBlocksOfType(tempCargoList, x => x.IsSameConstructAs(Me));

    for (int i = 0; i < tempCargoList.Count; i++)
    {
        var cargo = tempCargoList[i];

        if (!IsLargeCargoContainer(cargo))
        {
            continue;
        }

        string name = cargo.CustomName;
        bool managed = false;

        if (name.Contains(OreTag))
        {
            oreContainers.Add(cargo);
            managed = true;
        }
        if (name.Contains(IngotTag))
        {
            ingotContainers.Add(cargo);
            managed = true;
        }
        if (name.Contains(ComponentTag))
        {
            componentContainers.Add(cargo);
            managed = true;
        }
        if (name.Contains(MiscTag))
        {
            miscContainers.Add(cargo);
            managed = true;
        }

        if (managed)
        {
            allManagedContainers.Add(cargo);
            AddCountInventory(cargo, 0);
        }
        else
        {
            unassignedContainers.Add(cargo);
        }
    }

    GridTerminalSystem.GetBlocksOfType(assemblers, x => x.IsSameConstructAs(Me) && x.IsFunctional);
    GridTerminalSystem.GetBlocksOfType(refineries, x => x.IsSameConstructAs(Me) && x.IsFunctional);

    for (int i = 0; i < assemblers.Count; i++)
    {
        if (assemblers[i].InventoryCount > 1)
        {
            AddCountInventory(assemblers[i], 1);
        }
    }

    for (int i = 0; i < refineries.Count; i++)
    {
        if (refineries[i].InventoryCount > 1)
        {
            AddCountInventory(refineries[i], 1);
        }
    }

    IndexPullSources();

    EnsureAtLeastOnePerCategory();
    AssignAllUnassignedContainers();
    RebuildManagedList();
    RebuildCountInventoriesForManagedCargo();
}

void AddCountInventory(IMyTerminalBlock block, int inventoryIndex)
{
    long key = (block.EntityId * 10L) + inventoryIndex;

    if (countedInventoryKeys.Contains(key))
    {
        return;
    }

    countedInventoryKeys.Add(key);
    countInventories.Add(new CountInventoryRef(block, inventoryIndex));
}

void RebuildCountInventoriesForManagedCargo()
{
    countInventories.Clear();
    countedInventoryKeys.Clear();

    for (int i = 0; i < allManagedContainers.Count; i++)
    {
        AddCountInventory(allManagedContainers[i], 0);
    }

    for (int i = 0; i < assemblers.Count; i++)
    {
        if (assemblers[i].InventoryCount > 1)
        {
            AddCountInventory(assemblers[i], 1);
        }
    }

    for (int i = 0; i < refineries.Count; i++)
    {
        if (refineries[i].InventoryCount > 1)
        {
            AddCountInventory(refineries[i], 1);
        }
    }
}

void IndexPullSources()
{
    tempBlockList.Clear();
    GridTerminalSystem.GetBlocksOfType(tempBlockList, x => x.IsSameConstructAs(Me) && x.HasInventory);

    for (int i = 0; i < tempBlockList.Count; i++)
    {
        var block = tempBlockList[i];

        if (block == Me)
        {
            continue;
        }

        if (block is IMyCargoContainer)
        {
            continue;
        }

        if (block is IMyReactor)
        {
            continue;
        }

        if (block is IMyGasGenerator)
        {
            continue;
        }

        if (block.BlockDefinition.TypeIdString.Contains("SafeZone"))
        {
            continue;
        }

        if (block is IMyAssembler || block is IMyRefinery)
        {
            if (block.InventoryCount > 1)
            {
                pullSources.Add(new SourceInventoryRef(block, 1));
            }

            continue;
        }

        for (int invIndex = 0; invIndex < block.InventoryCount; invIndex++)
        {
            pullSources.Add(new SourceInventoryRef(block, invIndex));
        }
    }
}

bool IsLargeCargoContainer(IMyCargoContainer cargo)
{
    return cargo.BlockDefinition.SubtypeId.IndexOf("Large", StringComparison.OrdinalIgnoreCase) >= 0;
}

void EnsureAtLeastOnePerCategory()
{
    if (oreContainers.Count == 0) AssignNextUnassigned(OreTag, oreContainers);
    if (ingotContainers.Count == 0) AssignNextUnassigned(IngotTag, ingotContainers);
    if (componentContainers.Count == 0) AssignNextUnassigned(ComponentTag, componentContainers);
    if (miscContainers.Count == 0) AssignNextUnassigned(MiscTag, miscContainers);
}

void AssignAllUnassignedContainers()
{
    while (unassignedContainers.Count > 0)
    {
        string nextTag = GetSmallestCategoryTag();
        List<IMyCargoContainer> nextList = GetCategoryListByTag(nextTag);
        AssignNextUnassigned(nextTag, nextList);
    }
}

string GetSmallestCategoryTag()
{
    int oreCount = oreContainers.Count;
    int ingotCount = ingotContainers.Count;
    int compCount = componentContainers.Count;
    int miscCount = miscContainers.Count;

    int min = oreCount;
    string tag = OreTag;

    if (ingotCount < min)
    {
        min = ingotCount;
        tag = IngotTag;
    }

    if (compCount < min)
    {
        min = compCount;
        tag = ComponentTag;
    }

    if (miscCount < min)
    {
        tag = MiscTag;
    }

    return tag;
}

List<IMyCargoContainer> GetCategoryListByTag(string tag)
{
    if (tag == OreTag) return oreContainers;
    if (tag == IngotTag) return ingotContainers;
    if (tag == ComponentTag) return componentContainers;
    return miscContainers;
}

void AutoAssignContainers()
{
    AutoExpandCategory(oreContainers, OreTag);
    AutoExpandCategory(ingotContainers, IngotTag);
    AutoExpandCategory(componentContainers, ComponentTag);
    AutoExpandCategory(miscContainers, MiscTag);

    AutoReassignEmptyExtras(oreContainers, OreTag);
    AutoReassignEmptyExtras(ingotContainers, IngotTag);
    AutoReassignEmptyExtras(componentContainers, ComponentTag);
    AutoReassignEmptyExtras(miscContainers, MiscTag);

    AssignAllUnassignedContainers();
    RebuildManagedList();
    RebuildCountInventoriesForManagedCargo();
}

void AutoExpandCategory(List<IMyCargoContainer> containers, string tag)
{
    if (containers.Count == 0)
    {
        AssignNextUnassigned(tag, containers);
        return;
    }

    bool hasSpace = false;

    for (int i = 0; i < containers.Count; i++)
    {
        if (HasRoom(containers[i].GetInventory(0)))
        {
            hasSpace = true;
            break;
        }
    }

    if (!hasSpace)
    {
        ReassignOneEmptyContainerTo(tag, containers);
    }
}

void AutoReassignEmptyExtras(List<IMyCargoContainer> containers, string currentTag)
{
    if (containers.Count <= 1)
    {
        return;
    }

    bool keepOneEmpty = true;

    for (int i = containers.Count - 1; i >= 0; i--)
    {
        if (containers.Count <= 1)
        {
            return;
        }

        var inv = containers[i].GetInventory(0);

        if (inv.ItemCount != 0)
        {
            continue;
        }

        if (keepOneEmpty)
        {
            keepOneEmpty = false;
            continue;
        }

        string newTag = GetMostNeededOtherCategoryTag(currentTag);

        if (newTag == currentTag)
        {
            continue;
        }

        RemoveTag(containers[i], currentTag);
        AddTag(containers[i], newTag);

        var targetList = GetCategoryListByTag(newTag);
        targetList.Add(containers[i]);
        containers.RemoveAt(i);
    }
}

string GetMostNeededOtherCategoryTag(string excludeTag)
{
    string bestTag = excludeTag;
    double bestNeed = double.MinValue;

    string[] tags = new string[] { OreTag, IngotTag, ComponentTag, MiscTag };

    for (int i = 0; i < tags.Length; i++)
    {
        string tag = tags[i];

        if (tag == excludeTag)
        {
            continue;
        }

        List<IMyCargoContainer> list = GetCategoryListByTag(tag);
        double need = GetCategoryNeedScore(list);

        if (need > bestNeed)
        {
            bestNeed = need;
            bestTag = tag;
        }
    }

    return bestTag;
}

double GetCategoryNeedScore(List<IMyCargoContainer> containers)
{
    if (containers.Count == 0)
    {
        return 1000;
    }

    int fullCount = 0;
    double totalFill = 0;

    for (int i = 0; i < containers.Count; i++)
    {
        double fill = GetFill(containers[i].GetInventory(0));
        totalFill += fill;

        if (fill >= FillLimit)
        {
            fullCount++;
        }
    }

    return totalFill + fullCount * 10 - containers.Count * 0.1;
}

void ReassignOneEmptyContainerTo(string newTag, List<IMyCargoContainer> targetList)
{
    IMyCargoContainer candidate = FindEmptyContainerForReassignment(newTag);

    if (candidate == null)
    {
        return;
    }

    string oldTag = GetContainerTag(candidate);
    List<IMyCargoContainer> oldList = GetCategoryListByTag(oldTag);

    if (oldList.Count <= 1)
    {
        return;
    }

    RemoveTag(candidate, oldTag);
    AddTag(candidate, newTag);

    oldList.Remove(candidate);
    targetList.Add(candidate);
}

IMyCargoContainer FindEmptyContainerForReassignment(string excludeTag)
{
    for (int i = 0; i < allManagedContainers.Count; i++)
    {
        var container = allManagedContainers[i];
        string tag = GetContainerTag(container);

        if (tag == excludeTag)
        {
            continue;
        }

        var inv = container.GetInventory(0);

        if (inv.ItemCount == 0)
        {
            List<IMyCargoContainer> list = GetCategoryListByTag(tag);

            if (list.Count > 1)
            {
                return container;
            }
        }
    }

    return null;
}

string GetContainerTag(IMyCargoContainer container)
{
    string name = container.CustomName;

    if (name.Contains(OreTag)) return OreTag;
    if (name.Contains(IngotTag)) return IngotTag;
    if (name.Contains(ComponentTag)) return ComponentTag;
    return MiscTag;
}

void AssignNextUnassigned(string tag, List<IMyCargoContainer> targetList)
{
    if (unassignedContainers.Count == 0)
    {
        return;
    }

    var container = unassignedContainers[0];
    unassignedContainers.RemoveAt(0);

    AddTag(container, tag);
    targetList.Add(container);
}

void RebuildManagedList()
{
    allManagedContainers.Clear();
    allManagedContainers.AddRange(oreContainers);
    allManagedContainers.AddRange(ingotContainers);
    allManagedContainers.AddRange(componentContainers);
    allManagedContainers.AddRange(miscContainers);
}

void AddTag(IMyTerminalBlock block, string tag)
{
    if (!block.CustomName.Contains(tag))
    {
        block.CustomName = block.CustomName + " " + tag;
    }
}

void RemoveTag(IMyTerminalBlock block, string tag)
{
    block.CustomName = block.CustomName.Replace(" " + tag, "").Replace(tag, "").Trim();
}

void PullFromConnectedInventories()
{
    if (pullSources.Count == 0)
    {
        return;
    }

    int moves = 0;
    int checkedSources = 0;

    while (moves < PullMovesPerRun && checkedSources < pullSources.Count)
    {
        if (pullSourceIndex >= pullSources.Count)
        {
            pullSourceIndex = 0;
        }

        var sourceRef = pullSources[pullSourceIndex];
        var block = sourceRef.Block;

        if (block == null || sourceRef.InventoryIndex >= block.InventoryCount)
        {
            pullSourceIndex++;
            pullItemIndex = 0;
            checkedSources++;
            continue;
        }

        var sourceInv = block.GetInventory(sourceRef.InventoryIndex);
        tempInventoryItems.Clear();
        sourceInv.GetItems(tempInventoryItems);

        if (tempInventoryItems.Count == 0)
        {
            pullSourceIndex++;
            pullItemIndex = 0;
            checkedSources++;
            continue;
        }

        if (pullItemIndex >= tempInventoryItems.Count)
        {
            pullItemIndex = 0;
            pullSourceIndex++;
            checkedSources++;
            continue;
        }

        var item = tempInventoryItems[pullItemIndex];
        var target = GetTargetManagedContainerForPull(item.Type, sourceInv);

        if (target == null)
        {
            pullItemIndex++;
            continue;
        }

        var targetInv = target.GetInventory(0);

        if (!HasRoom(targetInv))
        {
            pullItemIndex++;
            continue;
        }

        if (!sourceInv.CanTransferItemTo(targetInv, item.Type))
        {
            pullItemIndex++;
            continue;
        }

        bool moved = sourceInv.TransferItemTo(targetInv, pullItemIndex, null, true);

        if (moved)
        {
            moves++;
        }
        else
        {
            pullItemIndex++;
        }
    }
}

IMyCargoContainer GetTargetManagedContainerForPull(MyItemType itemType, IMyInventory sourceInv)
{
    List<IMyCargoContainer> list = GetCategoryList(itemType);

    if (list == null || list.Count == 0)
    {
        return null;
    }

    for (int i = 0; i < list.Count; i++)
    {
        var container = list[i];
        var inv = container.GetInventory(0);

        if (!HasRoom(inv))
        {
            continue;
        }

        if (!sourceInv.CanTransferItemTo(inv, itemType))
        {
            continue;
        }

        return container;
    }

    return null;
}

void SortManagedCargo()
{
    if (allManagedContainers.Count == 0)
    {
        return;
    }

    int moves = 0;
    int checkedContainers = 0;

    while (moves < SortMovesPerRun && checkedContainers < allManagedContainers.Count)
    {
        if (sourceContainerIndex >= allManagedContainers.Count)
        {
            sourceContainerIndex = 0;
        }

        var source = allManagedContainers[sourceContainerIndex];
        var sourceInv = source.GetInventory(0);

        tempInventoryItems.Clear();
        sourceInv.GetItems(tempInventoryItems);

        if (tempInventoryItems.Count == 0)
        {
            sourceContainerIndex++;
            sourceItemIndex = 0;
            checkedContainers++;
            continue;
        }

        if (sourceItemIndex >= tempInventoryItems.Count)
        {
            sourceItemIndex = 0;
            sourceContainerIndex++;
            checkedContainers++;
            continue;
        }

        var item = tempInventoryItems[sourceItemIndex];
        var correctList = GetCategoryList(item.Type);

        if (correctList == null || correctList.Count == 0)
        {
            sourceItemIndex++;
            continue;
        }

        if (correctList.Contains(source))
        {
            sourceItemIndex++;
            continue;
        }

        var target = GetTargetContainer(item.Type, source);

        if (target == null)
        {
            sourceItemIndex++;
            continue;
        }

        var targetInv = target.GetInventory(0);

        if (!HasRoom(targetInv))
        {
            sourceItemIndex++;
            continue;
        }

        if (!sourceInv.CanTransferItemTo(targetInv, item.Type))
        {
            sourceItemIndex++;
            continue;
        }

        bool moved = sourceInv.TransferItemTo(targetInv, sourceItemIndex, null, true);

        if (moved)
        {
            moves++;
        }
        else
        {
            sourceItemIndex++;
        }
    }
}

IMyCargoContainer GetTargetContainer(MyItemType itemType, IMyCargoContainer source)
{
    List<IMyCargoContainer> list = GetCategoryList(itemType);

    if (list == null || list.Count == 0)
    {
        return null;
    }

    var sourceInv = source.GetInventory(0);

    for (int i = 0; i < list.Count; i++)
    {
        var container = list[i];

        if (container == source)
        {
            continue;
        }

        var inv = container.GetInventory(0);

        if (!HasRoom(inv))
        {
            continue;
        }

        if (!sourceInv.CanTransferItemTo(inv, itemType))
        {
            continue;
        }

        return container;
    }

    return null;
}

List<IMyCargoContainer> GetCategoryList(MyItemType itemType)
{
    string typeId = itemType.TypeId.ToString();

    if (typeId.Contains("Ore"))
    {
        return oreContainers;
    }

    if (typeId.Contains("Ingot"))
    {
        return ingotContainers;
    }

    if (typeId.Contains("Component"))
    {
        return componentContainers;
    }

    return miscContainers;
}

bool HasRoom(IMyInventory inv)
{
    return GetFill(inv) < FillLimit;
}

double GetFill(IMyInventory inv)
{
    if ((double)inv.MaxVolume == 0)
    {
        return 1;
    }

    return (double)inv.CurrentVolume / (double)inv.MaxVolume;
}

void UpdateItemTotals()
{
    itemTotals.Clear();

    for (int i = 0; i < countInventories.Count; i++)
    {
        var entry = countInventories[i];

        if (entry.Block == null || entry.InventoryIndex >= entry.Block.InventoryCount)
        {
            continue;
        }

        var inv = entry.Block.GetInventory(entry.InventoryIndex);
        tempInventoryItems.Clear();
        inv.GetItems(tempInventoryItems);

        for (int j = 0; j < tempInventoryItems.Count; j++)
        {
            var item = tempInventoryItems[j];
            double amount = (double)item.Amount;

            if (itemTotals.ContainsKey(item.Type))
            {
                itemTotals[item.Type] += amount;
            }
            else
            {
                itemTotals[item.Type] = amount;
            }
        }
    }
}

void UpdateQueuedTotals()
{
    queuedTotals.Clear();

    for (int i = 0; i < assemblers.Count; i++)
    {
        tempQueueItems.Clear();
        assemblers[i].GetQueue(tempQueueItems);

        for (int j = 0; j < tempQueueItems.Count; j++)
        {
            var queued = tempQueueItems[j];
            var craftedItemId = CraftedItemFromBlueprint(queued.BlueprintId);

            if (!craftedItemId.HasValue)
            {
                continue;
            }

            double amount = (double)queued.Amount;

            if (queuedTotals.ContainsKey(craftedItemId.Value))
            {
                queuedTotals[craftedItemId.Value] += amount;
            }
            else
            {
                queuedTotals[craftedItemId.Value] = amount;
            }
        }
    }
}

void RunAutocrafting()
{
    if (assemblers.Count == 0)
    {
        return;
    }

    RunComponentAutocrafting();

    if (!initialToolPlanBuilt)
    {
        BuildInitialToolPlan();
    }

    RunPlannedToolAutocrafting();
}

void RunComponentAutocrafting()
{
    foreach (var entry in ComponentTargets)
    {
        string subtype = entry.Key;
        int targetAmount = entry.Value;

        var componentId = MyItemType.MakeComponent(subtype);
        double currentAmount = GetCurrentAmount(componentId);
        double queuedAmount = GetQueuedAmount(componentId);
        double missing = targetAmount - currentAmount - queuedAmount;

        if (missing < 1)
        {
            continue;
        }

        double toQueue = missing;
        var blueprint = MyDefinitionId.Parse("MyObjectBuilder_BlueprintDefinition/" + GetComponentBlueprintSubtype(subtype));

        QueueBlueprint(blueprint, toQueue);
    }
}

void BuildInitialToolPlan()
{
    plannedToolTotals.Clear();

    BuildToolPlanForChain(WelderChain, TopToolTargets["Welder4Item"]);
    BuildToolPlanForChain(GrinderChain, TopToolTargets["AngleGrinder4Item"]);
    BuildToolPlanForChain(DrillChain, TopToolTargets["HandDrill4Item"]);

    initialToolPlanBuilt = true;
}

void BuildToolPlanForChain(List<ToolTier> chain, int topTierTarget)
{
    double[] available = new double[chain.Count];
    double[] planned = new double[chain.Count];

    for (int i = 0; i < chain.Count; i++)
    {
        var itemId = MakeToolItemId(chain[i].ItemSubtype);
        available[i] = GetCurrentAmount(itemId) + GetQueuedAmount(itemId);
    }

    planned[chain.Count - 1] = topTierTarget;

    for (int i = chain.Count - 2; i >= 0; i--)
    {
        double higherMissing = planned[i + 1] - available[i + 1];

        if (higherMissing < 0)
        {
            higherMissing = 0;
        }

        planned[i] = higherMissing;
    }

    for (int i = 0; i < chain.Count; i++)
    {
        var itemId = MakeToolItemId(chain[i].ItemSubtype);
        plannedToolTotals[itemId] = planned[i];
    }
}

void RunPlannedToolAutocrafting()
{
    RunPlannedToolChain(WelderChain);
    RunPlannedToolChain(GrinderChain);
    RunPlannedToolChain(DrillChain);
}

void RunPlannedToolChain(List<ToolTier> chain)
{
    for (int i = 0; i < chain.Count; i++)
    {
        var tier = chain[i];
        var itemId = MakeToolItemId(tier.ItemSubtype);

        double plannedTotal;
        if (!plannedToolTotals.TryGetValue(itemId, out plannedTotal))
        {
            continue;
        }

        if (plannedTotal < 1)
        {
            continue;
        }

        double currentAmount = GetCurrentAmount(itemId);
        double queuedAmount = GetQueuedAmount(itemId);
        double missing = plannedTotal - currentAmount - queuedAmount;

        if (missing < 1)
        {
            continue;
        }

        QueueToolTier(tier, missing);
    }
}

void QueueToolTier(ToolTier tier, double amount)
{
    double toQueue = Math.Min(amount, MaxQueuePerComponentPerCheck);
    var candidates = GetToolBlueprintCandidates(tier);

    for (int i = 0; i < candidates.Count; i++)
    {
        if (QueueBlueprint(candidates[i], toQueue))
        {
            return;
        }
    }
}

List<MyDefinitionId> GetToolBlueprintCandidates(ToolTier tier)
{
    var results = new List<MyDefinitionId>();
    var seen = new HashSet<string>();

    for (int i = 0; i < tier.BlueprintSubtypes.Length; i++)
    {
        AddToolBlueprintCandidate(results, seen, tier.BlueprintSubtypes[i]);
    }

    string baseSubtype = tier.ItemSubtype;
    if (baseSubtype.EndsWith("Item"))
    {
        baseSubtype = baseSubtype.Substring(0, baseSubtype.Length - 4);
    }

    AddToolBlueprintCandidate(results, seen, baseSubtype);
    AddToolBlueprintCandidate(results, seen, tier.ItemSubtype);

    for (int pos = 10; pos <= 200; pos += 10)
    {
        string prefix = "Position" + pos.ToString("0000") + "_";
        AddToolBlueprintCandidate(results, seen, prefix + baseSubtype);
        AddToolBlueprintCandidate(results, seen, prefix + tier.ItemSubtype);
    }

    return results;
}

void AddToolBlueprintCandidate(List<MyDefinitionId> results, HashSet<string> seen, string subtype)
{
    if (string.IsNullOrWhiteSpace(subtype))
    {
        return;
    }

    string full = "MyObjectBuilder_BlueprintDefinition/" + subtype;

    if (seen.Contains(full))
    {
        return;
    }

    MyDefinitionId id;
    if (!MyDefinitionId.TryParse(full, out id))
    {
        return;
    }

    seen.Add(full);
    results.Add(id);
}

MyDefinitionId MakeToolItemId(string subtype)
{
    return MyDefinitionId.Parse("MyObjectBuilder_PhysicalGunObject/" + subtype);
}

string GetComponentBlueprintSubtype(string componentSubtype)
{
    if (componentSubtype == "Construction") return "ConstructionComponent";
    if (componentSubtype == "Girder") return "GirderComponent";
    if (componentSubtype == "Motor") return "MotorComponent";
    if (componentSubtype == "Computer") return "ComputerComponent";
    if (componentSubtype == "Reactor") return "ReactorComponent";
    if (componentSubtype == "Medical") return "MedicalComponent";
    if (componentSubtype == "Detector") return "DetectorComponent";
    if (componentSubtype == "RadioCommunication") return "RadioCommunicationComponent";
    if (componentSubtype == "Thrust") return "ThrustComponent";
    if (componentSubtype == "GravityGenerator") return "GravityGeneratorComponent";
    return componentSubtype;
}

bool QueueBlueprint(MyDefinitionId blueprint, double amount)
{
    int availableCount = 0;

    for (int i = 0; i < assemblers.Count; i++)
    {
        if (!assemblers[i].CanUseBlueprint(blueprint))
        {
            continue;
        }

        if (assemblers[i].Mode != MyAssemblerMode.Assembly)
        {
            assemblers[i].Mode = MyAssemblerMode.Assembly;
        }

        availableCount++;
    }

    if (availableCount == 0)
    {
        return false;
    }

    double each = amount / availableCount;

    for (int i = 0; i < assemblers.Count; i++)
    {
        var assembler = assemblers[i];

        if (!assembler.CanUseBlueprint(blueprint))
        {
            continue;
        }

        if (amount <= 0)
        {
            break;
        }

        double add = Math.Min(each, amount);
        assembler.AddQueueItem(blueprint, (VRage.MyFixedPoint)add);
        amount -= add;
    }

    return true;
}

double GetCurrentAmount(MyDefinitionId id)
{
    double value;
    itemTotals.TryGetValue(id, out value);
    return value;
}

double GetQueuedAmount(MyDefinitionId id)
{
    double value;
    queuedTotals.TryGetValue(id, out value);
    return value;
}

string NormalizeBlueprintSubtype(string subtype)
{
    if (subtype.StartsWith("Position") && subtype.Length > 13 && subtype[12] == '_')
    {
        return subtype.Substring(13);
    }

    return subtype;
}

MyDefinitionId? CraftedItemFromBlueprint(MyDefinitionId blueprintId)
{
    string s = blueprintId.ToString();

    if (!s.StartsWith("MyObjectBuilder_BlueprintDefinition/"))
    {
        return null;
    }

    string subtype = s.Replace("MyObjectBuilder_BlueprintDefinition/", "");
    subtype = NormalizeBlueprintSubtype(subtype);

    foreach (var entry in ComponentTargets)
    {
        string componentSubtype = entry.Key;
        if (GetComponentBlueprintSubtype(componentSubtype) == subtype)
        {
            return MyItemType.MakeComponent(componentSubtype);
        }
    }

    for (int i = 0; i < WelderChain.Count; i++)
    {
        for (int j = 0; j < WelderChain[i].BlueprintSubtypes.Length; j++)
        {
            if (NormalizeBlueprintSubtype(WelderChain[i].BlueprintSubtypes[j]) == subtype)
            {
                return MakeToolItemId(WelderChain[i].ItemSubtype);
            }
        }

        if (NormalizeBlueprintSubtype(WelderChain[i].ItemSubtype.Replace("Item", "")) == subtype)
        {
            return MakeToolItemId(WelderChain[i].ItemSubtype);
        }

        if (NormalizeBlueprintSubtype(WelderChain[i].ItemSubtype) == subtype)
        {
            return MakeToolItemId(WelderChain[i].ItemSubtype);
        }
    }

    for (int i = 0; i < GrinderChain.Count; i++)
    {
        for (int j = 0; j < GrinderChain[i].BlueprintSubtypes.Length; j++)
        {
            if (NormalizeBlueprintSubtype(GrinderChain[i].BlueprintSubtypes[j]) == subtype)
            {
                return MakeToolItemId(GrinderChain[i].ItemSubtype);
            }
        }

        if (NormalizeBlueprintSubtype(GrinderChain[i].ItemSubtype.Replace("Item", "")) == subtype)
        {
            return MakeToolItemId(GrinderChain[i].ItemSubtype);
        }

        if (NormalizeBlueprintSubtype(GrinderChain[i].ItemSubtype) == subtype)
        {
            return MakeToolItemId(GrinderChain[i].ItemSubtype);
        }
    }

    for (int i = 0; i < DrillChain.Count; i++)
    {
        for (int j = 0; j < DrillChain[i].BlueprintSubtypes.Length; j++)
        {
            if (NormalizeBlueprintSubtype(DrillChain[i].BlueprintSubtypes[j]) == subtype)
            {
                return MakeToolItemId(DrillChain[i].ItemSubtype);
            }
        }

        if (NormalizeBlueprintSubtype(DrillChain[i].ItemSubtype.Replace("Item", "")) == subtype)
        {
            return MakeToolItemId(DrillChain[i].ItemSubtype);
        }

        if (NormalizeBlueprintSubtype(DrillChain[i].ItemSubtype) == subtype)
        {
            return MakeToolItemId(DrillChain[i].ItemSubtype);
        }
    }

    return null;
}

void EchoStatus()
{
    Echo("RIOT Inventory Sorter");
    Echo("");
    Echo("Managed cargo:");
    Echo("Ore: " + oreContainers.Count);
    Echo("Ingots: " + ingotContainers.Count);
    Echo("Components: " + componentContainers.Count);
    Echo("Misc: " + miscContainers.Count);
    Echo("");
    Echo("Assemblers: " + assemblers.Count);
    Echo("Refineries: " + refineries.Count);
    Echo("Pull sources: " + pullSources.Count);
    Echo("Count invs: " + countInventories.Count);
}

double GetPlannedToolAmount(string subtype)
{
    double value;
    plannedToolTotals.TryGetValue(MakeToolItemId(subtype), out value);
    return value;
}
