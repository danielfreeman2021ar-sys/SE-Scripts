#region Pinpoint
const string CAMERA_NAME = "AimCam", AIM_TAG = "[Aim]", LCD_TAG = "LCD_PINPOINT", PINPOINT_CAM_NAME = "PinCam";
const double PINPOINT_CAM_FOV_Y_DEG = 60.0, CTRL_REFRESH_INTERVAL_SEC = 10.0, USER_ROLL_GAIN = 2.0, PROJECTILE_SPEED_MPS = 2000.0,
SMALL_RAIL_PROJECTILE_SPEED_MPS = 1000.0, FIXED_CANNON_PROJECTILE_SPEED_MPS = 500.0, FIXED_AUTOCANNON_PROJECTILE_SPEED_MPS = 400.0,
SIM_SPEED_SMOOTHING = 0.12,
MANUAL_LOCK_DISTANCE_M = 6000.0, TRACK_DISTANCE_M = 6000, CONE_HALF_ANGLE_DEG = 10.0, LOCK_MICROCONE_HALF_ANGLE_DEG = 0.35,
KP_MIN = 1.0, KP_MAX = 6.0, KD_NEAR = 0.55, KD_FAR = 0.22, KP_FULL_FACE_ANGLE_DEG = 20.0,
KP_DEADZONE_ANGLE_DEG = 1.0, KP_EXP_RAMP_ANGLE_DEG = 5.0, KP_EXPONENT = 6.0, MAX_RATE = 3.0, TARGET_MEMORY_SEC = 6.0, MAX_TARGET_ACCEL_MPS2 = 50.0, ACCEL_FILTER = 0.25;
const double CENTER_VIEW_WEIGHT = 1200.0, CENTER_VIEW_EXPONENT = 4.0;
const double PREVIEW_PERSIST_SEC = 1.5, DIRECT_AHEAD_HALF_ANGLE_DEG = 2.0, DIRECT_AHEAD_STRONG_BONUS = 2500.0;
const int SCAN_RING_COUNT = 2, SCAN_POINTS_PER_RING = 8, LOCK_MICROCONE_POINTS = 9, CONFIRM_MAX_CAMERA_ATTEMPTS = 2, ACCEL_SOLVE_ITERS = 6, CONFIRM_TICK_INTERVAL = 6, UI_TICK_INTERVAL = 30, VOXEL_SCAN_COOLDOWN_TICKS = 30, ACQUIRE_VOXEL_SCAN_COOLDOWN_TICKS = 30, ACQUIRE_MAX_RAYCASTS_PER_PASS = 8, LOST_MAX_CONSECUTIVE_MISSES = 40;
const int DIRECT_AHEAD_CENTER_ATTEMPTS = 6;
const int TURRET_FOCUS_INTERVAL_TICKS = 12;
const int BURST_SHOTS_SHORT = 1, BURST_SHOTS_MEDIUM = 2, BURST_SHOTS_LONG = 4;
const double BURST_SHORT_ANGLE_DEG = 2.0, BURST_MEDIUM_ANGLE_DEG = 0.7;
const uint UNLOCK_SUPPRESS_TICKS = 120;
uint _supprTick = 0;
const int TURRET_RR_BATCH_PER_TICK = 10;
IMyCameraBlock _aimCam, _pinCam;
readonly List<IMyCameraBlock> _cams = new List<IMyCameraBlock>();
readonly List<IMyCameraBlock> _allCamsScratch = new List<IMyCameraBlock>();
IMyShipController _ctrl;
readonly List<IMyGyro> _gyros = new List<IMyGyro>();
IMyGyro _activeGyro;
readonly List<IMyTerminalBlock> _aimBlocks = new List<IMyTerminalBlock>();
IMyTerminalBlock _aimRefBlock;
readonly List<IMyLargeTurretBase> _turrets = new List<IMyLargeTurretBase>();
readonly List<IMyTerminalBlock> _lcdBlocks = new List<IMyTerminalBlock>();
readonly List<IMyTerminalBlock> _terminalScratch = new List<IMyTerminalBlock>();
readonly List<IMyShipController> _controllersScratch = new List<IMyShipController>();
bool _locked = false;
long _targetEntityId;
Vector3D _tPos, _lastPos, _tVel, _tAcc;
double _elapsedSec, _lastSeen;
Vector3D _lastVel;
double _lastVelT;
readonly List<ScanRay> _acquirePattern = new List<ScanRay>();
readonly List<ScanRay> _lockPattern = new List<ScanRay>();
int _lockIndex;
int _consecutiveMisses = 0;
double _prevAimPitch = 0.0;
double _prevAimYaw = 0.0;
double _currentAimKp = 0.0;
uint _nextTurretProcessTick;
double _projectileSpeedMps = PROJECTILE_SPEED_MPS;
double _simSpeedRatio = 1.0;
uint _nextTurretFocusTick;
struct ScanRay { public float PitchDeg, YawDeg; public ScanRay(float p, float y) { PitchDeg = p; YawDeg = y; } }
bool _previewValid = false;
Vector3D _previewPos = Vector3D.Zero;
long _previewEntityId = 0;
Vector3D _previewVel = Vector3D.Zero;
double _previewExpireAt = 0.0;
enum RaycastResultKind { None, CannotScan, ObstructedByVoxelOrPlanet, Self, OtherEntity, TargetEntity }
struct RaycastResult { public RaycastResultKind Kind; public MyDetectedEntityInfo Info; public RaycastResult(RaycastResultKind k, MyDetectedEntityInfo i) { Kind = k; Info = i; } }
class CameraScanScheduler
{
    readonly List<IMyCameraBlock> _cams;
    int _rr;
    public CameraScanScheduler(List<IMyCameraBlock> cams) { _cams = cams; _rr = 0; }
    public IMyCameraBlock PickCameraThatCanScan(double distance, out int camIndex)
    {
        camIndex = -1;
        int n = _cams.Count;
        if (n == 0) return null;
        for (int k = 0; k < n; k++)
        {
            int idx = (_rr + k) % n;
            var cam = _cams[idx];
            if (cam != null && cam.CanScan(distance))
            {
                _rr = (idx + 1) % n;
                camIndex = idx;
                return cam;
            }
        }
        return null;
    }
}
CameraScanScheduler _camScheduler;
uint _tick, _nextConfirmTick, _nextUiTick;
bool _lcdConfigured;
uint _confirmBlockedUntilTick;
uint _acquireBlockedUntilTick;
int _turretRrIndex;
double _nextCtrlRefreshAt;
void InitPinpoint()
{
    _tick = 0;
    _nextConfirmTick = 0;
    _nextUiTick = 0;
    _lcdConfigured = false;
    _confirmBlockedUntilTick = 0;
    _acquireBlockedUntilTick = 0;
    _allCamsScratch.Clear();
    GridTerminalSystem.GetBlocksOfType(_allCamsScratch, c => c != null && c.IsSameConstructAs(Me));
    _cams.Clear();
    _pinCam = null;
    _nextTurretProcessTick = 0;
    for (int i = 0; i < _allCamsScratch.Count; i++)
    {
        var c = _allCamsScratch[i];
        if (c == null) continue;
        if (_pinCam == null && c.CustomName != null && c.CustomName.Contains(PINPOINT_CAM_NAME))
            _pinCam = c;
        if (c.CustomName != null && c.CustomName.Contains(CAMERA_NAME))
        {
            c.EnableRaycast = true;
            _cams.Add(c);
        }
    }
    _aimCam = _cams.Count > 0 ? _cams[0] : null;
    _camScheduler = new CameraScanScheduler(_cams);
    _gyros.Clear();
    GridTerminalSystem.GetBlocksOfType(_gyros, g => g.CubeGrid == Me.CubeGrid);
    _activeGyro = null;
    _aimBlocks.Clear();
    GridTerminalSystem.GetBlocksOfType(_aimBlocks, b => b.CubeGrid == Me.CubeGrid && b.CustomName.Contains(AIM_TAG));
    _turrets.Clear();
    GridTerminalSystem.GetBlocksOfType(_turrets, t => t != null && t.IsSameConstructAs(Me));
    _ctrl = FindController();
    _aimRefBlock = _aimBlocks.Count > 0 ? _aimBlocks[0] : ((_ctrl as IMyTerminalBlock) ?? (_aimCam as IMyTerminalBlock));
    _lcdBlocks.Clear();
    _terminalScratch.Clear();
    GridTerminalSystem.GetBlocksOfType(_terminalScratch, b => b.CubeGrid == Me.CubeGrid && b.CustomName.Contains(LCD_TAG));
    for (int i = 0; i < _terminalScratch.Count; i++)
    {
        var b = _terminalScratch[i];
        if (b is IMyTextSurface || b is IMyTextSurfaceProvider) _lcdBlocks.Add(b);
    }
    BuildAcquirePattern();
    BuildLockPattern();
    _locked = false;
    _targetEntityId = 0;
    _elapsedSec = 0;
    _lastSeen = 0;
    _tPos = Vector3D.Zero;
    _lastPos = Vector3D.Zero;
    _tVel = Vector3D.Zero;
    _tAcc = Vector3D.Zero;
    _lastVel = Vector3D.Zero;
    _lastVelT = 0;
    _lockIndex = 0;
    _consecutiveMisses = 0;
    _turretRrIndex = 0;
    _previewValid = false;
    _previewPos = Vector3D.Zero;
    _previewEntityId = 0;
    _previewVel = Vector3D.Zero;
    _previewExpireAt = 0.0;
    _prevAimPitch = 0.0;
    _prevAimYaw = 0.0;
    _currentAimKp = 0.0;
    _nextCtrlRefreshAt = 0.0;
    _projectileSpeedMps = DetectAimProjectileSpeed();
    _simSpeedRatio = 1.0;
    _nextTurretFocusTick = 0;
    ConfigureLcdSurfacesOnce();
    ClearGyros();
}
void ConfigureSurface(IMyTextSurface s)
{
    if (s == null) return;
    var transparent = new Color(0, 0, 0, 0);
    s.ContentType = ContentType.SCRIPT;
    s.Script = "";
    s.BackgroundColor = transparent;
    s.ScriptBackgroundColor = transparent;
    s.ScriptForegroundColor = Color.White;
}
void ConfigureLcdSurfacesOnce()
{
    if (_lcdConfigured) return;
    _lcdConfigured = true;
    for (int i = 0; i < _lcdBlocks.Count; i++)
    {
        var b = _lcdBlocks[i];
        if (b is IMyTextSurface)
        {
            ConfigureSurface((IMyTextSurface)b);
        }
        else if (b is IMyTextSurfaceProvider)
        {
            var p = (IMyTextSurfaceProvider)b;
            for (int si = 0; si < p.SurfaceCount; si++)
                ConfigureSurface(p.GetSurface(si));
        }
    }
}
IMyShipController FindController()
{
    _controllersScratch.Clear();
    GridTerminalSystem.GetBlocksOfType(_controllersScratch, c => c.CubeGrid == Me.CubeGrid && c.CanControlShip);
    for (int i = 0; i < _controllersScratch.Count; i++) if (_controllersScratch[i].IsUnderControl) return _controllersScratch[i];
    return _controllersScratch.Count > 0 ? _controllersScratch[0] : null;
}
void UpdateSimSpeedRatio()
{
    double dt = Runtime.TimeSinceLastRun.TotalSeconds;
    if (dt <= 1e-6) return;
    double raw = (1.0 / 60.0) / dt;
    raw = Clamp(raw, 0.1, 1.0);
    _simSpeedRatio = _simSpeedRatio * (1.0 - SIM_SPEED_SMOOTHING) + raw * SIM_SPEED_SMOOTHING;
}
int AdaptiveIntervalTicks(int baseTicks)
{
    if (baseTicks <= 1) return 1;
    double sim = Clamp(_simSpeedRatio, 0.25, 1.0);
    int adjusted = (int)Math.Ceiling(baseTicks / sim);
    return adjusted < 1 ? 1 : adjusted;
}
bool TryGetWeaponProjectileSpeed(IMyTerminalBlock b, out double speed)
{
    speed = PROJECTILE_SPEED_MPS;
    if (b == null) return false;
    var gun = b as IMyUserControllableGun;
    if (gun == null) return false;
    string s = gun.BlockDefinition.SubtypeId;
    if (s == "LargeRailgun") { speed = PROJECTILE_SPEED_MPS; return true; }
    if (s == "SmallRailgun") { speed = SMALL_RAIL_PROJECTILE_SPEED_MPS; return true; }
    if (s == "LargeBlockLargeCalibreGun" || s == "SmallBlockMediumCalibreGun") { speed = FIXED_CANNON_PROJECTILE_SPEED_MPS; return true; }
    if (s == "SmallBlockAutoCannon") { speed = FIXED_AUTOCANNON_PROJECTILE_SPEED_MPS; return true; }
    return false;
}
double DetectAimProjectileSpeed()
{
    double speed = PROJECTILE_SPEED_MPS;
    bool found = false;
    for (int i = 0; i < _aimBlocks.Count; i++)
    {
        double s;
        if (!TryGetWeaponProjectileSpeed(_aimBlocks[i], out s)) continue;
        if (!found || s < speed)
        {
            speed = s;
            found = true;
        }
    }
    return found ? speed : PROJECTILE_SPEED_MPS;
}
void BuildAcquirePattern()
{
    _acquirePattern.Clear();
    _acquirePattern.Add(new ScanRay(0f, 0f));
    if (SCAN_RING_COUNT <= 0) return;
    for (int r = 1; r <= SCAN_RING_COUNT; r++)
    {
        double frac = (double)r / SCAN_RING_COUNT, rad = CONE_HALF_ANGLE_DEG * frac;
        for (int i = 0; i < SCAN_POINTS_PER_RING; i++)
        {
            double a = 2.0 * Math.PI * i / SCAN_POINTS_PER_RING;
            _acquirePattern.Add(new ScanRay((float)(rad * Math.Sin(a)), (float)(rad * Math.Cos(a))));
        }
    }
}
void BuildLockPattern()
{
    _lockPattern.Clear();
    _lockPattern.Add(new ScanRay(0f, 0f));
    double d = LOCK_MICROCONE_HALF_ANGLE_DEG;
    _lockPattern.Add(new ScanRay((float)(d), (float)(0)));
    _lockPattern.Add(new ScanRay((float)(d), (float)(d)));
    _lockPattern.Add(new ScanRay((float)(0), (float)(d)));
    _lockPattern.Add(new ScanRay((float)(-d), (float)(d)));
    _lockPattern.Add(new ScanRay((float)(-d), (float)(0)));
    _lockPattern.Add(new ScanRay((float)(-d), (float)(-d)));
    _lockPattern.Add(new ScanRay((float)(0), (float)(-d)));
    _lockPattern.Add(new ScanRay((float)(d), (float)(-d)));
    if (LOCK_MICROCONE_POINTS > 0 && _lockPattern.Count > LOCK_MICROCONE_POINTS) _lockPattern.RemoveRange(LOCK_MICROCONE_POINTS, _lockPattern.Count - LOCK_MICROCONE_POINTS);
}
void PinpointTick(string argument, UpdateType updateSource)
{
    _tick++;
    _elapsedSec += Runtime.TimeSinceLastRun.TotalSeconds;
    UpdateSimSpeedRatio();
    if (_ctrl == null || _ctrl.Closed || _ctrl.CubeGrid.GetCubeBlock(_ctrl.Position) == null || _elapsedSec >= _nextCtrlRefreshAt)
    {
        _ctrl = FindController();
        _nextCtrlRefreshAt = _elapsedSec + CTRL_REFRESH_INTERVAL_SEC;
        if (_aimBlocks.Count == 0)
            _aimRefBlock = (_ctrl as IMyTerminalBlock) ?? (_aimCam as IMyTerminalBlock);
    }
    if ((updateSource & (UpdateType.Terminal | UpdateType.Trigger | UpdateType.Script)) != 0) HandleCommand(argument);
    if (_aimCam == null || _ctrl == null || _gyros.Count == 0 || _cams.Count == 0 || _aimRefBlock == null)
    {
        _previewValid = false;
        _currentAimKp = 0.0;
        if (_tick >= _nextUiTick)
        {
            _nextUiTick = _tick + (uint)AdaptiveIntervalTicks(UI_TICK_INTERVAL);
            DrawAllLcd();
        }
        return;
    }
    if (_locked) TrackLead();
    else _currentAimKp = 0.0;
    if (_tick >= _nextUiTick)
    {
        _nextUiTick = _tick + (uint)AdaptiveIntervalTicks(UI_TICK_INTERVAL);
        UpdatePreviewTarget();
        DrawAllLcd();
    }
}
double ComputeForwardDot(Vector3D targetPos)
{
    var refBlock = GetAimReferenceBlock();
    if (refBlock == null) return -1.0;
    Vector3D toTarget = targetPos - refBlock.GetPosition();
    double len2 = toTarget.LengthSquared();
    if (len2 < 1e-9) return -1.0;
    return Vector3D.Dot(refBlock.WorldMatrix.Forward, Vector3D.Normalize(toTarget));
}
IMyTerminalBlock GetAimReferenceBlock()
{
    return _aimRefBlock ?? (_ctrl as IMyTerminalBlock) ?? (_aimCam as IMyTerminalBlock) ?? Me;
}
double ComputeDirectAheadBonus(Vector3D targetPos)
{
    double dot = ComputeForwardDot(targetPos);
    double minDot = Math.Cos(DIRECT_AHEAD_HALF_ANGLE_DEG * Math.PI / 180.0);
    if (dot <= minDot) return 0.0;
    double t = (dot - minDot) / (1.0 - minDot);
    t = Clamp(t, 0.0, 1.0);
    return DIRECT_AHEAD_STRONG_BONUS * t * t;
}
void SetPreviewCandidate(long entityId, Vector3D pos, Vector3D vel)
{
    _previewValid = true;
    _previewEntityId = entityId;
    _previewPos = pos;
    _previewVel = vel;
    _previewExpireAt = _elapsedSec + PREVIEW_PERSIST_SEC;
}
void ClearPreviewCandidate()
{
    _previewValid = false;
    _previewEntityId = 0;
    _previewPos = Vector3D.Zero;
    _previewVel = Vector3D.Zero;
    _previewExpireAt = 0.0;
}
bool TryAcquireBestTarget(out long bestEntity, out Vector3D bestPos, out Vector3D bestVel)
{
    bestEntity = 0;
    bestPos = Vector3D.Zero;
    bestVel = Vector3D.Zero;
    if (_tick < _supprTick || _tick < _acquireBlockedUntilTick) return false;
    if (TryAcquireTurretTarget(out bestEntity, out bestPos, out bestVel))
        return true;
    if (_cams.Count == 0) return false;
    double bestAcquireScore = -1.0;
    int raycastsUsed = 0;
    bool voxelObstructed = false;
    int centerAttempts = DIRECT_AHEAD_CENTER_ATTEMPTS;
    int maxCenterAttempts = _cams.Count * 2;
    if (centerAttempts > maxCenterAttempts) centerAttempts = maxCenterAttempts;
    for (int i = 0; i < centerAttempts; i++)
    {
        if (raycastsUsed >= ACQUIRE_MAX_RAYCASTS_PER_PASS) break;
        int camIdx;
        var cam = _camScheduler.PickCameraThatCanScan(MANUAL_LOCK_DISTANCE_M, out camIdx);
        if (cam == null) break;
        var info = cam.Raycast(MANUAL_LOCK_DISTANCE_M, 0f, 0f);
        raycastsUsed++;
        if (info.Type == MyDetectedEntityType.Asteroid || info.Type == MyDetectedEntityType.Planet)
        {
            voxelObstructed = true;
            break;
        }
        Vector3D center;
        if (!TryUpdateBestAcquireCandidate(ref info, ref bestAcquireScore, ref bestEntity, ref bestPos, ref bestVel, out center))
            continue;
        if (ComputeForwardDot(center) >= Math.Cos(0.75 * Math.PI / 180.0))
            return true;
    }
    for (int i = 0; i < _acquirePattern.Count && raycastsUsed < ACQUIRE_MAX_RAYCASTS_PER_PASS; i++)
    {
        int camIdx;
        var cam = _camScheduler.PickCameraThatCanScan(MANUAL_LOCK_DISTANCE_M, out camIdx);
        if (cam == null) break;
        var ray = _acquirePattern[i];
        var info = cam.Raycast(MANUAL_LOCK_DISTANCE_M, ray.PitchDeg, ray.YawDeg);
        raycastsUsed++;
        if (info.Type == MyDetectedEntityType.Asteroid || info.Type == MyDetectedEntityType.Planet)
        {
            voxelObstructed = true;
            break;
        }
        Vector3D center;
        if (!TryUpdateBestAcquireCandidate(ref info, ref bestAcquireScore, ref bestEntity, ref bestPos, ref bestVel, out center))
            continue;
        if (IsStrongAcquireCandidate(ref info) && ComputeForwardDot(center) >= Math.Cos(DIRECT_AHEAD_HALF_ANGLE_DEG * Math.PI / 180.0))
            break;
    }
    if (bestEntity == 0 && voxelObstructed)
        _acquireBlockedUntilTick = _tick + (uint)AdaptiveIntervalTicks(ACQUIRE_VOXEL_SCAN_COOLDOWN_TICKS);
    return bestEntity != 0;
}
bool TryAcquireTurretTarget(out long bestEntity, out Vector3D bestPos, out Vector3D bestVel)
{
    bestEntity = 0;
    bestPos = Vector3D.Zero;
    bestVel = Vector3D.Zero;
    int n = _turrets.Count;
    if (n == 0) return false;
    double bestAcquireScore = -1.0;
    for (int step = 0; step < n; step++)
    {
        int idx = (_turretRrIndex + step) % n;
        var t = _turrets[idx];
        if (t == null || t.Closed || !t.IsFunctional || !t.HasTarget) continue;
        MyDetectedEntityInfo info = t.GetTargetedEntity();
        if (!IsEnemyLargeGrid(ref info)) continue;
        Vector3D center = GetDetectedCenterWorld(ref info);
        double score = ComputeAcquireScore(ref info);
        if (score <= bestAcquireScore) continue;
        bestAcquireScore = score;
        bestEntity = info.EntityId;
        bestPos = center;
        bestVel = info.Velocity;
    }
    _turretRrIndex = n == 0 ? 0 : ((_turretRrIndex + 1) % n);
    return bestEntity != 0;
}
bool TryUpdateBestAcquireCandidate(ref MyDetectedEntityInfo info, ref double bestAcquireScore, ref long bestEntity, ref Vector3D bestPos, ref Vector3D bestVel, out Vector3D center)
{
    center = Vector3D.Zero;
    if (!IsEnemyLargeGrid(ref info)) return false;
    center = GetDetectedCenterWorld(ref info);
    double acquireScore = ComputeAcquireScore(ref info);
    if (acquireScore <= bestAcquireScore) return true;
    bestAcquireScore = acquireScore;
    bestEntity = info.EntityId;
    bestPos = center;
    bestVel = info.Velocity;
    return true;
}
void UpdatePreviewTarget()
{
    if (_locked || _tick < _supprTick)
    {
        ClearPreviewCandidate();
        return;
    }
    long bestEntity;
    Vector3D bestPos;
    Vector3D bestVel;
    if (TryAcquireBestTarget(out bestEntity, out bestPos, out bestVel))
    {
        SetPreviewCandidate(bestEntity, bestPos, bestVel);
        return;
    }
    if (_previewValid && _elapsedSec <= _previewExpireAt)
        return;
    ClearPreviewCandidate();
}
void HandleCommand(string arg)
{
    arg = (arg ?? "").Trim().ToLowerInvariant();
    if (arg == "lock") TryManualLock();
    else if (arg == "unlock") DropLock();
    else if (arg == "reinit") InitPinpoint();
}
void SetLockFromSolution(long entityId, Vector3D pos, Vector3D vel)
{
    _locked = true;
    _targetEntityId = entityId;
    _tPos = pos;
    _lastPos = pos;
    _tVel = vel;
    _tAcc = Vector3D.Zero;
    _lastVel = vel;
    _lastVelT = _elapsedSec;
    _lastSeen = _elapsedSec;
    _lockIndex = 0;
    _consecutiveMisses = 0;
    _nextTurretFocusTick = 0;
    TryForceTurretsTrackLockedTarget();
}
bool IsEnemyLargeGrid(ref MyDetectedEntityInfo info)
{
    if (info.IsEmpty()) return false;
    if (info.EntityId == Me.CubeGrid.EntityId) return false;
    if (info.Type != MyDetectedEntityType.LargeGrid) return false;
    if (info.Relationship != MyRelationsBetweenPlayerAndBlock.Enemies) return false;
    return true;
}
double ComputeSizeScore(ref MyDetectedEntityInfo info)
{
    Vector3D sz = info.BoundingBox.Max - info.BoundingBox.Min;
    return sz.LengthSquared();
}
double ComputeBalancedSizeScore(ref MyDetectedEntityInfo info)
{
    double raw = ComputeSizeScore(ref info);
    if (raw <= 0.0) return 0.0;
    return Math.Sqrt(raw) * 6.0;
}
double ComputeCenterViewScore(Vector3D targetPos)
{
    var refBlock = GetAimReferenceBlock();
    if (refBlock == null) return 0.0;
    Vector3D toTarget = targetPos - refBlock.GetPosition();
    double len2 = toTarget.LengthSquared();
    if (len2 < 1e-9) return 0.0;
    Vector3D dir = Vector3D.Normalize(toTarget);
    double dot = Vector3D.Dot(refBlock.WorldMatrix.Forward, dir);
    double minDot = Math.Cos(CONE_HALF_ANGLE_DEG * Math.PI / 180.0);
    if (dot <= minDot) return 0.0;
    double t = (dot - minDot) / (1.0 - minDot);
    t = Clamp(t, 0.0, 1.0);
    return CENTER_VIEW_WEIGHT * Math.Pow(t, CENTER_VIEW_EXPONENT);
}
double ComputeAcquireScore(ref MyDetectedEntityInfo info)
{
    Vector3D center = GetDetectedCenterWorld(ref info);
    double sizeScore = ComputeBalancedSizeScore(ref info);
    double centerScore = ComputeCenterViewScore(center) * 1.35;
    double aheadBonus = ComputeDirectAheadBonus(center);
    return sizeScore + centerScore + aheadBonus;
}
bool IsStrongAcquireCandidate(ref MyDetectedEntityInfo info)
{
    Vector3D center = GetDetectedCenterWorld(ref info);
    double centerScore = ComputeCenterViewScore(center);
    if (centerScore < CENTER_VIEW_WEIGHT * 0.9) return false;
    return ComputeBalancedSizeScore(ref info) >= 20.0;
}
Vector3D GetDetectedCenterWorld(ref MyDetectedEntityInfo info)
{
    return (info.BoundingBox.Min + info.BoundingBox.Max) * 0.5;
}
bool ProcessLockedTurrets()
{
    if (!_locked || _targetEntityId == 0) return false;
    int n = _turrets.Count;
    if (n == 0) return false;
    int batch = TURRET_RR_BATCH_PER_TICK;
    if (batch > n) batch = n;
    bool updated = false;
    for (int k = 0; k < batch; k++)
    {
        int idx = _turretRrIndex;
        _turretRrIndex++;
        if (_turretRrIndex >= n) _turretRrIndex = 0;
        var t = _turrets[idx];
        if (t == null || !t.IsFunctional || !t.HasTarget) continue;
        MyDetectedEntityInfo info = t.GetTargetedEntity();
        if (info.IsEmpty()) continue;
        if (!IsEnemyLargeGrid(ref info)) continue;
        if (info.EntityId != _targetEntityId) continue;
        UpdateTarget(ref info);
        updated = true;
    }
    return updated;
}
void TryManualLock()
{
    if (_tick < _supprTick) return;
    if (_locked) return;
    if (!_previewValid || _previewEntityId == 0 || _elapsedSec > _previewExpireAt)
        UpdatePreviewTarget();
    if (!_previewValid || _previewEntityId == 0 || _elapsedSec > _previewExpireAt)
        return;
    SetLockFromSolution(_previewEntityId, _previewPos, _previewVel);
    ClearPreviewCandidate();
}
void TrackLead()
{
    bool turretUpdated = false;
    if (_tick >= _nextTurretProcessTick)
    {
        _nextTurretProcessTick = _tick + (uint)AdaptiveIntervalTicks(1);
        turretUpdated = ProcessLockedTurrets();
        if (turretUpdated) _consecutiveMisses = 0;
    }
    if (_tick >= _nextTurretFocusTick)
    {
        _nextTurretFocusTick = _tick + (uint)AdaptiveIntervalTicks(TURRET_FOCUS_INTERVAL_TICKS);
        TryForceTurretsTrackLockedTarget();
    }
    PredictTarget();
    bool doConfirm = !turretUpdated && _tick >= _nextConfirmTick && _tick >= _confirmBlockedUntilTick;
    if (doConfirm) _nextConfirmTick = _tick + (uint)AdaptiveIntervalTicks(CONFIRM_TICK_INTERVAL);
    if (doConfirm)
    {
        RaycastResultKind confirmKind;
        bool confirmed = ConfirmLock(TRACK_DISTANCE_M, out confirmKind);
        if (confirmed) _consecutiveMisses = 0;
        else
        {
            if (confirmKind == RaycastResultKind.None || confirmKind == RaycastResultKind.OtherEntity)
                _consecutiveMisses++;
            else if (confirmKind == RaycastResultKind.CannotScan || confirmKind == RaycastResultKind.ObstructedByVoxelOrPlanet)
            {
                if (confirmKind == RaycastResultKind.ObstructedByVoxelOrPlanet)
                    _confirmBlockedUntilTick = _tick + (uint)AdaptiveIntervalTicks(VOXEL_SCAN_COOLDOWN_TICKS);
                if (turretUpdated || (_elapsedSec - _lastSeen) < 0.25)
                    _consecutiveMisses = 0;
            }
        }
        if (_consecutiveMisses >= LOST_MAX_CONSECUTIVE_MISSES)
            _consecutiveMisses = LOST_MAX_CONSECUTIVE_MISSES;
    }
    Vector3D shooterVel = _ctrl.GetShipVelocities().LinearVelocity, origin = GetAimOriginWorld();
    Vector3D dirWorld = SolveLead(origin, _tPos, _tVel, _tAcc, shooterVel, _projectileSpeedMps);
    AimAlongWorldDirection(dirWorld);
}
void DropLock()
{
    _locked = false;
    _targetEntityId = 0;
    ClearAllTargetingData();
    ClearPreviewCandidate();
    ClearGyros();
}
void ClearAllTargetingData()
{
    _tPos = Vector3D.Zero;
    _lastPos = Vector3D.Zero;
    _tVel = Vector3D.Zero;
    _tAcc = Vector3D.Zero;
    _lastVel = Vector3D.Zero;
    _lastSeen = 0;
    _lastVelT = 0;
    _lockIndex = 0;
    _consecutiveMisses = 0;
    _supprTick = _tick + UNLOCK_SUPPRESS_TICKS;
    _prevAimPitch = 0.0;
    _prevAimYaw = 0.0;
    _currentAimKp = 0.0;
    _nextTurretFocusTick = 0;
}
double ComputeAimErrorAngleDeg(Vector3D targetPos)
{
    var refBlock = GetAimReferenceBlock();
    if (refBlock == null) return 180.0;
    Vector3D toTarget = targetPos - refBlock.GetPosition();
    double len2 = toTarget.LengthSquared();
    if (len2 < 1e-12) return 0.0;
    Vector3D dir = Vector3D.Normalize(toTarget);
    double dot = Clamp(Vector3D.Dot(refBlock.WorldMatrix.Forward, dir), -1.0, 1.0);
    return Math.Acos(dot) * 180.0 / Math.PI;
}
int GetAdaptiveBurstFollowupShots()
{
    if (!_locked || _targetEntityId == 0) return BURST_SHOTS_SHORT;
    double errDeg = ComputeAimErrorAngleDeg(_tPos);
    if (errDeg >= BURST_SHORT_ANGLE_DEG) return BURST_SHOTS_SHORT;
    if (errDeg >= BURST_MEDIUM_ANGLE_DEG) return BURST_SHOTS_MEDIUM;
    return BURST_SHOTS_LONG;
}
void TryForceTurretsTrackLockedTarget()
{
    if (!_locked || _targetEntityId == 0) return;
    for (int i = 0; i < _turrets.Count; i++)
    {
        var t = _turrets[i];
        if (t == null || t.Closed || !t.IsFunctional) continue;
        t.TrackTarget(_tPos, _tVel);
    }
}
bool ConfirmLock(double distance, out RaycastResultKind kind)
{
    kind = RaycastResultKind.CannotScan;
    if (_aimCam == null || _cams.Count == 0) { kind = RaycastResultKind.CannotScan; return false; }
    Vector3D camPos = _aimCam.WorldMatrix.Translation, toTarget = _tPos - camPos;
    if (toTarget.LengthSquared() < 1e-12) { kind = RaycastResultKind.None; return false; }
    var off = _lockPattern[_lockIndex];
    _lockIndex = (_lockIndex + 1) % _lockPattern.Count;
    MatrixD aimM = _aimCam.WorldMatrix;
    Vector3D baseDirWorld = Vector3D.Normalize(toTarget), baseDirLocal = Vector3D.TransformNormal(baseDirWorld, MatrixD.Transpose(aimM));
    double yawRad = off.YawDeg * Math.PI / 180.0, pitchRad = off.PitchDeg * Math.PI / 180.0;
    MatrixD yawM = MatrixD.CreateFromAxisAngle(Vector3D.Up, yawRad), pitchM = MatrixD.CreateFromAxisAngle(Vector3D.Right, pitchRad);
    Vector3D pertLocal = Vector3D.TransformNormal(baseDirLocal, pitchM * yawM), pertWorld = Vector3D.TransformNormal(pertLocal, aimM);
    RaycastResult best = new RaycastResult(RaycastResultKind.CannotScan, new MyDetectedEntityInfo());
    int attempts = CONFIRM_MAX_CAMERA_ATTEMPTS;
    if (attempts > _cams.Count) attempts = _cams.Count;
    for (int attempt = 0; attempt < attempts; attempt++)
    {
        int camIdx;
        var cam = _camScheduler.PickCameraThatCanScan(distance, out camIdx);
        if (cam == null)
        {
            best = new RaycastResult(RaycastResultKind.CannotScan, new MyDetectedEntityInfo());
            continue;
        }
        RaycastResult r = RaycastAlongDirection(cam, pertWorld, distance);
        best = r;
        if (r.Kind == RaycastResultKind.TargetEntity)
        {
            var info = r.Info;
            if (!IsEnemyLargeGrid(ref info)) { kind = RaycastResultKind.OtherEntity; return false; }
            UpdateTarget(ref info);
            kind = RaycastResultKind.TargetEntity;
            return true;
        }
    }
    kind = best.Kind;
    return false;
}
RaycastResult RaycastAlongDirection(IMyCameraBlock cam, Vector3D dirWorld, double distance)
{
    if (cam == null) return new RaycastResult(RaycastResultKind.CannotScan, new MyDetectedEntityInfo());
    if (!cam.CanScan(distance)) return new RaycastResult(RaycastResultKind.CannotScan, new MyDetectedEntityInfo());
    Vector3D origin = cam.WorldMatrix.Translation, targetPos = origin + Vector3D.Normalize(dirWorld) * distance;
    MyDetectedEntityInfo info = cam.Raycast(targetPos);
    if (info.IsEmpty()) return new RaycastResult(RaycastResultKind.None, info);
    if (info.Type == MyDetectedEntityType.Asteroid || info.Type == MyDetectedEntityType.Planet)
        return new RaycastResult(RaycastResultKind.ObstructedByVoxelOrPlanet, info);
    if (info.EntityId == Me.CubeGrid.EntityId) return new RaycastResult(RaycastResultKind.Self, info);
    if (info.EntityId == _targetEntityId) return new RaycastResult(RaycastResultKind.TargetEntity, info);
    return new RaycastResult(RaycastResultKind.OtherEntity, info);
}
Vector3D GetAimOriginWorld()
{
    if (_aimBlocks.Count > 0)
    {
        Vector3D sum = Vector3D.Zero;
        for (int i = 0; i < _aimBlocks.Count; i++) sum += _aimBlocks[i].GetPosition();
        return sum / _aimBlocks.Count;
    }
    var refBlock = GetAimReferenceBlock();
    if (refBlock != null) return refBlock.GetPosition();
    return _aimCam != null ? _aimCam.WorldMatrix.Translation : Me.GetPosition();
}
void UpdateTarget(ref MyDetectedEntityInfo info)
{
    Vector3D newPos = GetDetectedCenterWorld(ref info);
    double dtPos = _elapsedSec - _lastSeen;
    if (dtPos < 1e-6) dtPos = 0;
    Vector3D reportedVel = info.Velocity;
    if (dtPos > 1e-3)
    {
        Vector3D derivedVel = (newPos - _lastPos) / dtPos;
        if (reportedVel.LengthSquared() > 1e-6) _tVel = _tVel * 0.4 + reportedVel * 0.6;
        else _tVel = _tVel * 0.7 + derivedVel * 0.3;
    }
    else
    {
        if (reportedVel.LengthSquared() > 1e-6) _tVel = _tVel * 0.4 + reportedVel * 0.6;
    }
    double dtVel = _elapsedSec - _lastVelT;
    if (dtVel > 1e-3)
    {
        Vector3D dv = _tVel - _lastVel, aMeas = dv / dtVel;
        double maxA2 = MAX_TARGET_ACCEL_MPS2 * MAX_TARGET_ACCEL_MPS2;
        double aMeas2 = aMeas.LengthSquared();
        if (aMeas2 > maxA2 && aMeas2 > 1e-9) aMeas = Vector3D.Normalize(aMeas) * MAX_TARGET_ACCEL_MPS2;
        _tAcc = _tAcc * (1.0 - ACCEL_FILTER) + aMeas * ACCEL_FILTER;
        _lastVel = _tVel;
        _lastVelT = _elapsedSec;
    }
    else if (_lastVelT <= 0)
    {
        _lastVel = _tVel;
        _lastVelT = _elapsedSec;
    }
    _lastPos = newPos;
    _tPos = newPos;
    _lastSeen = _elapsedSec;
}
void PredictTarget()
{
    double dt = _elapsedSec - _lastSeen;
    if (dt < 0) dt = 0;
    _tPos = _lastPos + _tVel * dt + 0.5 * _tAcc * dt * dt;
}
Vector3D SolveLead(Vector3D origin, Vector3D targetPos, Vector3D targetVel, Vector3D targetAccel, Vector3D shooterVel, double projSpeed)
{
    Vector3D r0 = targetPos - origin;
    double range = r0.Length();
    if (range < 1e-6) return _aimCam.WorldMatrix.Forward;
    Vector3D v = targetVel - shooterVel, a = targetAccel;
    double a2 = a.LengthSquared(), maxA2 = MAX_TARGET_ACCEL_MPS2 * MAX_TARGET_ACCEL_MPS2;
    if (a2 > maxA2 && a2 > 1e-9) a = Vector3D.Normalize(a) * MAX_TARGET_ACCEL_MPS2;
    double t = range / projSpeed;
    if (t < 0.0) t = 0.0;
    if (t > 10.0) t = 10.0;
    for (int i = 0; i < ACCEL_SOLVE_ITERS; i++)
    {
        Vector3D p = r0 + v * t + 0.5 * a * t * t;
        double dist = p.Length();
        if (dist < 1e-6) break;
        double f = dist - projSpeed * t;
        Vector3D dpdt = v + a * t;
        double dfdt = Vector3D.Dot(p, dpdt) / dist - projSpeed;
        if (Math.Abs(dfdt) < 1e-6) break;
        double tNew = t - f / dfdt;
        if (tNew < 0.0) tNew = 0.0;
        if (tNew > 15.0) tNew = 15.0;
        if (Math.Abs(tNew - t) < 1e-4) { t = tNew; break; }
        t = tNew;
    }
    Vector3D pFinal = r0 + v * t + 0.5 * a * t * t;
    if (pFinal.LengthSquared() < 1e-12) pFinal = r0;
    return Vector3D.Normalize(pFinal);
}
void ClearGyros()
{
    _activeGyro = null;
    for (int i = 0; i < _gyros.Count; i++)
    {
        var g = _gyros[i];
        if (g == null) continue;
        g.GyroOverride = false;
        g.Pitch = 0f;
        g.Yaw = 0f;
        g.Roll = 0f;
    }
}
IMyGyro GetActiveGyro()
{
    if (_activeGyro != null && !_activeGyro.Closed && _activeGyro.IsFunctional)
        return _activeGyro;
    _activeGyro = null;
    for (int i = 0; i < _gyros.Count; i++)
    {
        var g = _gyros[i];
        if (g == null || g.Closed || !g.IsFunctional) continue;
        _activeGyro = g;
        break;
    }
    return _activeGyro;
}
static void BuildGyroTransform(MatrixD refWorld, MatrixD gyroWorld, out MatrixD t)
{
    t = MatrixD.Identity;
    t.M11 = refWorld.M11 * gyroWorld.M11 + refWorld.M12 * gyroWorld.M12 + refWorld.M13 * gyroWorld.M13;
    t.M12 = refWorld.M11 * gyroWorld.M21 + refWorld.M12 * gyroWorld.M22 + refWorld.M13 * gyroWorld.M23;
    t.M13 = refWorld.M11 * gyroWorld.M31 + refWorld.M12 * gyroWorld.M32 + refWorld.M13 * gyroWorld.M33;
    t.M21 = refWorld.M21 * gyroWorld.M11 + refWorld.M22 * gyroWorld.M12 + refWorld.M23 * gyroWorld.M13;
    t.M22 = refWorld.M21 * gyroWorld.M21 + refWorld.M22 * gyroWorld.M22 + refWorld.M23 * gyroWorld.M23;
    t.M23 = refWorld.M21 * gyroWorld.M31 + refWorld.M22 * gyroWorld.M32 + refWorld.M23 * gyroWorld.M33;
    t.M31 = refWorld.M31 * gyroWorld.M11 + refWorld.M32 * gyroWorld.M12 + refWorld.M33 * gyroWorld.M13;
    t.M32 = refWorld.M31 * gyroWorld.M21 + refWorld.M32 * gyroWorld.M22 + refWorld.M33 * gyroWorld.M23;
    t.M33 = refWorld.M31 * gyroWorld.M31 + refWorld.M32 * gyroWorld.M32 + refWorld.M33 * gyroWorld.M33;
}
double ComputeFacingKp(Vector3D desiredDirWorld)
{
    if (_aimRefBlock == null || desiredDirWorld.LengthSquared() < 1e-12) return KP_MIN;
    Vector3D desiredDir = Vector3D.Normalize(desiredDirWorld);
    double dot = Vector3D.Dot(_aimRefBlock.WorldMatrix.Forward, desiredDir);
    if (dot <= 0.0) return KP_MIN;
    double rampDot = Math.Cos(KP_EXP_RAMP_ANGLE_DEG * Math.PI / 180.0);
    double deadzoneDot = Math.Cos(KP_DEADZONE_ANGLE_DEG * Math.PI / 180.0);
    if (dot >= deadzoneDot) return KP_MAX;
    if (dot <= rampDot) return KP_MIN;
    double t = (dot - rampDot) / (deadzoneDot - rampDot);
    t = Clamp(t, 0.0, 1.0);
    double expNorm = (Math.Exp(KP_EXPONENT * t) - 1.0) / (Math.Exp(KP_EXPONENT) - 1.0);
    return KP_MIN + (KP_MAX - KP_MIN) * expNorm;
}
void AimAlongWorldDirection(Vector3D dirWorld)
{
    if (dirWorld.LengthSquared() < 1e-12) return;
    MatrixD refWorld = _aimRefBlock.WorldMatrix;
    Vector3D dirLocal = Vector3D.TransformNormal(Vector3D.Normalize(dirWorld), MatrixD.Transpose(refWorld));
    double yawErr = Math.Atan2(dirLocal.X, -dirLocal.Z);
    double pitchErr = Math.Atan2(dirLocal.Y, -dirLocal.Z);
    double dt = Runtime.TimeSinceLastRun.TotalSeconds;
    if (dt < 1e-4) dt = 1.0 / 60.0;
    double kp = ComputeFacingKp(dirWorld);
    _currentAimKp = kp;
    double errMag = Math.Sqrt(yawErr * yawErr + pitchErr * pitchErr);
    double nearAngle = KP_FULL_FACE_ANGLE_DEG * Math.PI / 180.0;
    double dampT = 1.0 - Clamp(errMag / nearAngle, 0.0, 1.0);
    double kd = KD_FAR + (KD_NEAR - KD_FAR) * dampT;
    double yawRate = (yawErr - _prevAimYaw) / dt;
    double pitchRate = (pitchErr - _prevAimPitch) / dt;
    _prevAimYaw = yawErr;
    _prevAimPitch = pitchErr;
    double yawCmd = yawErr * kp - yawRate * kd;
    double pitchCmd = pitchErr * kp - pitchRate * kd;
    ApplyGyros(pitchCmd, yawCmd, 0.0, refWorld);
}
void ApplyGyros(double pitch, double yaw, double roll, MatrixD refWorld)
{
    pitch = Clamp(pitch, -MAX_RATE, MAX_RATE);
    yaw = Clamp(yaw, -MAX_RATE, MAX_RATE);
    roll = Clamp(roll, -MAX_RATE, MAX_RATE);

    double userRoll = 0.0;
    if (_ctrl != null) userRoll = Clamp(_ctrl.RollIndicator * USER_ROLL_GAIN, -MAX_RATE, MAX_RATE);

    double p = -pitch;
    double y = yaw;
    double r = roll;

    MatrixD rollRefWorld = (_ctrl != null) ? _ctrl.WorldMatrix : refWorld;

    var active = GetActiveGyro();
    for (int i = 0; i < _gyros.Count; i++)
    {
        var g = _gyros[i];
        if (g == null || g.Closed || !g.IsFunctional) continue;
        if (active == null || g.EntityId != active.EntityId)
        {
            g.GyroOverride = false;
            g.Pitch = 0f;
            g.Yaw = 0f;
            g.Roll = 0f;
        }
    }
    if (active == null) return;

    MatrixD tAim;
    BuildGyroTransform(refWorld, active.WorldMatrix, out tAim);

    MatrixD tRoll;
    BuildGyroTransform(rollRefWorld, active.WorldMatrix, out tRoll);

    double gp = p * tAim.M11 + y * tAim.M21 + r * tAim.M31;
    double gy = p * tAim.M12 + y * tAim.M22 + r * tAim.M32;
    double gr = p * tAim.M13 + y * tAim.M23 + r * tAim.M33;

    gp += userRoll * tRoll.M31;
    gy += userRoll * tRoll.M32;
    gr += userRoll * tRoll.M33;

    active.Pitch = (float)Clamp(gp, -MAX_RATE, MAX_RATE);
    active.Yaw = (float)Clamp(gy, -MAX_RATE, MAX_RATE);
    active.Roll = (float)Clamp(gr, -MAX_RATE, MAX_RATE);
    active.GyroOverride = true;
}
double Clamp(double v, double lo, double hi) { if (v < lo) return lo; if (v > hi) return hi; return v; }
bool TryGetTarget(out Vector3D pos, out Vector3D vel)
{
    pos = Vector3D.Zero;
    vel = Vector3D.Zero;
    if (!_locked) return false;
    if ((_elapsedSec - _lastSeen) > TARGET_MEMORY_SEC) return false;
    PredictTarget();
    pos = _tPos;
    vel = _tVel;
    return true;
}
#endregion
#region HUD
string _hudLockLine = "Not Locked";
string _hudDistanceLine = "Distance: 0.0 m";
string _hudSpeedLine = "Tgt spd: 0.0 m/s";
string _hudKpLine = "KP: 0.00";
string _hudSalvoModeLine = "Salvo: Railgun Manual";
string _hudRelMsg = "NO TARGET";
Color _hudRelCol = Color.White;
float _hudRangeFrac = 0f;
Color _hudRangeFillColor = Color.White;
readonly string[] _hudMissileLabels = new string[8];
readonly Color[] _hudMissileLabelColors = new Color[8];
readonly bool[] _hudMissileLabelVisible = new bool[8];
int _hudMissileCount = 0;
const float BOX_SIZE_PX = 70f, BOX_THICKNESS_PX = 3f;
void DrawAllLcd()
{
    if (_lcdBlocks.Count == 0) return;
    ConfigureLcdSurfacesOnce();
    UpdateHudCache();
    for (int i = 0; i < _lcdBlocks.Count; i++)
    {
        var b = _lcdBlocks[i];
        if (b is IMyTextSurface) DrawToSurface((IMyTextSurface)b);
        else if (b is IMyTextSurfaceProvider)
        {
            var p = (IMyTextSurfaceProvider)b;
            for (int si = 0; si < p.SurfaceCount; si++) DrawToSurface(p.GetSurface(si));
        }
    }
}
void UpdateHudCache()
{
    _hudLockLine = _locked ? "Locked" : "Not Locked";
    _hudKpLine = "KP: " + _currentAimKp.ToString("0.00");
    _hudSalvoModeLine = "Salvo: " + GetSalvoModeLabel();
    double tgtSpeed = 0.0;
    double tgtDist = 0.0;
    var shipPos = _ctrl != null ? _ctrl.GetPosition() : Me.GetPosition();
    var shipVel = _ctrl != null ? _ctrl.GetShipVelocities().LinearVelocity : Vector3D.Zero;
    Vector3D relVel = Vector3D.Zero;
    double closingRate = 0.0;
    _hudRelMsg = "NO TARGET";
    _hudRelCol = Color.White;
    if (_locked && _targetEntityId != 0)
    {
        tgtSpeed = _tVel.Length();
        Vector3D toTarget = _tPos - shipPos;
        tgtDist = toTarget.Length();
        relVel = _tVel - shipVel;
        if (toTarget.LengthSquared() > 1e-9)
        {
            Vector3D losDir = Vector3D.Normalize(toTarget);
            closingRate = Vector3D.Dot(relVel, losDir);
        }
        double rateAbs = Math.Abs(closingRate);
        if (closingRate < 0.0)
        {
            _hudRelMsg = "TARGET CLOSING " + rateAbs.ToString("0.0") + " m/s";
            _hudRelCol = Color.Red;
        }
        else
        {
            _hudRelMsg = "TARGET MOVING AWAY " + rateAbs.ToString("0.0") + " m/s";
            _hudRelCol = new Color(0, 255, 0);
        }
    }
    _hudDistanceLine = "Distance: " + tgtDist.ToString("0.0") + " m";
    _hudSpeedLine = "Tgt spd: " + tgtSpeed.ToString("0.0") + " m/s";
    double frac = (TRACK_DISTANCE_M > 1e-6) ? (1.0 - (tgtDist / TRACK_DISTANCE_M)) : 0.0;
    _hudRangeFrac = (float)Clamp(frac, 0.0, 1.0);
    _hudRangeFillColor = (tgtDist > 0.0 && tgtDist <= 2000.0) ? new Color(0, 255, 0) : Color.White;
    _hudMissileCount = Math.Min(8, _mMissiles.Count);
    for (int i = 0; i < _hudMissileCount; i++)
    {
        _hudMissileLabelVisible[i] = false;
        _hudMissileLabels[i] = "";
        _hudMissileLabelColors[i] = Color.White;
        if (_locked)
        {
            string label;
            Color labelCol;
            if (_mMissiles[i].TryGetHudLabel(_tPos, out label, out labelCol))
            {
                _hudMissileLabelVisible[i] = true;
                _hudMissileLabels[i] = label;
                _hudMissileLabelColors[i] = labelCol;
            }
        }
    }
    for (int i = _hudMissileCount; i < 8; i++)
    {
        _hudMissileLabelVisible[i] = false;
        _hudMissileLabels[i] = "";
        _hudMissileLabelColors[i] = Color.White;
    }
}
void DrawMultiline(MySpriteDrawFrame frame, string text, Vector2 topLeft, float lineHeight, Color col, float scale)
{
    if (string.IsNullOrEmpty(text)) return;
    int start = 0;
    int len = text.Length;
    for (int i = 0; i <= len; i++)
    {
        bool end = (i == len);
        bool nl = (!end && text[i] == '\n');
        if (end || nl)
        {
            int count = i - start;
            if (count > 0)
            {
                string line = text.Substring(start, count);
                frame.Add(new MySprite(
                SpriteType.TEXT,
                line,
                topLeft,
                null,
                col,
                "Monospace",
                TextAlignment.LEFT,
                scale));
            }
            topLeft.Y += lineHeight;
            start = i + 1;
        }
    }
}
void DrawToSurface(IMyTextSurface s)
{
    var texSize = s.TextureSize;
    var surfSize = s.SurfaceSize;
    var offset = (texSize - surfSize) * 0.5f;
    var pink = new Color(255, 105, 180);
    var green = new Color(0, 255, 0);
    var red = new Color(255, 0, 0);
    var gray = new Color(120, 120, 120);
    using (var frame = s.DrawFrame())
    {
        var p = offset + new Vector2(10f, 10f);
        float scale = 0.55f;
        float lh = 22f;
        frame.Add(new MySprite(SpriteType.TEXT, "Status: " + _hudLockLine, p, null, Color.White, "Monospace", TextAlignment.LEFT, scale));
        p.Y += lh;
        frame.Add(new MySprite(SpriteType.TEXT, _hudDistanceLine, p, null, Color.White, "Monospace", TextAlignment.LEFT, scale));
        p.Y += lh;
        frame.Add(new MySprite(SpriteType.TEXT, _hudSpeedLine, p, null, Color.White, "Monospace", TextAlignment.LEFT, scale));
        p.Y += lh;
        frame.Add(new MySprite(SpriteType.TEXT, _hudKpLine, p, null, Color.White, "Monospace", TextAlignment.LEFT, scale));
        p.Y += lh;
        frame.Add(new MySprite(SpriteType.TEXT, _hudSalvoModeLine, p, null, Color.White, "Monospace", TextAlignment.LEFT, scale));
        p.Y += lh;
        {
            float y = surfSize.Y * 0.33f;
            var pos = offset + new Vector2(surfSize.X * 0.5f, y);
            frame.Add(new MySprite(SpriteType.TEXT, _hudRelMsg, pos, null, _hudRelCol, "Monospace", TextAlignment.CENTER, 0.85f));
        }
        {
            float barW = surfSize.X * 0.65f;
            if (barW > 420f) barW = 420f;
            if (barW < 220f) barW = 220f;
            float barH = 12f;
            float barY = surfSize.Y * (2f / 3f);
            var barPos = offset + new Vector2(surfSize.X * 0.5f, barY);
            frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", barPos, new Vector2(barW, barH), new Color(255, 255, 255, 40)));
            float fillW = barW * _hudRangeFrac;
            if (fillW > 0.5f)
            {
                var fillPos = new Vector2(barPos.X - (barW - fillW) * 0.5f, barPos.Y);
                frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", fillPos, new Vector2(fillW, barH), _hudRangeFillColor));
            }
        }
        {
            float size = 28f;
            float gap = 36f;
            float baseY = surfSize.Y * 0.45f;
            for (int i = 0; i < _hudMissileCount; i++)
            {
                var m = _mMissiles[i];
                bool left = i < 4;
                int row = left ? i : i - 4;
                float x = left ? 30f : surfSize.X - 30f;
                float y = baseY + row * gap;
                var pos = offset + new Vector2(x, y);
                Color col = gray;
                if (m.IsDetonated) col = red;
                else if (m.IsActive) col = green;
                frame.Add(new MySprite(SpriteType.TEXTURE, "Circle", pos, new Vector2(size, size), col));
                if (_hudMissileLabelVisible[i])
                {
                    var tpos = pos + new Vector2(left ? 22f : -22f, 0);
                    frame.Add(new MySprite(SpriteType.TEXT, _hudMissileLabels[i], tpos, null, _hudMissileLabelColors[i], "Monospace", TextAlignment.CENTER, 0.4f));
                }
            }
        }
        if (_pinCam != null)
        {
            Vector2 px;
            if (_locked)
            {
                if (TryProjectWorldToSurface(_pinCam, s, _tPos, out px))
                    DrawBox(frame, px, BOX_SIZE_PX, BOX_THICKNESS_PX, pink);
            }
            else if (_previewValid)
            {
                if (TryProjectWorldToSurface(_pinCam, s, _previewPos, out px))
                    DrawBox(frame, px, BOX_SIZE_PX, BOX_THICKNESS_PX, green);
            }
        }
    }
}
bool TryProjectWorldToSurface(IMyCameraBlock cam, IMyTextSurface s, Vector3D worldPos, out Vector2 px)
{
    px = Vector2.Zero;

    MatrixD wm = cam.WorldMatrix;
    Vector3D fromCam = worldPos - wm.Translation;
    if (fromCam.LengthSquared() < 1e-12) return false;

    Vector3D local = Vector3D.TransformNormal(fromCam, MatrixD.Transpose(wm));
    // In Space Engineers local "forward" is -Z, so depth is the negated Z axis.
    double depth = -local.Z;
    if (depth <= 1e-6) return false;

    var surfSize = s.SurfaceSize;
    var offset = (s.TextureSize - surfSize) * 0.5f;

    double aspect = (double)surfSize.X / surfSize.Y;
    double fovY = PINPOINT_CAM_FOV_Y_DEG * Math.PI / 180.0;
    double tanY = Math.Tan(fovY * 0.5);
    double tanX = tanY * aspect;

    double xN = (local.X / depth) / tanX;
    double yN = (local.Y / depth) / tanY;

    if (Math.Abs(xN) > 1.0 || Math.Abs(yN) > 1.0) return false;

    float xPx = (float)((xN * 0.5 + 0.5) * surfSize.X);
    float yPx = (float)((-yN * 0.5 + 0.5) * surfSize.Y);

    px = offset + new Vector2(xPx, yPx);
    return true;
}
void DrawBox(MySpriteDrawFrame frame, Vector2 center, float size, float thickness, Color col)
{
    float half = size * 0.5f;
    frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(center.X, center.Y - half), new Vector2(size, thickness), col));
    frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(center.X, center.Y + half), new Vector2(size, thickness), col));
    frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(center.X - half, center.Y), new Vector2(thickness, size), col));
    frame.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", new Vector2(center.X + half, center.Y), new Vector2(thickness, size), col));
}
#endregion
#region MissileLogic
const string M_TAG = "###";
const int M_IGNITE_DELAY_TICKS = 10;
const int M_GUIDE_DELAY_TICKS = 40;
const int M_MERGE_REENABLE_DELAY_TICKS = 120;
const int M_CONNECTOR_SCAN_PERIOD_TICKS = 10;
const double M_CONNECTOR_THROWOUT_DISTANCE = 30.0;
const double M_TARGET_VEL_BIAS_METERS = 25.0;
const double M_TARGET_VEL_BIAS_MIN_SPEED = 5.0;
const double M_GLOBAL_TIMESTEP = 1.0 / 60.0;
const double M_MAX_GYRO_RATE = 18.0;
const double M_GYRO_GAIN = 12.0;
const double M_DAMPING_GAIN = 0.25;
const double M_LEAD_TIME_MAX = 6.0;
const double M_PN_START_DISTANCE = 500.0;
const double M_PN_GAIN = 4.0;
const double M_PN_MIN_CLOSING = 5.0;
const double M_RAYCAST_RANGE = 5000.0;
const int M_LAUNCHER_DOCK_CHECK_PERIOD_TICKS = 10;
const int M_FAILSAFE_SCAN_PERIOD_TICKS = 30;
const double M_PLAYER_STREAM_LIMIT_M = 7000.0;
const double M_STREAM_MARGIN_M = 250.0;
const double M_HIT_RADIUS_M = 25.0;
const int M_HUD_PERSIST_TICKS = 120;
const int M_MAX_TRACKED_MISSILES = 8;
const double M_SPREAD_RADIUS_M = 80.0;
const double M_SPREAD_CONVERGE_DISTANCE = 900.0;
const double M_SPREAD_HARD_COLLAPSE_DISTANCE = 300.0;
const double M_CAMERA_FUSE_ARM_DISTANCE_M = 100.0;
readonly List<IMyShipMergeBlock> _mPendingLaunchMerges = new List<IMyShipMergeBlock>();
bool _mLaunchInProgress = false;
int _mLaunchTimer = 0;
const int M_LAUNCH_INTERVAL_TICKS = 1;
Vector3D _mLaunchBasisRight = Vector3D.Zero;
Vector3D _mLaunchBasisUp = Vector3D.Zero;
const double M_CAMERA_FUSE_DETONATE_DISTANCE_M = 5.0;
IMyShipController _mRef;
readonly List<Missile> _mMissiles = new List<Missile>();
readonly List<IMyTerminalBlock> _mTaggedAtLaunch = new List<IMyTerminalBlock>();
readonly List<IMyShipMergeBlock> _mMerges = new List<IMyShipMergeBlock>();
bool _mPendingMergeReenable = false;
int _mMergeReenableTimer = 0;
int _mConnectorScanTimer = 0;
int _mDockCheckTimer = 0;
int _mFailsafeScanTimer = 0;
bool _mLauncherDocked = true;
readonly List<IMyShipMergeBlock> _mLauncherMergesCached = new List<IMyShipMergeBlock>();
readonly List<IMyShipConnector> _mLauncherConnectorsCached = new List<IMyShipConnector>();
readonly List<IMyThrust> _mLauncherThrustersCached = new List<IMyThrust>();
readonly List<IMyGasTank> _mLauncherTanksCached = new List<IMyGasTank>();
readonly List<IMyBatteryBlock> _mLauncherBatteriesCached = new List<IMyBatteryBlock>();
readonly List<IMyTerminalBlock> _mTaggedAtLaunchCache = new List<IMyTerminalBlock>();
int _mCacheRefreshTimer = 0;
const int M_CACHE_REFRESH_PERIOD_TICKS = 600;
static class MUtil
{
    public static void DisableThruster(IMyThrust t)
    {
        if (t == null) return;
        t.ThrustOverridePercentage = 0f;
        t.ThrustOverride = 0f;
        t.Enabled = false;
        t.ApplyAction("OnOff_Off");
    }
    public static void ResetConnectorOff(IMyShipConnector c)
    {
        if (c == null) return;
        c.Disconnect();
        c.Enabled = false;
        c.ApplyAction("OnOff_Off");
        c.ThrowOut = false;
        c.CollectAll = false;
    }
    public static void ResetConnectorOn(IMyShipConnector c)
    {
        if (c == null) return;
        if (!c.Enabled)
        {
            c.Enabled = true;
            c.ApplyAction("OnOff_On");
        }
    }
}
void InitMissile()
{
    _mRef = GetReferenceController_M();
    _mDockCheckTimer = 0;
    _mFailsafeScanTimer = 0;
    _mConnectorScanTimer = 0;
    _mLauncherDocked = true;
    RefreshMissileLauncherCaches_M();
    _mCacheRefreshTimer = 0;
}
void RefreshMissileLauncherCaches_M()
{
    _mTaggedAtLaunchCache.Clear();
    GridTerminalSystem.GetBlocksOfType(_mTaggedAtLaunchCache, b =>
    b != null &&
    b.CustomName != null && b.CustomName.Contains(M_TAG));
    _mLauncherMergesCached.Clear();
    GridTerminalSystem.GetBlocksOfType(_mLauncherMergesCached, m =>
    m != null &&
    m.CustomName != null && m.CustomName.Contains(M_TAG) &&
    m.IsSameConstructAs(Me));
    _mLauncherConnectorsCached.Clear();
    GridTerminalSystem.GetBlocksOfType(_mLauncherConnectorsCached, c =>
    c != null &&
    c.CustomName != null && c.CustomName.Contains(M_TAG) &&
    c.IsSameConstructAs(Me));
    _mLauncherThrustersCached.Clear();
    GridTerminalSystem.GetBlocksOfType(_mLauncherThrustersCached, t =>
    t != null &&
    t.CustomName != null && t.CustomName.Contains(M_TAG) &&
    t.IsSameConstructAs(Me));
    _mLauncherTanksCached.Clear();
    GridTerminalSystem.GetBlocksOfType(_mLauncherTanksCached, t =>
    t != null &&
    t.CustomName != null && t.CustomName.Contains(M_TAG) &&
    t.IsSameConstructAs(Me));
    _mLauncherBatteriesCached.Clear();
    GridTerminalSystem.GetBlocksOfType(_mLauncherBatteriesCached, b =>
    b != null &&
    b.CustomName != null && b.CustomName.Contains(M_TAG) &&
    b.IsSameConstructAs(Me));
}
bool LauncherHasAnyEnabledMerges_M()
{
    for (int i = 0; i < _mLauncherMergesCached.Count; i++)
    {
        var m = _mLauncherMergesCached[i];
        if (m == null) continue;
        if (!m.IsSameConstructAs(Me)) continue;
        if (m.Enabled) return true;
    }
    return false;
}
void EnforceMaxTrackedMissiles_M()
{
    while (_mMissiles.Count > M_MAX_TRACKED_MISSILES)
    {
        int idx = -1;
        for (int i = 0; i < _mMissiles.Count; i++)
        {
            var m = _mMissiles[i];
            if (m == null) continue;
            if (m.IsDetachedFromLauncher()) { idx = i; break; }
        }
        if (idx < 0)
            break;
        _mMissiles[idx].ForceSelfDestruct();
        _mMissiles.RemoveAt(idx);
    }
}
void MissileSystemTick(string argument, UpdateType updateSource)
{
    if (_mRef == null || _mRef.CubeGrid.GetCubeBlock(_mRef.Position) == null)
        _mRef = GetReferenceController_M();
    if (!string.IsNullOrWhiteSpace(argument) && argument.Equals("Fire", StringComparison.OrdinalIgnoreCase))
        FireAllMissiles_M();
    if (_mPendingMergeReenable)
    {
        _mMergeReenableTimer++;
        if (_mMergeReenableTimer >= M_MERGE_REENABLE_DELAY_TICKS)
        {
            ReenableLauncherMerges_M();
            _mPendingMergeReenable = false;
        }
    }
    _mCacheRefreshTimer++;
    if (_mCacheRefreshTimer >= M_CACHE_REFRESH_PERIOD_TICKS)
    {
        _mCacheRefreshTimer = 0;
        RefreshMissileLauncherCaches_M();
    }
    _mDockCheckTimer++;
    if (_mDockCheckTimer >= M_LAUNCHER_DOCK_CHECK_PERIOD_TICKS)
    {
        _mDockCheckTimer = 0;
        _mLauncherDocked = LauncherHasAnyEnabledMerges_M();
        if (!_mLauncherDocked)
        {
            _mFailsafeScanTimer = 0;
            _mConnectorScanTimer = 0;
        }
    }
    if (_mLauncherDocked)
    {
        _mFailsafeScanTimer++;
        if (_mFailsafeScanTimer >= M_FAILSAFE_SCAN_PERIOD_TICKS)
        {
            _mFailsafeScanTimer = 0;
            FailsafeDisableDockedMissileThrusters_M();
        }
        _mConnectorScanTimer++;
        if (_mConnectorScanTimer >= M_CONNECTOR_SCAN_PERIOD_TICKS)
        {
            _mConnectorScanTimer = 0;
            AutoLockReadyConnectors_M();
        }
    }

    ProcessPendingMissileLaunches_M();

    Vector3D targetPos;
    Vector3D targetVel;
    if (!TryGetTarget(out targetPos, out targetVel))
    {
        targetPos = ForwardAimPoint_M();
        targetVel = Vector3D.Zero;
    }
    Vector3D launcherPos = _mRef != null ? _mRef.GetPosition() : Me.GetPosition();
    Vector3D gravity = _mRef != null ? _mRef.GetNaturalGravity() : Vector3D.Zero;
    double dt = Runtime.TimeSinceLastRun.TotalSeconds;
    if (dt < 1.0 / 120.0) dt = 1.0 / 120.0;
    if (dt > 1.0 / 20.0) dt = 1.0 / 20.0;
    for (int i = _mMissiles.Count - 1; i >= 0; i--)
    {
        var m = _mMissiles[i];
        m.Tick(targetPos, targetVel, gravity, launcherPos, dt);
        if (m.ShouldDispose())
            _mMissiles.RemoveAt(i);
    }
    EnforceMaxTrackedMissiles_M();
}
void ProcessPendingMissileLaunches_M()
{
    if (!_mLaunchInProgress) return;
    if (_mPendingLaunchMerges.Count == 0)
    {
        _mLaunchInProgress = false;
        _mPendingMergeReenable = true;
        _mMergeReenableTimer = 0;
        _mLauncherDocked = false;
        _mDockCheckTimer = 0;
        _mFailsafeScanTimer = 0;
        _mConnectorScanTimer = 0;
        return;
    }

    _mLaunchTimer++;
    if (_mLaunchTimer < M_LAUNCH_INTERVAL_TICKS)
        return;
    _mLaunchTimer = 0;

    var merge = _mPendingLaunchMerges[0];
    _mPendingLaunchMerges.RemoveAt(0);
    if (merge == null || merge.CubeGrid.GetCubeBlock(merge.Position) == null)
        return;

    var view = new List<IMyTerminalBlock>(_mTaggedAtLaunch);
    int stagger = _mMissiles.Count & 1;
    int spreadIndex = _mMissiles.Count & 7;
    _mMissiles.Add(new Missile(Me, merge, view, stagger, spreadIndex, _mLaunchBasisRight, _mLaunchBasisUp));
    EnforceMaxTrackedMissiles_M();

    merge.Enabled = false;
    merge.ApplyAction("OnOff_Off");

    if (_mPendingLaunchMerges.Count == 0)
    {
        _mLaunchInProgress = false;
        _mPendingMergeReenable = true;
        _mMergeReenableTimer = 0;
        _mLauncherDocked = false;
        _mDockCheckTimer = 0;
        _mFailsafeScanTimer = 0;
        _mConnectorScanTimer = 0;
    }
}

void DisableLauncherConnectorsForLaunch_M()
{
    for (int i = 0; i < _mLauncherConnectorsCached.Count; i++)
        MUtil.ResetConnectorOff(_mLauncherConnectorsCached[i]);
}
void FailsafeDisableDockedMissileThrusters_M()
{
    if (!LauncherHasAnyEnabledMerges_M())
        return;
    for (int i = 0; i < _mLauncherThrustersCached.Count; i++)
    {
        var t = _mLauncherThrustersCached[i];
        if (t == null) continue;
        if (!t.IsSameConstructAs(Me)) continue;
        MUtil.DisableThruster(t);
    }
}
Vector3D ForwardAimPoint_M()
{
    if (_mRef != null)
        return _mRef.GetPosition() + _mRef.WorldMatrix.Forward * M_RAYCAST_RANGE;
    return Me.GetPosition() + Me.WorldMatrix.Forward * M_RAYCAST_RANGE;
}
void AutoLockReadyConnectors_M()
{
    for (int i = 0; i < _mLauncherConnectorsCached.Count; i++)
    {
        var c = _mLauncherConnectorsCached[i];
        if (c == null) continue;
        if (!c.IsSameConstructAs(Me))
            continue;
        MUtil.ResetConnectorOn(c);
        if (c.Status == MyShipConnectorStatus.Connectable)
            c.Connect();
    }
}
void FireAllMissiles_M()
{
    if (_mLaunchInProgress) return;
    if (_mTaggedAtLaunchCache.Count == 0 || _mLauncherMergesCached.Count == 0)
        RefreshMissileLauncherCaches_M();

    _mTaggedAtLaunch.Clear();
    for (int i = 0; i < _mTaggedAtLaunchCache.Count; i++)
    {
        var b = _mTaggedAtLaunchCache[i];
        if (b == null) continue;
        if (b.CubeGrid == null) continue;
        _mTaggedAtLaunch.Add(b);
    }

    for (int i = 0; i < _mLauncherTanksCached.Count; i++)
    {
        var t = _mLauncherTanksCached[i];
        if (t == null) continue;
        t.Stockpile = false;
    }

    for (int i = 0; i < _mLauncherBatteriesCached.Count; i++)
    {
        var b = _mLauncherBatteriesCached[i];
        if (b == null) continue;
        b.ChargeMode = ChargeMode.Discharge;
    }

    _mPendingLaunchMerges.Clear();
    for (int i = 0; i < _mLauncherMergesCached.Count; i++)
    {
        var m = _mLauncherMergesCached[i];
        if (m == null) continue;
        if (!m.IsSameConstructAs(Me)) continue;
        if (m.CubeGrid.GetCubeBlock(m.Position) == null) continue;
        _mPendingLaunchMerges.Add(m);
    }

    if (_mPendingLaunchMerges.Count == 0)
        return;

    _mLaunchBasisRight = (_mRef != null) ? _mRef.WorldMatrix.Right : Me.WorldMatrix.Right;
    _mLaunchBasisUp = (_mRef != null) ? _mRef.WorldMatrix.Up : Me.WorldMatrix.Up;

    DisableLauncherConnectorsForLaunch_M();

    _mLaunchInProgress = true;
    _mLaunchTimer = 0;
}
void ReenableLauncherMerges_M()
{
    for (int i = 0; i < _mLauncherMergesCached.Count; i++)
    {
        var m = _mLauncherMergesCached[i];
        if (m == null) continue;
        if (!m.IsSameConstructAs(Me)) continue;
        m.Enabled = true;
        m.ApplyAction("OnOff_On");
    }
    _mLauncherDocked = true;
    _mDockCheckTimer = 0;
}
IMyShipController GetReferenceController_M()
{
    var ctrls = new List<IMyShipController>();
    GridTerminalSystem.GetBlocksOfType(ctrls, c => c.IsSameConstructAs(Me));
    if (ctrls.Count == 0) return null;
    IMyShipController main = null;
    for (int i = 0; i < ctrls.Count; i++)
    {
        if (ctrls[i].IsMainCockpit) { main = ctrls[i]; break; }
    }
    return main ?? ctrls[0];
}
class Missile
{
    readonly IMyTerminalBlock _launcherMe;
    readonly IMyShipMergeBlock _merge;
    readonly List<IMyTerminalBlock> _viewAtLaunch;
    readonly int _spreadIndex;
    readonly Vector3D _basisRight;
    readonly Vector3D _basisUp;
    readonly int _stagger;
    IMyThrust _refThrust;
    public bool IsActive => _active && !_detonateTriggered;
    public bool IsDetonated => _detonateTriggered;
    public Vector3D Position =>
    (Gyro != null) ? Gyro.GetPosition() :
    (_merge != null ? _merge.GetPosition() : Vector3D.Zero);
    public double Speed => _misVel.Length();
    public IMyGyro Gyro;
    public readonly List<IMyThrust> Thrusters = new List<IMyThrust>();
    public readonly List<IMyShipConnector> Connectors = new List<IMyShipConnector>();
    public readonly List<IMyGasTank> Tanks = new List<IMyGasTank>();
    public readonly List<IMyBatteryBlock> Batteries = new List<IMyBatteryBlock>();
    public readonly List<IMyWarhead> Warheads = new List<IMyWarhead>();
    public readonly List<IMySensorBlock> Sensors = new List<IMySensorBlock>();
    readonly List<MyDetectedEntityInfo> _sensorDetectedScratch = new List<MyDetectedEntityInfo>(16);
    IMyCameraBlock _fuseCam;
    Vector3D _misPrevPos;
    Vector3D _misVel;
    double _prevYaw;
    double _prevPitch;
    bool _hasPnHistory;
    Vector3D _pnPrevMisPos;
    Vector3D _pnPrevTgtPos;
    int _ticks;
    bool _initialized;
    bool _ignited;
    bool _active;
    bool _throwOutTriggered;
    bool _detonateTriggered;
    bool _armedBySensor;
    bool _payloadEmpty;
    bool _launchedWithPayload;
    enum HudState { Normal, Hit, Lost }
    HudState _hudState = HudState.Normal;
    int _hudPersistTicksLeft = 0;
    bool _impactOrLostLatched = false;
    double _lastDistToTarget = double.PositiveInfinity;
    double _lastDistToLauncher = double.PositiveInfinity;
    double _minDistToTarget = double.PositiveInfinity;
    public Missile(IMyTerminalBlock launcherMe, IMyShipMergeBlock merge, List<IMyTerminalBlock> viewAtLaunch, int stagger, int spreadIndex, Vector3D basisRight, Vector3D basisUp)
    {
        _launcherMe = launcherMe;
        _merge = merge;
        _viewAtLaunch = viewAtLaunch;
        _misPrevPos = Vector3D.Zero;
        _misVel = Vector3D.Zero;
        _stagger = stagger & 1;
        _spreadIndex = spreadIndex & 7;
        _basisRight = basisRight;
        _basisUp = basisUp;
    }
    public bool IsDetachedFromLauncher()
    {
        if (_merge == null) return true;
        if (_launcherMe == null) return true;
        return !_merge.IsSameConstructAs(_launcherMe);
    }
    public void ForceSelfDestruct()
    {
        if (_detonateTriggered) return;
        if (!IsDetachedFromLauncher()) return;
        _detonateTriggered = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].IsArmed = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].Detonate();
        if (!_impactOrLostLatched)
            LatchLost();
    }
    void TriggerDetonationAndLatchHit()
    {
        if (_detonateTriggered) return;
        _detonateTriggered = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].IsArmed = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].Detonate();
        if (!_impactOrLostLatched)
            LatchHit();
    }
    bool TryCameraFuseDetonate()
    {
        if (_detonateTriggered) return false;
        if (_fuseCam == null || _fuseCam.Closed) return false;
        if (double.IsInfinity(_lastDistToTarget) || double.IsNaN(_lastDistToTarget)) return false;
        if (_lastDistToTarget > M_CAMERA_FUSE_ARM_DISTANCE_M) return false;
        double d = M_CAMERA_FUSE_DETONATE_DISTANCE_M;
        if (!_fuseCam.CanScan(d)) return false;
        MyDetectedEntityInfo info = _fuseCam.Raycast(d);
        if (info.IsEmpty()) return false;
        if (info.Type != MyDetectedEntityType.LargeGrid && info.Type != MyDetectedEntityType.SmallGrid)
            return false;
        long myGridId = (_merge != null && _merge.CubeGrid != null) ? _merge.CubeGrid.EntityId : 0;
        if (myGridId != 0 && info.EntityId == myGridId)
            return false;
        if (info.Relationship != MyRelationsBetweenPlayerAndBlock.Enemies)
            return false;
        TriggerDetonationAndLatchHit();
        return true;
    }
    public void Tick(Vector3D targetPos, Vector3D targetVel, Vector3D gravity, Vector3D launcherPos, double dt)
    {
        if (_hudPersistTicksLeft > 0)
        {
            _hudPersistTicksLeft--;
            return;
        }
        if (_merge != null && _merge.IsSameConstructAs(_launcherMe))
            return;
        UpdateLastKnownDistances(targetPos, launcherPos);
        if (!_initialized)
        {
            InitialiseAfterDetach();
            return;
        }
        if (TryCameraFuseDetonate())
            return;
        if (!_detonateTriggered && _lastDistToTarget <= M_HIT_RADIUS_M)
        {
            TriggerDetonationAndLatchHit();
            return;
        }
        _ticks++;
        if (_ticks > M_IGNITE_DELAY_TICKS && !_ignited)
            Ignite();
        HandleTerminalRange(targetPos);
        HandleSensorFuse(targetPos, launcherPos);
        if (!_impactOrLostLatched)
        {
            double streamEdge = M_PLAYER_STREAM_LIMIT_M - M_STREAM_MARGIN_M;
            if (_lastDistToLauncher > streamEdge && _lastDistToTarget > streamEdge)
                LatchLost();
        }
        if (_ticks < M_GUIDE_DELAY_TICKS || !_active)
        {
            UpdateKinematicsOnly(dt);
            return;
        }
        if (((_ticks + _stagger) & 1) == 0)
            RunGuidance(targetPos, targetVel, gravity, dt);
        UpdateKinematicsOnly(dt);
    }
    void UpdateLastKnownDistances(Vector3D targetPos, Vector3D launcherPos)
    {
        Vector3D misPos = Position;
        if (misPos == Vector3D.Zero) return;
        _lastDistToTarget = Vector3D.Distance(misPos, targetPos);
        _lastDistToLauncher = Vector3D.Distance(misPos, launcherPos);
        if (_lastDistToTarget < _minDistToTarget)
            _minDistToTarget = _lastDistToTarget;
    }
    void LatchHit()
    {
        _impactOrLostLatched = true;
        _hudState = HudState.Hit;
        _hudPersistTicksLeft = M_HUD_PERSIST_TICKS;
    }
    void LatchLost()
    {
        _impactOrLostLatched = true;
        _hudState = HudState.Lost;
        _hudPersistTicksLeft = M_HUD_PERSIST_TICKS;
    }
    public bool TryGetHudLabel(Vector3D targetPos, out string label, out Color labelCol)
    {
        label = "";
        labelCol = Color.White;
        if (_hudState == HudState.Hit)
        {
            label = "HIT";
            labelCol = Color.Red;
            return true;
        }
        if (_hudState == HudState.Lost)
        {
            label = "LOST";
            labelCol = new Color(0, 128, 255);
            return true;
        }
        if (double.IsInfinity(_lastDistToTarget) || double.IsNaN(_lastDistToTarget))
            return false;
        label = ((double)_lastDistToTarget).ToString("0");
        labelCol = Color.White;
        return true;
    }
    void InitialiseAfterDetach()
    {
        if (_merge == null || _merge.CubeGrid.GetCubeBlock(_merge.Position) == null) return;
        var localGrid = _merge.CubeGrid;
        _viewAtLaunch.RemoveAll(b => b == null || b.CubeGrid != localGrid);
        Thrusters.Clear();
        Connectors.Clear();
        Tanks.Clear();
        Batteries.Clear();
        Warheads.Clear();
        Sensors.Clear();
        Gyro = null;
        _refThrust = null;
        _fuseCam = null;
        for (int i = 0; i < _viewAtLaunch.Count; i++)
        {
            var b = _viewAtLaunch[i];
            var fb = b as IMyFunctionalBlock;
            if (fb != null) fb.Enabled = true;
            var g = b as IMyGyro;
            if (g != null && Gyro == null) Gyro = g;
            var t = b as IMyThrust;
            if (t != null) Thrusters.Add(t);
            var c = b as IMyShipConnector;
            if (c != null) Connectors.Add(c);
            var tank = b as IMyGasTank;
            if (tank != null) Tanks.Add(tank);
            var bat = b as IMyBatteryBlock;
            if (bat != null) Batteries.Add(bat);
            var wh = b as IMyWarhead;
            if (wh != null) Warheads.Add(wh);
            var s = b as IMySensorBlock;
            if (s != null) Sensors.Add(s);
            var cam = b as IMyCameraBlock;
            if (cam != null && _fuseCam == null)
            {
                cam.EnableRaycast = true;
                _fuseCam = cam;
            }
        }
        for (int i = 0; i < Tanks.Count; i++)
            Tanks[i].Stockpile = false;
        for (int i = 0; i < Batteries.Count; i++)
            Batteries[i].ChargeMode = ChargeMode.Discharge;
        for (int i = 0; i < Thrusters.Count; i++)
            MUtil.DisableThruster(Thrusters[i]);
        for (int i = 0; i < Connectors.Count; i++)
            MUtil.ResetConnectorOff(Connectors[i]);
        for (int i = 0; i < Warheads.Count; i++)
            Warheads[i].IsArmed = false;
        ChooseDominantThrustAxis();
        _misPrevPos = (Gyro != null) ? Gyro.GetPosition() : _merge.GetPosition();
        _misVel = Vector3D.Zero;
        _hasPnHistory = false;
        _armedBySensor = false;
        _payloadEmpty = false;
        _launchedWithPayload = !IsPayloadConnectorInventoryEmpty();
        _minDistToTarget = double.PositiveInfinity;
        _initialized = true;
        _active = (Gyro != null && _refThrust != null && Thrusters.Count > 0);
    }
    void HandleTerminalRange(Vector3D targetPos)
    {
        Vector3D misPos = (Gyro != null) ? Gyro.GetPosition() : _merge.GetPosition();
        double dist = Vector3D.Distance(misPos, targetPos);
        if (!_throwOutTriggered && dist <= M_CONNECTOR_THROWOUT_DISTANCE)
        {
            _throwOutTriggered = true;
            for (int i = 0; i < Connectors.Count; i++)
            {
                var c = Connectors[i];
                if (c == null) continue;
                c.Disconnect();
                c.Enabled = true;
                c.ApplyAction("OnOff_On");
                c.CollectAll = false;
                c.ThrowOut = true;
            }
        }
    }
    void HandleSensorFuse(Vector3D targetPos, Vector3D launcherPos)
    {
        if (_detonateTriggered) return;
        if (!_throwOutTriggered) return;
        bool enemyDetectedInSensor = false;
        bool enemyWithinSensorFuseDistance = false;
        Vector3D missilePos = Position;
        if (Sensors.Count > 0)
        {
            for (int si = 0; si < Sensors.Count; si++)
            {
                var s = Sensors[si];
                if (s == null) continue;
                if (!s.IsActive) continue;
                _sensorDetectedScratch.Clear();
                s.DetectedEntities(_sensorDetectedScratch);
                for (int i = 0; i < _sensorDetectedScratch.Count; i++)
                {
                    var info = _sensorDetectedScratch[i];
                    if (info.IsEmpty()) continue;
                    if (info.Type != MyDetectedEntityType.LargeGrid && info.Type != MyDetectedEntityType.SmallGrid)
                        continue;
                    if (info.Relationship != MyRelationsBetweenPlayerAndBlock.Enemies)
                        continue;
                    enemyDetectedInSensor = true;
                    if (missilePos != Vector3D.Zero && Vector3D.Distance(missilePos, info.Position) <= M_CAMERA_FUSE_DETONATE_DISTANCE_M)
                        enemyWithinSensorFuseDistance = true;
                }
                if (enemyWithinSensorFuseDistance) break;
            }
        }
        if (enemyDetectedInSensor)
            _armedBySensor = true;
        if (!_armedBySensor) return;
        if (!_launchedWithPayload)
        {
            if (!enemyWithinSensorFuseDistance) return;
        }
        else if (!_payloadEmpty)
        {
            if (IsPayloadConnectorInventoryEmpty())
                _payloadEmpty = true;
            else
                return;
        }
        _detonateTriggered = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].IsArmed = true;
        for (int w = 0; w < Warheads.Count; w++)
            Warheads[w].Detonate();
        if (!_impactOrLostLatched)
            LatchHit();
    }
    bool IsPayloadConnectorInventoryEmpty()
    {
        if (Connectors.Count == 0) return true;
        var c = Connectors[0];
        if (c == null || c.CubeGrid.GetCubeBlock(c.Position) == null) return true;
        var inv = c.GetInventory();
        if (inv == null) return true;
        return inv.ItemCount == 0;
    }
    void ChooseDominantThrustAxis()
    {
        if (Thrusters.Count == 0) return;
        var sums = new Dictionary<Vector3D, double>();
        for (int i = 0; i < Thrusters.Count; i++)
        {
            var t = Thrusters[i];
            Vector3D fwd = t.WorldMatrix.Forward;
            double val = t.MaxEffectiveThrust;
            double cur;
            if (!sums.TryGetValue(fwd, out cur)) cur = 0;
            sums[fwd] = cur + val;
        }
        Vector3D dominant = Vector3D.Zero;
        double best = -1;
        foreach (var kv in sums)
        {
            if (kv.Value > best) { best = kv.Value; dominant = kv.Key; }
        }
        for (int i = Thrusters.Count - 1; i >= 0; i--)
        {
            if (Thrusters[i].WorldMatrix.Forward != dominant)
                Thrusters.RemoveAt(i);
        }
        _refThrust = (Thrusters.Count > 0) ? Thrusters[0] : null;
    }
    void Ignite()
    {
        for (int i = 0; i < Tanks.Count; i++)
            Tanks[i].Stockpile = false;
        for (int i = 0; i < Thrusters.Count; i++)
        {
            var t = Thrusters[i];
            t.Enabled = true;
            t.ApplyAction("OnOff_On");
            t.ThrustOverridePercentage = 1f;
        }
        _ignited = true;
    }
    Vector3D ApplyTargetVelocityBias(Vector3D targetPos, Vector3D targetVel)
    {
        double spd = targetVel.Length();
        if (spd < M_TARGET_VEL_BIAS_MIN_SPEED) return targetPos;
        Vector3D dir = targetVel / spd;
        return targetPos + dir * M_TARGET_VEL_BIAS_METERS;
    }
    Vector3D GetSpreadOffsetWorld()
    {
        if (_spreadIndex == 0) return Vector3D.Zero;
        double d = _minDistToTarget;
        if (double.IsInfinity(d) || double.IsNaN(d)) return Vector3D.Zero;
        if (d <= M_SPREAD_HARD_COLLAPSE_DISTANCE)
            return Vector3D.Zero;
        double k = d / M_SPREAD_CONVERGE_DISTANCE;
        if (k < 0) k = 0;
        if (k > 1) k = 1;
        int ringSlot = _spreadIndex - 1;
        double ang = (2.0 * Math.PI) * (ringSlot / 7.0);
        double x = Math.Cos(ang) * M_SPREAD_RADIUS_M;
        double y = Math.Sin(ang) * M_SPREAD_RADIUS_M;
        return (_basisRight * x + _basisUp * y) * k;
    }
    void RunGuidance(Vector3D targetPosRaw, Vector3D targetVel, Vector3D gravity, double dt)
    {
        if (Gyro == null || _refThrust == null) return;
        if (dt <= 1e-6) dt = 1.0 / 60.0;
        Vector3D misPos = Gyro.GetPosition();
        Vector3D targetPos = ApplyTargetVelocityBias(targetPosRaw, targetVel);
        Vector3D toTargetBase = targetPos - misPos;
        if (toTargetBase.LengthSquared() < 1) return;
        targetPos += GetSpreadOffsetWorld();
        Vector3D toTarget = targetPos - misPos;
        double distSq = toTarget.LengthSquared();
        if (distSq < 1) return;
        double dist = Math.Sqrt(distSq);
        Vector3D desiredDir;
        if (dist <= M_PN_START_DISTANCE)
        {
            if (!_hasPnHistory)
            {
                _pnPrevMisPos = misPos;
                _pnPrevTgtPos = targetPos;
                _hasPnHistory = true;
                desiredDir = Vector3D.Normalize(toTarget);
            }
            else
            {
                Vector3D losOld = _pnPrevTgtPos - _pnPrevMisPos;
                Vector3D losNew = targetPos - misPos;
                if (losOld.LengthSquared() < 1 || losNew.LengthSquared() < 1)
                {
                    desiredDir = Vector3D.Normalize(toTarget);
                }
                else
                {
                    losOld = Vector3D.Normalize(losOld);
                    losNew = Vector3D.Normalize(losNew);
                    Vector3D losDelta = (losNew - losOld) / dt;
                    Vector3D relVel = targetVel - _misVel;
                    double vClosing = relVel.Length();
                    if (vClosing < M_PN_MIN_CLOSING) vClosing = M_PN_MIN_CLOSING;
                    Vector3D lateral = losDelta * (M_PN_GAIN / vClosing);
                    Vector3D steerVec = losNew + lateral;
                    if (steerVec.LengthSquared() < 1e-6)
                        desiredDir = losNew;
                    else
                        desiredDir = Vector3D.Normalize(steerVec);
                }
                _pnPrevMisPos = misPos;
                _pnPrevTgtPos = targetPos;
            }
        }
        else
        {
            double speed = _misVel.Length();
            if (speed < 10) speed = 100;
            double leadT = dist / speed;
            if (leadT > M_LEAD_TIME_MAX) leadT = M_LEAD_TIME_MAX;
            if (leadT < 0) leadT = 0;
            Vector3D aimPoint = targetPos + targetVel * leadT;
            Vector3D desired = aimPoint - misPos;
            if (desired.LengthSquared() < 1)
                desiredDir = Vector3D.Normalize(toTarget);
            else
                desiredDir = Vector3D.Normalize(desired);
            _hasPnHistory = false;
        }
        Vector3D finalDir = desiredDir;
        if (gravity.LengthSquared() > 0)
        {
            Vector3D compensated = desiredDir - gravity;
            if (compensated.LengthSquared() > 1e-6)
                finalDir = Vector3D.Normalize(compensated);
        }
        Vector3D missileForward = _refThrust.WorldMatrix.Backward;
        Vector3D missileUp = _refThrust.WorldMatrix.Up;
        GyroTurn(finalDir, missileForward, missileUp, Gyro, ref _prevPitch, ref _prevYaw, dt);
    }
    void UpdateKinematicsOnly(double dt)
    {
        if (dt <= 1e-6) dt = 1.0 / 60.0;
        Vector3D pos = (Gyro != null) ? Gyro.GetPosition() : (_merge != null ? _merge.GetPosition() : Vector3D.Zero);
        if (pos == Vector3D.Zero) return;
        _misVel = (pos - _misPrevPos) / dt;
        _misPrevPos = pos;
    }
    void GyroTurn(Vector3D targetDirWorld, Vector3D refForwardWorld, Vector3D refUpWorld, IMyGyro gyro, ref double prevPitch, ref double prevYaw, double dt)
    {
        if (dt <= 1e-6) dt = 1.0 / 60.0;
        Quaternion q = Quaternion.CreateFromForwardUp((Vector3)refForwardWorld, (Vector3)refUpWorld);
        Quaternion inv = Quaternion.Inverse(q);
        Vector3D localTarget = Vector3D.Transform(targetDirWorld, inv);
        double az, el;
        Vector3D.GetAzimuthAndElevation(localTarget, out az, out el);
        double yaw = az;
        double pitch = el;
        yaw += M_DAMPING_GAIN * ((yaw - prevYaw) / dt);
        pitch += M_DAMPING_GAIN * ((pitch - prevPitch) / dt);
        prevYaw = az;
        prevPitch = el;
        MatrixD refMatrix = MatrixD.CreateWorld(Vector3D.Zero, refForwardWorld, refUpWorld).GetOrientation();
        Vector3 v = Vector3.Transform(new Vector3((float)pitch, (float)yaw, 0f), refMatrix);
        Vector3 gv = Vector3.Transform(v, Matrix.Transpose(gyro.WorldMatrix.GetOrientation()));
        if (double.IsNaN(gv.X) || double.IsNaN(gv.Y) || double.IsNaN(gv.Z)) return;
        gyro.GyroOverride = true;
        gyro.Pitch = (float)MathHelper.Clamp(-gv.X * M_GYRO_GAIN, -M_MAX_GYRO_RATE, M_MAX_GYRO_RATE);
        gyro.Yaw = (float)MathHelper.Clamp(-gv.Y * M_GYRO_GAIN, -M_MAX_GYRO_RATE, M_MAX_GYRO_RATE);
        gyro.Roll = (float)MathHelper.Clamp(-gv.Z * M_GYRO_GAIN, -M_MAX_GYRO_RATE, M_MAX_GYRO_RATE);
    }
    bool IsStillValidBlocks()
    {
        if (_merge == null || _merge.CubeGrid.GetCubeBlock(_merge.Position) == null) return false;
        if (!_active) return false;
        if (Gyro == null || Gyro.CubeGrid.GetCubeBlock(Gyro.Position) == null) return false;
        if (_refThrust == null || _refThrust.CubeGrid.GetCubeBlock(_refThrust.Position) == null) return false;
        return true;
    }
    public bool ShouldDispose()
    {
        if (!_initialized) return false;
        if (_hudPersistTicksLeft > 0)
            return false;
        bool valid = IsStillValidBlocks();
        if (!valid && !_impactOrLostLatched)
        {
            if (_lastDistToTarget <= M_HIT_RADIUS_M)
            {
                TriggerDetonationAndLatchHit();
                return false;
            }
            else
            {
                LatchLost();
                return false;
            }
        }
        if (!valid)
            return true;
        return false;
    }
}
#endregion#region HUD
#region SalvoModes
const int _salvoRefreshTicks = 30;
uint _salvoNextRefreshTick;
readonly List<IMyBlockGroup> _salvoBG = new List<IMyBlockGroup>();
readonly List<SalvoGroup> _salvos = new List<SalvoGroup>();
enum SalvoMode
{
    Manual,
    Burst
}
void InitSalvo()
{
    _salvoNextRefreshTick = 0;
    SalvoRefresh();
}
void SalvoTick(string argument, UpdateType updateSource)
{
    if (_tick >= _salvoNextRefreshTick)
    {
        _salvoNextRefreshTick = _tick + (uint)_salvoRefreshTicks;
        SalvoRefresh();
    }
    if ((updateSource & (UpdateType.Terminal | UpdateType.Trigger | UpdateType.Script)) != 0 && !string.IsNullOrWhiteSpace(argument))
    {
        var a = argument.Trim();
        if (a.Equals("Salvo", StringComparison.OrdinalIgnoreCase))
            SalvoCycleMode();
    }
    for (int i = 0; i < _salvos.Count; i++)
        _salvos[i].Update();
}
void SalvoRefresh()
{
    _salvoBG.Clear();
    GridTerminalSystem.GetBlockGroups(_salvoBG, g =>
        g != null && g.Name != null && g.Name.IndexOf(AIM_TAG, StringComparison.OrdinalIgnoreCase) >= 0);
    _salvos.RemoveAll(x => x.B == null || !_salvoBG.Contains(x.B));
    for (int i = 0; i < _salvos.Count; i++)
        _salvos[i].Refresh(this);
    for (int i = 0; i < _salvoBG.Count; i++)
    {
        var g = _salvoBG[i];
        bool found = false;
        for (int j = 0; j < _salvos.Count; j++)
        {
            if (_salvos[j].B == g)
            {
                found = true;
                break;
            }
        }
        if (!found)
            _salvos.Add(new SalvoGroup(g, this));
    }
}
void SalvoCycleMode()
{
    for (int i = 0; i < _salvos.Count; i++)
        _salvos[i].CycleMode();
}
string GetSalvoModeLabel()
{
    if (_salvos.Count == 0) return "Railgun Manual";
    bool anyBurst = false;
    bool anyManual = false;
    for (int i = 0; i < _salvos.Count; i++)
    {
        if (_salvos[i].Mode == SalvoMode.Burst) anyBurst = true;
        else anyManual = true;
    }
    if (anyBurst && anyManual) return "Railgun Mixed";
    return anyBurst ? "Railgun Burst" : "Railgun Manual";
}
class SalvoGroup
{
    public IMyBlockGroup B { get; private set; }
    public SalvoMode Mode = SalvoMode.Manual;
    int _i = 0;
    int _ticksSinceLast = 0;
    bool _ready = true;
    int _burstShotsRemaining = 0;
    int _burstTimer = 0;
    bool _burstPendingShot = false;
    const int ManualSpacingTicks = 20;
    const int BurstSpacingTicks = 6;
    readonly List<IMyUserControllableGun> _rg = new List<IMyUserControllableGun>();
    readonly Dictionary<long, bool> _waitingForRechargeStart = new Dictionary<long, bool>();
    readonly Dictionary<long, bool> _lastIsShooting = new Dictionary<long, bool>();
    readonly HashSet<long> _ignoreShotEdge = new HashSet<long>();
    readonly Program _p;
    static readonly MyDefinitionId E = new MyDefinitionId(typeof(MyObjectBuilder_GasProperties), "Electricity");
    const float IdlePowerDraw = 0.0002f;
    const float Epsilon = 1e-6f;
    public SalvoGroup(IMyBlockGroup b, Program p)
    {
        B = b;
        _p = p;
        Refresh(p);
    }
    public void CycleMode()
    {
        Mode = Mode == SalvoMode.Manual ? SalvoMode.Burst : SalvoMode.Manual;
        ResetState();
    }
    void ResetState()
    {
        _burstShotsRemaining = 0;
        _burstTimer = 0;
        _burstPendingShot = false;
        _ticksSinceLast = 0;
        _ready = true;
        if (_rg.Count == 0) return;
        int idx = FindNextShootable(-1);
        if (idx >= 0) _i = idx;
        else
        {
            idx = FindNextFunctional(-1);
            if (idx >= 0) _i = idx;
        }
        ApplyEnabledState();
    }
    public void Refresh(Program p)
    {
        _rg.Clear();
        if (B == null) return;
        B.GetBlocksOfType<IMyUserControllableGun>(_rg, x =>
        {
            if (x == null || x.Closed) return false;
            if (!p.Me.IsSameConstructAs(x)) return false;
            return x.BlockDefinition.SubtypeId == "LargeRailgun";
        });
        _rg.Sort((a, b) => a.CustomName.CompareTo(b.CustomName));
        _i = _rg.Count == 0 ? 0 : (_i % _rg.Count);
        var keep = new HashSet<long>();
        for (int k = 0; k < _rg.Count; k++)
            if (_rg[k] != null && !_rg[k].Closed) keep.Add(_rg[k].EntityId);
        if (_waitingForRechargeStart.Count > 0)
        {
            var keys = new List<long>(_waitingForRechargeStart.Keys);
            for (int k = 0; k < keys.Count; k++)
                if (!keep.Contains(keys[k])) _waitingForRechargeStart.Remove(keys[k]);
        }
        if (_lastIsShooting.Count > 0)
        {
            var keys = new List<long>(_lastIsShooting.Keys);
            for (int k = 0; k < keys.Count; k++)
                if (!keep.Contains(keys[k])) _lastIsShooting.Remove(keys[k]);
        }
        if (_ignoreShotEdge.Count > 0)
        {
            var keys = new List<long>(_ignoreShotEdge);
            for (int k = 0; k < keys.Count; k++)
                if (!keep.Contains(keys[k])) _ignoreShotEdge.Remove(keys[k]);
        }
        int functional = FunctionalGunCount();
        if (_burstShotsRemaining > functional - 1)
            _burstShotsRemaining = Math.Max(0, functional - 1);
        if (_rg.Count == 0)
        {
            _i = 0;
            _burstShotsRemaining = 0;
            _burstTimer = 0;
            _burstPendingShot = false;
            _ready = true;
            return;
        }
        if (_i < 0 || _i >= _rg.Count || _rg[_i] == null || _rg[_i].Closed || !_rg[_i].IsFunctional)
        {
            int idx = FindNextShootable(-1);
            if (idx < 0) idx = FindNextFunctional(-1);
            _i = idx >= 0 ? idx : 0;
        }
        ApplyEnabledState();
    }
    public void Update()
    {
        int n = _rg.Count;
        if (n == 0) return;
        if (_i < 0 || _i >= n) _i = 0;
        if (_rg[_i] == null || _rg[_i].Closed || !_rg[_i].IsFunctional)
        {
            int fallback = FindNextShootable(_i);
            if (fallback < 0) fallback = FindNextFunctional(_i);
            _i = fallback;
        }
        if (_i < 0) return;

        for (int k = 0; k < n; k++)
        {
            var g = _rg[k];
            if (g == null || g.Closed) continue;
            bool lastShoot;
            if (!_lastIsShooting.TryGetValue(g.EntityId, out lastShoot)) lastShoot = false;
            bool nowShoot = g.IsShooting;
            _lastIsShooting[g.EntityId] = nowShoot;
            if (nowShoot && !lastShoot)
            {
                if (_ignoreShotEdge.Contains(g.EntityId))
                    _ignoreShotEdge.Remove(g.EntityId);
                else
                    OnRailFired(g, true);
            }
        }

        _ticksSinceLast++;

        if (Mode == SalvoMode.Burst)
            UpdateBurstMode();
        else
            UpdateManualMode();

        ApplyEnabledState();
    }
    void UpdateManualMode()
    {
        _burstShotsRemaining = 0;
        _burstTimer = 0;
        _burstPendingShot = false;
        if (!_ready && _ticksSinceLast >= ManualSpacingTicks)
        {
            int idx = FindNextShootable(_i);
            if (idx >= 0)
            {
                _i = idx;
                _ready = true;
                return;
            }
            idx = FindNextFunctional(_i);
            if (idx >= 0)
            {
                _i = idx;
                _ready = true;
            }
        }
    }
    void UpdateBurstMode()
    {
        if (_burstPendingShot)
        {
            FireCurrentWeapon();
            _burstPendingShot = false;
            _burstShotsRemaining--;
            return;
        }
        if (_burstShotsRemaining <= 0) return;
        _burstTimer++;
        if (_burstTimer < BurstSpacingTicks) return;
        int nextIdx = FindNextShootable(_i);
        if (nextIdx < 0) nextIdx = FindNextFunctional(_i);
        if (nextIdx < 0)
        {
            _burstShotsRemaining = 0;
            _burstTimer = 0;
            _burstPendingShot = false;
            return;
        }
        _i = nextIdx;
        _burstTimer = 0;
        _burstPendingShot = true;
    }
    void ApplyEnabledState()
    {
        for (int k = 0; k < _rg.Count; k++)
        {
            var g = _rg[k];
            if (g == null || g.Closed) continue;
            if (k == _i)
            {
                g.Enabled = true;
                continue;
            }
            g.Enabled = !CanDisable(g);
        }
    }
    void FireCurrentWeapon()
    {
        var g = _rg[_i];
        if (g == null || g.Closed || !g.IsFunctional) return;
        g.Enabled = true;
        _ignoreShotEdge.Add(g.EntityId);
        g.ShootOnce();
        OnRailFired(g, false);
    }
    void OnRailFired(IMyUserControllableGun g, bool manualShot)
    {
        int idx = IndexOfGun(g);
        if (idx >= 0) _i = idx;
        _ticksSinceLast = 0;
        _ready = false;
        _waitingForRechargeStart[g.EntityId] = true;
        if (!manualShot) return;
        if (Mode == SalvoMode.Burst)
        {
            int maxFollowups = _p != null ? _p.GetAdaptiveBurstFollowupShots() : BURST_SHOTS_SHORT;
            int ready = ReadyOtherGunCount(g.EntityId);
            _burstShotsRemaining = Math.Max(0, Math.Min(ready, maxFollowups));
            _burstTimer = 0;
            _burstPendingShot = false;
        }
        else
        {
            _burstShotsRemaining = 0;
            _burstTimer = 0;
            _burstPendingShot = false;
        }
    }
    int IndexOfGun(IMyUserControllableGun g)
    {
        if (g == null) return -1;
        for (int k = 0; k < _rg.Count; k++)
        {
            var x = _rg[k];
            if (x == null || x.Closed) continue;
            if (x.EntityId == g.EntityId) return k;
        }
        return -1;
    }
    int FunctionalGunCount()
    {
        int c = 0;
        for (int k = 0; k < _rg.Count; k++)
        {
            var g = _rg[k];
            if (g != null && !g.Closed && g.IsFunctional) c++;
        }
        return c;
    }
    int ReadyOtherGunCount(long excludeEntityId)
    {
        int c = 0;
        for (int k = 0; k < _rg.Count; k++)
        {
            var g = _rg[k];
            if (g == null || g.Closed || !g.IsFunctional) continue;
            if (g.EntityId == excludeEntityId) continue;
            if (CanShootNow(g)) c++;
        }
        return c;
    }
    int FindNextFunctional(int startIdx)
    {
        int n = _rg.Count;
        for (int step = 1; step <= n; step++)
        {
            int idx = (startIdx + step) % n;
            var g = _rg[idx];
            if (g == null || g.Closed || !g.IsFunctional) continue;
            return idx;
        }
        return -1;
    }
    int FindNextShootable(int startIdx)
    {
        int n = _rg.Count;
        for (int step = 1; step <= n; step++)
        {
            int idx = (startIdx + step) % n;
            var g = _rg[idx];
            if (g == null || g.Closed || !g.IsFunctional) continue;
            if (!CanShootNow(g)) continue;
            return idx;
        }
        return -1;
    }
    bool CanShootNow(IMyUserControllableGun g)
    {
        if (g == null || g.Closed || !g.IsFunctional) return false;
        if (g.IsShooting) return false;
        return !IsRecharging(g);
    }
    bool CanDisable(IMyUserControllableGun g)
    {
        bool recharging = IsRecharging(g);
        bool waitingStart;
        if (_waitingForRechargeStart.TryGetValue(g.EntityId, out waitingStart) && waitingStart)
        {
            if (recharging)
                _waitingForRechargeStart[g.EntityId] = false;
        }
        bool stillWaitingStart;
        _waitingForRechargeStart.TryGetValue(g.EntityId, out stillWaitingStart);
        return !recharging && !stillWaitingStart;
    }
    bool IsRecharging(IMyUserControllableGun g)
    {
        var sink = g.Components.Get<MyResourceSinkComponent>();
        if (sink == null) return false;
        return sink.CurrentInputByType(E) > (IdlePowerDraw + Epsilon);
    }
}
#endregion
#region AmmoLimiter
const string AP_WEAPON_TAG = "Limit";
const string AP_MAGAZINE_TAG = "AMMO";
double AP_ProtectionFillRatio = 0.1;
double AP_ProtectionSiphonThreshold = 2.0;
int AP_RefreshPeriodTicks = 240;
int AP_WorkBudgetPerTick = 3;
readonly List<IMyInventory> AP_ProtectedInventories = new List<IMyInventory>();
readonly List<IMyInventory> AP_MagazineInventories = new List<IMyInventory>();
readonly List<IMyUserControllableGun> AP_GunsScratch = new List<IMyUserControllableGun>();
readonly List<IMyTerminalBlock> AP_InvBlocksScratch = new List<IMyTerminalBlock>();
int AP_TickCounter = 0;
int AP_RefreshCounter = 0;
void InitAmmoProtect()
{
    AP_TickCounter = 0;
    AP_RefreshCounter = 0;
    AmmoProtectRefresh();
}
void AmmoProtectTick()
{
    if (++AP_RefreshCounter >= AP_RefreshPeriodTicks)
    {
        AP_RefreshCounter = 0;
        AmmoProtectRefresh();
    }
    AmmoProtectRunStep();
}
void AmmoProtectRefresh()
{
    AP_ProtectedInventories.Clear();
    AP_MagazineInventories.Clear();
    AP_GunsScratch.Clear();
    GridTerminalSystem.GetBlocksOfType(AP_GunsScratch, b =>
    b != null &&
    b.IsFunctional &&
    b.IsSameConstructAs(Me) &&
    b.CustomName != null &&
    b.CustomName.IndexOf(AP_WEAPON_TAG, StringComparison.OrdinalIgnoreCase) >= 0
    );
    for (int gi = 0; gi < AP_GunsScratch.Count; gi++)
    {
        var invOwner = AP_GunsScratch[gi] as IMyInventoryOwner;
        if (invOwner == null) continue;
        for (int i = 0; i < invOwner.InventoryCount; i++)
        {
            var inv = invOwner.GetInventory(i);
            if (inv != null) AP_ProtectedInventories.Add(inv);
        }
    }
    AP_InvBlocksScratch.Clear();
    GridTerminalSystem.GetBlocksOfType(AP_InvBlocksScratch, b =>
    b != null &&
    b.IsFunctional &&
    b.IsSameConstructAs(Me) &&
    b.HasInventory &&
    b.CustomName != null &&
    b.CustomName.IndexOf(AP_MAGAZINE_TAG, StringComparison.OrdinalIgnoreCase) >= 0
    );
    for (int bi = 0; bi < AP_InvBlocksScratch.Count; bi++)
    {
        var io = AP_InvBlocksScratch[bi] as IMyInventoryOwner;
        if (io == null) continue;
        for (int i = 0; i < io.InventoryCount; i++)
        {
            var inv = io.GetInventory(i);
            if (inv != null) AP_MagazineInventories.Add(inv);
        }
    }
}
void AmmoProtectRunStep()
{
    int nInv = AP_ProtectedInventories.Count;
    if (nInv == 0) return;
    int processed = 0;
    int start = (AP_TickCounter++ % nInv);
    for (int n = 0; n < nInv; n++)
    {
        int idx = (start + n) % nInv;
        var inv = AP_ProtectedInventories[idx];
        if (inv == null) continue;
        var owner = inv.Owner as IMyTerminalBlock;
        if (owner == null || owner.Closed || !owner.IsFunctional) continue;
        int itemCount = inv.ItemCount;
        if (itemCount <= 0)
        {
            AmmoProtectSetConveyors(owner, true);
            if (++processed >= AP_WorkBudgetPerTick) break;
            continue;
        }
        var first = inv.GetItemAt(0);
        if (!first.HasValue)
        {
            AmmoProtectSetConveyors(owner, true);
            if (++processed >= AP_WorkBudgetPerTick) break;
            continue;
        }
        MyItemType ammoType = first.Value.Type;
        var ammoInfo = ammoType.GetItemInfo();
        if (ammoInfo.Volume <= 0)
        {
            if (++processed >= AP_WorkBudgetPerTick) break;
            continue;
        }
        double maxVol = (double)inv.MaxVolume;
        int capCount = (int)Math.Ceiling(maxVol * AP_ProtectionFillRatio / (double)ammoInfo.Volume);
        if (capCount < 0) capCount = 0;
        MyFixedPoint total = AmmoProtectCountOfType(inv, ammoType);
        AmmoProtectSetConveyors(owner, total < (MyFixedPoint)capCount);
        if (AP_MagazineInventories.Count > 0)
        {
            MyFixedPoint siphonTrigger = (MyFixedPoint)(capCount * AP_ProtectionSiphonThreshold);
            if (total > siphonTrigger)
            {
                MyFixedPoint excess = total - (MyFixedPoint)capCount;
                if (excess > 0)
                    AmmoProtectSiphonExcess(inv, ammoType, ref excess);
            }
        }
        if (++processed >= AP_WorkBudgetPerTick)
            break;
    }
}
MyFixedPoint AmmoProtectCountOfType(IMyInventory inv, MyItemType t)
{
    MyFixedPoint sum = 0;
    int c = inv.ItemCount;
    for (int i = 0; i < c; i++)
    {
        var it = inv.GetItemAt(i);
        if (!it.HasValue) continue;
        if (it.Value.Type == t) sum += it.Value.Amount;
    }
    return sum;
}
void AmmoProtectSiphonExcess(IMyInventory src, MyItemType ammoType, ref MyFixedPoint excess)
{
    if (excess <= 0) return;
    int srcCount = src.ItemCount;
    for (int dstIdx = 0; dstIdx < AP_MagazineInventories.Count && excess > 0; dstIdx++)
    {
        var dst = AP_MagazineInventories[dstIdx];
        if (dst == null) continue;
        var dstOwner = dst.Owner as IMyTerminalBlock;
        if (dstOwner == null || dstOwner.Closed || !dstOwner.IsFunctional) continue;
        if (!dst.CanItemsBeAdded(excess, ammoType))
            continue;
        for (int i = 0; i < srcCount && excess > 0; i++)
        {
            var it = src.GetItemAt(i);
            if (!it.HasValue) continue;
            if (it.Value.Type != ammoType) continue;
            var moveAmount = it.Value.Amount < excess ? it.Value.Amount : excess;
            if (moveAmount <= 0) continue;
            bool ok = src.TransferItemTo(dst, i, null, true, moveAmount);
            if (ok)
            {
                excess -= moveAmount;
                srcCount = src.ItemCount;
                if (i >= srcCount) break;
            }
        }
    }
}
void AmmoProtectSetConveyors(IMyTerminalBlock owner, bool enabled)
{
    if (owner == null) return;
    try
    {
        owner.SetValueBool("UseConveyorSystem", enabled);
        return;
    }
    catch { }
    var invOwner = owner as IMyInventoryOwner;
    if (invOwner == null) return;
    try
    {
        invOwner.UseConveyorSystem = enabled;
    }
    catch { }
}
#endregion

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
    InitPinpoint();
    InitMissile();
    InitSalvo();
    InitAmmoProtect();
}
public void Main(string argument, UpdateType updateSource)
{
    PinpointTick(argument, updateSource);
    SalvoTick(argument, updateSource);
    MissileSystemTick(argument, updateSource);
    AmmoProtectTick();
}
//ver 1.30 x
