
const string VERSION = "2.5.4T";

// << Quick Setup >>
const string GroupName = "RTAS"; /
const string DesiGroup = "Artillery Turrets"; 

string[] Controllers = { "Control Seat" };

string[] Cameras = { "Camera RAILS", "Camera Backup" };


int TickDelay = 2; 
int RCDelay = 1; 
int RefreshDelay = 48; 

double TickFactor = 0.7; 
double RCFactor = 0.3; 

bool IsBackup = false;
const string MainPB = "Programmable Block RTAS";

bool RemoteBlocks = true; 

bool FleetIntegration = true; 
const string FleetShipName = ""; 
const string FleetServerName = "Fleet Server";

double FleetJumpSeconds = 8;
double PrecalcBufferSeconds = 2;
double[] FleetJumpRanges = { 5000, 7500, 10000 };



double ADeviationLimit = 0.4; 
double FreeAxisMult = 2; /

RotationController PitchPIDControl = new RotationController(7, 3.5, 2); 
int PitchPIDWindow = 8; 
RotationController YawPIDControl = new RotationController(7, 3.5, 2); 
int YawPIDWindow = 8;
bool PreventManualControl = true;

string SequenceTag = "SQC"; 
double[] SequenceDelays = { 0.5, 0.2, 0.05 };

int CtrlLimit = 3; 
double[] SalvoDelays = { 1, 0.5, 0.2 };

const double RCRange = 3000;
bool MaxRCRange = false;
int RCFails = 8;

int BiasRCCount = 10; 
int PlanetarySkipCount = 100;
int VoxelSkipCount = 200; 

double MaxJerk = 14;

double AIAlarmDistance = 2500;

bool IdleRot = false;


double[] Convergences = { 2000, 1400, 800 };
double BreakMatchSeconds = 3;

double[] Depths = { 5, 10, 20 };

double MTVol = 125;


bool Own = false;
bool Friendly = false;
bool Neutral = false;
bool Enemy = true;
bool Voxels = false;

bool SmallGrid = true;
bool LargeGrid = true;
bool Station = true;
bool Character = true;

double PrecalcSeconds = 12;
double PrecalcMeters = 5000; 

double ProtectionFillRatio = 0.05; 
double ProtectionSiphonThreshold = 2; 
string WeaponTag = "Protected"; 
string MagazineTag = "Magazine"; 

bool TripDet = true;
int AFCount = 2; 
double AFDelay = 3; 

bool LaunchTimers = true;
bool TankLoad = true;
bool ConnLoad = true;

List<BombLauncher> BLaunchers = new List<BombLauncher>()
{
    new BombLauncher(MergeBlock: "Bomb Merge Block", Warhead: "Bomb Warhead", Delay_Seconds: 3.5),
};




bool GraphicalScreens = true;
bool DynamicScreens = true;
double DynamicDistance = 100;

static Color GUIBackgroundColor = new Color(150, 150, 150, 10);


const string NoGUITag = "NoGUI"; 

const string HUDLCDTag = "hudlcd";
Vector2I HUDSize = new Vector2I(1700, 1300); 

const string BackupScrTag = "Backup";





const string Light_LockState = "Lock State";
const string Light_TurretInfo = "Turret Info";
const string Light_RailCharge = "Rail Charge";
const string Light_AIAlarm = "Proximity Alarm";

readonly bool UseTgtSearch = true, UseTgtLock = true, UseTgtLoss = true, UseAIAlarm = true;

string LogName = "RTAS Logger";


bool Profiler = true;
int ProfileGracePeriod = 30;
float ProfilingFactor = .01f;



const double О = Math.PI; uint Ó = 0, Н = 0, М = 0, Л = 0, К = 0, Й = 0, И = uint.MaxValue, З = 0, П = 0, Ж = 0, Е = 0, Д = 0, Г = 0, В = 0, Б = uint.MaxValue, А = uint.
MaxValue, Џ = 0, Ў = 0, Ѝ = 0; int Ќ, Ћ, Р = 0, С = 0, ј = 0, Ѱ = 0, Ѯ = 0, ѭ = 0, Ѭ = 0, ѫ = 0, Ѫ = 0, ǎ = 0, ř = 0, ѩ = 0, Ѩ = -1, ѧ = 0, Ѧ = 0, ѯ = 0, ѥ = 0, ѣ = 0, Ѣ = 0, ѡ = 0, Ѡ = 0, џ = 0, ў = 0; long[]
ѝ; long ќ = 0, ћ = 0; byte њ; double љ, Ѥ, Ǘ, Ѽ, Ғ, Ґ, ҏ = 1, Ҏ, ҍ, Ҍ = 0, ҋ = 100, Ҋ = 0, ҁ = 0, Ҁ = 0, ѿ = 0, Ѿ = 0, ґ = 0, ѽ = 0, ѻ = 0, Ѻ = 0, ѹ = 0; double[] Ѹ; double ѷ = 0, Ѷ
= 0, ѵ = 0; const float Ѵ = 166666.666667f; bool Ơ = false, ѳ = false, Ѳ = false, ѱ = false, ї = false, л = false, ш = false, й = false, и = false, з = false,
ж = false, е = false, д = false, г = false, в = false, б = false, а = false, Я = false, Ю = false, Э = false, Ь = false, Ы = false, Ъ = false, Щ = true, Ш = false, Ч =
false, Ц = false, Х = false, Ф = false, У = false, к = false, Т = false, м = true, і = false, є = false, ѓ = false, ђ = false, ё = false, ѐ = false, я = false, ю = false,
э = false, ь = false, ы = true, ъ = false, ѕ = false, щ = false, ч = false, ц = false, х = false, ф = false, у = false, т = false; string с = "", р = ""; List<
IMyCameraBlock> Έ = new List<IMyCameraBlock>(), п = new List<IMyCameraBlock>(); IMyCameraBlock о = null; IMyProgrammableBlock н = null; List<
IMyShipController> Њ = new List<IMyShipController>(); List<IMyTerminalBlock> ϑ = new List<IMyTerminalBlock>(); List<IMySoundBlock> Ѐ = new List<
IMySoundBlock>(); List<IMyOffensiveCombatBlock> Ϗ = new List<IMyOffensiveCombatBlock>(); List<IMyFlightMovementBlock> ώ = new List<
IMyFlightMovementBlock>(); List<IMyLargeTurretBase> ύ = new List<IMyLargeTurretBase>(), ό = new List<IMyLargeTurretBase>(); List<IMyTurretControlBlock
> ϋ = new List<IMyTurretControlBlock>(), ϊ = new List<IMyTurretControlBlock>(); List<IMyUserControllableGun> ω = new List<
IMyUserControllableGun>(), ψ = new List<IMyUserControllableGun>(); List<IMyGyro> ŀ = new List<IMyGyro>(); List<IMyLightingBlock> χ = new List<
IMyLightingBlock>(), φ = new List<IMyLightingBlock>(), υ = new List<IMyLightingBlock>(), τ = new List<IMyLightingBlock>(); List<IMyBeacon> σ = new
List<IMyBeacon>(); List<IMyJumpDrive> ς = new List<IMyJumpDrive>(); List<Ƭ> ρ = new List<Ƭ>(); HashSet<Ƭ> π = new HashSet<Ƭ>(); List<ĕ> ο =
new List<ĕ>(), ξ = new List<ĕ>(); HashSet<long> ν = new HashSet<long>(); List<ʡ> μ = new List<ʡ>(); List<Ƣ> λ = new List<Ƣ>(); HashSet<
MyRelationsBetweenPlayerAndBlock> κ = new HashSet<MyRelationsBetweenPlayerAndBlock>(); ץ ϐ; Dictionary<int, ٵ> ι = new Dictionary<int, ٵ>(); IMyShipController ǔ =
null; ɥ ý = new ɥ(); ĕ ƌ = null; IMyTurretControlBlock Ϧ = null; MatrixD ʺ, ϥ; Vector3D Ϥ, ϣ, Ϣ, ϡ, Ϡ = Vector3D.Zero, ϟ = Vector3D.Zero, Ϟ, ϧ =
Vector3D.Zero; Vector3D? ϝ = null; List<ȼ> ϛ = new List<ȼ>(); IMyOffensiveCombatBlock Ϛ; IMyFlightMovementBlock ϙ; MyShipVelocities Ϙ; List<
string> ϗ = new List<string>(); IEnumerator<bool> ϖ, ϕ; ӑ ϔ = new ӑ(); S ϓ = new S(); ɚ Ǯ = new ɚ(); ń ͽ = new ń(); ũ ϒ = new ũ(); Ĳ Ϝ, θ;
MyDetectedEntityInfo Ϋ = new MyDetectedEntityInfo(); IMyGridTerminalSystem ɐ; static Color Τ = new Color(255, 255, 255), Σ = new Color(0, 255, 50), Ρ = new
Color(100, 100, 255), Π = new Color(100, 180, 100), Ο = new Color(100, 255, 100, 20), Ξ = new Color(40, 220, 30, 220), Ν = new Color(100, 0, 255, 15),
Μ = new Color(255, 70, 0), Λ = new Color(200, 200, 200), Κ = new Color(120, 120, 120), Ι = new Color(100, 170, 255, 20), Θ = new Color(100, 100,
255, 60), Η = new Color(100, 200, 255, 20), Ζ = new Color(200, 100, 20, 50), Ε = new Color(255, 255, 255), Δ = new Color(100, 200, 255), Γ = new
Color(150, 130, 0, 10), Β = new Color(50, 50, 50), Α = new Color(150, 50, 0, 10), ΐ = new Color(255, 150, 0), Ώ = new Color(80, 255, 255, 220), Ύ = new
Color(80, 255, 255, 35), Υ = new Color(100, 100, 255), Ό = new Color(255, 0, 0), Φ = new Color(0, 50, 200), η = new Color(0, 255, 0), ζ = new Color(255
, 200, 0), ε = new Color(20, 0, 255), δ = new Color(5, 125, 125), γ = new Color(255, 0, 150), β = new Color(150, 255, 150), α = new Color(255, 255,
0), ΰ = new Color(150, 0, 0), ί = new Color(0, 0, 25), ή = new Color(0, 0, 255), έ = new Color(0, 255, 100); Program(){
    Runtime.UpdateFrequency
= IsBackup ? UpdateFrequency.Update10 : UpdateFrequency.Update1; ADeviationLimit = Math.Cos(ADeviationLimit * О / 180); Ѽ = Ґ = 499; Ғ = 899;
    AFDelay *= 60; ѝ = new long[20]; Ќ = TickDelay; Ћ = RCDelay; Ҏ = (TickDelay + 1) * 100 / 6; Ϝ = PitchPIDControl.Initialize(PitchPIDWindow); θ =
    YawPIDControl.Initialize(YawPIDWindow); Ѹ = new double[(int)Math.Round(90f / (TickDelay + 1))]; њ = (byte)((SmallGrid ? 1 : 0) + (LargeGrid ? 2 : 0) + (
    Station ? 4 : 0) + (Character ? 8 : 0) + (IdleRot ? 16 : 0)); if (!string.IsNullOrEmpty(Storage))
    {
        string[] Ä = Storage.Split('\\'); if (Ä.Length == 18)
        {
            bool.TryParse(Ä[0], out Ѳ); bool.TryParse(Ä[1], out ї); bool.TryParse(Ä[2], out ѱ); bool.TryParse(Ä[3], out Ь); bool.TryParse(Ä[4],
            out Ơ); int.TryParse(Ä[5], out С); if (С >= Convergences.Length) С = 0; int.TryParse(Ä[6], out ј); if (ј >= Depths.Length) ј = 0; int.TryParse
            (Ä[7], out Ѱ); if (Ѱ > (FleetIntegration ? 2 : 1)) Ѱ = 0; int.TryParse(Ä[8], out Ѭ); if (Ѭ >= SalvoDelays.Length) Ѭ = 0; bool.TryParse(Ä[9], out
            Э); bool.TryParse(Ä[10], out Ъ); bool.TryParse(Ä[11], out Ч); int.TryParse(Ä[12], out Ǯ.ɗ); int.TryParse(Ä[13], out Ǯ.ɖ); int.
            TryParse(Ä[14], out ѫ); int.TryParse(Ä[15], out Ѫ); if (Ѫ >= SequenceDelays.Length) Ѫ = 0; bool.TryParse(Ä[16], out я); bool.TryParse(Ä[17],
            out л);
        }
        else Storage = "";
    }
    Ǘ = Convergences[С]; ҍ = SalvoDelays[Ѭ]; Ѥ = Depths[ј]; ϐ = new ץ(); г = true; ϖ = Ϫ(); if (Enemy) κ.Add(
            MyRelationsBetweenPlayerAndBlock.Enemies); if (Neutral)
    {
        κ.Add(MyRelationsBetweenPlayerAndBlock.Neutral); κ.Add(MyRelationsBetweenPlayerAndBlock.NoOwnership
            );
    }
    if (Friendly) { κ.Add(MyRelationsBetweenPlayerAndBlock.Friends); κ.Add(MyRelationsBetweenPlayerAndBlock.FactionShare); }
    if (
            Own) κ.Add(MyRelationsBetweenPlayerAndBlock.Owner); ϕ = ז(); ք(); ɐ = GridTerminalSystem; ȷ.ȶ = new ȷ(ɐ.GetBlockWithName(LogName)); if (
            !IsBackup) Main("refresh", UpdateType.None);
}
void ά() => ǔ = Њ.Count == 0 ? null : (Њ.Find(Ǌ => Ǌ.IsUnderControl) ?? Њ[0]); void Save() =>
            Storage = $@"{Ѳ}\{ї}\{ѱ}\{Ь}\{Ơ}\{С}\{ј}\{Ѱ}\{Ѭ}\{Э}\{Ъ}\{Ч}\{Ǯ.ɗ}\{Ǯ.ɖ}\{ѫ}\{Ѫ}\{я}\{л}"; void Main(string Ϊ, UpdateType Ω)
{
    if (!г)
        return; if (IsBackup)
    {
        н = ɐ.GetBlockWithName(MainPB) as IMyProgrammableBlock; if (н != null && н.IsFunctional && н.Enabled)
        {
            Echo($"< RTAS - v{VERSION} >\n-- BACKUP MODE --\n\nMain Programmable block active\nStanding by...\n\n\nDesignated Main PB:\n{MainPB}"
        ); Runtime.UpdateFrequency = UpdateFrequency.Update100; return;
        }
        else if (Runtime.UpdateFrequency != UpdateFrequency.Update1)
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
    }
    var Ψ = DateTime.Now; к = false; ά(); ʺ = ǔ?.WorldMatrix ?? MatrixD.Identity; if (Ω ==
            UpdateType.Update1)
    {
        var Χ = Ψ.ToBinary(); Ó++; ȷ.ȶ.Ȳ(Ψ, Ó); if (Ó > 20)
        {
            int Ǌ = 0; for (int H = 0; H < 20; H++) if (DateTime.FromBinary(ѝ[H] + (long)(20 *
            Ѵ)) >= Ψ) Ǌ++; Ќ = (int)((TickDelay + 1) / (1 - (1 - (ҏ = (float)Ǌ / 20)) * TickFactor)) - 1; Ћ = (int)((RCDelay + 1) / (1 - (1 - ҏ) * RCFactor)) - 1;
        }
        ѝ[ѩ] = Χ;
        if (++ѩ >= 20) ѩ = 0; if (Ó % 10 == 0 && в) ϓ.ķ(ʺ.Translation, this); if (Ó % 3 == 0 && і && !ъ && ў < 2)
        {
            if (Ϛ.LastScan < ѡ) { ў++; Д = Ó + 4; }
            ѡ = Ϛ.LastScan; if (Ó <
        Д) { Ϛ.Enabled = false; Ϛ.Enabled = true; }
        }
        if (в)
        {
            foreach (var Č in λ) Č.ƺ(Ó, ѫ, SequenceDelays[Ѫ]); foreach (var Ⱥ in Ǯ.ə) Ⱥ.ƺ();
            foreach (var Ⱥ in Ǯ.ɝ.ToList()) Ⱥ.ȑ(Ǯ, TripDet); if (ý.Ƀ) foreach (var Ⱥ in Ǯ.Õ.ToList()) Ⱥ.Ǳ(ý, ϣ, ý.ɢ, Ǯ, TripDet); Ϙ = ǔ.GetShipVelocities(
            ); Ս(); if (б && Ó - М >= 9 * ҏ) { ϔ.Ӕ(); М = 0; б = false; }
            if (а && Ó - Л >= 24 * ҏ) { ϔ.Ӕ(); Л = 0; а = false; }
            if (Я && Ó - К >= 27 * ҏ) { ϔ.Ӕ(); К = 0; Я = false; }
            ϒ.ŵ(Ơ);
            if (ч && !ц) ϐ.ן = Ψ.AddSeconds(FleetJumpSeconds).Ticks; if (Т && Ó > П) { Т = false; Ǘ = Convergences[С]; }
            if ((Ó % (Ћ + 1)) == 0 && ь && Ơ && ѳ && (ѣ == 0 || ѣ
            -- == 0)) ւ(); if ((Ó % 2) == 0) { Ǯ.ɒ = false; foreach (var Ⱥ in Ǯ.Õ.ToList()) Ⱥ.ș(ý, ϣ, ʺ, Ϙ.LinearVelocity, Ó, Ǯ, TripDet); }
            if (Ó >= Б)
            {
                ǔ.
            DampenersOverride = щ; Б = uint.MaxValue;
            }
        }
    }
    bool Ђ = Ϊ.Equals("refresh"); if (!string.IsNullOrEmpty(Ϊ) && !Ђ) ϗ.Add(Ϊ); if (Profiler)
    {
        ѷ = Runtime.
            LastRunTimeMs; if (Ó > ProfileGracePeriod) { ѵ = ѵ * (1 - ProfilingFactor) + ѷ * ProfilingFactor; if (ѷ > Ѷ) Ѷ = ѷ; }
    }
    if ((Ω != UpdateType.Update1 || (Ó % (Ќ + 1)) != 0
            ) && !Ђ) return; if (((++Н) % (RefreshDelay + 1)) == 0 || Ђ)
    {
        в = false; ɐ.GetBlocksOfType(Њ, Ǌ => Ǌ.IsSameConstructAs(Me) && Ǌ.IsFunctional &&
            Controllers.Contains(Ǌ.CustomName)); if (Њ.Count == 0)
        {
            var ņ = "ERROR: No working ship controllers with specified names found"; ϓ.õ(ņ);
            Echo(ņ); return;
        }
        Њ = Controllers.Select(ȟ => Њ.Find(Ǌ => Ǌ.CustomName == ȟ)).ToList(); if (ǔ == null) ά(); var Ё = ɐ.GetBlockGroupWithName(
            GroupName); if (Ё == null) { var ņ = $"ERROR: Group '{GroupName}' not found"; Echo(ņ); ϓ.õ(ņ); return; }
        Ё.GetBlocksOfType(Έ, ú => ú.Enabled && ú.
            IsFunctional); ϒ.Ǩ(Έ, ʺ.Forward, RemoteBlocks); if (Έ.Count == 0)
        {
            var ņ = $"ERROR: No cameras contained in group '{GroupName}'"; Echo(ņ); ϓ.õ(ņ
            ); return;
        }
        ɐ.GetBlocksOfType(Њ, Ǌ => Ǌ.IsSameConstructAs(Me) && Ǌ.IsFunctional && Controllers.Contains(Ǌ.CustomName)); љ = Έ[0].
            RaycastTimeMultiplier; Έ.Clear(); Ё.GetBlocksOfType(Ѐ, ú => ú.Enabled && ú.IsFunctional); Ё.GetBlocksOfType(ϑ, ú => ú.IsSameConstructAs(Me) && ú.
            IsFunctional && ú is IMyTextSurfaceProvider); ϓ.N(ϑ, BackupScrTag, GraphicalScreens, HUDSize, RemoteBlocks); ϑ.Clear(); ϔ.N(Ѐ, RemoteBlocks); ɐ
            .GetBlocksOfType(п, Ǌ => Ǌ.IsSameConstructAs(Me) && Cameras.Contains(Ǌ.CustomName)); п = п.Aggregate(new List<IMyCameraBlock>(), (
            ϼ, Ǌ) => { ϼ.Insert(ϼ.Count == 0 ? 0 : ϼ.First().IsActive ? ϼ.IndexOf(ϼ.Last(Ǒ => Ǒ.IsActive)) : ϼ.Count - 1, Ǌ); return ϼ; }); Ё.
            GetBlocksOfType(Ϗ, ú => ú.IsFunctional && ú.IsSameConstructAs(Me)); Ё.GetBlocksOfType(ώ, ú => ú.IsFunctional && ú.IsSameConstructAs(Me)); Ю = Ϗ.Count
            != 0 && ώ.Count != 0; if (Ю) { Ϛ = Ϗ[0]; ϙ = ώ[0]; }
        Ё.GetBlocksOfType(σ, ú => ú.IsFunctional && ú.IsSameConstructAs(Me)); Ё.GetBlocksOfType(χ, ú
            => ú.IsFunctional && ú.IsSameConstructAs(Me) && ú.CustomName.Contains(Light_LockState)); Ё.GetBlocksOfType(φ, ú => ú.IsFunctional &&
            ú.IsSameConstructAs(Me) && ú.CustomName.Contains(Light_TurretInfo)); Ё.GetBlocksOfType(υ, ú => ú.IsFunctional && ú.
            IsSameConstructAs(Me) && ú.CustomName.Contains(Light_RailCharge)); Ё.GetBlocksOfType(τ, ú => ú.IsFunctional && ú.IsSameConstructAs(Me) && ú.
            CustomName.Contains(Light_AIAlarm)); foreach (var ʹ in χ) ʹ.Enabled = true; foreach (var ʹ in φ) ʹ.Enabled = true; foreach (var ʹ in υ) ʹ.
            Enabled = true; Ё.GetBlocksOfType(ύ, Ⱥ => Ⱥ.IsFunctional && Ⱥ.Enabled && Ⱥ.IsSameConstructAs(Me)); Ё.GetBlocksOfType(ς, Ⱥ => Ⱥ.IsFunctional &&
            Ⱥ.Enabled); if (ς.Count != 0) ѹ = ς[0].MaxJumpDistanceMeters; Ы = false; ѯ = 0; ѥ = 0; ν = ο.Select(Ⱥ => Ⱥ.Đ).ToHashSet(); ο = ύ.Aggregate(ο, (ϼ, Ⱥ
            ) => { ĕ Ј; if (ν.Add(Ⱥ.EntityId)) { if ((Ј = ĕ.ʢ(Ⱥ)) == null) ν.Remove(Ⱥ.EntityId); else ο.Add(Ј); } return ϼ; }); foreach (var Ⱥ in ο.
            Select(Ⱥ => Ⱥ.Ǫ.ɂ)) { if (Ⱥ == 1) { Ы = true; ѯ++; } else ѥ++; }
        ύ.Clear(); if (!Ы) Ь = false; Ё.GetBlocksOfType(ω, Ⱥ => !(Ⱥ is IMyLargeTurretBase) && Ⱥ.
            IsFunctional && (Ⱥ.Enabled || (ѫ > 0 && Ⱥ.CustomName.Contains(SequenceTag))) && Ⱥ.IsSameConstructAs(Me)); foreach (var Č in ω)
        {
            var Ѓ = false;
            foreach (var Ї in μ) if (Ї.Đ == Č.GetId()) { Ѓ = true; break; }
            if (!Ѓ) { bool û; var Ö = new ʡ(Č, ѫ, SequenceTag, out û); if (û) μ.Add(Ö); }
        }
        ω.Clear();
        ш = μ.Count != 0; var Љ = new Dictionary<string, List<IMyUserControllableGun>>(); ɐ.GetBlocksOfType(ψ, Č => !(Č is IMyLargeTurretBase
        ) && Č.IsFunctional && Č.IsSameConstructAs(Me) && Č.CustomName.Contains(SequenceTag)); foreach (var Č in ψ)
        {
            if (Љ.ContainsKey(Č.
        BlockDefinition.SubtypeId)) Љ[Č.BlockDefinition.SubtypeId].Add(Č);
            else Љ[Č.BlockDefinition.SubtypeId] = new List<IMyUserControllableGun>{Č
};
        }
        ψ.Clear(); foreach (var І in Љ) { if (λ.Find(Č => Č.ƶ == І.Key) != null) continue; λ.Add(new Ƣ(І.Key)); }
        for (int H = λ.Count - 1; H >= 0; H
--) if (!λ[H].ǃ(Љ.ContainsKey(λ[H].ƶ) ? Љ[λ[H].ƶ] : new List<IMyUserControllableGun>(), Ó)) λ.RemoveAt(H); Љ.Clear(); var Ѕ = ɐ.
GetBlockGroupWithName(DesiGroup); if (Ѕ != null)
        {
            Ѕ.GetBlocksOfType(ό, ú => ú.IsSameConstructAs(Me)); foreach (var Ⱥ in ό)
            {
                foreach (var Є in ο)
                {
                    if (Є.Đ ==
Ⱥ.GetId()) { bool Ѓ = false; foreach (var Ņ in ξ) if (Ņ.Đ == Ⱥ.GetId()) { Ѓ = true; break; } if (!Ѓ) ξ.Add(Є); break; }
                }
            }
            ό.Clear(); if (
RemoteBlocks)
            {
                Ѕ.GetBlocksOfType(ϊ, Ǌ => Ǌ.Enabled && Ǌ.IsFunctional); for (int H = ϋ.Count - 1; H >= 0; H--) if (ϋ[H].Closed || !ϋ[H].IsFunctional) ϋ.
RemoveAt(H); foreach (var Ͽ in ϊ) if (!ϋ.Contains(Ͽ)) ϋ.Add(Ͽ); ϊ.Clear();
            }
            else Ѕ.GetBlocksOfType(ϋ);
        }
        var ϲ = new List<
IMyUserControllableGun>(); ɐ.GetBlocksOfType(ϲ, ú => ú.CustomName.Contains(WeaponTag) && ú.IsSameConstructAs(Me) && ú.IsFunctional); ϲ.FindAll(ú => !(ú
is IMyLargeInteriorTurret) && !(ú is IMySmallMissileLauncher && !(ú is IMySmallMissileLauncherReload))).Select(ú => ú as
IMyInventoryOwner).Aggregate(ρ, (ϯ, Ϯ) => { for (int H = 0; H < Ϯ.InventoryCount; H++) ϯ.Add(new Ƭ(Ϯ.GetInventory(H))); return ϯ; }); if (ρ.Count != 0)
        {
            var
ϰ = new List<IMyInventoryOwner>(); Ё.GetBlocksOfType(ϰ, û => {
    var ú = û as IMyTerminalBlock; return ú.IsFunctional && ú.
IsSameConstructAs(Me) && ú.CustomName.Contains(MagazineTag);
}); ϰ.Aggregate(π, (ϯ, Ϯ) => {
    for (int H = 0; H < Ϯ.InventoryCount; H++) ϯ.Add(new Ƭ(Ϯ.
GetInventory(H))); return ϯ;
});
        }
        Ё.GetBlocksOfType(ŀ, ú => ú.IsFunctional && ú.Enabled && ú.IsSameConstructAs(Me)); ͽ.N(ŀ); ŀ.Clear(); if (
FleetIntegration && ϐ.פ == null) ϐ.פ = ɐ.GetBlockWithName(FleetServerName); Ǯ.ə.Sort(); Ǯ.ɑ(ɐ, LaunchTimers, TankLoad, ConnLoad); в = true; З = Ó;
    }
    if (!в)
        return; if (о == null || !о.IsActive || о.Closed) о = п.Find(Ǌ => Ǌ.IsActive); ϥ = о?.WorldMatrix ?? ʺ; if (ѓ)
    {
        Ґ += Ҏ; if (Ґ >= 500)
        {
            ϔ.ӏ("Alert 1", .45f)
        ; Ґ = 0; К = Ó; Я = true;
        }
    }
    ա(); փ(); if (FleetIntegration)
    {
        if (х)
        {
            if (у)
            {
                if (Ψ.Ticks > ϐ.ן)
                {
                    у = !(ф = true); Ў = Ó + 600; Ѝ = Ó + (uint)(
        PrecalcBufferSeconds * 60);
                }
                else if (!ϐ.ך.HasValue) т = у = х = false;
            }
            ϧ = ф ? ϧ : ϐ.ך.Value; if (ф ? Ó >= Ў : !ֆ((float)(ϧ - ʺ.Translation).Length()))
            {
                Ў = Ѝ = 0; х = ф =
        false;
            }
            else if (ф && т && Ó >= Ѝ) { т = !(ё = true); ϡ = ϧ; Е = Ó + (uint)(PrecalcSeconds * 60); }
        }
        else if (ч)
        {
            if (у)
            {
                if (Ψ.Ticks < ϐ.ן) ϧ = (ϐ.ך = ʺ.
        Translation + ʺ.Forward * FleetJumpRanges[џ]).Value;
                else { у = !(ф = true); Ў = Ó + 600; Ѝ = Ó + (uint)(PrecalcBufferSeconds * 60); }
            }
            if (ф)
            {
                if (Ó >= Ў)
                {
                    Ў = 0; ч
        = ц = ф = false;
                }
                else { ϐ.Ӓ(); ф = true; }
                if (т && Ó >= Ѝ) { т = !(ё = true); ϡ = ϧ; Е = Ó + (uint)(PrecalcSeconds * 60); }
            }
            else if (!ֆ((float)
        FleetJumpRanges[џ])) { ч = ц = ф = false; Ў = 0; ϐ.Ӓ(); }
        }
        string ϭ = ý.Ƀ ? є || ѕ ? р : ý.ɋ : ""; long? Ϭ = null, ϫ = null; if (ý.Ƀ) { Ϭ = ý.ɟ; ϫ = ý.Đ; }
        if (ý.Ƀ && !ѕ)
        {
            Vector3D? Ȃ =
        ý.ɡ; if (!ý.Ʉ) Ȃ = null; ϐ.ط((FleetShipName == "" ? Me.CubeGrid.CustomName : FleetShipName).Split('\n')[0], ʺ.Translation, Me.CubeGrid.
        EntityId, ϭ, ý.Ɉ, !м, ý.ɤ, Ȃ, ý.ɢ, ý.ɠ, ϫ, ý.ɟ, Ó, ι);
        }
        else ϐ.ط((FleetShipName == "" ? Me.CubeGrid.CustomName : FleetShipName).Split('\n')[0], ʺ.
        Translation, Me.CubeGrid.EntityId, ϭ, ý.Ƀ ? ý.Ɉ : -1, null, null, null, null, null, ϫ, Ϭ, Ó, ι);
    }
    д = ξ.Count != 0 || ϋ.Count != 0; м = ý.Ƀ && !ѕ && (!Х || (Ѳ && !ý.Ʉ)
        ); foreach (var Ⱥ in Ǯ.ə.ToList()) if (!Ⱥ.Ȉ()) { Ǯ.ə.Remove(Ⱥ); Ǯ.Õ.Remove(Ⱥ); }
    ϖ.MoveNext(); Ϣ = ǔ.GetNaturalGravity(); if (ý.Ƀ && Ъ && Ǯ
        .ə.Count != 0) { if (Щ || (Ó - AFDelay) >= Й) { for (int H = 0; H < AFCount; H++) Ǯ.ɬ(Ó); Й = Ó; Щ = false; } }
    else { Й = 0; Щ = true; }
}
IEnumerator<bool> Ϫ()
{
    while (true)
    {
        if (ϗ.Count != 0)
        {
            bool ϩ = false; foreach (var Ϩ in ϗ)
            {
                switch (Ϩ)
                {
                    case "raycast_switch": Ơ = !Ơ; break;
                    case "target_switch":
                        ѳ = !
    ѳ; break;
                    case "target_break":
                        if (ý.Ƀ) { Т = true; Ǘ = (ý.ɤ - ʺ.Translation).Length(); П = Ó + (uint)(60f * BreakMatchSeconds * ҏ); }
                        ý = new ɥ();
                        if (й) թ(); ѕ = false; ք(); if (а || б) { ϔ.Ӕ(); Л = М = 0; а = б = false; ϩ = true; }
                        break;
                    case "mode_switch": Ѳ = !Ѳ; break;
                    case "designator_assist":
                        if (
                        д) ѱ = !ѱ; break;
                    case "convergence_cycle": if (Ó > П) { if (++С == Convergences.Length) С = 0; Ǘ = Convergences[С]; } break;
                    case "depth_cycle":
                        if (Ѳ) { if (++ј == Depths.Length) ј = 0; Ѥ = Depths[ј]; }
                        break;
                    case "screen_cycle": if (++Ѱ == (FleetIntegration ? 3 : 2)) Ѱ = 0; break;
                    case
    "keen_ai_switch":
                        Э = !Э; break;
                    case "sequence_mode_cycle": foreach (var Č in λ) Č.ƻ(); if (++ѫ == 3) ѫ = 0; break;
                    case "sequence_delay_cycle":
                        if (ѫ > 0 && ++
    Ѫ == SequenceDelays.Length) Ѫ = 0; break;
                    case "turret_switch": ї = !ї; break;
                    case "fire_switch": л = !л; break;
                    case "salvo_switch":
                        Ь = !Ь;
                        break;
                    case "salvo_delay_cycle": if (Ь) { if (++Ѭ == SalvoDelays.Length) Ѭ = 0; ҍ = SalvoDelays[Ѭ]; } break;
                    case "target_align":
                        Ч = !Ч; if (Ч)
                        {
                            if (ý
                        .Ƀ) Ш = true; if (й) й = false;
                        }
                        else Ш = false; թ(); break;
                    case "rail_align":
                        {
                            if (й && Ѩ == 0) { й = false; թ(); Ш = Ч && ý.Ƀ; break; }
                            var ϱ = Ѩ; Ѩ = 0; bool
                        B = false; foreach (var û in μ) { if (û.Ǫ.ɂ == Ѩ) { й = true; с = "Railguns"; Ш = false; B = true; break; } }
                            if (!B) Ѩ = ϱ; ϩ = true; break;
                        }
                    case
                        "artillery_align":
                        {
                            if (й && Ѩ == 1) { й = false; թ(); Ш = Ч && ý.Ƀ; break; }
                            var ϱ = Ѩ; Ѩ = 1; bool B = false; foreach (var û in μ)
                            {
                                if (û.Ǫ.ɂ == Ѩ)
                                {
                                    й = true; с = "Artillery"
                        ; Ш = false; B = true; break;
                                }
                            }
                            if (!B) Ѩ = ϱ; ϩ = true; break;
                        }
                    case "stop_align": й = false; Ш = false; թ(); break;
                    case "jump_precalc":
                        if (у || Ó < Ѝ)
                        {
                            т = !т; break;
                        }
                        else if (!ф && ϐ.ך.HasValue) break; ё = !ё; if (ё)
                        {
                            Ж = Ó; Е = Ó + (uint)Math.Round(PrecalcSeconds * 60); ϡ = ʺ.Forward *
                            PrecalcMeters + ʺ.Translation; Ϡ = Vector3D.Zero;
                        }
                        break;
                    case "load_torpedoes": Ǯ.ɑ(ɐ, LaunchTimers, TankLoad, ConnLoad); ϩ = true; break;
                    case
    "autofire_switch":
                        Ъ = !Ъ; break;
                    case "fire_next": Ǯ.ɬ(Ó); ϩ = true; break;
                    case "detonate_all": Ǯ.ɨ(); ϩ = true; break;
                    case "target_intercept":
                        if (ý.Ƀ) Ǯ.ɪ(
    2); break;
                    case "side_displacement": Ǯ.ɪ(3); break;
                    case "target_flyby": if (ý.Ƀ) Ǯ.ɪ(4); break;
                    case "velocity_matching":
                        Ǯ.ɪ(5); break
    ;
                    case "copy_target":
                        if (ѳ && !ѕ && FleetIntegration)
                        {
                            double Ͼ = -2; long Ƌ = 0; foreach (var Ͻ in ι.Values.Aggregate(new List<MyTuple<
    long, Vector3D>>(), (ϼ, û) => { if (û.Ԃ && û.ק && !û.ײ.IsZero()) ϼ.Add(new MyTuple<long, Vector3D>(û.ש, û.ײ)); return ϼ; }))
                            {
                                double ϻ = (Ͻ.
    Item2 - (о?.WorldMatrix.Translation ?? ʺ.Translation)).Normalized().Dot(ʺ.Forward); if (ϻ > Ͼ) { Ͼ = ϻ; Ƌ = Ͻ.Item1; }
                            }
                            if (Ͼ != -2)
                            {
                                ѕ = !(ь = false)
    ; ћ = Ƌ;
                            }
                        }
                        else ѕ = false; break;
                    case "jump_fleet":
                        if (!ф && FleetIntegration)
                        {
                            if (ч) { if (ц) { ч = ц = у = т = false; ϐ.Ӓ(); } else ц = true; break; }
                            if (ϐ.ך.HasValue) { double ő = (ϐ.ך.Value - ʺ.Translation).LengthSquared(); if (ő < 25000000 || ő > ѹ * ѹ) break; у = х = !х; break; }
                            ч = у = true; ϐ.ӆ(
                            ʺ.Translation + ʺ.Forward * FleetJumpRanges[џ], FleetJumpSeconds);
                        }
                        break;
                    case "jump_fleet_cycle":
                        if (FleetIntegration && ч && !ц && ++
                            џ == FleetJumpRanges.Length) џ = 0; break;
                    case "server_wipe": ϐ.Ӆ(); break;
                    case "server_disconnect": ϐ.פ = null; ϐ.Һ(); break;
                    case
    "drop_next":
                        if (BLaunchers.Count != 0) { var Ϻ = BLaunchers[Ѯ].Drop(ɐ, Ó); Ѯ++; if (Ѯ >= BLaunchers.Count) Ѯ = 0; if (Ϻ.HasValue) ϛ.Add(Ϻ.Value); }
                        ϩ =
    true; break;
                    case "clear_counters": Ǯ.ɗ = 0; Ǯ.ɖ = 0; break;
                    default:
                        {
                            if (Ϩ.Contains(':'))
                            {
                                var Ϲ = Ϩ.Split(new char[] { ':' }, 2); if (Ϲ[0] ==
    "fire_tag") Ǯ.ɫ(Ϲ[1], Ó);
                                else if (Ϲ[0] == "detonate_tag") Ǯ.ɧ(Ϲ[1]);
                            }
                            break;
                        }
                }
            }
            ϗ.Clear(); if (!ѳ)
            {
                ý = new ɥ(); if (й) թ(); ք(); ѕ = false; if (а || б)
                {
                    ϔ
    .Ӕ(); Л = М = 0; а = б = false;
                }
            }
            if (ϩ || ϗ.Count > 1) yield return true;
        }
        if (Ó >= Е) ё = false; Ф = false; var Ϋ = Х ? ђ ? Ϧ.GetTargetedEntity() : ƌ == null
    ? new MyDetectedEntityInfo() : ƌ.ʥ() : new MyDetectedEntityInfo(); if (ѱ && д && !ѕ)
        {
            long ϸ = ƌ == null ? 0 : ƌ.Đ; if (!Х)
            {
                foreach (var Ǌ in ϋ)
                { Ϋ = Ǌ.GetTargetedEntity(); Х = !Ϋ.IsEmpty() && (!ý.Ƀ || Ϋ.EntityId == ý.Đ); if (Х) { ђ = true; Ϧ = Ǌ; break; } }
            }
            if (!Х) foreach (var Ņ in ξ) if (Х =
                !(Ϋ = Ņ.ʥ()).IsEmpty() && (!ý.Ƀ || Ϋ.EntityId == ý.Đ)) { ђ = false; ƌ = Ņ; ϸ = Ņ.Đ; break; }
            if (ѳ)
            {
                bool Ϸ = false; if (!Х && ý.Ƀ && (ý.ɤ - ʺ.Translation
                ).LengthSquared() < 4000000 && А == uint.MaxValue || Ó >= А) { Ϸ = true; А = Ó + (uint)(60 * ҏ); }
                else А = uint.MaxValue; foreach (var Ņ in ξ)
                {
                    if (Ϸ
                ) Ņ.ʓ(); Ņ.ʖ(ї && Х && (ђ || ϸ != Ņ.Đ), њ);
                }
            }
            yield return true;
        }
        else ƌ = null; Ф = false; foreach (var Ⱥ in ο.ToList()) if (!Ⱥ.N())
            {
                ο.Remove(
                Ⱥ); ξ.Remove(Ⱥ);
            }
        if (ý.Ƀ)
        {
            int Ř = ѯ - (ѕ || !ѱ || ђ ? 0 : ý.Ƀ && ю ? 1 : ξ.Count(Ⱥ => Ⱥ.Ǫ.ɂ == 1)); if (ѭ >= Ř) ѭ = 0; int ϵ = -1; foreach (var Ⱥ in ο.ToList
                ())
            {
                if (!Ⱥ.Ĕ || (ѱ && !ѕ && (Х ? (!ђ && ƌ.Đ == Ⱥ.Đ) : ξ.Contains(Ⱥ)))) continue; Ⱥ.ʖ(ї, њ); if (ї)
                {
                    bool ϴ; var ϳ = է(Ⱥ.Ǫ, Ⱥ.ʣ(), ϣ, ý.ɢ, ы ? ý.ɣ :
                Vector3D.Zero, Ⱥ.ɷ(ǔ), Ϣ, out ϴ); if (ϳ.HasValue && Ⱥ.ʪ(ϳ.Value) && л && !ϴ)
                    {
                        if (Ь && Ⱥ.Ǫ.ɂ == 1)
                        {
                            ϵ++; if ((И == uint.MaxValue || Ó >= И) && ѭ == ϵ)
                            {
                                Ⱥ.ʦ(); И
                = Ó + (uint)(ҍ * 60); if (++ѭ >= Ř) ѭ = 0;
                            }
                        }
                        else Ⱥ.ʦ();
                    }
                    if ((++ř) >= CtrlLimit) { ř = 0; yield return true; }
                }
            }
        }
        else foreach (var Ⱥ in ο) Ⱥ.ʖ(ї
                && (!ѱ || !ξ.Contains(Ⱥ)), њ); if (ř > (CtrlLimit - 2)) { ř = 0; yield return true; }
        Ф = false; foreach (var Ϻ in ϛ.ToList()) if (Ó >= Ϻ.Ⱥ)
            {
                Ϻ.Ȼ.
                IsArmed = true; ϛ.Remove(Ϻ);
            }
        ѓ = UseAIAlarm && і && Ц && ((ý.Ƀ && !є) || (!ý.Ƀ && !(Э && ѳ && Ơ))) && (Ϟ - ʺ.Translation).LengthSquared() <=
                AIAlarmDistance * AIAlarmDistance; if (Ф || ř > 0) { ř = 0; yield return true; }
        foreach (var Ⱥ in Ǯ.ə.ToList())
        {
            if (TankLoad && Ⱥ.ʌ == -1) Ⱥ.ɜ(); if (ř >= 5)
            {
                ř =
                0; yield return true;
            }
        }
        if (ρ.Count != 0) ϕ.MoveNext(); if (PreventManualControl) ǔ.SetValueBool("ControlGyros", !(ͽ.Ĕ && ý.Ƀ && ((й && !
                и) || Ш || х))); double ց = 0; ҋ = 100; Ҋ = 0; Ҍ = 0; Ѧ = 0; ѧ = 0; foreach (var ɮ in μ)
        {
            if (ɮ.Ǫ.ɂ != 0) continue; double ʰ = 0; var ր = ɮ.ɱ().Split('\n',
                ' '); if (ր.Length < 7 || !double.TryParse(ր[6], out ʰ)) continue; Ҍ += ʰ; ց += 5; Ѧ++; double տ = ʰ / 5; if (տ == 100) ѧ++; else if (տ > Ҋ) Ҋ = տ; if (տ < ҋ) ҋ
                = տ;
        }
        Ҍ /= ց; if (υ.Count != 0)
        {
            var Ś = Ҍ == 100 ? έ : Color.Lerp(ί, ή, (float)(Ҍ / 100)); foreach (var ʹ in υ)
            {
                ʹ.Color = Ś; ʹ.
                BlinkIntervalSeconds = Ҍ == 100 ? 1 : 0; ʹ.BlinkLength = 60f;
            }
        }
        var վ = new StringBuilder(); float ս = 1 - (float)Р / RCFails; if (м)
        {
            int ռ = (int)Math.Round(ս * 10);
            int ջ = 10 - ռ; for (int H = 0; H < ռ; H++) վ.Append("|||"); for (int H = 0; H < ջ; H++) վ.Append("--");
        }
        if (χ.Count != 0)
        {
            var Ś = ѳ ? ý.Ƀ ? ѕ ? δ : м ? Color.
            Lerp(ζ, η, ս) : ε : Х ? γ : Ơ || (ѱ && д) ? Φ : Ό : Ό; foreach (var ʹ in χ) ʹ.Color = Ś;
        }
        if (φ.Count != 0)
        {
            var Ś = ї ? л ? ΰ : α : β; foreach (var ʹ in φ) ʹ.Color = Ś;
        }
        foreach (var ʹ in τ) ʹ.Enabled = ѓ; double չ = (RefreshDelay + 1) * (Ќ + 1) / 60; Echo($"{ٶ("<", Color.Gold)} RTAS {ٶ($"- v{VERSION}", Color.LightGray)} {ٶ(">", Color.Gold)}\nRunning...\n{ٶ($"Detected Simulation Speed = {ҏ:0.00}\nAdjusted delay = {Ќ}\nRefresh frequency = {չ:0} seconds ({չ - (Ó - З) / 60:0})", Color.Gray)}{(Profiler ? $"\n\n{ٶ("< Profiler Output >", Color.Orange)}\nLast run: {ѷ:0.00} ms\nAverage: {ٶ($"{ѵ:0.00} ms", Color.Yellow)}\nMaximum: {Ѷ:0.00} ms" : "")}\n\n///////////////\n\n"
        ); var M = new StringBuilder(); if (ý.Ƀ)
        {
            var Ĝ = ϣ - ʺ.Translation; Ѿ = Ĝ.Length(); ѿ = (ý.ɢ - Ϙ.LinearVelocity).Dot(Ĝ) / Ѿ; Ҁ = Math.Abs(ѿ); ҁ =
        ý.ɢ.Length();
        }
        switch (Ѱ)
        {
            case 0:
                {
                    string[] ո ={
$"Targeting: {(ѳ?(!ý.Ƀ?(Ơ||(д&&ѱ)?"Searching...":"Standby"):"LOCKED"):"Disabled")}",$"Aimpoint: {(Ѳ?$"Offset (depth {Ѥ} m)":"Center")}",Ѳ&&ѱ?$" >> Point: {(!ý.Ƀ?"No Target":ý.Ʉ?"Found":"N/A")}":" "," ",λ
.Count!=0?$"Sequencer: {(ѫ==0?"Disabled":ѫ==1?$"Semi-Auto ({SequenceDelays[Ѫ]} seconds)":$"Burst Fire ({SequenceDelays[Ѫ]} seconds)")}"
:" ",Ѧ!=0?$"Rails: ({Ѧ}) {(Ҍ==100?">> FULLY CHARGED <<":(Ҋ-ҋ)<5?$"CHRG: {Ҍ:0.00}%":$"AVG: {Ҍ:0.00}% MIN:{ҋ:0}% {(Ҋ==100?$"-- FULL: {ѧ}":$"MAX: {Ҋ:0}%")}")}"
:" "," ",ο.Count!=0?
$"Turrets: ({ο.Count}) {(ї?(ý.Ƀ?(л?"Engaging":"Tracking"):(л?"Ready (Armed)":"Ready")):(л?"Free (Armed)":"Free"))}":" ",ο.Count!=0?Ь?$"Salvo Mode ({ҍ:0.0} sec)":"Concentration Mode":" "," ",ͽ.Ĕ?$"Alignment: {(ш&&й?$"Axial - {(ý.Ƀ?(и?"Out of range":(з?"ALIGNED":"Aligning")):"Ready")} ({с})":(Ш?$"Target - {(ж?"ALIGNED":"Aligning")}":$"Idle{(Ч?" (Rot. to Target Waiting)":"")}"))}"
:" "}; string[] շ ={$"RC: {(Ơ?ѳ&&!ѕ&&(!ý.Ƀ||!д||!ѱ)?"ACTIVE":"Idle":"Disabled")}",$"{(!ý.Ƀ?$"Convergence: {(Т?$"Break Matching - {Ǘ:0} m":$"#{С+1} - {Ǘ:0} m")}":м?$"RC LOCK: [{վ}] {ս*100:0}%":(ý.Ƀ?ѕ?"Fleet LOCK":"Designator LOCK":""))}"
," ",д?$"Designators: ({ξ.Count+ϋ.Count}) {(ѱ?ѳ?ý.Ƀ?м?Х?"Lock Mismatch!":"No Target":ѕ?"Idle":"LOCKED":Х?"Waiting for Point...":"Searching...":"Idle":"Disabled")}"
:" ",Ю?$"AI Targeting: {(ý.Ƀ?є?"Target Locked":Ц?"Lock Mismatch":"Searching...":Ц?Э&&Ơ&&ѳ?Т?"Waiting for Break Match":"Acquiring...":"Targets Available":"Searching...")}"
:" "," ",Ǯ.ə.Count!=0?$"Torpedoes{(Ъ?" (Autofiring)":"")}:":" ",Ǯ.ə.Count!=0?Ǯ.ɒ?$"Closest: {Ǯ.ɓ:0.00} m":Ǯ.Õ.Count!=0?
$"Fired: {Ǯ.Õ.Count}":"None Fired":" ",Ǯ.Õ.Count>0?$"{Ǯ.Õ[0].ʍ} -- {Ǯ.Õ[0].ʎ} <":" ",Ǯ.Õ.Count>1?$"{Ǯ.Õ[1].ʍ} -- {Ǯ.Õ[1].ʎ} <":" ",Ǯ.Õ.Count>
2?$"{Ǯ.Õ[2].ʍ} -- {Ǯ.Õ[2].ʎ} <":" "}; string[] ն ={
$"Target: {(!ý.Ƀ?"NONE":$"LOCKED ({(є||ѕ?р.Replace('$','S').Replace('ß','S'):ý.ɋ)})")}",ý.Ƀ?$"Method: {(ѕ?"Fleet LOCK":м?$"RC LOCK ({ǎ} cams)":"Designator LOCK")}":"","",ý.Ƀ?$"Dist: {Ѿ:0.0}":"",ý.Ƀ?
$"Vel: {ҁ:0.0} / {Ҁ:0.0} | {(Ҁ<0.05?"---":(ѿ<0?"<--":"-->"))}":"","",ѓ?$"WARNING: Proximity ALERT ({AIAlarmDistance:0.#} meters)":ý.Ƀ&&Ó<Џ?"WARNING: Variable Geometry":Ơ&&ѳ?ѣ!=0?
$"{(э?"Voxel":"Planetary")} Raycast - Compensating...":е?(!ý.Ƀ?ǎ==0?"No Search Cameras!":"Raycast Search Unsustainable!":ǎ==0?"Raycast Line of Sight Broken!":
"Raycast Lock Unsustainable!"+(ǎ!=0?$" Max Range: {ґ:0.0} m":"")):"":""}; for (int H = 0; H < 11; H++) M.AppendLine($"{ո[H]}${շ[H]}"); M.AppendLine("ß");
                    foreach (var պ in ն) M.AppendLine(պ == "" ? " " : $" ${պ}$ "); break;
                }
            case 1:
                {
                    M.Append($"Torpedo Control\nTarget: {(!ý.Ƀ ? "N/A" : $"LOCKED ({ý.ɋ})")}\nLoaded: {Ǯ.ə.Count}\nLaunched: {Ǯ.Õ.Count}\nTotal Launches: {Ǯ.ɗ}\nTotal proxi/sensor detonations: {Ǯ.ɖ}\n\nStatus Readouts\n"
                    ); foreach (var Ō in Ǯ.ə) M.Append($"{Ō.ʍ} -- {(Ō.ʌ == -1 ? "Fuelling..." : Ō.ʎ)}\n"); break;
                }
            case 2:
                {
                    bool ב; M.Append($"Fleet Integration{((ב = ϐ.פ == null) ? "$No server connection!" : (ב = ι.Values.Count(û => û.Ԃ) == 0) ? "$No Allies connected!" : "")}\nTarget: {(!ý.Ƀ ? "N/A" : $"LOCKED ({ý.ɋ})")}"
                    ); if (!ב)
                    {
                        M.AppendLine("\n"); foreach (var ŗ in ι.Values) M.AppendLine(
                    $"{ŗ.ٴ}${(ŗ.ק ? (ŗ.ײ.IsZero() ? "Copying: " : ŗ.צ ? "Desi: " : "RC: ") + ŗ.ظ : "No Target")}");
                    }
                    break;
                }
        }
        if (σ.Count != 0)
        {
            var Ⱥ = $"{(ѳ ? ý.Ƀ ? $"Lock ({(ѕ ? "Fleet" : м ? "RC" : "Desi")})" : Ơ || (ѱ && д) ? "Searching" : "No RC/Desi" : "Off")} | {ο.Count} {(ї ? ý.Ƀ ? л ? "Fire" : "Track" : л ? "Rdy (Arm)" : "Rdy" : л ? "Free (Arm)" : "Free")} | {(ͽ.Ĕ ? ш && й ? и ? $"No Range! {с}" : з ? $"RDY: {с}" : $"Algn.. {с}" : Ш ? ж ? "Tgt ALIGNED" : "Tgt Align.." : Ч ? "Tgt WAIT" : "Idle" : "No Gyro")} | {(Ѧ != 0 ? $"Rails: {Ѧ} - {Ҍ:0.0}%" : "No Rails")}"
                    ; foreach (var ú in σ) ú.HudText = Ⱥ;
        }
        var ח = M.ToString(); ϓ.õ(ח); Echo(ח); yield return true;
    }
}
IEnumerator<bool> ז()
{
    int ř = 0; var ו
                    = new List<MyInventoryItem>(); while (true)
    {
        if (ρ.Count == 0) yield return false; bool ה = Ѣ % 50 == 0; foreach (var H in ρ.ToList())
        {
            if (
                    H.ƫ.Owner == null || !(H.ƫ.Owner as IMyTerminalBlock).IsFunctional) continue; if (ה) H.Ƥ(); MyItemType ד = H.ƪ[0]; int ג = (int)Math.
                    Ceiling((double)H.ƫ.MaxVolume * ProtectionFillRatio / ד.GetItemInfo().Volume); ו.Clear(); H.ƫ.GetItems(ו); MyFixedPoint ט = 0; foreach (
                    var և in ו) ט += և.Amount; (H.ƫ.Owner as IMyInventoryOwner).UseConveyorSystem = ט < ג; if (ט > ג * (MyFixedPoint)
                    ProtectionSiphonThreshold)
            {
                var א = ט - ג; foreach (var V in π)
                {
                    if (V.ƫ.Owner == null || !(V.ƫ.Owner as IMyTerminalBlock).IsFunctional) continue; if (H.ƨ(V.ƫ, ד)
                    && V.ƫ.MaxVolume - V.ƫ.CurrentVolume >= א * ד.GetItemInfo().Volume) foreach (var և in ו)
                        {
                            if (א <= 0) break; if (H.ƫ.TransferItemTo(V.ƫ, և,
                    א > և.Amount ? և.Amount : א)) א -= և.Amount;
                        }
                    if (א <= 0) break;
                }
            }
            if (++ř == 10) { ř = 0; yield return true; }
        }
        Ѣ++; yield return true;
    }
}
bool ֆ(
                    float ǖ)
{
    if (ς.Count == 0 || ǖ < 5000 || ǖ > ѹ) return false; foreach (var օ in ς)
    {
        if (ǖ > օ.MaxJumpDistanceMeters) return false; օ.
                    JumpDistanceMeters = ǖ;
    }
    return true;
}
void ք() { for (int H = 0; H < Ѹ.Length; H++) Ѹ[H] = 0; ѽ = ѻ = Ѻ = 0; }
void փ()
{
    if (!ý.Ƀ) { ы = true; return; }
    if (++Ѡ == Ѹ.Length) Ѡ
                    = 0; Ѹ[Ѡ] = (ϟ - (ϟ = ý.ɣ)).Length(); ѻ = ý.ɣ.Length(); if (ѻ > ѽ) ѽ = ѻ; ы = (Ѻ = Ѹ.Sum() / Ѹ.Length) < MaxJerk;
}
void ւ()
{
    bool Փ = !ý.Ƀ; if (!Փ && ý.ɤ.
                    LengthSquared() < 1000000) { Փ = true; բ(); }
    ű ƭ; double յ = 0; Vector3D? ե = null; if (ъ && Ц) ե = ϙ.LookAtPosition; var Ǔ = Փ && !ե.HasValue ? ϒ.Ǚ(Ǘ, MaxRCRange ?
                    ґ : RCRange, MTVol, Me.CubeGrid.EntityId, ϥ.Forward, ϥ.Translation, out ƭ, out ǎ) : ϒ.ƾ(ե ?? ϣ, ʺ.Translation, MTVol, Me.CubeGrid.
                    EntityId, BiasRCCount, out ƭ, out ǎ, out յ); if (ǎ == 0) { е = true; ѣ = 0; if (++Р > RCFails) բ(); }
    else
    {
        if (э = ƭ == ű.Ű) ѣ = VoxelSkipCount;
        else if (ƭ == ű.Ů
                    && !Ϣ.IsZero()) ѣ = PlanetarySkipCount; ґ = ǎ * (Ћ + 1) * (100f / 6f) * љ; bool դ = ƭ == ű.ų && (Ǔ.Type == MyDetectedEntityType.LargeGrid || Ǔ.Type ==
                    MyDetectedEntityType.SmallGrid || (Voxels && (Ǔ.Type == MyDetectedEntityType.Asteroid || Ǔ.Type == MyDetectedEntityType.Planet))) && κ.Contains(Ǔ.
                    Relationship); if (!Փ) Р++; var Ⱥ = դ ? ټ(Ǔ, ʺ.Translation, Ѥ, true, ý) : new ɥ(); if (Փ || (դ && ý.Đ == Ⱥ.Đ)) { Р = 0; ý = Ⱥ; if (к = Ⱥ.Ƀ) { if (Փ) գ(); м = true; } }
        if (Р >
                    RCFails) բ(); е = Փ ? !MaxRCRange && ґ < RCRange : ґ < յ; Ф = true;
    }
}
void գ()
{
    if (Ч) Ш = true; Т = false; Ǘ = Convergences[С]; ϣ = Ѳ ? ċ.ù(ý.ɡ, ý.ɤ, ý.ɠ) : ý.ɤ; Ѿ = (
                    ϣ - ʺ.Translation).Length(); ў = 0;
}
void բ() { ý = new ɥ(); е = Ш = ѕ = false; if (й) թ(); if (UseTgtLoss) ϔ.ӏ("Alert 1", .5f); ք(); }
void ա()
{
    ь = Ơ
                    && ѳ; var ՙ = ђ ? Ϧ.GetTargetedEntity() : new MyDetectedEntityInfo(); if (!ѱ || (ђ && ՙ.IsEmpty())) { ђ = false; Ϧ = null; Х = false; }
    Ϋ = Х ? ђ ? ՙ : ƌ ==
                    null ? new MyDetectedEntityInfo() : ƌ.ʥ() : new MyDetectedEntityInfo(); Х = !Ϋ.IsEmpty() && (!ý.Ƀ || Ϋ.EntityId == ý.Đ); var Ֆ = !ý.Ƀ ? Vector3D
                    .Zero : (Х ? Ϋ.Position : ċ.Ċ((Ó - ý.ɟ) / 60, ý.ɤ, ý.ɢ, ы ? ý.ɣ : Vector3D.Zero)); ю = Х && !ѕ && (!Ѳ || (ý.Ƀ && ý.Ʉ)) && !Т; var Օ = ý.Ƀ && Ѳ ? ċ.ù(ý.ɡ, Ֆ, ю ? Ϋ
                    .Orientation : ý.ɠ) : Vector3D.Zero; Ϥ = !ý.Ƀ ? Vector3D.Zero : (Ѳ ? Օ : Ֆ); і = Ю && Enemy; bool Ք = false; bool Փ = !ý.Ƀ; if (і)
    {
        var Ւ = ϙ.
                    CurrentWaypoint; bool Ց = ϙ.Enabled, Ր = ъ; Ц = Ϛ.SearchEnemyComponent.FoundEnemyId.HasValue && Ւ != null; if (Ó >= Г && Ր) В = Ó + 60; ъ = !Т && Ơ && ѳ && Э && Փ && !ѕ && Ц
                    && (Ó < Г || !Ր) && Ó > В; if (!Ր && ъ) щ = ǔ.DampenersOverride; else if (Ր && !ъ) Б = Ó + 10; Ϛ.SelectedAttackPattern = ъ ? 1 : 3; Ϛ.Enabled = true; Ϛ.
                    ApplyAction("ActivateBehavior_On"); Ϛ.SetValueBool("CanTargetCharacters", false); Ϛ.SetValueFloat("UpdateInterval", 5); if (!ъ)
        {
            Ϛ.
                    SetValue<long>("OffensiveCombatIntercept_GuidanceType", 0); Ϛ.SetValueBool("OffensiveCombatIntercept_OverrideCollisionAvoidance",
                    true);
        }
        Ϛ.SetValue<long>("TargetingGroup", 0); Ϛ.SetValue<long>("TargetLock", Neutral ? 6 : 2); ϙ.ApplyAction("ActivateBehavior_On");
        if (Ц)
        {
            bool Տ = Ց && ϝ.HasValue; Ϟ = Ւ.Matrix.Translation; ϝ = ъ ? ϙ.LookAtPosition : Ւ.Matrix.Translation; if (ý.Ƀ)
            {
                ϝ = null; Ք = true; р = Ϛ.
        DetailedInfo.Split('\n')[0].Split(new char[] { ' ' }, 3)[2]; if (р.Length > 25) р = р.Substring(0, 22) + "...";
            }
            else if (ъ)
            {
                float ǌ = (float)(Ϟ - ʺ.
        Translation).Length(); Ϛ.SetValue("OffensiveCombatStayAtRange_MinimalDistance", ǌ + 100f); Ϛ.SetValue(
        "OffensiveCombatStayAtRange_MaximalDistance", ǌ + 150f); if (ϝ.HasValue) { if (!Տ) Г = Ó + 20; Ϥ = ϝ.Value; }
            }
        }
        if ((ϙ.Enabled = ъ) && !Ր) Г = Ó + 20;
    }
    else є = Ц = ъ = false; if (ѳ)
    {
        if (ѕ)
        {
            var Վ = ι.
        Values.ToList().FindAll(û => û.Ԃ && û.ק && û.ש == ћ); if (Վ.Count != 0)
            {
                var V = Վ.Find(û => û.צ) ?? Վ.First(); ý = ټ(V, ý); р = V.ظ; е = ь = !(к = true); if (ϒ.
        ş) ϒ.ŝ(); if (Փ) գ();
            }
            else if (!Փ) { if (Ơ || (ѱ && Х && (!Ѳ || ý.Ʉ))) ь = true; else բ(); ѕ = false; }
        }
        else if (ю)
        {
            ý = ټ(Ϋ, ʺ.Translation, Ѥ, ý.Ʉ, ý); е
        = ь = !(к = true); if (ϒ.ş) ϒ.ŝ(); if (Փ) գ();
        }
        else if (!Ơ && !Փ) բ(); if (!ý.Ƀ && (Ơ || ѱ) && UseTgtSearch && !ѓ) Ѽ += Ҏ; if (ý.Ƀ && UseTgtLock && !ѓ) Ғ += Ҏ
        ; if (Ѽ >= 500 && UseTgtSearch && !ѓ) { ϔ.ӏ("Alert 2", .15f); Ѽ = 0; М = Ó; б = true; }
        if (Ғ >= 900 && UseTgtLock && !ѓ)
        {
            ϔ.ӏ("Alert 2", .4f); Ғ = 0; Л = Ó; а
        = true;
        }
    }
    else Ш = false; ϣ = ý.Ƀ && к ? (Ѳ ? ċ.ù(ý.ɡ, ý.ɤ, ý.ɠ) : ý.ɤ) : Ϥ; є = ý.Ƀ && (ϣ - Ϟ).LengthSquared() <= 62500; if (Ք && є) ќ = ý.Đ; є = є && (Ք || ќ == ý.
        Đ);
}
void Ս()
{
    bool Ռ = ý.Ƀ && Ш && ͽ.Ĕ; У = ý.Ƀ && й && ͽ.Ĕ; if (!У && !Ռ && !х) { ͽ.ĺ(false); return; }
    foreach (var Č in μ.ToList()) if (!Č.N(ѫ,
        SequenceTag)) μ.Remove(Č); int Ջ = 0; var Ź = Vector3D.Zero; var ă = ʺ; if (х && (у || Ó < Ѝ))
    {
        var Ȓ = (ϧ - ă.Translation).Normalized(); var ժ = ը(Ȓ, ă); ͽ.ĺ(
        true); ͽ.ľ(ժ.X, ժ.Y, ǔ.RollIndicator * FreeAxisMult, ʺ); ж = Ȓ.Dot(ă.Forward) >= ADeviationLimit; Ф = true;
    }
    else
    {
        if (ё)
        {
            if (!Ϡ.IsZero() && (ʺ.
        Translation - Ϡ).LengthSquared() >= PrecalcMeters * PrecalcMeters * 0.81f) ё = false;
            else Ϡ = ʺ.Translation;
        }
        ʡ ճ = null; if (У)
        {
            foreach (var û in μ)
            {
                if (!û.Ĕ || û.Ǫ.ɂ != Ѩ) continue; var ղ = û.ɰ(); ճ = ճ ?? û; Ź += û.Ǫ.ȿ / 2f * ղ.Forward + ղ.Translation; Ջ++;
            }
            if (Ջ == 0) { й = У = false; թ(); с = ""; }
            else ă
                .Translation = ё ? ϡ : Ź / Ջ;
        }
        if (У)
        {
            var ϳ = է(ճ.Ǫ, ă.Translation, ϣ, ý.ɢ, ы ? ý.ɣ : Vector3D.Zero, ճ.ɷ(Ϙ, ʺ.Translation), Ϣ, out и); if (ϳ.
                HasValue && !и)
            {
                var ժ = ը(ϳ.Value, ă); var ձ = ý.ɢ - Ϙ.LinearVelocity + (Ó - ý.ɟ) / 60f * (ы ? ý.ɣ : Vector3D.Zero); var Ĝ = ϣ - ă.Translation; var հ = Ĝ.
                Length(); var կ = ձ - ċ.þ(ձ, Ĝ); var ծ = կ - ċ.þ(կ, ă.Up); var խ = կ - ċ.þ(կ, ă.Left); ժ += new Vector2D(խ.Length() / հ * (խ.Dot(ă.Up) > 0 ? 1 : -1), ծ.Length
                () / հ * (ծ.Dot(ă.Left) > 0 ? -1 : 1)); double մ = ժ.X, լ = ժ.Y, ի = (ă.Forward.Dot(ʺ.Forward) > .9 || ă.Forward.Dot(ʺ.Backward) > .9 ? ǔ.
                RollIndicator : ă.Forward.Dot(ʺ.Left) > .9 || ă.Forward.Dot(ʺ.Right) > .9 ? ǔ.RotationIndicator.X : ǔ.RotationIndicator.Y) * FreeAxisMult; ͽ.ĺ(true)
                ; ͽ.ľ(մ, լ, ի, ă); з = ϳ.Value.Dot(ă.Forward) >= ADeviationLimit; Ф = true;
            }
            else ͽ.ĺ(false);
        }
        else if (Ռ)
        {
            if (ё) ă.Translation = ϡ; var Ȓ = (ϣ
                - ă.Translation).Normalized(); var ժ = ը(Ȓ, ă); ͽ.ĺ(true); ͽ.ľ(ժ.X, ժ.Y, ǔ.RollIndicator * FreeAxisMult, ʺ); ж = Ȓ.Dot(ă.Forward) >=
                ADeviationLimit; Ф = true;
        }
        else ͽ.ĺ(false);
    }
}
void թ() { Ϝ.Į(); ͽ.ĺ(false); }
Vector2D ը(Vector3D ý, MatrixD Ș)
{
    double Ģ, ě; ċ.Ğ(Ș, ý, out Ģ, out ě); Ģ
                = Ϝ.ň(Ģ); ě = θ.ň(ě); return new Vector2D(Ģ, ě);
}
Vector3D? է(Ǫ զ, Vector3D Ǐ, Vector3D י, Vector3D ع, Vector3D ٲ, Vector3D ٯ, Vector3D
                ȱ, out bool ϴ)
{
    var ٮ = י - Ǐ; var ي = ٮ.LengthSquared(); var ى = ٯ.Dot(י + Math.Sqrt(ي) / զ.Ɂ * ع) > 0; var و = զ.Ɂ * զ.Ɂ; var ձ = ى ? ع : ع - ٯ; var ه = ٮ.
                Dot(ձ); var û = ձ.LengthSquared() - و; var ن = Math.Sqrt(ه * ه - û * ي); double[] ٱ = { (-ه + ن) / û, (-ه - ن) / û }; if (ϴ = double.IsNaN(ن) || (ٱ[0] <= 0 && ٱ[1
                ] <= 0)) return null; var Ⱥ = ٱ[0] <= 0 ? ٱ[1] : ٱ[0] < ٱ[1] ? ٱ[0] : ٱ[1]; var ل = ٮ + (ձ + 0.5 * (ٲ - ȱ) * Ⱥ) * Ⱥ; var ك = ل.Length(); ل /= ك; if (ϴ = ى ? ك > զ.ɀ : ك * ك
                > (ل * զ.Ɂ + ٯ).LengthSquared() * զ.Ⱦ * զ.Ⱦ) return null; return ى ? (ل * ((ه = ٯ.Dot(ل)) + (ه > ن ? -1 : 1) * Math.Sqrt(ه * ه - ٯ.LengthSquared() + و)) - ٯ
                ).Normalized() : ل;
}
Vector3D ق(Vector3D ف, Vector3D ـ, Vector3D ؿ, uint ؾ, Vector3D ؽ, MatrixD ؼ, bool ػ)
{
    var غ = ف + ـ * (Ó - ؾ) / 60f; var
                م = غ - ؽ; var ٳ = (م / (Ó - ؾ)).LengthSquared(); if (ٳ > 16 && ٳ < 160000) { Џ = Ó + 600; if (ػ) return ċ.ġ(ċ.ù(ؿ, ؽ, ؼ) + م, ؽ, ؼ); }
    return ؿ;
}
ɥ ټ(ٵ ڃ, ɥ ڂ
                )
{
    string M; switch (ڃ.أ)
    {
        case 3: M = "Large Grid"; break;
        case 2: M = "Small Grid"; break;
        default:
            M = ((MyDetectedEntityType)ڃ.أ).
                ToString(); break;
    }
    double ځ; ځ = ڃ.ר + (DateTime.Now - new DateTime(ڃ.ء)).TotalSeconds; var ڀ = Ó - (uint)Math.Round(ځ * 60); var Ǌ = ڃ.ײ; var ٿ = ڃ.
                ת; return new ɥ()
                {
                    ɤ = Ǌ,
                    ɡ = ڂ.Ƀ ? ق(ڂ.ɤ, ڂ.ɢ, ڂ.ɡ, ڀ, Ǌ, ٿ, ڂ.Ʉ) : ڃ.ױ,
                    Ʉ = !ڃ.ױ.IsZero(),
                    ɢ = ڃ.װ,
                    ɠ = ٿ,
                    Đ = ڃ.ש,
                    ɋ = M,
                    Ɉ = ڃ.أ,
                    Ƀ = true,
                    Ɇ = ڃ.ظ,
                    ɟ = ڀ,
                    ɣ = !ڂ.Ƀ
                || Ó == ڂ.ɟ ? Vector3D.Zero : (ڃ.װ - ڂ.ɢ) * 60f / (Ó - ڂ.ɟ)
                };
}
ɥ ټ(MyDetectedEntityInfo Ǔ, Vector3D Ǐ, double پ, bool ٽ, ɥ ڂ)
{
    string M; switch (
                (int)Ǔ.Type)
    { case 3: M = "Large Grid"; break; case 2: M = "Small Grid"; break; default: M = Ǔ.Type.ToString(); break; }
    var ٻ = Ǔ.Velocity;
    var ٺ = Ǔ.Position; var ĥ = ڂ.Ƀ ? (Ó - ڂ.ɟ) / 60f : 0; var ϼ = !ڂ.Ƀ || Ó == ڂ.ɟ ? Vector3D.Zero : (Ǔ.Velocity - ڂ.ɢ) / ĥ; var ٹ = Ǔ.HitPosition.Value; var
    ٸ = Ǔ.Orientation; var ٷ = ٽ ? (!ڂ.Ƀ || !ڂ.Ʉ ? ċ.ġ(ٹ + (ٹ - Ǐ).Normalized() * پ, ٺ, ٸ) : ڂ.ɡ) : Vector3D.Zero; if (ڂ.Ƀ) ٷ = ق(ڂ.ɤ, ڂ.ɢ, ٷ, ڂ.ɟ, ٺ, ٸ, ڂ.Ʉ);
    return new ɥ { ɤ = ٺ, ɣ = ϼ, ɢ = ٻ, ɟ = Ó, Đ = Ǔ.EntityId, ɋ = M, Ɉ = (int)Ǔ.Type, ɡ = ٷ, ɠ = ٸ, Ʉ = ٽ, Ƀ = true };
}
string ٶ(string Á, Color Ǝ) =>
    $"[Color=#FF{Ǝ.R:X2}{Ǝ.G:X2}{Ǝ.B:X2}]{Á}[/Color]"; class ٵ
{
    public string ٴ, ظ; public int أ; public Vector3D د; public long ء, ؠ; public Vector3D ײ, ױ, װ; public MatrixD ת; public
    long ש; public double ר; public bool Ԃ = true, ק = true, צ = false;
}
class ץ
{
    public IMyTerminalBlock פ = null; DateTime ף = DateTime.
    MinValue, ע = DateTime.MinValue; bool ס = false, נ = false; public long ן = 0, מ = 0; int ם = 0; StringBuilder ל = new StringBuilder(); HashSet<int> כ =
    new HashSet<int>(); public Vector3D? ך = null; string آ(Vector3D? Ȃ) => Ȃ.HasValue ? Ȃ.Value.ToString("0.0000").Trim('{', '}') : "";
    string ؤ(MatrixD? ش) { if (!ش.HasValue) return ""; var Ġ = ش.Value; return $"{آ(Ġ.Col0)}/{آ(Ġ.Col1)}/{آ(Ġ.Col2)}"; }
    bool ض(string Á, out
    Vector3D Ȃ) => Vector3D.TryParse(Á, out Ȃ); bool ص(string Á, out MatrixD ش)
    {
        string[] Ϲ = Á.Split('/'); ش = MatrixD.Zero; if (Ϲ.Length != 3)
            return false; Vector3D س, ز, ر; if (!(ض(Ϲ[0], out س) && ض(Ϲ[1], out ز) && ض(Ϲ[2], out ر))) return false; ش = new MatrixD(س.X, ز.X, ر.X, س.Y, ز.Y, ر
            .Y, س.Z, ز.Z, ر.Z); return true;
    }
    public void ط(string ذ, Vector3D خ, long ح, string ϭ, int ج, bool? ث, Vector3D? ت, Vector3D? ة,
            Vector3D? ب, MatrixD? ا, long? ϫ, long? ئ, long إ, Dictionary<int, ٵ> Պ)
    {
        var Ψ = DateTime.Now; bool Ԋ = false; string[] һ = null; if (פ == null || Ψ < ף || (!
            ס && !(Ԋ = Ҿ(ref һ)))) { Պ.Clear(); return; }
        string[] ӄ ={$"{ح}",$"{Ψ.Ticks}",ذ,آ(خ),$"{(إ-(ئ??-300))/60f}",ϭ,$"{ج}",
$"{ث?.ToString()??""}",آ(ت),آ(ة),آ(ب),ؤ(ا),$"{ϫ??0}"}; ulong Ҽ; if (!ulong.TryParse((һ ?? (һ = פ.CustomData.Split('\n')))[0], out Ҽ) || һ.Length < 3 || (Ҽ & (
1UL << ם)) == 0) { Ӆ(); return; }
        else if (Ψ > ע.AddSeconds(5) && !Ԋ) { if (!Ҿ(ref һ)) return; }
        bool Ӄ = true; כ.Clear(); if (Ӄ)
        {
            for (int H = 0; H < 64; H
++) if (H != ם && (Ҽ & (1UL << H)) != 0) כ.Add(H); foreach (var H in Պ.Keys.ToList()) if (!כ.Contains(H)) Պ.Remove(H);
        }
        ٵ ӂ = null; Ӄ = false; int
Ӂ = -1; for (int H = 0; H < һ.Length; H++) һ[H] = һ[H].Trim(); ל.Clear().AppendLine(һ[0]); if (נ)
        {
            if (!ך.HasValue)
            {
                ל.AppendLine("\n"); נ =
false;
            }
            else if (Ψ.Ticks > ן) { ל.AppendLine("\n"); Ӓ(); } else ל.AppendLine(آ(ך)).AppendLine(ן + "");
        }
        else
        {
            Vector3D Ӏ; if (ض(һ[1], out Ӏ)
&& long.TryParse(һ[2], out ן) && ן != 0) { ך = Ӏ; var ҿ = (new DateTime(ן) - Ψ).TotalSeconds * 60f; if (ҿ > מ) מ = (long)ҿ; }
            else { ך = null; ן = 0; }
            ל.
AppendLine(һ[1]).AppendLine(һ[2]);
        }
        for (int H = 3; H < һ.Length || ((H - 3) / 13 <= ם); H++)
        {
            if (H < һ.Length)
            {
                int ɉ = (H - 3) / 13; if (ɉ != Ӂ)
                {
                    if (Ӄ = כ.
Contains(ɉ)) { ӂ = Պ.ContainsKey(ɉ) ? Պ[ɉ] : Պ[ɉ] = new ٵ(); כ.Remove(ɉ); }
                    Ӂ = ɉ;
                }
                if (Ӄ)
                {
                    string µ = һ[H]; switch ((H - 3) % 13)
                    {
                        case 0:
                            if (!long.
TryParse(µ, out ӂ.ؠ)) ӂ.ؠ = 0; break;
                        case 1:
                            if (!long.TryParse(µ, out ӂ.ء)) ӂ.ء = 0; Ӄ = ӂ.Ԃ = (Ψ - new DateTime(ӂ.ء)).TotalSeconds < 4; if (!Ӄ) ӂ.ק =
false; break;
                        case 2: ӂ.ٴ = µ; break;
                        case 3: ض(µ, out ӂ.د); break;
                        case 4: if (!double.TryParse(µ, out ӂ.ר)) ӂ.ר = 1; Ӄ = ӂ.ק = ӂ.ר < 0.5; break;
                        case
5:
                            ӂ.ظ = µ.Trim(); break;
                        case 6: if (!int.TryParse(µ, out ӂ.أ)) Ӄ = ӂ.ק = false; break;
                        case 7:
                            if (!bool.TryParse(µ.Trim(), out ӂ.צ)) ӂ.צ =
false; break;
                        case 8: ض(µ, out ӂ.ײ); break;
                        case 9: ض(µ, out ӂ.ױ); break;
                        case 10: ض(µ, out ӂ.װ); break;
                        case 11:
                            if (!ص(µ, out ӂ.ת)) ӂ.ת =
MatrixD.Zero; break;
                        case 12: if (!long.TryParse(µ, out ӂ.ש)) ӂ.ש = 0; break;
                    }
                }
            }
            ל.AppendLine((H - 3) / 13 == ם ? ӄ[(H - 3) % 13] : H < һ.Length ? һ[H] : "")
;
        }
        foreach (var H in כ) Պ.Remove(H); פ.CustomData = ל.ToString().TrimEnd(); ע = Ψ;
    }
    bool Ҿ(ref string[] һ)
    {
        if (פ == null) return false;
        if (ס) ҽ(); ulong Ҽ; һ = һ ?? פ.CustomData.Split('\n'); if (!ulong.TryParse(һ[0], out Ҽ) || һ.Length < 3) { Ӆ(); return false; }
        else if (Ҽ ==
        ulong.MaxValue) return false;
        else for (ם = 0; ם < 64; ם++) if ((Ҽ & (1UL << ם)) == 0) { Ҽ |= 1UL << ם; break; }
        ל.Clear().AppendLine(һ[0] = $"{Ҽ}"); for (
        int H = 1; H < һ.Length; H++) ל.AppendLine(һ[H].Trim()); פ.CustomData = ל.ToString().TrimEnd(); ס = true; return true;
    }
    public void ҽ()
    {
        if
        (!ס || פ == null) return; ס = false; ulong Ҽ; string[] һ = פ.CustomData.Split('\n'); if (!ulong.TryParse(һ[0], out Ҽ)) return; Ҽ &= ~(1UL << ם)
        ; ל.Clear().AppendLine($"{Ҽ}"); for (int H = 3; H < һ.Length; H++) ל.AppendLine((H - 3) / 13 == ם ? "" : һ[H]); פ.CustomData = ל.ToString().Trim
        (); ם = 0; if (!נ) { ך = null; ן = 0; }
    }
    public void Ӆ()
    {
        if (פ == null) return; ף = DateTime.Now.AddSeconds(10); Һ(); if (!נ) { ך = null; ן = 0; }
        פ.
        CustomData = "0\n\n";
    }
    public void Һ() { ס = false; ם = 0; }
    public void ӆ(Vector3D ӕ, double ӓ)
    {
        ך = ӕ; ן = DateTime.Now.AddSeconds(ӓ).Ticks; נ = true;
    }
    public void Ӓ() { if (!נ) return; ך = null; ן = 0; }
}
class ӑ
{
    private List<IMySoundBlock> Ӑ = new List<IMySoundBlock>(); public void N(
    List<IMySoundBlock> Ӑ, bool I)
    {
        if (I)
        {
            for (int H = this.Ӑ.Count - 1; H >= 0; H--) if (this.Ӑ[H].Closed || !this.Ӑ[H].IsFunctional) this.Ӑ.
    RemoveAt(H); foreach (var M in Ӑ) { bool B = false; foreach (var Ɍ in this.Ӑ) if (Ɍ.EntityId == M.EntityId) B = true; if (!B) this.Ӑ.Add(M); }
        }
        else
            this.Ӑ = Ӑ;
    }
    public void Ӕ() { foreach (var M in Ӑ) { if (M.Closed || !M.IsFunctional) continue; M.Stop(); } }
    public void ӏ(string ϔ, float
            ӎ)
    { foreach (var M in Ӑ) { if (M.Closed || !M.IsFunctional) continue; M.SelectedSound = ϔ; M.LoopPeriod = ӎ; M.Play(); } }
}
class Ӎ
{
    public
            static List<Ӎ> ӌ = new List<Ӎ>(); public Dictionary<long, MySprite> Ӌ = new Dictionary<long, MySprite>(); public float ӊ, Ӊ; public Ӎ(
            float ӈ, float Ӈ)
    { ӊ = ӈ; Ӊ = Ӈ; }
    public override bool Equals(object ƣ) { Ӎ ҹ = ƣ as Ӎ; return ҹ != null && ӊ == ҹ.ӊ && Ӊ == ҹ.Ӊ; }
    public override
            int GetHashCode()
    { return ((int)(short)ӊ) | (short)Ӊ << 16; }
    const string ҫ = "SquareSimple"; public MySprite? қ(int Ƌ)
    {
        MySprite Ə; if (
            Ӌ.TryGetValue(0xFFFFFFFFL & Ƌ, out Ə)) return Ə; return null;
    }
    public MySprite Қ(RectangleF ҙ, Color Ǝ, int Ƌ, bool Ҕ = true)
    {
        var Ə =
            new MySprite(SpriteType.TEXTURE, ҫ, ҙ.Center, ҙ.Size, Ǝ); if (Ҕ) Ӌ.Add(0xFFFFFFFFL & Ƌ, Ə); return Ə;
    }
    public MySprite? Ҙ(short Ƌ)
    {
        MySprite Ə; if (Ӌ.TryGetValue((0xFFFFL & Ƌ) << 32, out Ə)) return Ə; return null;
    }
    public MySprite җ(Vector2 Ź, Color Ǝ, TextAlignment Ɣ,
        string Җ, float ҕ, short Ƌ, bool Ҕ = true)
    {
        var Ə = new MySprite(SpriteType.TEXT, Җ, Ź, null, Ǝ, null, Ɣ, ҕ); if (Ҕ) Ӌ.Add((0xFFFFL & Ƌ) << 32, Ə);
        return Ə;
    }
    public List<MySprite> ғ(Vector2 Ź, Vector2 Ҝ, Color Ŷ, Color Ҟ, Color Ҹ, Color Ҷ, string ҵ, string Ҵ, float M) => new List<
        MySprite>{new MySprite(SpriteType.TEXTURE,ҫ,Ź,Ҝ,Ŷ),new MySprite(SpriteType.TEXTURE,ҫ,new Vector2(Ź.X+Ҝ.X/2f,Ź.Y),new Vector2(6,Ҝ
.Y),Ҟ),new MySprite(SpriteType.TEXTURE,ҫ,new Vector2(Ź.X-Ҝ.X/2f,Ź.Y),new Vector2(6,Ҝ.Y),Ҟ),new MySprite(SpriteType.TEXT,ҵ
,new Vector2(Ź.X,Ź.Y-Ҝ.Y/2.5f),null,Ҹ,null,TextAlignment.CENTER,M),new MySprite(SpriteType.TEXT,Ҵ,Ź,null,Ҷ,null,
TextAlignment.CENTER,M)};
}
struct ҳ
{
    const float Ҳ = 10f, ұ = 0.55f, Ұ = 0.45f, ү = 0.3f, Ү = 0.4f, ҭ = 15f, ҷ = 15f, Ҭ = 18f, Ҫ = 40f, ҩ = 60f, Ҩ = 20f, ҧ = 40f, Ҧ = 10f, ҥ =
30f, Ҥ = 1.414213562373095f; const string ң = "Targeting Status", Ң = "Aimpoint Mode", ҡ = "Raycasting", Ҡ = "Designators", ҝ =
"AI Targeting", ҟ = "Targets Available", Ӗ = "Torpedoes", Ԥ = "(Autofiring) Torpedoes", Ԣ = "Target", ԡ = "Fuelling...", Ԡ = "Searching...", ԟ =
"Acquiring...", Ԟ = "Aligining...", ԝ = "ALIGNED", Ԝ = "Fleet Jump Overriding...", ԛ = "ACTIVE", Ԛ = "Directional Bias Mode", ԙ = "WAITING", Ԙ = "LOCKED", ԣ
= "Target Locked", ԗ = "Standby", ԕ = "Standing By", Ԕ = "Disabled", ԓ = "Idle", Ԓ = "Center", ԑ = "Target Point Found", Ԑ = "Target Point N/A"
, ԏ = "RC", Ԏ = "DESI", ԍ = "FLEET", Ԍ = "No Target", ԋ = "Lock Mismatch", Ԗ = "Waiting for Point", ԥ = "Waiting for Break Match", Ի = "---", Չ =
"<--", Շ = "-->", Ն = ">> Fully Charged <<", Յ = "Average", Մ = "Charge", Ճ = "Autoaim", Ղ = "Out of Range", Ձ = "Target Alignment", Հ =
"Jump Precalculation", Կ = "Axial Sequencer", Ծ = "Semi-Auto", Խ = "Burst Fire", Ո = "Turrets", Լ = "Engaging", Ժ = "Tracking", Թ = "Ready", Ը = "Free", Է = " (Armed)",
Զ = "Concentration Mode", Ե = "No Launched Torpedoes", Դ = "Switch screens for more information", Գ = "", Բ = "SquareHollow", Ա =
"No Torpedo launches yet", ԧ = "No proxi/sensor detonations yet", Ԧ = "1 Torpedo launch", ԉ = "1 Proxi/sensor detonation", Ӱ = "Torpedo Control", Ӻ =
"Interception", Ӯ = "Side Displacement", ӭ = "Target Flyby", Ӭ = "Velocity Matching", ӫ = "Target Lost - Holding Course", Ӫ = "No Torpedoes Loaded", ө
= "Target Lock Unsustainable", Ө = "Search Unsustainable", ӧ = "No Search Cameras", Ӧ = "Raycast Line of Sight Broken", ӥ =
"Voxel Raycast", Ӥ = "Planetary Raycast", ӣ = "System compensating...", Ӣ = "Proximity Alarm", ӡ = "Variable Geometry", Ӡ =
"Grid Alterations Detected", ӟ = "Acceleration", Ӟ = "Fleet Integration", ӝ = "No Server Connection!", Ӝ = "No Allied Vessels Connected to Server.", ӛ =
"Following Fleet Jump", Ӛ = "Fleet Jump Available", ә = "Coordinating Fleet Jump", Ә = "Activate again to Initiate", ӯ = "Initiated - Jump on Signal", ӗ =
"Wait for Signal to Initiate Jump", ӱ = "Destination Unreachable By Jump", Ԉ = "Activate to Follow Jump", Ԇ = ">> Initiate Jump! <<"; public IMyTerminalBlock ԅ;
    public IMyTextSurface Ԅ; public bool ԃ; public bool Ԃ; public int ԁ; RectangleF Ԁ; float ӿ, Ӿ, ӽ, Ӽ, ԇ; Ӎ ӻ; Dictionary<long, MyTuple<
    string, string, Vector3D, Vector3D>> ӹ; HashSet<MyTuple<string, Vector3D>> Ӹ; public ҳ(IMyTerminalBlock F, int ӷ, bool Ӷ, float ӵ, float Ӵ
    , float ӳ, float Ӳ = 1)
    {
        ԅ = F; ԃ = Ӷ; Ӿ = ӵ; ӽ = Ӳ; Ӽ = Ӵ; ԇ = ӳ; ԁ = ӷ; ӹ = new Dictionary<long, MyTuple<string, string, Vector3D, Vector3D>>(); Ӹ = new
    HashSet<MyTuple<string, Vector3D>>(); if (F is IMyTextSurface)
        {
            Ԅ = (IMyTextSurface)F; Ԁ = new RectangleF((Ԅ.TextureSize - Ԅ.SurfaceSize) /
    2f, Ԅ.SurfaceSize); float Ί = Ԁ.Width, ď = Ԁ.Height; if ((ӻ = Ӎ.ӌ.Find(Č => Č.ӊ == Ί && Č.Ӊ == ď)) == null) Ӎ.ӌ.Add(ӻ = new Ӎ(Ί, ď)); ӿ = Ԅ.
    SurfaceSize.X / 512f; Ԃ = true; return;
        }
        if (F is IMyTextSurfaceProvider)
        {
            Ԅ = (F as IMyTextSurfaceProvider).GetSurface(ӷ); if (Ԅ == null)
            {
                Ԃ = false
    ; Ԁ = new RectangleF(); ӻ = null; ӿ = 0; return;
            }
            Ԃ = true; Ԁ = new RectangleF((Ԅ.TextureSize - Ԅ.SurfaceSize) / 2f, Ԅ.SurfaceSize); float Ί = Ԁ.
    Width, ď = Ԁ.Height; ; if ((ӻ = Ӎ.ӌ.Find(Č => Č.ӊ == Ί && Č.Ӊ == ď)) == null) Ӎ.ӌ.Add(ӻ = new Ӎ(Ί, ď)); ӿ = Ԅ.SurfaceSize.X / 512f; return;
        }
        Ԁ = new
    RectangleF(); ӻ = null; ӿ = 0; Ԅ = null; Ԃ = false;
    }
    public Vector2 Ɗ(Vector3D Ɖ, MatrixD ƈ, MatrixD ƀ, Vector3D Ƈ, out bool Ɔ)
    {
        var ƅ = ƀ.Translation
    - ƈ.Translation + Ƈ; var Ƅ = ƅ.Dot(ƈ.Forward) / Ɖ.Dot(ƈ.Forward) / ӽ; Ɔ = Ƅ > 0; return new Vector2((float)(Ɖ.Dot(ƈ.Right) * Math.Abs(Ƅ) - ƅ.
    Dot(ƈ.Right) + Ӽ / 2f) / Ӽ * Ԁ.Width, (float)(-Ɖ.Dot(ƈ.Up) * Math.Abs(Ƅ) + ƅ.Dot(ƈ.Up) + ԇ / 2f) / ԇ * Ԁ.Height);
    }
    public bool ƃ(ref Vector2 Ƃ,
    RectangleF ð, bool Ɓ)
    {
        if (!Ɓ && ð.Contains(Ƃ)) return false; if (Ƃ == ð.Center) return true; Ƃ = (Ƃ = Vector2.Normalize(Ƃ - ð.Center)) * (Math.Abs(Ƃ.
    X) > Math.Abs(Ƃ.Y) ? ð.Width / 2 / Ƃ.X * Math.Sign(Ƃ.X) : ð.Height / 2 / Ƃ.Y * Math.Sign(Ƃ.Y)) + ð.Center; return true;
    }
    public static string ſ
    (string ž, double Ž, double ż) => ż > Ž ? $"{(ż / 1000).ToString(ž)} km" : $"{ż.ToString(ž)} m"; public void Ż(Program ĵ)
    {
        if (ԅ.Closed
    || !ԅ.IsFunctional) return; using (var ź = Ԅ.DrawFrame())
        {
            Vector2 Ź; bool Ÿ = ԃ && ĵ.ý.Ƀ; MatrixD ŷ = ĵ.ǔ.WorldMatrix; var Ŷ = Ԅ.
    ScriptBackgroundColor; if (ԃ)
            {
                MatrixD ƀ = ԅ.WorldMatrix, ƈ = ĵ.о == null ? ĵ.ǔ.WorldMatrix : ĵ.о.WorldMatrix; if (ĵ.о != null && ĵ.о.BlockDefinition.SubtypeId ==
    "SmallCameraBlock") ƈ.Translation += ƈ.Forward * 0.25; if (ĵ.о == null)
                {
                    if (ĵ.ǔ.BlockDefinition.SubtypeId == "LargeBlockCockpitSeat") ƈ.Translation += ƈ.
    Up * 0.5 + ƈ.Backward * 0.2;
                    else if (ĵ.ǔ.BlockDefinition.SubtypeId == "LargeBlockCockpitIndustrial") ƈ.Translation += ƈ.Up * 0.08 + ƈ.
    Forward * 0.2;
                    else if (ĵ.ǔ.BlockDefinition.SubtypeId == "SmallBlockCockpit") ƈ.Translation += ƈ.Up * 0.465 + ƈ.Backward * 0.32;
                    else if (ĵ.ǔ.
    BlockDefinition.SubtypeId == "DBSmallBlockFighterCockpit") ƈ.Translation += ƈ.Up * 0.43 + ƈ.Backward * 0.745;
                }
                ŷ = ƈ; var Ƈ = (ԅ.BlockDefinition.
    SubtypeId == "LargeFullBlockLCDPanel" || ԅ.BlockDefinition.SubtypeId == "SmallFullBlockLCDPanel" ? ƀ.Left : ƀ.Forward) * Ӿ; Vector2 Ɵ; foreach (
    var Ō in ĵ.Ǯ.Õ)
                {
                    bool Ɔ; Ɵ = Ɗ(Ō.ˊ.WorldMatrix.Translation - ƈ.Translation, ƈ, ƀ, Ƈ, out Ɔ); if (Ɔ && Ԁ.Contains(Ɵ))
                    {
                        ź.Add(new MySprite(
    SpriteType.TEXTURE, Բ, Ɵ, new Vector2(Ҩ * ӿ), Σ, null, TextAlignment.CENTER, (float)О / 4f)); Ź = new Vector2(Ɵ.X, Ɵ.Y + Ҥ * ӿ * Ҩ / 2f + Ұ * 4f); ź.Add(ӻ.җ(Ź
    , new Color(Σ, 0.6f), TextAlignment.CENTER, Ō.ʍ, ү, 0, false)); Ź.Y += ү * 32f; ź.Add(ӻ.җ(Ź, new Color(Ō.ʌ == 2 ? Color.Orange : Color.Yellow
    , 0.6f), TextAlignment.CENTER, Ō.ʉ && Ō.ʌ != 5 ? ӫ : Ō.ʌ == 2 ? Ӻ : Ō.ʌ == 3 ? Ӯ : Ō.ʌ == 4 ? ӭ : Ӭ, ү, 0, false)); Ź.Y += ү * 32f; ź.Add(ӻ.җ(Ź, new Color(Ō.ʌ ==
    2 && Ō.ʈ < 850 ? Color.Orange : Color.Yellow, 0.6f), TextAlignment.CENTER, $"{Ō.ʈ:0.0} m{(Ō.ʌ == 3 ? " from ship" : Ō.ʌ == 5 ? "/s" : Գ)}", ү, 0,
    false));
                    }
                }
                bool Ɲ; var Ɯ = new RectangleF(Ԁ.Position + new Vector2(5), Ԁ.Size - new Vector2(10)); if (ĵ.FleetIntegration)
                {
                    ӹ.Clear(); Ӹ.
    Clear(); foreach (var û in ĵ.ι.Values)
                    {
                        if (û.Ԃ)
                        {
                            if (û.ק && !û.ײ.IsZero() && (!ĵ.ý.Ƀ || ĵ.ý.Đ != û.ש) && !ӹ.ContainsKey(û.ש)) ӹ[û.ש] = new
    MyTuple<string, string, Vector3D, Vector3D>(û.ظ, û.ٴ, û.ױ.IsZero() ? û.ײ : ċ.ù(û.ױ, û.ײ, û.ת), û.װ); if (!û.د.IsZero()) Ӹ.Add(new MyTuple<
    string, Vector3D>(û.ٴ, û.د));
                        }
                    }
                    foreach (var ƛ in Ӹ)
                    {
                        Ɵ = Ɗ(ƛ.Item2 - ƈ.Translation, ƈ, ƀ, Ƈ, out Ɲ); if (!ƃ(ref Ɵ, Ɯ, !Ɲ))
                        {
                            ź.Add(new MySprite(
    SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(Ҧ * 2f), Ώ)); ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(Ҧ * 1.8f), Ŷ)); ź.Add
    (new MySprite(SpriteType.TEXT, ƛ.Item1, new Vector2(Ɵ.X, Ɵ.Y - Ҧ - ү * 40f), null, Color.White, null, TextAlignment.CENTER, ү)); ź.Add(
    new MySprite(SpriteType.TEXT, $"{ſ("0.0", 1000, (ĵ.ʺ.Translation - ƛ.Item2).Length())}", new Vector2(Ɵ.X, Ɵ.Y + Ҧ * 1.15f), null, Color.
    White, null, TextAlignment.CENTER, ү));
                        }
                        else
                        {
                            ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(Ɲ ? 8 : 10), Ώ)); if (!Ɲ) ź.
    Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(6), Ŷ));
                        }
                    }
                    foreach (var ƚ in ӹ.Values)
                    {
                        Ɵ = Ɗ(ƚ.Item3 - ƈ.Translation, ƈ,
    ƀ, Ƈ, out Ɲ); if (!ƃ(ref Ɵ, Ɯ, !Ɲ))
                        {
                            ź.Add(new MySprite(SpriteType.TEXTURE, "SquareSimple", Ɵ, new Vector2(ҥ), ΐ)); ź.Add(new
    MySprite(SpriteType.TEXTURE, "SquareSimple", Ɵ, new Vector2(ҥ * 0.9f), Ŷ)); ź.Add(new MySprite(SpriteType.TEXT, ƚ.Item1, new Vector2(Ɵ.X,
    Ɵ.Y - ҥ / 2f - Ұ * 36f), null, Color.White, null, TextAlignment.CENTER, Ұ)); ź.Add(new MySprite(SpriteType.TEXT, ƚ.Item2, new Vector2(Ɵ.X
    + ҥ / 2f + 6f, Ɵ.Y - ү * 16f), null, Color.White, null, TextAlignment.LEFT, ү)); ź.Add(new MySprite(SpriteType.TEXT,
    $"Dist {ſ("0.0", 1000, (ĵ.ʺ.Translation - ƚ.Item3).Length())}", new Vector2(Ɵ.X, Ɵ.Y + ҥ / 2f * 1.15f), null, Color.White, null, TextAlignment.CENTER, ү)); ź.Add(new MySprite(SpriteType.TEXT,
    $"Vel {ƚ.Item4.Length():0} m/s", new Vector2(Ɵ.X, Ɵ.Y + ҥ / 2f * 1.15f + ү * 40f), null, Color.White, null, TextAlignment.CENTER, ү));
                        }
                        else
                        {
                            ź.Add(new MySprite(
    SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(Ɲ ? 8 : 10), ΐ)); if (!Ɲ) ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(6), Ŷ));
                        }
                    }
                    Vector3D? ƙ = ĵ.у || ĵ.ф ? ĵ.ϧ : ĵ.ϐ.ך; if (ƙ.HasValue)
                    {
                        Ɵ = Ɗ(ƙ.Value - ƈ.Translation, ƈ, ƀ, Ƈ, out Ɲ); Ɵ.Y -= 6.6666f; if (!ƃ(ref Ɵ, Ɯ, !Ɲ))
                        {
                            ź.Add(new
                    MySprite(SpriteType.TEXTURE, "Triangle", Ɵ, new Vector2(40), Υ)); ź.Add(new MySprite(SpriteType.TEXTURE, "Triangle", Ɵ, new Vector2(36),
                    Ŷ)); Ɵ.Y += 6.6666f; ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(3), Υ));
                        }
                        else
                        {
                            ź.Add(new MySprite(SpriteType.
                    TEXTURE, "Circle", Ɵ, new Vector2(Ɲ ? 8 : 10), Υ)); if (!Ɲ) ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(6), Ŷ));
                        }
                    }
                }
                if (ĵ.Ц
                    && (!ĵ.ý.Ƀ || !ĵ.є))
                {
                    Ɵ = Ɗ((ĵ.ϝ ?? ĵ.Ϟ) - ƈ.Translation, ƈ, ƀ, Ƈ, out Ɲ); Ɵ.Y -= 5f; if (!ƃ(ref Ɵ, Ɯ, !Ɲ))
                    {
                        ź.Add(new MySprite(SpriteType.
                    TEXTURE, "Triangle", Ɵ, new Vector2(30), Μ)); ź.Add(new MySprite(SpriteType.TEXTURE, "Triangle", Ɵ, new Vector2(26), Ŷ));
                    }
                    else
                    {
                        ź.Add(new
                    MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new Vector2(Ɲ ? 8 : 10), Μ)); if (!Ɲ) ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ɵ, new
                    Vector2(6), Ŷ));
                    }
                }
                if (ĵ.ý.Ƀ)
                {
                    Ɵ = Ɗ(ĵ.ϣ - ƈ.Translation, ƈ, ƀ, Ƈ, out Ɲ); if (!ƃ(ref Ɵ, new RectangleF(Ԁ.Position + new Vector2(Ҳ), Ԁ.Size - new
                    Vector2(2 * Ҳ)), !Ɲ))
                    {
                        ź.Add(new MySprite(SpriteType.TEXTURE, Բ, Ɵ, new Vector2(Ҫ * ӿ), ĵ.ѕ ? Ώ : ĵ.м ? Color.Red : Ρ)); Vector2 Ƙ, Ɨ, Ɩ, ƞ, ƕ;
                        TextAlignment Ɣ, Ɠ = TextAlignment.CENTER; bool ƒ = false; if (Ɵ.X < Ԁ.X + Ҳ * ӿ + ҩ)
                        {
                            Ƙ = new Vector2(Ɵ.X + Ҫ / 2 + Ұ * 10, Ɵ.Y - Ҫ / 2 + Ұ * 4); Ɨ = new Vector2(Ƙ.X, Ƙ.Y + Ұ
                        * 35); Ɩ = new Vector2(Ƙ.X, Ɨ.Y + Ұ * 20 + ү * 16); ƞ = new Vector2(Ƙ.X, Ɩ.Y + ү * 32); ƕ = new Vector2(Ɵ.X, Ɵ.Y + Ҫ / 2 + ү * 8); Ɣ = TextAlignment.LEFT; ƒ =
                        true;
                        }
                        else if (Ɵ.X > Ԁ.Right - Ҳ * ӿ - ҩ)
                        {
                            Ƙ = new Vector2(Ɵ.X - Ҫ / 2 - Ұ * 16, Ɵ.Y - Ҫ / 2 + Ұ * 4); Ɨ = new Vector2(Ƙ.X, Ƙ.Y + Ұ * 28); Ɩ = new Vector2(Ƙ.X, Ɨ.Y + Ұ
                        * 20 + ү * 16); ƞ = new Vector2(Ƙ.X, Ɩ.Y + ү * 32); ƕ = new Vector2(Ɵ.X, Ɵ.Y + Ҫ / 2 + ү * 8); Ɣ = TextAlignment.RIGHT; ƒ = true;
                        }
                        else if (Ɵ.Y < Ԁ.Y + Ҳ * ӿ + ҩ)
                        {
                            Ƙ = new Vector2(Ɵ.X, Ɵ.Y + Ҫ / 2 + Ұ * 8); Ɨ = new Vector2(Ƙ.X, Ƙ.Y + Ұ * 28); Ɩ = new Vector2(Ƙ.X, Ɨ.Y + Ұ * 20 + ү * 16); ƞ = new Vector2(Ƙ.X, Ɩ.Y + ү * 32);
                            ƕ = new Vector2(Ɵ.X + Ҫ / 2 + Ұ * 8, Ɵ.Y - Ұ * 16); Ɣ = TextAlignment.CENTER;
                        }
                        else if (Ɵ.Y > Ԁ.Bottom - Ҳ * ӿ - ҩ)
                        {
                            ƞ = new Vector2(Ɵ.X, Ɵ.Y - Ҫ / 2 - Ұ * 32); Ɩ
                            = new Vector2(Ɵ.X, ƞ.Y - ү * 32); Ɨ = new Vector2(Ɵ.X, Ɩ.Y - ү * 16 - Ұ * 20); Ƙ = new Vector2(Ɵ.X, Ɨ.Y - Ұ * 28); ƕ = new Vector2(Ɵ.X + Ҫ / 2 + Ұ * 8, Ɵ.Y - Ұ *
                            16); Ɣ = TextAlignment.CENTER; Ɠ = TextAlignment.LEFT;
                        }
                        else
                        {
                            Ɨ = new Vector2(Ɵ.X, Ɵ.Y + Ҫ / 2 + Ұ * 8); Ɩ = new Vector2(Ɨ.X, Ɨ.Y + Ұ * 20 + ү * 16); ƞ =
                            new Vector2(Ɨ.X, Ɩ.Y + ү * 32); Ƙ = new Vector2(Ɨ.X, Ɵ.Y - Ҫ / 2 - Ұ * 36); ƕ = new Vector2(Ɵ.X + Ҫ / 2 + Ұ * 8, Ɵ.Y - Ұ * 16); Ɣ = TextAlignment.CENTER; Ɠ =
                            TextAlignment.LEFT;
                        }
                        int Ũ = ĵ.ι.Values.Count(û => û.Ԃ && û.ק && û.ש == ĵ.ý.Đ); if (Ũ != 0) ź.Add(ӻ.җ(ƕ, Ώ, Ɠ, ƒ ? $"Allies: {Ũ}" : $"{Ũ}", ƒ ? ү : Ұ, 0, false)); ź
                            .Add(ӻ.җ(Ɨ, Μ, Ɣ, $"Dist {ſ("0.00", 1000, ĵ.Ѿ)}", Ұ, 0, false)); ź.Add(ӻ.җ(Ɩ, Μ, Ɣ, $"Vel {ĵ.ҁ:0.0} m/s", ү, 0, false)); ź.Add(ӻ.җ(ƞ, Μ, Ɣ,
                            $"Orth {ĵ.Ҁ:0.0} m/s {(ĵ.Ҁ < 0.05 ? Ի : ĵ.ѿ < 0 ? Չ : Շ)}", ү, 0, false)); ź.Add(ӻ.җ(Ƙ, Σ, Ɣ, ĵ.ѕ || ĵ.є ? ĵ.р : ĵ.ý.ɋ, Ұ, 0, false)); Ÿ = false;
                    }
                    else
                    {
                        var Ƒ = Ɵ - Ԁ.Center; float Ɛ = (float)Math.Acos(
                            Vector2.Dot(Ƒ, new Vector2(0f, -1f)) / Ƒ.Length()) * (Ƒ.X < 0 ? -1f : 1f); ź.Add(new MySprite(SpriteType.TEXTURE, "Triangle", Ɵ, new Vector2(
                            15f, 19f) * ӿ, new Color(255, 25, 0), null, TextAlignment.CENTER, Ɛ));
                    }
                }
            }
            Ź = new Vector2(Ԁ.Position.X + Ҳ * ӿ, Ԁ.Position.Y + Ҳ * ӿ); ź.Add(ӻ.Ҙ(
                            1) ?? ӻ.җ(Ź, Τ, TextAlignment.LEFT, ң, ұ, 1)); MySprite? Ə; Color Ǝ; string ƍ; bool ƌ = ĵ.ѱ && ĵ.д; short Ƌ = (short)(ĵ.ѳ ? ĵ.ý.Ƀ ? ĵ.ѕ ? 106 : ĵ.м ?
                            2 : 3 : ƌ ? 4 : ĵ.Ơ ? 5 : 6 : 7); Ź.Y += ұ * 16 + Ұ * 16; Ə = ӻ.Ҙ(Ƌ); if (!Ə.HasValue)
            {
                if (ĵ.ѳ)
                {
                    if (ĵ.ý.Ƀ) { Ǝ = ĵ.ѕ ? Ώ : ĵ.м ? Σ : Ρ; ƍ = Ԙ; }
                    else
                    {
                        if (ĵ.Ơ || ƌ)
                        {
                            Ǝ = ƌ ? Ρ :
                            Color.Yellow; ƍ = Ԡ;
                        }
                        else { Ǝ = Color.Yellow; ƍ = ԗ; }
                    }
                }
                else { Ǝ = Color.Red; ƍ = Ԕ; }
                Ə = ӻ.җ(new Vector2(Ź.X + 5, Ź.Y), Ǝ, TextAlignment.LEFT, ƍ, Ұ, Ƌ);
            }
            ź.Add(Ə.Value); Ź.Y += ұ * 18 + Ұ * 16; ź.Add(ӻ.Ҙ(8) ?? ӻ.җ(Ź, Τ, TextAlignment.LEFT, Ң, ұ, 8)); Ź.Y += ұ * 16 + Ұ * 16; ź.Add(ĵ.Ѳ ? ӻ.җ(new Vector2(Ź
            .X + 5, Ź.Y), Π, TextAlignment.LEFT, $"Offset (depth {ĵ.Ѥ} m)", Ұ, 0, false) : ӻ.Ҙ(9) ?? ӻ.җ(new Vector2(Ź.X + 5, Ź.Y), Λ, TextAlignment.
            LEFT, Ԓ, Ұ, 9)); if (ĵ.Ѳ && ƌ)
            {
                Ƌ = (short)(ĵ.ý.Ƀ && ĵ.ý.Ʉ ? 10 : ĵ.Х && ĵ.ѳ ? 12 : 11); ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(new Vector2(Ź.X + 5, Ź.Y + Ұ * 32), ĵ.ý.Ƀ && ĵ.ý.Ʉ
            ? Λ : ĵ.Х && ĵ.ѳ ? Color.Red : new Color(Π, 0.2f), TextAlignment.LEFT, ĵ.ý.Ƀ && ĵ.ý.Ʉ ? ԑ : Ԑ, Ұ, Ƌ));
            }
            float Ŵ; if (ĵ.Ѱ == 0 && ĵ.ý.Ƀ)
            {
                Ź.Y += Ұ * 48 + ұ *
            16; ź.Add(ӻ.Ҙ(83) ?? ӻ.җ(Ź, Τ, TextAlignment.LEFT, ӟ, ұ, 83)); Ź += new Vector2(5f, ұ * 16 + ү * 24); ź.Add(ӻ.җ(Ź, Λ, TextAlignment.LEFT,
            $"Maximum: {ĵ.ѽ:0.00} m/s/s", ү, 0, false)); Ź.Y += ү * 36; ź.Add(ӻ.җ(Ź, Λ, TextAlignment.LEFT, $"Current: {ĵ.ѻ:0.00} m/s/s", ү, 0, false)); Ź.Y += ү * 36; ź.Add(ӻ.җ(Ź, ĵ
            .ы ? Κ : Color.Yellow, TextAlignment.LEFT, $"Jerk: {ĵ.Ѻ:0.00} m/s^3", ү, 0, false)); Ŵ = (Ԁ.Height - 2 * Ҳ * ӿ) / 3f; Ź = new Vector2(Ԁ.X + Ҳ * ӿ, Ԁ.
            Center.Y - Ŵ / 2f); float ŏ = ĵ.м ? (float)ĵ.Р / ĵ.RCFails : 1; if (ĵ.ѕ) ź.Add(ӻ.қ(4) ?? ӻ.Қ(new RectangleF(Ź.X, Ź.Y, ҭ, Ŵ), Ύ, 4));
                else if (!ĵ.м) ź.
            Add(ӻ.қ(1) ?? ӻ.Қ(new RectangleF(Ź.X, Ź.Y, ҭ, Ŵ), Ν, 1));
                else if (ŏ != 0) ź.Add(ӻ.Қ(new RectangleF(Ź.X, Ź.Y, ҭ, Ŵ * ŏ), Ο, 0, false)); Ƌ = (short
            )(ĵ.ѕ ? 92 : ĵ.м ? 13 : 14); ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(new Vector2(Ź.X + ҭ / 2f, Ź.Y - ү * 36), ĵ.ѕ ? Ώ : ĵ.м ? Σ : Ρ, TextAlignment.CENTER, ĵ.ѕ ? ԍ : ĵ.м ? ԏ : Ԏ, ү, Ƌ
            )); if (ĵ.м)
                {
                    Ǝ = Color.Lerp(Σ, Color.Red, ŏ); ź.Add(ӻ.җ(new Vector2(Ź.X + ҭ / 2f, Ź.Y + Ŵ + ү * 15f), Ǝ, TextAlignment.CENTER,
            $"{100 - ŏ * 100:0}%", ү, 1, false)); Ź.Y += Ŵ * ŏ; Ǝ = Ξ; ź.Add(ŏ == 0 ? ӻ.қ(2) ?? ӻ.Қ(new RectangleF(Ź.X, Ź.Y, ҭ, Ŵ), Ǝ, 2) : ӻ.Қ(new RectangleF(Ź.X, Ź.Y, ҭ, Ŵ * (1 - ŏ)),
            Ǝ, 0, false));
                }
            }
            Ź = new Vector2(Ԁ.Right - Ҳ * ӿ, Ԁ.Y + Ҳ * ӿ); ź.Add(ӻ.Ҙ(16) ?? ӻ.җ(Ź, Τ, TextAlignment.RIGHT, ҡ, ұ, 16)); if (ĵ.Ơ)
            {
                if (!ĵ.ý.Ƀ || ĵ
            .м) { if (ĵ.ѳ) { Ǝ = ĵ.ý.Ƀ && ĵ.ϒ.ş ? Μ : Σ; ƍ = ĵ.ý.Ƀ && ĵ.ϒ.ş ? Ԛ : ԛ; Ƌ = (short)(ĵ.ý.Ƀ && ĵ.ϒ.ş ? 90 : 17); } else { Ǝ = Color.Yellow; ƍ = ԓ; Ƌ = 18; } }
                else
                {
                    Ǝ = ĵ.
            ѕ ? Ώ : Ρ; ƍ = ԓ; Ƌ = (short)(ĵ.ѕ ? 91 : 19);
                }
            }
            else { Ǝ = Color.Red; ƍ = Ԕ; Ƌ = 20; }
            Ź.Y += ұ * 16 + Ұ * 16; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(new Vector2(Ź.X - 5, Ź.Y), Ǝ,
            TextAlignment.RIGHT, ƍ, Ұ, Ƌ, false)); Ź.Y += Ұ * 32; if (ĵ.Т) { ƍ = $"Break Matching - {ĵ.Ǘ:0.0} m"; Ǝ = Μ; }
            else
            {
                ƍ = $"Convergence #{ĵ.С + 1} - {ĵ.Ǘ:0} m"
            ; Ǝ = Λ; if (ĵ.ý.Ƀ) Ǝ.A = 50;
            }
            ź.Add(ӻ.җ(new Vector2(Ź.X - 5, Ź.Y), Ǝ, TextAlignment.RIGHT, ƍ, Ұ, 0, false)); if (ĵ.д)
            {
                Ź.Y += ұ * 18 + Ұ * 16; ź.Add(ӻ
            .Ҙ(23) ?? ӻ.җ(Ź, Τ, TextAlignment.RIGHT, Ҡ, ұ, 23)); if (ĵ.ѱ)
                {
                    if (ĵ.ѳ)
                    {
                        if (ĵ.ý.Ƀ)
                        {
                            if (ĵ.м)
                            {
                                if (ĵ.Х) { ƍ = ԋ; Ǝ = Color.Red; Ƌ = 24; }
                                else
                                {
                                    ƍ = Ԍ; Ǝ =
            Color.Yellow; Ƌ = 25;
                                }
                            }
                            else if (ĵ.ѕ) { ƍ = ԓ; Ǝ = Ώ; Ƌ = 109; } else { ƍ = ԣ; Ǝ = Ρ; Ƌ = 26; }
                        }
                        else
                        {
                            if (ĵ.Х) { if (ĵ.Ѳ) { ƍ = Ԗ; Ǝ = Μ; Ƌ = 27; } else { ƍ = ԥ; Ǝ = Μ; Ƌ = 80; } }
                            else { ƍ = Ԡ; Ǝ = Λ; Ƌ = 28; }
                        }
                    }
                    else { ƍ = ԓ; Ǝ = Color.Yellow; Ƌ = 29; }
                }
                else { ƍ = Ԕ; Ǝ = Λ; Ƌ = 30; }
                Ź.Y += ұ * 16 + Ұ * 16; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(new Vector2(Ź.X - 5, Ź.
                            Y), Ǝ, TextAlignment.RIGHT, ƍ, Ұ, Ƌ));
            }
            if (ĵ.і && (ĵ.Ѱ == 0 || !ĵ.д))
            {
                Ź.Y += ұ * 18 + Ұ * 16; Ƌ = 31; if (ĵ.д) Ƌ++; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, Τ,
                            TextAlignment.RIGHT, ҝ, ұ, Ƌ)); if (ĵ.ý.Ƀ) { if (ĵ.є) { ƍ = ԣ; Ǝ = Σ; Ƌ = 33; } else if (ĵ.Ц) { ƍ = ԋ; Ǝ = Color.Red; Ƌ = 35; } else { ƍ = Ԡ; Ǝ = Λ; Ƌ = 37; } }
                else if (ĵ.Ц)
                {
                    if (ĵ.
                            Э && ĵ.Ơ && ĵ.ѳ) { if (ĵ.Т) { ƍ = ԥ; Ǝ = Μ; Ƌ = 39; } else { ƍ = ԟ; Ǝ = Μ; Ƌ = 41; } }
                    else { ƍ = ҟ; Ǝ = Color.Red; Ƌ = 43; }
                }
                else
                {
                    ƍ = Ԡ; Ǝ = ĵ.Э ? Color.Yellow : Λ; Ƌ = (short
                            )(ĵ.Э ? 45 : 37);
                }
                Ź.Y += ұ * 16 + Ұ * 16; if (ĵ.д) Ƌ++; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(new Vector2(Ź.X - 5, Ź.Y), Ǝ, TextAlignment.RIGHT, ƍ, Ұ, Ƌ));
            }
            if (ĵ.Ѱ == 0
                            )
            {
                if (!ԃ || Ÿ)
                {
                    Ź.Y += ұ * 18 + Ұ * 16; Ƌ = 49; if (ĵ.д) Ƌ++; if (ĵ.Ю) Ƌ++; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, Τ, TextAlignment.RIGHT, Ԣ, ұ, Ƌ)); Ź += new Vector2(-5
                            , Ұ * 16 + ұ * 16); ź.Add(ӻ.җ(Ź, ĵ.ý.Ƀ ? Σ : Λ, TextAlignment.RIGHT, ĵ.ý.Ƀ ? ĵ.ѕ || ĵ.є ? ĵ.р : ĵ.ý.ɋ : Ԍ, Ұ, 0, false)); if (ĵ.ý.Ƀ)
                    {
                        Ź.Y += Ұ * 32; ź.Add(ӻ.
                            җ(Ź, Μ, TextAlignment.RIGHT, $"Dist {ſ("0.00", 1000, ĵ.Ѿ)}", Ұ, 0, false)); Ź.Y += Ұ * 20 + ү * 16; ź.Add(ӻ.җ(Ź, Μ, TextAlignment.RIGHT,
                            $"Vel {ĵ.ҁ:0.0} m/s", ү, 0, false)); Ź.Y += ү * 32; ź.Add(ӻ.җ(Ź, Μ, TextAlignment.RIGHT, $"Orth {ĵ.Ҁ:0.0} m/s {(ĵ.Ҁ < 0.05 ? Ի : ĵ.ѿ < 0 ? Չ : Շ)}", ү, 0, false)); int
                            Ũ = ĵ.ι.Values.Count(û => û.Ԃ && û.ק && û.ש == ĵ.ý.Đ); if (Ũ != 0) ź.Add(ӻ.җ(Ź + new Vector2(0, ү * 32), Ώ, TextAlignment.RIGHT,
                            $"Allies Targeting: {Ũ}", ү, 0, false));
                    }
                }
            }
            else
            {
                Ź = new Vector2(Ԁ.Center.X, Ԁ.Y + Ҳ * ӿ + ұ * 86 + Ұ * 96); ź.Add(ӻ.қ(5) ?? ӻ.Қ(new RectangleF(Ԁ.X, Ź.Y - ұ * 10, Ԁ.Width, Ԁ
                            .Height * 4 / 5f), GUIBackgroundColor, 5)); ź.Add(ӻ.қ(6) ?? ӻ.Қ(new RectangleF(Ԁ.X + Ҳ, Ź.Y + ұ * 40, Ԁ.Width - 2 * Ҳ, 3f), Λ, 6)); if (ĵ.Ѱ == 1)
                {
                    ź.
                            Add(ӻ.Ҙ(65) ?? ӻ.җ(Ź, Τ, TextAlignment.CENTER, Ӱ, ұ, 65)); Ź.Y += ұ * 42 + Ұ * 16; ź.Add(ӻ.җ(new Vector2(Ԁ.X + Ҳ, Ź.Y), Λ, TextAlignment.LEFT, ĵ.Ǯ
                            .ɗ == 0 ? Ա : ĵ.Ǯ.ɗ == 1 ? Ԧ : $"{ĵ.Ǯ.ɗ} Torpedo launches", Ұ, 0, false)); ź.Add(ӻ.җ(new Vector2(Ԁ.Right - Ҳ, Ź.Y), Λ, TextAlignment.RIGHT, ĵ.Ǯ
                            .ɖ == 0 ? ԧ : ĵ.Ǯ.ɖ == 1 ? ԉ : $"{ĵ.Ǯ.ɖ} Proxi/sensor detonations", Ұ, 0, false)); if (ĵ.Ǯ.ə.Count != 0)
                    {
                        int ř = 3, Ř = (int)((Ԁ.Height - Ź.Y - Ұ * 16)
                            / Ұ / 32f); ź.Add(ӻ.җ(new Vector2(Ԁ.X + Ҳ, Ź.Y + Ұ * 32), Λ, TextAlignment.LEFT, $"Total torpedoes loaded: {ĵ.Ǯ.ə.Count}", Ұ, 0, false)); ź
                            .Add(ӻ.җ(new Vector2(Ԁ.Right - Ҳ, Ź.Y + Ұ * 32), Λ, TextAlignment.RIGHT, $"Torpedoes fired: {ĵ.Ǯ.Õ.Count}", Ұ, 0, false)); foreach (var
                            Ō in ĵ.Ǯ.ə)
                        {
                            float Ŗ = Ź.Y + Ұ * 32 * ř; ź.Add(ӻ.җ(new Vector2(Ԁ.X + Ҳ, Ŗ), Λ, TextAlignment.LEFT, Ō.ʍ, Ұ, 0, false)); ź.Add(ӻ.җ(new Vector2(
                            Ԁ.Right - Ҳ, Ŗ), Ō.ʌ == 2 ? Ō.ʉ ? Color.Yellow : Σ : Λ, TextAlignment.RIGHT, Ō.ʌ == -1 ? ԡ : Ō.ʎ, Ұ, 0, false)); if (ř++ == Ř) break;
                        }
                    }
                    else ź.Add(ӻ.Ҙ(
                            66) ?? ӻ.җ(Ź + new Vector2(0, Ԁ.Height / 3f), Λ, TextAlignment.CENTER, Ӫ, Ұ, 66));
                }
                else
                {
                    ź.Add(ӻ.Ҙ(93) ?? ӻ.җ(Ź, Τ, TextAlignment.CENTER, Ӟ,
                            ұ, 93)); Ź.Y += ұ * 42 + Ұ * 16; if (ĵ.ϐ.פ == null) ź.Add(ӻ.Ҙ(94) ?? ӻ.җ(Ź + new Vector2(0, Ԁ.Height / 3f), Color.Yellow, TextAlignment.CENTER, ӝ,
                            Ұ, 94));
                    else if (ĵ.ι.Values.Count(û => û.Ԃ) == 0) ź.Add(ӻ.Ҙ(95) ?? ӻ.җ(Ź + new Vector2(0, Ԁ.Height / 3f), Λ, TextAlignment.CENTER, Ӝ, Ұ, 95)
                            );
                    else
                    {
                        int ř = 0, Ř = (int)((Ԁ.Height - Ź.Y - Ұ * 16) / Ұ / 32f); foreach (var ŗ in ĵ.ι.Values)
                        {
                            if (!ŗ.Ԃ) continue; float Ŗ = Ź.Y + Ұ * 32f * ř; ź.Add
                            (ӻ.җ(new Vector2(Ԁ.X + Ҳ, Ŗ), ŗ.ק && ĵ.ý.Ƀ && ŗ.ש == ĵ.ý.Đ ? Ώ : Λ, TextAlignment.LEFT, ŗ.ٴ, Ұ, 0, false)); ź.Add(ӻ.җ(new Vector2(Ԁ.Right - Ҳ, Ŗ
                            ), ŗ.ק ? ŗ.ײ.IsZero() ? Ώ : ŗ.צ ? Ρ : Σ : Κ, TextAlignment.RIGHT, ŗ.ק ? (ŗ.ײ.IsZero() ? "Copying: " : ŗ.צ ? "Desi: " : "RC: ") + ŗ.ظ : "No Target", Ұ, 0
                            , false)); if (ř++ == Ř) break;
                        }
                    }
                }
            }
            if (ĵ.Ѧ != 0)
            {
                Ź = new Vector2(Ԁ.Center.X, Ԁ.Y + Ҳ * ӿ); ź.Add(ӻ.җ(Ź, Τ, TextAlignment.CENTER,
                            $"Railguns ({ĵ.Ѧ})", Ү, 0, false)); Ŵ = (Ԁ.Width - 2 * Ҳ * ӿ) / 3f; Ź = new Vector2(Ź.X - Ŵ / 2f, Ź.Y + Ү * 32); float ŏ = (float)ĵ.Ҍ / 100f; if (ŏ != 1) ź.Add(ӻ.Қ(new
                            RectangleF(Ź.X + Ŵ * ŏ, Ź.Y, Ŵ * (1 - ŏ), ҷ), Ι, 0, false)); if (ĵ.Ҍ != 0) ź.Add(ĵ.Ҍ == 100 ? ӻ.қ(3) ?? ӻ.Қ(new RectangleF(Ź.X, Ź.Y, Ŵ, ҷ), Θ, 3) : ӻ.Қ(new
                            RectangleF(Ź.X, Ź.Y, Ŵ * ŏ, ҷ), Θ, 0, false)); bool ŕ; ƍ = $"{((ŕ = ((ĵ.ѧ != 0 ? 100 : ĵ.Ҋ) - ĵ.ҋ) > 5) ? Յ : Մ)}: {ĵ.Ҍ:0.00}%"; Ź.X += Ŵ / 2f; ź.Add(ӻ.җ(new
                            Vector2(Ź.X, Ź.Y - Ү * 16 + ҷ / 2f), Λ, TextAlignment.CENTER, ƍ, Ү, 0, false)); Ź.Y += ҷ + ү * 6; if (ĵ.ѧ != 0) ź.Add(ĵ.Ҍ == 100 ? ӻ.Ҙ(52) ?? ӻ.җ(Ź, Μ,
                            TextAlignment.CENTER, Ն, ү, 52) : ӻ.җ(Ź, Color.Yellow, TextAlignment.CENTER, $"Full: {ĵ.ѧ}", ү, 0, false)); if (ĵ.Ҍ != 100 && ŕ)
                {
                    ź.Add(ӻ.җ(new Vector2
                            (Ź.X - Ŵ / 2f, Ź.Y), Color.Yellow, TextAlignment.LEFT, $"MIN: {ĵ.ҋ:0.00}%", ү, 0, false)); ź.Add(ӻ.җ(new Vector2(Ź.X + Ŵ / 2f, Ź.Y), Color.
                            Yellow, TextAlignment.RIGHT, $"MAX: {ĵ.Ҋ:0.00}%", ү, 0, false));
                }
            }
            bool Ŕ = ĵ.ϐ.ך.HasValue; if ((ĵ.х || ĵ.ч || Ŕ) && ĵ.Ѱ == 0)
            {
                Ź = new Vector2(Ԁ.
                            Center.X, Ԁ.Center.Y - 4f * Ԁ.Height / 10f); if (ĵ.х) { Ƌ = 107; Ǝ = Color.Green; ƍ = ӛ; }
                else if (ĵ.ч) { Ƌ = 97; Ǝ = Ώ; ƍ = ә; }
                else
                {
                    Ƌ = 100; Ǝ = Color.White; ƍ = Ӛ;
                }
                ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, Ǝ, TextAlignment.CENTER, ƍ, ұ, Ƌ)); Ź.Y += ұ * 32; float œ = ĵ.ф ? ĵ.Ў - ĵ.Ó : (float)(new DateTime(ĵ.ϐ.ן) - DateTime.
                Now).TotalSeconds * 60f + 600; float Œ = (ĵ.ч ? (float)ĵ.FleetJumpSeconds * 60f : ĵ.ϐ.מ) + 600; float ŏ = œ / Œ; ź.Add(ӻ.Қ(new RectangleF(new
                Vector2(Ź.X - Ԁ.Width / 4f * ŏ, Ź.Y), new Vector2(Ԁ.Width / 2f * ŏ, Ҭ)), ĵ.у || Ŕ ? Η : Ζ, 0, false)); ŏ = 600 / Œ; ź.Add(ӻ.Қ(new RectangleF(new Vector2(Ź.
                X - Ԁ.Width / 4f * ŏ, Ź.Y), new Vector2(4f, Ҭ)), Μ, 0, false)); ź.Add(ӻ.Қ(new RectangleF(new Vector2(Ź.X + Ԁ.Width / 4f * ŏ - 4f, Ź.Y), new
                Vector2(4f, Ҭ)), Μ, 0, false)); Ź.Y += Ұ * 6; if (ĵ.х || ĵ.ч)
                {
                    if (ĵ.ё) { Ƌ = 102; Ǝ = Color.Red; ƍ = "ACTIVE"; }
                    else if (ĵ.т)
                    {
                        Ƌ = 103; Ǝ = Color.Yellow; ƍ =
                "Waiting";
                    }
                    else { Ƌ = 104; Ǝ = Κ; ƍ = "OFF"; }
                    ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, Ǝ, TextAlignment.CENTER, $"Precalc: {ƍ}", Ұ, Ƌ));
                }
                Ź.Y += ұ * 16 + Ұ * 16; Ǝ = Λ; double ő =
                1; bool Ő = !ĵ.ч && ((ő = ((ĵ.х ? ĵ.ϧ : ĵ.ϐ.ך.Value) - ĵ.ʺ.Translation).LengthSquared()) < 25000000 || ő > ĵ.ѹ * ĵ.ѹ); if (ĵ.ф) { Ƌ = 96; Ǝ = Μ; ƍ = Ԇ; }
                else if (ĵ.ч) { if (ĵ.ц) { Ƌ = 98; ƍ = ӯ; } else { Ƌ = 99; ƍ = Ә; } } else if (Ő) { Ƌ = 102; ƍ = ӱ; Ǝ = Color.Red; } else if (ĵ.х) { Ƌ = 108; ƍ = ӗ; } else { Ƌ = 101; ƍ = Ԉ; }
                ź.
                Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, Ǝ, TextAlignment.CENTER, ƍ, Ұ, Ƌ)); Ź.Y += Ұ * 32; ź.Add(ӻ.җ(Ź, Ő ? Color.Yellow : Λ, TextAlignment.CENTER, $"{(!ĵ.ч ? $"Jump Distance: {Math.Sqrt(ő) / 1000:0.00} km" : $"Calibration: {ĵ.FleetJumpRanges[ĵ.џ] / 1000:0.00} km")}{(Ő ? " (Unreachable)" : "")}"
                , Ұ, 0, false));
            }
            else if (ĵ.ё)
            {
                Ź = new Vector2(Ԁ.Center.X, Ԁ.Center.Y - 3f * Ԁ.Height / 8f); ź.Add(ӻ.Ҙ(84) ?? ӻ.җ(Ź, Color.Aqua,
                TextAlignment.CENTER, Հ, ұ, 84)); float ŏ = (ĵ.Е - ĵ.Ó) / ((float)(ĵ.Е - ĵ.Ж)); ź.Add(ӻ.Қ(new RectangleF(new Vector2(Ź.X - Ԁ.Width / 4f * ŏ, Ź.Y), new
                Vector2(Ԁ.Width / 2f * ŏ, Ҭ)), Η, 0, false)); Ź.Y += ұ * 16 + Ұ * 18; ź.Add(ӻ.Ҙ(85) ?? ӻ.җ(Ź, Κ, TextAlignment.CENTER,
                $"Calibration: {ĵ.PrecalcMeters} meters", Ұ, 85)); if (ĵ.ý.Ƀ)
                {
                    Ź.Y += Ұ * 32; ź.Add(ӻ.җ(Ź, Λ, TextAlignment.CENTER, $"Distance after jump: {(ĵ.ϡ - ĵ.ϣ).Length():0.00} meters",
                Ұ, 0, false));
                }
            }
            if (ĵ.Ѱ == 0)
            {
                if (ĵ.ͽ.Ĕ && (ĵ.й || ĵ.Ч))
                {
                    Ź = new Vector2(Ԁ.Center.X, Ԁ.Center.Y - Ԁ.Height / 4f); bool Ŏ = ĵ.х && (ĵ.у || ĵ.Ó < ĵ.Ѝ
                ); ź.Add(ӻ.җ(Ź, Color.Red, TextAlignment.CENTER, $">> {(ĵ.ш && ĵ.й ? Ճ : Ձ)} {((ĵ.У || ĵ.Ш) && !Ŏ ? ԛ : ԙ)} <<", ұ, 0, false)); Ź.Y += ұ * 16 + Ұ * 16;
                    if (ĵ.й)
                    {
                        ź.Add(ӻ.җ(Ź, Κ, TextAlignment.CENTER, $"{ĵ.с} Autoaim", Ұ, 0, false)); Ź.Y += Ұ * 32; ź.Add(ӻ.җ(Ź, ĵ.з && ĵ.У && !ĵ.и && !Ŏ ? Μ : Λ,
                    TextAlignment.CENTER, Ŏ ? Ԝ : ĵ.У ? ĵ.и ? Ղ : ĵ.з ? ԝ : Ԟ : ԕ, Ұ, 0, false));
                    }
                    else ź.Add(ӻ.җ(Ź, ĵ.ж && ĵ.Ш && ĵ.ý.Ƀ && !Ŏ ? Μ : Λ, TextAlignment.CENTER, Ŏ ? Ԝ : ĵ.Ш && ĵ.ý.
                    Ƀ ? ĵ.ж ? ԝ : Ԟ : ԕ, Ұ, 0, false));
                }
                if (ĵ.λ.Count != 0)
                {
                    Ź = new Vector2(Ԁ.X + Ҳ * ӿ, Ԁ.Bottom - Ҳ * ӿ - ү * 80 - Ұ * 48 - ұ * 32 - (ĵ.ο.Count == 0 ? 0 : ұ * 32 + Ұ * 48)); ź
                    .Add(ӻ.җ(Ź, Τ, TextAlignment.LEFT, Կ, ұ, 0, false)); Ź = new Vector2(Ź.X + 5, Ź.Y + ұ * 15 + Ұ * 15); ź.Add(ӻ.җ(Ź, Λ, TextAlignment.LEFT, ĵ.ѫ == 0 ?
                    Ԕ : ĵ.ѫ == 1 ? $"{Ծ} ({ĵ.SequenceDelays[ĵ.Ѫ]} seconds)" : $"{Խ} ({ĵ.SequenceDelays[ĵ.Ѫ]} seconds)", Ұ, 0, false));
                }
                if (ĵ.ο.Count != 0)
                {
                    Ź = new Vector2(Ԁ.X + Ҳ * ӿ + 5, Ԁ.Bottom - Ҳ * ӿ - ү * 16); if (ĵ.ѥ != 0)
                    {
                        ź.Add(ӻ.җ(Ź, Λ, TextAlignment.LEFT, $"Assault Turrets: {ĵ.ѥ}", ү, 0,
                    false)); if (ĵ.ѯ != 0) Ź.Y -= ү * 32;
                    }
                    if (ĵ.ѯ != 0) ź.Add(ӻ.җ(Ź, Λ, TextAlignment.LEFT, $"Artillery Turrets: {ĵ.ѯ}", ү, 0, false)); Ź = new Vector2
                    (Ź.X - 5, Ԁ.Bottom - Ҳ * ӿ - ү * 80 - Ұ * 48 - ұ * 32); ź.Add(ӻ.Ҙ(53) ?? ӻ.җ(Ź, Τ, TextAlignment.LEFT, Ո, ұ, 53)); Ź = new Vector2(Ź.X + 5, Ź.Y + ұ * 15 + Ұ * 15)
                    ; Ƌ = (short)((ĵ.ї ? ĵ.ý.Ƀ ? ĵ.л ? 54 : 56 : 58 : 60) + (ĵ.л && (!ĵ.ý.Ƀ || !ĵ.ї) ? 1 : 0)); ƍ =
                    $"{(ĵ.ї ? ĵ.ý.Ƀ ? ĵ.л ? Լ : Ժ : Թ : Ը)}{(ĵ.л && (!ĵ.ý.Ƀ || !ĵ.ї) ? Է : Գ)}"; ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, ĵ.ї && ĵ.л ? ĵ.ý.Ƀ ? Μ : Color.Yellow : Λ, TextAlignment.LEFT, ƍ, Ұ, Ƌ)); if (ĵ.Ы)
                    {
                        Ź.Y += Ұ * 28; ź.Add((ĵ.Ь ? null : ӻ.Ҙ(79
                    )) ?? ӻ.җ(Ź, Λ, TextAlignment.LEFT, ĵ.Ь ? $"Salvo Mode ({ĵ.ҍ} seconds)" : Զ, Ұ, 79, !ĵ.Ь));
                    }
                }
                if (ĵ.Ǯ.ə.Count != 0 || ĵ.Ъ)
                {
                    Ź = new Vector2(Ԁ.
                    Right - Ҳ * ӿ, Ԁ.Bottom - Ҳ * ӿ - ү * 80 - Ұ * 48 - ұ * 32); Ƌ = (short)(ĵ.Ъ ? 78 : 62); ź.Add(ӻ.Ҙ(Ƌ) ?? ӻ.җ(Ź, ĵ.Ъ ? Μ : Τ, TextAlignment.RIGHT, ĵ.Ъ ? Ԥ : Ӗ, ұ, Ƌ)); int
                    ō = ĵ.Ǯ.Õ.Count; Ź = new Vector2(Ź.X - 5, Ź.Y + ұ * 16 + Ұ * 16); ź.Add(ō == 0 ? ӻ.Ҙ(63) ?? ӻ.җ(Ź, Λ, TextAlignment.RIGHT, Ե, Ұ, 63) : ӻ.җ(Ź, ō < 4 && !ĵ.Ǯ.
                    ɒ ? Κ : Λ, TextAlignment.RIGHT, ĵ.Ǯ.ɒ ? $"Closest: {ĵ.Ǯ.ɓ:0.00} m" : $"Launched Torpedoes: {ō}", Ұ, 0, false)); for (int H = 0; H < ō && H < 3; H
                    ++)
                    {
                        Ź = new Vector2(Ź.X, Ź.Y + (H == 0 ? Ұ * 24 : ү * 16) + ү * 16); var Ō = ĵ.Ǯ.Õ[H]; ź.Add(ӻ.җ(Ź, Ō.ʌ == 2 ? Ō.ʉ ? Color.Yellow : Σ : Λ, TextAlignment.
                    RIGHT, $"{Ō.ʍ} -- {Ō.ʎ}", ү, 0, false));
                    }
                    Ź = new Vector2(Ź.X, Ԁ.Bottom - Ҳ * ӿ); ź.Add(ӻ.Ҙ(64) ?? ӻ.җ(Ź, Κ, TextAlignment.RIGHT, Դ, ү, 64));
                }
                Ź =
                    new Vector2(Ԁ.Right - Ҳ * ӿ - ҧ * 2, Ԁ.Center.Y - ҧ); if (ĵ.Ц && (!ĵ.ý.Ƀ || !ĵ.є))
                {
                    var Ś = ĵ.ѓ && (ĵ.ѐ = !ĵ.ѐ) ? Color.Yellow : Μ; var ŋ = ((ĵ.ϝ ?? ĵ.Ϟ) - ŷ.
                    Translation).Normalized(); bool ś = ŋ.Dot(ŷ.Forward) > 0; var Ų = new Vector2((float)ŋ.Dot(ŷ.Right), (float)ŋ.Dot(ŷ.Down)) * .85f * ҧ + new
                    Vector2(ҧ) + Ź; ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ų, new Vector2(ҧ / (ś ? 6f : 4f)), Ś)); if (!ś) ź.Add(new MySprite(SpriteType.
                    TEXTURE, "Circle", Ų, new Vector2(ҧ / 7f), Ŷ));
                }
                if (ĵ.ý.Ƀ)
                {
                    var ŋ = (ĵ.ý.ɤ - (ĵ.ё ? ĵ.ϡ : ŷ.Translation)).Normalized(); bool ś = ŋ.Dot(ŷ.Forward) >
                    0; var Ų = new Vector2((float)ŋ.Dot(ŷ.Right), (float)ŋ.Dot(ŷ.Down)) * .85f * ҧ + new Vector2(ҧ) + Ź; ź.Add(new MySprite(SpriteType.
                    TEXTURE, "Circle", Ų, new Vector2(ҧ / (ś ? 6f : 4f)), Δ)); if (!ś) ź.Add(new MySprite(SpriteType.TEXTURE, "Circle", Ų, new Vector2(ҧ / 7f), Ŷ));
                }
                ź
                    .Add(new MySprite(SpriteType.TEXTURE, "CircleHollow", new Vector2(Ź.X, Ź.Y + ҧ), new Vector2(ҧ * 2), Ε, null, TextAlignment.LEFT)); ź
                    .Add(ӻ.Қ(new RectangleF(Ź.X + ҧ - 1, Ź.Y + ҧ * 5f / 12f, 2, ҧ * 5f / 12f), Ε, 0, false)); ź.Add(ӻ.Қ(new RectangleF(Ź.X + ҧ - 1, Ź.Y + ҧ * 7f / 6f, 2, ҧ * 5f /
                    12f), Ε, 0, false)); ź.Add(ӻ.Қ(new RectangleF(Ź.X + ҧ * 5f / 12f, Ź.Y + ҧ - 1, ҧ * 5f / 12f, 2), Ε, 0, false)); ź.Add(ӻ.Қ(new RectangleF(Ź.X + ҧ * 7f / 6f
                    , Ź.Y + ҧ - 1, ҧ * 5f / 12f, 2), Ε, 0, false)); Ź = new Vector2(Ԁ.Center.X, Ԁ.Center.Y + Ԁ.Height / 4f); if (ĵ.ѓ) ź.AddRange(ӻ.ғ(Ź, new Vector2(Ԁ.
                    Width / 3f, Ұ * 75), Α, Β, Color.Red, Κ, Ӣ, $"Enemy Detected ({ĵ.AIAlarmDistance} m)", Ұ));
                else if (ĵ.Ó < ĵ.Џ) ź.AddRange(ӻ.ғ(Ź, new Vector2(Ԁ
                    .Width / 3f, Ұ * 75), Α, Β, Color.Red, Κ, ӡ, Ӡ, Ұ));
                else if (ĵ.Ơ && ĵ.ѳ)
                {
                    if (ĵ.ѣ != 0) ź.AddRange(ӻ.ғ(new Vector2(Ԁ.Center.X, Ԁ.Center.Y + Ԁ.
                    Height / 4f), new Vector2(Ԁ.Width / 3f, Ұ * 75), Γ, Β, Color.Red, Κ, ĵ.э ? ӥ : Ӥ, ӣ, Ұ));
                    else if (ĵ.е) ź.AddRange(ӻ.ғ(new Vector2(Ԁ.Center.X, Ԁ.
                    Center.Y + Ԁ.Height / 4f), new Vector2(Ԁ.Width / 3f, Ұ * 75), Γ, Β, Color.Yellow, Κ, !ĵ.ý.Ƀ ? ĵ.ǎ == 0 ? ӧ : Ө : ĵ.ǎ == 0 ? Ӧ : ө, ĵ.ǎ != 0 ?
                    $"Max Range: {ĵ.ґ:0.0} m" : "RC Search Disabled", Ұ));
                }
            }
            ź.Dispose();
        }
    }
}
enum ű { Ű, ů, Ů, ŭ, Ŭ, ū, Ū, ų }
class ũ
{
    List<ǜ> ŧ = new List<ǜ>(), Ŧ = new List<ǜ>(), ť = new
                    List<ǜ>(); LinkedList<ǜ> Ť = new LinkedList<ǜ>(); LinkedListNode<ǜ> ţ; int Ţ = 0, š = 0, Š = 0; public bool ş = false; public int Ş
    {
        get
        {
            return
                    ŧ.Count;
        }
    }
    public void ŝ() { Ť.Clear(); ţ = null; Š = 0; ş = false; }
    public void ŵ(bool Ơ) { foreach (var Ǌ in ŧ) Ǌ.Ǜ.EnableRaycast = Ơ; }
    public MyDetectedEntityInfo Ǚ(double Ǘ, double ǖ, double Ư, long Ƴ, Vector3D Ǖ, Vector3D ǔ, out ű Ʈ, out int ǎ)
    {
        if ((ǎ = ť.Count) == 0)
        {
            Ʈ =
    ű.Ů; return new MyDetectedEntityInfo();
        }
        ǜ Ǒ; MyDetectedEntityInfo? Ǔ; int ǒ = Ţ >= ť.Count ? 0 : Ţ, ǘ = 0; do
        {
            if (Ţ >= ť.Count) Ţ = 0; Ǔ = (Ǒ = ť[Ţ
    ++]).Ƶ(ǖ, (Ǖ * Ǘ + ǔ - Ǒ.Ǜ.WorldMatrix.Translation).Normalized(), Ƴ, false, false, 0, Ư, out Ʈ); if (Ʈ == ű.Ŭ) ǎ--;
        } while (!Ǔ.HasValue && Ţ != ǒ
    && ++ǘ < ť.Count); return Ǔ ?? new MyDetectedEntityInfo();
    }
    public MyDetectedEntityInfo ƾ(Vector3D ǐ, Vector3D Ǐ, double Ư, long Ƴ,
    int ư, out ű Ʈ, out int ǎ, out double ż)
    {
        Ʈ = ű.ū; var Ǎ = ǐ - Ǐ; var ǌ = ż = Ǎ.Length(); Ǎ /= ǌ; Ŧ = ŧ.FindAll(ǋ => ǋ.Ǜ.CanScan(ǋ.Ǜ.WorldMatrix.
    Translation + Ǎ)); if ((ǎ = Ŧ.Count) == 0) return new MyDetectedEntityInfo(); ǜ Ǒ; MyDetectedEntityInfo? Ǔ = null; Vector3D Ĝ; double ǩ; if (Š == 0 || Ť.
    First == null)
        {
            for (int H = 0; H < 2; H++)
            {
                int ǒ = š >= Ŧ.Count ? š = 0 : š; bool Ǣ = false, ǧ = false; int ǘ = 0; do
                {
                    if (š >= Ŧ.Count) { š = 0; Ǣ = true; }
                    if ((Ǣ && š
    == ǒ) || ++ǘ >= Ŧ.Count) break; Ǔ = (Ǒ = Ŧ[š++]).Ƶ((ǩ = (Ĝ = ǐ - Ǒ.Ǜ.WorldMatrix.Translation).Length()) + 50, Ĝ / ǩ, Ƴ, false, H == 1, ư, Ư, out Ʈ); if (Ʈ
    == ű.Ū) ǧ = true;
                } while (!Ǔ.HasValue); if (Ǔ.HasValue || !ǧ) break;
            }
            if (ư != 0 && Ʈ == ű.ů)
            {
                Š = ư; Ť = Ŧ.Aggregate(new LinkedList<ǜ>(), (Ǧ, ǥ) => {
                    var Ǥ = Ǧ.First; while (Ǥ != null && (ǐ - Ǥ.Value.Ǜ.WorldMatrix.Translation).LengthSquared() < (ǐ - ǥ.Ǜ.WorldMatrix.Translation).
                    LengthSquared()) Ǥ = Ǥ.Next; if (Ǥ == null) Ǧ.AddLast(ǥ); else Ǧ.AddBefore(Ǥ, ǥ); return Ǧ;
                }); ţ = Ť.First; ş = true;
            }
        }
        if (Š != 0 && Ť.First != null)
        {
            for (int
                    H = 0; H < 2; H++)
            {
                var ǣ = ţ; bool Ǣ = false; do
                {
                    if (ţ == null) { if (Ǣ) break; ţ = Ť.First; Ǣ = true; }
                    if (ţ == ǣ && Ǣ) break; Ǔ = ţ.Value.Ƶ((ǩ = (Ĝ = ǐ - ţ.
                    Value.Ǜ.WorldMatrix.Translation).Length()) + 50, Ĝ / ǩ, Ƴ, true, H == 1, ư, Ư, out Ʈ); if (!Ǔ.HasValue || Ʈ == ű.ů) ţ = ţ.Next;
                } while (Ʈ == ű.ů || !Ǔ.
                    HasValue); if (Ǔ.HasValue && Ʈ != ű.ů) break;
            }
            Š--;
        }
        else ş = false; return Ǔ ?? new MyDetectedEntityInfo();
    }
    public void Ǩ(List<IMyCameraBlock
                    > ǡ, Vector3D Ǖ, bool Ǡ)
    {
        if (!Ǡ) { ŧ.Clear(); ť.Clear(); }
        if (ǡ.Count == 0) return; bool ǟ, Ǟ; foreach (var Ǒ in ǡ)
        {
            ǟ = ŧ.Select(ǋ => ǋ.Ǜ).
                    Contains(Ǒ); Ǟ = ť.Select(ǋ => ǋ.Ǜ).Contains(Ǒ); if (ǟ && Ǟ) continue; var ǝ = new ǜ(Ǒ); if (!ǟ) ŧ.Add(ǝ); if (!Ǟ && Ǒ.CanScan(Ǒ.WorldMatrix.
                    Translation + Ǖ)) ť.Add(ǝ);
        }
        foreach (var Ǒ in ŧ.ToList()) if (Ǒ.Ǜ.Closed || !Ǒ.Ǜ.IsFunctional) { ŧ.Remove(Ǒ); ť.Remove(Ǒ); }
    }
}
class ǜ
{
    public
                    IMyCameraBlock Ǜ; int ǚ = 0; bool ǉ = false; public ǜ(IMyCameraBlock ƿ) { Ǜ = ƿ; }
    public MyDetectedEntityInfo? Ƶ(double ż, Vector3D ƴ, long Ƴ, bool Ʋ,
                    bool Ʊ, int ư, double Ư, out ű Ʈ)
    {
        if (!Ǜ.CanScan(1f, new Vector3D(ƴ.Dot(Ǜ.WorldMatrix.Right), ƴ.Dot(Ǜ.WorldMatrix.Up), ƴ.Dot(Ǜ.
                    WorldMatrix.Backward)))) { if (ǚ > 0) ǚ--; Ʈ = ű.Ŭ; return null; }
        if (ż > Ǜ.AvailableScanRange) { if (ǚ > 0) ǚ--; Ʈ = ű.ŭ; return null; }
        if ((!ǉ || Ʊ) && Ʋ) ǉ = ++ǚ
                    == ư;
        else if (ǚ > 0) { ǉ = --ǚ > 0; Ʈ = ű.Ū; return null; }
        var ƭ = Ǜ.Raycast(Ǜ.WorldMatrix.Translation + ƴ * ż); Ʈ = ƭ.IsEmpty() || ƭ.BoundingBox.
                    Volume < Ư ? ű.Ů : ƭ.EntityId == Ƴ ? ű.ů : ƭ.Type == MyDetectedEntityType.Asteroid || ƭ.Type == MyDetectedEntityType.Planet ? ű.Ű : ű.ų; return ƭ;
    }
}
class Ƭ
{
    public IMyInventory ƫ; public List<MyItemType> ƪ = new List<MyItemType>(); HashSet<long> Ʃ = new HashSet<long>(); public Ƭ(
IMyInventory Ƨ)
    { ƫ = Ƨ; Ƨ.GetAcceptedItems(ƪ); }
    public bool ƨ(IMyInventory Ƨ, MyItemType Ʀ)
    {
        if (Ƨ.Owner == null || Ʃ.Contains(Ƨ.Owner.EntityId)
) return false; bool ƥ = ƫ.CanTransferItemTo(Ƨ, Ʀ); if (!ƥ) Ʃ.Add(Ƨ.Owner.EntityId); return ƥ;
    }
    public void Ƥ() => Ʃ.Clear(); public
override bool Equals(object ƣ) => (ƣ is Ƭ && (ƣ as Ƭ).ƫ == ƫ) || (ƣ is IMyInventory && (ƣ as IMyInventory) == ƫ); public override int
GetHashCode() => ƫ.GetHashCode();
}
class Ƣ
{
    public string ƶ; public List<à> ơ = new List<à>(); uint Ʒ = 0, ǈ = 0; bool Ǉ = false; bool? ǆ = null; int ǅ = -
1; public Ƣ(string Ǆ) { ƶ = Ǆ; }
    public bool ǃ(List<IMyUserControllableGun> ǂ, uint Ó)
    {
        if (Ǉ) return true; foreach (var ǁ in ǂ)
        {
            bool B
= false; foreach (var Ö in ơ) if (Ö.ģ == ǁ) { B = true; break; }
            if (B) continue; ơ.Add(new à(ǁ));
        }
        for (int H = ơ.Count - 1; H >= 0; H--) if (ơ[H].ģ.
Closed || !ơ[H].ģ.IsFunctional) ơ.RemoveAt(H); ǀ(Ó); return ơ.Count != 0;
    }
    public void ǀ(uint Ó)
    {
        int H = ǅ == -1 || ǅ >= ơ.Count ? 0 : ǅ; do
        {
            if (H >=
ơ.Count) { ǅ = -1; return; }
            if (!ơ[H].ģ.Closed && ơ[H].ģ.IsFunctional && ơ[H].Ò() && !ơ[H].Ô(Ó)) { ǅ = H; return; }
            if (++H >= ơ.Count) H = 0;
        }
        while (H != (ǅ == -1 ? 0 : ǅ)); ǅ = -1;
    }
    public void ƾ(bool ƽ, bool Ƽ = false)
    {
        if (!ƽ)
        {
            if (ǆ == false) return; foreach (var Ö in ơ) Ö.ģ.Enabled = true;
            ǆ = false; return;
        }
        for (int H = 0; H < ơ.Count; H++) ơ[H].ģ.Enabled = (!Ƽ && H == ǅ) || !ơ[H].Ò(); ǆ = true;
    }
    public void ƻ()
    {
        Ǉ = false; ǅ = -1; ƾ(
            false);
    }
    public void ƺ(uint Ó, int ƹ, double Ƹ)
    {
        if (ƹ == 0)
        {
            ƾ(false); foreach (var Ö in ơ) if (!Ö.ģ.Closed && Ö.ģ.IsFunctional && Ö.Ò() && !Ö
            .Ô(Ó) && Ö.ģ.IsShooting) Ö.Õ(Ó); return;
        }
        if (ǅ == -1 || ǅ >= ơ.Count || ơ[ǅ].ģ.Closed || !ơ[ǅ].ģ.IsFunctional) ǀ(Ó); if (ǅ == -1)
        {
            ƻ(); return;
        }
        ƾ(true, ƹ == 1 && Ó < ǈ); if (ƹ == 1 ? ơ[ǅ].ģ.IsShooting && Ó >= ǈ : Ǉ ? Ó >= Ʒ : ơ[ǅ].ģ.IsShooting)
        {
            ơ[ǅ].Õ(Ó); if (Ǉ = ƹ == 2) ơ[ǅ].ģ.ShootOnce();
            else
                ǈ = Ó + (uint)Math.Round(Ƹ * 60); ǀ(Ó); if (ǅ == -1) Ǉ = false; else if (Ǉ) Ʒ = Ó + (uint)Math.Round(Ƹ * 60);
        }
    }
}
class à
{
    public
                IMyUserControllableGun ģ; bool ß, Þ = false; long Ý; uint Ü; static MyDefinitionId Û = new MyDefinitionId(typeof(MyObjectBuilder_GasProperties),
                "Electricity"); MyResourceSinkComponent Ú; const float Ù = 0.0002f, Ø = 1e-6f; public à(IMyUserControllableGun Ö)
    {
        ģ = Ö; Ú = Ö.Components.Get<
                MyResourceSinkComponent>(); ß = Ö.BlockDefinition.SubtypeId.Contains("Railgun"); switch (Ö.BlockDefinition.SubtypeId)
        {
            case "SmallBlockAutoCannon":
                Ü =
                24; break;
            case "SmallBlockMediumCalibreGun": Ü = 360; break;
            case "LargeBlockLargeCalibreGun": Ü = 720; break;
            case "LargeRailgun":
                Ü =
                3552; break;
            case "SmallRailgun": Ü = 1200; break;
            default:
                Ü = (uint)(Ö is IMySmallGatlingGun ? 5 : Ö is IMySmallMissileLauncher ? Ö.
                CubeGrid.GridSizeEnum == MyCubeSize.Large ? 30 : 60 : 60); break;
        }
        Ý = -Ü;
    }
    public void Õ(uint Ó) { Ý = Ó; Þ = ß; }
    public bool Ô(uint Ó) => Ó < Ý + Ü;
    public bool Ò() { if (!ß) return true; var Ñ = Ú.MaxRequiredInputByType(Û) > Ù + Ø; if (Ñ) Þ = false; return !Ñ && !Þ; }
}
class Ð : Í
{
    Vector2I Ï;
    public Ð(IMyTextSurface ª, Vector2I Î) : base(ª) { Ï = Î; }
    public override void W(string V, char U, char T) => ñ(V, U, T, Ï, 1, á.Font);
}
class
    Í
{
    public IMyTextSurface á; Dictionary<int, string> ô = new Dictionary<int, string>(); Dictionary<int, string> ò = new Dictionary<int
    , string>(); public Í(IMyTextSurface ª) { á = ª; }
    protected void ñ(string V, char U, char T, Vector2 ð, float ï, string î = "White")
    {
        var í = V.Split(T); var ó = new StringBuilder[í.Length]; StringBuilder ì = new StringBuilder("  \n  "), ê = new StringBuilder(); var é =
        á.MeasureStringInPixels(ì, î, ï) / 2f; float è = 0; for (int H = 0; H < í.Length; H++)
        {
            ó[H] = new StringBuilder(); var Ê = í[H].Split('\n');
            foreach (var µ in Ê)
            {
                è += é.Y; var ç = µ.Split(U); float æ = 0; foreach (var å in ç) æ += á.MeasureStringInPixels(ì.Clear().Append(å), î, ï).X;
                int ä = Math.Max((int)Math.Floor((ð.X - æ) / (ç.Length - 1) / é.X), 1); string ã; if (ô.ContainsKey(ä)) ã = ô[ä];
                else
                {
                    ê.Clear().Append(' ', ä
                ); while (ç.Length != 1) { ê.Append(' '); if (á.MeasureStringInPixels(ê, î, ï).X * (ç.Length - 1) > (ð.X - æ)) break; }
                    ô.Add(ä, ã = ê.ToString()
                );
                }
                for (int â = 0; â < ç.Length - 1; â++) ó[H].Append(ç[â]).Append(ã); ó[H].AppendLine(ç[ç.Length - 1]);
            }
        }
        int ë = Math.Max((int)Math.
                Floor((ð.Y - è) / (í.Length - 1) / é.Y), 1); string Ì; if (ò.ContainsKey(ë)) Ì = ò[ë]; else ò.Add(ë, Ì = ê.Clear().Append('\n', ë).ToString()); ê.
                Clear(); for (int H = 0; H < í.Length - 1; H++) ê.Append(ó[H].ToString()).Append(Ì); ê.Append(ó[í.Length - 1].ToString()); á.WriteText(ê.
                ToString().Trim());
    }
    public void X() => á.WriteText(""); public virtual void W(string V, char U, char T) => ñ(V, U, T, á.SurfaceSize, á.
                FontSize, á.Font);
}
class S
{
    const string R = "[RTAS - Text Surface Config]"; List<Í> Q = new List<Í>(), P = new List<Í>(); List<ҳ> O = new List
                <ҳ>(); public void N(List<IMyTerminalBlock> M, string L, bool K, Vector2I J, bool I)
    {
        if (!I) O.Clear();
        else for (int H = O.Count - 1; H
                >= 0; H--) if (O[H].ԅ.Closed || !O[H].ԅ.IsFunctional) O.RemoveAt(H); Q.Clear(); bool G = false; recheck: foreach (var F in M)
        {
            if (!G && F.
                CustomName.Contains(L)) continue; var E = F as IMyTextSurfaceProvider; int D = 0; bool C = F.CustomName.Contains(HUDLCDTag); if (E != null && (D = E
                .SurfaceCount) == 1)
            {
                if (K && !F.CustomName.Contains(NoGUITag))
                {
                    if (C) Q.Add(new Ð(F as IMyTextSurface, J)); bool B = false; if (I)
                        foreach (var Y in O) if (Y.ԅ.EntityId == F.EntityId) { B = true; break; }
                    if (B) continue; string A = F.BlockDefinition.SubtypeId; ҳ f; if (A ==
                        "TransparentLCDLarge") f = new ҳ(F, 0, true, 1.25f, 2.5f, 2.5f, 0.91f);
                    else if (A == "LargeLCDPanel" || A == "LargeTextPanel") f = new ҳ(F, 0, true, 1.25f, 2.5f,
                        2.5f);
                    else if (A == "LargeFullBlockLCDPanel") f = new ҳ(F, 0, true, -1.25f, 2.5f, 2.5f);
                    else if (A == "HoloLCDLarge") f = new ҳ(F, 0, true,
                        0.85f, 2.5f, 2.5f);
                    else if (A == "TransparentLCDSmall" || A == "SmallTextPanel" || A == "SmallLCDPanel") f = new ҳ(F, 0, true, 0.25f, 0.5f, 0.5f);
                    else if (A == "SmallFullBlockLCDPanel") f = new ҳ(F, 0, true, -0.25f, 0.5f, 0.5f);
                    else if (A == "HoloLCDSmall") f = new ҳ(F, 0, true, 0.05f, 0.5f
                    , 0.5f);
                    else f = new ҳ(F, 0, false, 0, 2.5f, 2.5f); O.Add(f);
                }
                else { var f = E.GetSurface(0); Q.Add(C ? new Ð(f, J) : new Í(f)); }
                continue;
            }
            if (D == 0) continue; var Ë = F.CustomData; if (Ë == "") { F.CustomData = q(D); return; }
            var Ê = Ë.Split('\n'); if (Ê.Length < D + 1 || !Ê.Contains(R
            )) { F.CustomData += "\n\n" + q(D); return; }
            int É = Array.FindIndex(Ê, Á => Á.Equals(R)); var È = true; var Ç = new List<int>(); for (int H = É
            + 1; H < Ê.Length && Ê[H] != ""; H++)
            {
                var µ = Ê[H]; if (!µ.Contains(" = ")) { È = false; break; }
                bool Æ; int Å; string[] Ä = µ.Split(new char[]{
'='}, 2); if (int.TryParse(Ä[0].Trim(' '), out Å) && Å <= D - 1 && Å >= 0)
                {
                    var f = E.GetSurface(Å); if (Ä[1].Trim().ToLower() == "text")
                    {
                        Q.Add(
C ? new Ð(f, J) : new Í(f)); Ç.Add(Å);
                    }
                    else if (bool.TryParse(Ä[1].Trim(' '), out Æ))
                    {
                        if (Æ)
                        {
                            if (!K) Q.Add(C ? new Ð(f, J) : new Í(f));
                            else
                            {
                                bool B = false; if (I) foreach (var Y in O) if (Y.ԅ.EntityId == F.EntityId && Y.ԁ == Å) { B = true; break; }
                                if (!B) O.Add(new ҳ(F, Å, false, 0,
                            2.5f, 2.5f)); if (C) Q.Add(new Ð(f, J));
                            }
                        }
                        Ç.Add(Å);
                    }
                    else { È = false; break; }
                }
                else { È = false; break; }
            }
            if (È) for (int Ã = 0; Ã < D; Ã++)
                {
                    bool B =
                            false; foreach (var Â in Ç) if (Ã == Â) { B = true; break; }
                    if (!B) { È = false; break; }
                }
            if (!È)
            {
                var Á = new StringBuilder(); var À = q(D).Split('\n'
                            ); var º = false; foreach (var µ in Ê)
                {
                    if (µ.Equals(R)) º = true; if (!º) Á.Append($"{µ}\n");
                    else if (µ == "")
                    {
                        Á.Append($"{q(D)}\n"); º =
                            false;
                    }
                }
                F.CustomData = Á.ToString().TrimEnd('\n');
            }
        }
        if (!G && Q.Count == 0 && O.Count == 0) { G = true; goto recheck; }
        foreach (var ª in Q) if (!
                            (ª is Ð)) ª.á.ContentType = ContentType.TEXT_AND_IMAGE;
    }
    public string q(int h)
    {
        var Z = R + "\n"; for (int H = 0; H < h; H++) Z +=
                            $"{H} = False\n"; return Z.TrimEnd('\n');
    }
    public void õ(string Á) { foreach (var ª in Q.Concat(P)) ª.W(Á, '$', 'ß'); }
    public void ķ(Vector3D Ķ,
                            Program ĵ)
    {
        foreach (var ª in O)
        {
            if (ĵ.DynamicScreens && (ª.ԅ.GetPosition() - Ķ).LengthSquared() > ĵ.DynamicDistance * ĵ.DynamicDistance)
            {
                ª.Ԅ.ContentType = ContentType.TEXT_AND_IMAGE; if (P.Count(ĳ => ĳ.á == ª.Ԅ) == 0) P.Add(new Í(ª.Ԅ)); continue;
            }
            var Ĵ = P.Find(ĳ => ĳ.á == ª.
                Ԅ); if (Ĵ != null) P.Remove(Ĵ); ª.Ԅ.ContentType = ContentType.SCRIPT; ª.Ԅ.Script = ""; ª.Ż(ĵ);
        }
    }
}
class Ĳ
{
    double ı, İ, ĸ, į, ĭ; double[] Ĭ, ī
                = new double[4]; int Ī = 0, ĩ = 0; public Ĳ(double Ĩ, double ħ, double Ħ, double ĥ, int Ĥ) { ı = Ĩ; İ = ħ; ĸ = Ħ; į = ĥ; Ĭ = new double[Ĥ]; Į(); }
    public void Į() { ĭ = 0; for (int H = 0; H < Ĭ.Length; H++) Ĭ[H] = 0; for (int H = 0; H < 4; H++) ī[H] = 0; Ī = 0; ĩ = 0; }
    void Ŋ(double ŉ)
    {
        Ĭ[Ī++] = ŉ; if (Ī == Ĭ.
    Length) Ī = 0;
    }
    public double ň(double Ň)
    {
        double ĵ = Ň * ı; Ŋ(Ň); double H = 0; foreach (var ņ in Ĭ) H += ņ; H *= İ * į; double Ņ = (Ň - ĭ) * ĸ / į; ĭ = Ň; if (++
    ĩ == 4) ĩ = 0; ī[ĩ] = Ņ; Ņ = ī.Sum() / 4; return ĵ + H + Ņ;
    }
}
class ń
{
    public List<IMyGyro> Ń = new List<IMyGyro>(); IMyGyro ł = null; MatrixD Ł =
    MatrixD.Identity; public bool Ĕ { get; private set; }
    public void N(List<IMyGyro> ŀ)
    {
        foreach (var Ŀ in Ń.ToList()) if (Ŀ.Closed || !Ŀ.
    IsFunctional) { Ń.Remove(Ŀ); if (Ŀ == ł) ł = null; }
        foreach (var Ŀ in ŀ.ToList()) if (!Ŀ.Closed && Ŀ.IsFunctional && !Ń.Contains(Ŀ))
            {
                Ń.Add(Ŀ); if (Ń.
    Count >= 10) break;
            }
        Ĕ = Ń.Count != 0;
    }
    public void ľ(double Ģ, double ě, double Ľ, MatrixD ļ)
    {
        if (ł == null || ł.Closed || !ł.IsFunctional)
            return; if (Ģ == 0 && ě == 0 && Ľ == 0) { ł.Pitch = ł.Yaw = ł.Roll = 0; return; }
        MatrixD Ļ = ł.WorldMatrix; Ł.M11 = ļ.M11 * Ļ.M11 + ļ.M12 * Ļ.M12 + ļ.M13 * Ļ.M13; Ł
            .M12 = ļ.M11 * Ļ.M21 + ļ.M12 * Ļ.M22 + ļ.M13 * Ļ.M23; Ł.M13 = ļ.M11 * Ļ.M31 + ļ.M12 * Ļ.M32 + ļ.M13 * Ļ.M33; Ł.M21 = ļ.M21 * Ļ.M11 + ļ.M22 * Ļ.M12 + ļ.M23 * Ļ.
            M13; Ł.M22 = ļ.M21 * Ļ.M21 + ļ.M22 * Ļ.M22 + ļ.M23 * Ļ.M23; Ł.M23 = ļ.M21 * Ļ.M31 + ļ.M22 * Ļ.M32 + ļ.M23 * Ļ.M33; Ł.M31 = ļ.M31 * Ļ.M11 + ļ.M32 * Ļ.M12 + ļ.M33
            * Ļ.M13; Ł.M32 = ļ.M31 * Ļ.M21 + ļ.M32 * Ļ.M22 + ļ.M33 * Ļ.M23; Ł.M33 = ļ.M31 * Ļ.M31 + ļ.M32 * Ļ.M32 + ļ.M33 * Ļ.M33; ł.Pitch = (float)(-Ģ * Ł.M11 + ě * Ł.
            M21 + Ľ * Ł.M31); ł.Yaw = (float)(-Ģ * Ł.M12 + ě * Ł.M22 + Ľ * Ł.M32); ł.Roll = (float)(-Ģ * Ł.M13 + ě * Ł.M23 + Ľ * Ł.M33);
    }
    public void ĺ(bool č)
    {
        if (č)
        {
            bool Ĺ = ł != null && !ł.Closed && ł.IsFunctional; foreach (var Č in Ń)
            {
                if (!Ĺ) { if (Č.IsFunctional) { Č.GyroOverride = true; Ĺ = true; ł = Č; } }
                else Č.GyroOverride = Č.EntityId == ł.EntityId;
            }
        }
        else foreach (var Č in Ń) Č.GyroOverride = false;
    }
    public void Ě(bool č)
    {
        foreach (var
                Č in Ń) Č.Enabled = č;
    }
}
class ċ
{
    public static Vector3D Ċ(double ĉ, Vector3D Ĉ, Vector3D ć, Vector3D Ć) { return Ĉ + ć * ĉ + 0.5 * Ć * ĉ * ĉ; }
    public static void ą(Vector3D Ą, MatrixD ă, out double Ă, out double ā)
    {
        var Ā = Vector3D.TransformNormal(Ą, MatrixD.Transpose(ă));
        var ÿ = Ā.X == 0 && Ā.Z == 0 ? Vector3D.Zero : new Vector3D(Ā.X, 0, Ā.Z) / Math.Sqrt(Ā.X * Ā.X + Ā.Z * Ā.Z); Ă = ÿ.IsZero() ? 0 : Math.Acos(Vector3D.
        Forward.Dot(ÿ)) * Math.Sign(Ā.X); ā = ÿ.IsZero() ? 0 : Math.Acos(Ā.Dot(ÿ)) * Math.Sign(Ā.Y);
    }
    public static Vector3D þ(Vector3D ø, Vector3D
        ý)
    { if (ý.IsZero()) return Vector3D.Zero; return ø.Dot(ý) / ý.LengthSquared() * ý; }
    public static double ü(Vector3D û, Vector3D ú)
    {
        if (û.IsZero() || ú.IsZero()) return 0; return Math.Acos(û.Dot(ú) / Math.Sqrt(û.LengthSquared() * ú.LengthSquared()));
    }
    public
        static Vector3D ù(Vector3D ø, Vector3D Ď, MatrixD ö)
    {
        double Ã = ø.X * ö.M11 + ø.Y * ö.M21 + ø.Z * ö.M31; double Â = ø.X * ö.M12 + ø.Y * ö.M22 + ø.Z * ö.
        M32; double ğ = ø.X * ö.M13 + ø.Y * ö.M23 + ø.Z * ö.M33; return new Vector3D(Ã, Â, ğ) + Ď;
    }
    public static Vector3D ġ(Vector3D ø, Vector3D Ď,
        MatrixD Ġ)
    {
        ø -= Ď; double Ã = ø.X * Ġ.M11 + ø.Y * Ġ.M12 + ø.Z * Ġ.M13; double Â = ø.X * Ġ.M21 + ø.Y * Ġ.M22 + ø.Z * Ġ.M23; double ğ = ø.X * Ġ.M31 + ø.Y * Ġ.M32 + ø.Z *
        Ġ.M33; return new Vector3D(Ã, Â, ğ);
    }
    public static void Ğ(MatrixD ĝ, Vector3D Ĝ, out double Ģ, out double ě)
    {
        var ę = ĝ.Up; var Ę = ĝ
        .Forward; var ė = þ(Ĝ, ę); var Ė = Ĝ - ė; Ģ = (Ė.IsZero() && !ė.IsZero()) ? MathHelper.PiOver2 : (ę.Dot(Ĝ) > 0 ? 1 : -1) * ü(Ĝ, Ė); ě = (ĝ.Left.Dot(Ĝ) >
        0 ? -1 : 1) * ü(Ę, Ė); if (Math.Abs(ě) <= 1E-6 && Ĝ.Dot(Ę) < 0) ě = Math.PI;
    }
}
class ĕ
{
    const string R = "[RTAS - Turret Constraints]"; public
        bool Ĕ = true; bool ē = false, Ē = true, đ = false; public long Đ; IMyLargeTurretBase Ŝ; public Ǫ Ǫ { get; }
    public string ƶ; double ʞ = -180, ʝ =
        180, ʜ, ʛ; public Vector3D ɷ(IMyShipController ǔ)
    {
        var ɶ = ǔ.GetShipVelocities().AngularVelocity / 60; var ɵ = Ŝ.GetPosition() - ǔ.
        GetPosition(); double Ã = ɵ.Z * ɶ.Y - ɵ.Y * ɶ.Z; double Â = ɵ.X * ɶ.Z - ɵ.Z * ɶ.X; double ğ = ɵ.Y * ɶ.X - ɵ.X * ɶ.Y; var ɮ = new Vector3D(Ã, Â, ğ) + ǔ.
        GetShipVelocities().LinearVelocity; return ɮ;
    }
    public ĕ(IMyLargeTurretBase ʚ, int ɸ, bool ē)
    {
        Ŝ = ʚ; ƶ = Ŝ.BlockDefinition.SubtypeId; Ǫ = new Ǫ(ɸ, ē); Đ
        = Ŝ.GetId(); ʜ = ɸ == 1 ? -15 : ē ? -10 : -20; ʛ = ɸ == 1 ? 60 : ē ? 50 : 75; string[] ʘ = Ŝ.CustomData.Split('\n'); if (ʘ.Length > 1 && ʘ[0].Equals(R)) for (
        int H = 1; H < ʘ.Length; H++)
            {
                string µ = ʘ[H]; if (!µ.Contains("=")) continue; var ʗ = µ.Split('='); switch (ʗ[0].TrimEnd(' '))
                {
                    case
        "MinAzimuth":
                        double.TryParse(ʗ[1].TrimStart(' '), out ʞ); break;
                    case "MaxAzimuth": double.TryParse(ʗ[1].TrimStart(' '), out ʝ); break;
                    case
        "MinElevation":
                        double.TryParse(ʗ[1].TrimStart(' '), out ʜ); break;
                    case "MaxElevation": double.TryParse(ʗ[1].TrimStart(' '), out ʛ); break;
                }
            }
        else Ŝ.CustomData = R;
    }
    public bool N() { Ĕ = Ē && Ŝ != null && !Ŝ.Closed && Ŝ.IsWorking && Ŝ.Enabled; if (!Ĕ) return false; return true; }
    public
        void ʖ(bool ʕ, byte ʔ)
    {
        Ŝ.EnableIdleRotation = !ʕ && (ʔ & 16) == 16; Ŝ.SyncEnableIdleRotation(); if (ʕ != đ)
        {
            Ŝ.SetValueBool(
        "TargetSmallShips", !ʕ && (ʔ & 1) == 1); Ŝ.SetValueBool("TargetLargeShips", !ʕ && (ʔ & 2) == 2); Ŝ.SetValueBool("TargetStations", !ʕ && (ʔ & 4) == 4); Ŝ.
        SetValueBool("TargetCharacters", !ʕ && (ʔ & 8) == 8 && Ǫ.ɂ != 1); Ŝ.Shoot = false;
        }
        Ŝ.Range = ʕ ? 0 : (ē ? 600 : 800); đ = ʕ;
    }
    public void ʓ()
    {
        bool ʙ = Ŝ.
        TargetMeteors, ʟ = Ŝ.TargetMissiles, ʤ = Ŝ.TargetCharacters, ē = Ŝ.TargetSmallGrids, ʭ = Ŝ.TargetLargeGrids, ʬ = Ŝ.TargetStations, ʫ = Ŝ.
        EnableIdleRotation; float ǖ = Ŝ.Range; Ŝ.ResetTargetingToDefault(); Ŝ.TargetMeteors = ʙ; Ŝ.TargetMissiles = ʟ; Ŝ.TargetCharacters = ʤ; Ŝ.
        TargetSmallGrids = ē; Ŝ.TargetLargeGrids = ʭ; Ŝ.TargetStations = ʬ; Ŝ.EnableIdleRotation = ʫ;
    }
    public bool ʪ(Vector3D ʩ)
    {
        double ʨ, ʧ; var ă = Ŝ.
        WorldMatrix; ċ.ą(ʩ, ă, out ʨ, out ʧ); if (ʨ < ʞ || ʨ > ʝ) return false; if (ʧ < ʜ || ʧ > ʛ) return false; Ŝ.Azimuth = (float)-ʨ; Ŝ.Elevation = (float)ʧ; Ŝ.
        SyncAzimuth(); Ŝ.SyncElevation(); return true;
    }
    public void ʦ() { Ŝ.ShootOnce(); }
    public bool ɴ(string M)
    {
        return Ŝ.CustomName.Contains(M)
        ;
    }
    public MyDetectedEntityInfo ʥ() { return Ŝ.GetTargetedEntity(); }
    public Vector3D ʣ() { return Ŝ.GetPosition(); }
    public static
        ĕ ʢ(IMyLargeTurretBase Ŝ)
    {
        if (Ŝ.Closed || !Ŝ.IsFunctional || !Ŝ.Enabled) return null; var A = Ŝ.BlockDefinition.SubtypeId; int ɸ;
        bool ē = false; switch (A)
        {
            case "LargeCalibreTurret": ɸ = 1; break;
            case "LargeBlockMediumCalibreTurret": ɸ = 2; break;
            case
        "SmallBlockMediumCalibreTurret":
                ɸ = 2; ē = true; break;
            default: return null;
        }
        return new ĕ(Ŝ, ɸ, ē);
    }
}
class ʡ
{
    public long Đ; public int ʠ; IMyUserControllableGun Ö
        ; public bool Ĕ = false; public Ǫ Ǫ { get; }
    public ʡ(IMyUserControllableGun Ö, int ɳ, string ɲ, out bool ɹ)
    {
        ɹ = false; this.Ö = Ö; if (Ö.
        Closed || !Ö.IsFunctional || (!Ö.Enabled && (ɳ == 0 || !Ö.CustomName.Contains(ɲ)))) return; ʠ = (int)Ö.Orientation.Forward; int ɸ; bool ē =
        false; string Ǆ = Ö.BlockDefinition.SubtypeId; switch (Ǆ)
        {
            case "LargeRailgun": ɸ = 0; break;
            case "SmallRailgun": ɸ = 0; ē = true; break;
            case
        "LargeBlockLargeCalibreGun":
                ɸ = 1; break;
            case "SmallBlockMediumCalibreGun": ɸ = 2; break;
            default: return;
        }
        Ǫ = new Ǫ(ɸ, ē); Đ = Ö.EntityId; Ĕ = true; ɹ = true;
    }
    public
        Vector3D ɷ(MyShipVelocities Ȭ, Vector3D Ų)
    {
        var ɶ = Ȭ.AngularVelocity / 60; var ɵ = Ö.GetPosition() - Ų; double Ã = ɵ.Z * ɶ.Y - ɵ.Y * ɶ.Z; double Â = ɵ
        .X * ɶ.Z - ɵ.Z * ɶ.X; double ğ = ɵ.Y * ɶ.X - ɵ.X * ɶ.Y; var ɮ = new Vector3D(Ã, Â, ğ) + Ȭ.LinearVelocity; return ɮ;
    }
    public bool ɴ(string M)
    {
        return Ö.CustomName.Contains(M);
    }
    public bool N(int ɳ, string ɲ)
    {
        return Ĕ = !Ö.Closed && Ö.IsFunctional && (Ö.Enabled || (ɳ > 0 && Ö.
        CustomName.Contains(ɲ)));
    }
    public string ɱ() { return Ö.DetailedInfo; }
    public MatrixD ɰ() { return Ö.WorldMatrix; }
}
bool ɯ(char[] Ȟ, char[]
        â)
{ for (int H = 0; H < Ȟ.Length; H++) if (Ȟ[H] != â[H]) return false; return true; }
class ɺ : IComparable<ɺ>
{
    const double ʑ =
        0.70710678118654752440084436210485, ʏ = 0.57735026918962576450914878050196; public string ʎ, ʍ; public int ʌ = 0, ʋ = 0; public bool Ĕ = false, ʊ = false, ʉ = false; public
        double ʈ = 0; double ʇ = -1, ʆ = 0, ʐ = 0, ʅ = 0, ʄ = -1, ʃ = -1, ʂ = -1, ʁ = 0, ʀ = Math.PI + 1, ɿ = 0.2, ɾ = 0.9396, ǫ = 0, ɽ = 0, ɼ = 0, ɻ = 1, ʒ = -3, ʮ = 1.5, ʾ = 96, ͱ = 1.5; bool ˮ =
        false, ˬ = false, ˤ = false, ˣ = false, ǿ = true, ˢ = false, ˡ = false, ˠ = false, ˑ; bool[] ː; int ˏ = 2, Ͱ = -1, ˎ = 0; uint ˌ = 0, ˋ = 0; public IMyRemoteControl
        ˊ; IMySensorBlock ˉ; IMyBlockGroup ˈ; List<IMyTimerBlock> ˇ = new List<IMyTimerBlock>(); List<IMyWarhead> ˆ = new List<IMyWarhead>(
        ); List<IMyThrust> ˁ = new List<IMyThrust>(), ˀ = new List<IMyThrust>(); List<IMyGasTank> ȇ = new List<IMyGasTank>(); List<
        IMyBatteryBlock> ʿ = new List<IMyBatteryBlock>(); List<IMyPowerProducer> ˍ = new List<IMyPowerProducer>(); List<IMyShipConnector> ɍ = new List<
        IMyShipConnector>(); List<IMyShipMergeBlock> Ͷ = new List<IMyShipMergeBlock>(); List<IMyGyro> Ŀ = new List<IMyGyro>(); List<IMyCameraBlock> Έ = new
        List<IMyCameraBlock>(); List<MyDetectedEntityInfo> Ά = new List<MyDetectedEntityInfo>(); ń ͽ = new ń(); Vector3D ͼ, ͻ; static Vector3D
        [] ͺ = new Vector3D[]{new Vector3D(ʑ,0,-ʑ),new Vector3D(-ʑ,0,-ʑ),new Vector3D(0,ʑ,-ʑ),new Vector3D(0,-ʑ,-ʑ),new Vector3D(ʏ,ʏ
,-ʏ),new Vector3D(ʏ,-ʏ,-ʏ),new Vector3D(-ʏ,ʏ,-ʏ),new Vector3D(-ʏ,-ʏ,-ʏ),new Vector3D(1,0,0),new Vector3D(-1,0,0),new
Vector3D(0,1,0),new Vector3D(0,-1,0),new Vector3D(0,0,-1)}; public ɺ(IMyGridTerminalSystem ɐ, IMyRemoteControl Ơ, bool ɏ, bool Ɏ,
bool Ή, ɚ ͷ)
    {
        ˊ = Ơ; foreach (var ʹ in Ơ.CustomData.Split('\n'))
        {
            if (!ʹ.Contains(" = ")) continue; var ͳ = ʹ.Split('='); switch (ͳ[0].
TrimEnd(' '))
            {
                case "Tag": { ʍ = ͳ[1].TrimStart(' '); break; }
                case "FiringOrder":
                    {
                        string Ͳ = ͳ[1].TrimStart(' '); if (Ͳ == "Exclude")
                        {
                            ʊ = true;
                            break;
                        }
                        int ʰ; if (int.TryParse(Ͳ, out ʰ)) ʋ = ʰ; break;
                    }
                case "MaxVelocity":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʇ =
                            ʰ; break;
                    }
                case "ArmingDistance": { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʅ = ʰ; break; }
                case "ProximityFuse":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ǫ = ʰ; break;
                    }
                case "InitSeconds":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].
                        TrimStart(' '), out ʰ)) ʄ = ʰ; break;
                    }
                case "ThrustDevAngle":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʀ = Math.Cos(ʰ * Math.
                        PI / 180); break;
                    }
                case "CourseDevLength": { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ɿ = ʰ * ʰ; break; }
                case
                        "AutoKillSeconds":
                    { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʃ = ʰ; break; }
                case "NoTargetSeconds":
                    {
                        double ʰ; if (double.TryParse(ͳ
                        [1].TrimStart(' '), out ʰ)) ʂ = ʰ; break;
                    }
                case "SideFlyDirection":
                    {
                        switch (ͳ[1].TrimStart(' '))
                        {
                            case "Up": { Ͱ = 0; break; }
                            case "Down":
                                { Ͱ = 1; break; }
                            case "Left": { Ͱ = 2; break; }
                            case "Right": { Ͱ = 3; break; }
                        }
                        break;
                    }
                case "GyroRPM":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].
                                TrimStart(' '), out ʰ)) ʁ = ʰ * Math.PI / 30; break;
                    }
                case "SpinPeriodSeconds":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʆ = ʰ;
                        break;
                    }
                case "SpinDistance": { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ʐ = ʰ; break; }
                case "UseSensor":
                    {
                        bool ʰ; if (bool.
                        TryParse(ͳ[1].TrimStart(' '), out ʰ)) ˣ = ʰ; break;
                    }
                case "UseRaycast": { bool ʰ; if (bool.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ˡ = ʰ; break; }
                case "RaycastDistance": { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ɽ = ʰ; break; }
                case "RaycastFuse":
                    {
                        double ʰ; if (
                double.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ɼ = ʰ; break;
                    }
                case "RaycastElongation":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' ')
                , out ʰ)) ɻ = ʰ; break;
                    }
                case "BottomDownInGrav": { bool ʰ; if (bool.TryParse(ͳ[1].TrimStart(' '), out ʰ)) ˢ = ʰ; break; }
                case
                "PrecisionAngle":
                    { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ) && ʰ > 0) ɾ = Math.Cos(ʰ * О / 180); break; }
                case "NConstant":
                    {
                        double ʰ; if (
                double.TryParse(ͳ[1].TrimStart(' '), out ʰ) && ʰ > 0) ʒ = -ʰ; break;
                    }
                case "AConstant":
                    {
                        double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '),
                out ʰ) && ʰ >= 0) ʮ = ʰ; break;
                    }
                case "ARampdown": { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ) && ʰ >= 0) ʾ = ʰ; break; }
                case
                "RotationDevLength":
                    { double ʰ; if (double.TryParse(ͳ[1].TrimStart(' '), out ʰ) && ʰ >= 0) ͱ = ʰ; break; }
            }
        }
        if (ʍ != null)
        {
            foreach (var Ⱥ in ͷ.ə) if (Ⱥ.ʌ == 0 &&
                Ⱥ.ʍ.Equals(ʍ)) return; ˈ = ɐ.GetBlockGroupWithName(ʍ); if (ˈ == null) return; ˑ = Ɏ; if (!ʯ()) return; if (ɍ.Count != 0 && Ή) foreach (var Ǌ in
                ɍ) Ǌ.Connect(); if (ɏ) ɐ.GetBlocksOfType(ˇ, Ⱥ => Ⱥ.CustomName.Contains(ʍ)); ˤ = ɏ;
        }
    }
    public bool ʯ()
    {
        Ĕ = false; if (ˈ == null || ˊ.Closed)
            return false; var Ǭ = ˊ.WorldMatrix; ˈ.GetBlocksOfType(ɍ, ú => ú.IsSameConstructAs(ˊ)); ˈ.GetBlocksOfType(Ͷ, ú => ú.IsSameConstructAs(ˊ))
            ; if (ɍ.Count == 0 && Ͷ.Count == 0) return false; ˈ.GetBlocksOfType(ˁ, ú => ú.IsSameConstructAs(ˊ) && Ǭ.Forward.Dot(ú.WorldMatrix.
            Backward) > 0.9); if (ˁ.Count == 0) return false; ˈ.GetBlocksOfType(ˀ, ú => ú.IsSameConstructAs(ˊ) && Ǭ.Forward.Dot(ú.WorldMatrix.Backward) <
            0.9); ˈ.GetBlocksOfType(ȇ, ú => ú.IsSameConstructAs(ˊ)); if (ȇ.Count != 0 && ˑ && ȇ.Find(Ⱥ => Ⱥ.FilledRatio != 1) != null) ʌ = -1; ˈ.
            GetBlocksOfType(ˆ, ú => ú.IsSameConstructAs(ˊ)); ˈ.GetBlocksOfType(ʿ, ú => ú.IsSameConstructAs(ˊ)); ˈ.GetBlocksOfType(ˍ, ú => ú.IsSameConstructAs(
            ˊ) && !(ú is IMyBatteryBlock)); if (ʿ.Count == 0 && ˍ.Count == 0) return false; ˈ.GetBlocksOfType(Ŀ, ú => ú.IsSameConstructAs(ˊ)); if (Ŀ.
            Count == 0) return false; ͽ.N(Ŀ); ͽ.Ě(false); Ŀ.Clear(); if (ˣ)
        {
            var ʽ = new List<IMySensorBlock>(); ˈ.GetBlocksOfType(ʽ, ú => ú.
            IsSameConstructAs(ˊ)); if (ʽ.Count == 0) return false; ˉ = ʽ[0]; ˉ.Enabled = false;
        }
        ˈ.GetBlocksOfType(Έ, ú => ú.IsSameConstructAs(ˊ)); if (ˡ) foreach (var
            Ǒ in Έ) { Ǒ.Enabled = true; Ǒ.EnableRaycast = true; }
        foreach (var ȃ in ʿ) { ȃ.Enabled = true; ȃ.ChargeMode = ChargeMode.Recharge; }
        foreach
            (var Ȝ in ˍ) Ȝ.Enabled = true; foreach (var Ț in ˁ) Ț.Enabled = false; foreach (var Ț in ˀ) Ț.Enabled = false; if (Ͷ.Count(Ġ => Ġ.State ==
            MergeState.Locked) == 0) ˊ.DampenersOverride = false; Ĕ = ʇ > 0 && ʾ <= ʇ && ʅ > 0 && ǫ >= 0 && ʄ >= 0 && ʐ >= 0 && ʆ >= 0 && ʃ >= 0 && ʂ > 0 && ʁ > 0 && ͽ.Ĕ; ʎ = "Ready to Launch";
        return true;
    }
    void ʻ(MatrixD ʺ)
    {
        double ʹ = 0, ʼ = 0, ʸ = 0, ʷ = 0, ʶ = 0, ʵ = 0; foreach (var Ț in ˁ) if (!Ț.Closed && (Ț.Enabled ? Ț.IsWorking : Ț.
        IsFunctional)) ʵ += Ț.MaxEffectiveThrust; ː = new bool[] { true, true, true, true }; ǿ = true; foreach (var Ț in ˀ)
        {
            if (Ț.Closed || (Ț.Enabled ? !Ț.
        IsWorking : !Ț.IsFunctional)) continue; var Ą = Ț.WorldMatrix.Backward; if (Ą.Dot(ʺ.Left) > 0.9f) ː[0] = false;
            else if (Ą.Dot(ʺ.Right) > 0.9f) ː[1
        ] = false;
            else if (Ą.Dot(ʺ.Up) > 0.9f) ː[2] = false; else if (Ą.Dot(ʺ.Down) > 0.9f) ː[3] = false; double č = Ț.MaxEffectiveThrust; var ʴ = new
        Vector3D(Ą.Dot(ʺ.Right), Ą.Dot(ʺ.Up), Ą.Dot(ʺ.Backward)); if (ʴ.Y > 0.9f) ʸ += č;
            else if (ʴ.Y < -0.9f) ʷ += č;
            else if (ʴ.X > 0.9f) ʹ += č;
            else if (ʴ.X
        < -0.9f) ʼ += č;
            else ʶ += č;
        }
        ˠ = false; ͼ = new Vector3D(ʹ, ʸ, ʶ); ͻ = new Vector3D(ʼ, ʷ, ʵ); foreach (var ʳ in ː) if (ʳ)
            {
                if (ˠ)
                {
                    ǿ = false; return;
                }
                ˠ = true;
            }
    }
    Vector3D ʲ(Vector3D ƴ)
    {
        if (!ǿ) return new Vector3D(0, 0, ƴ.Z < 0 ? -ͻ.Z : 0); var ʱ = new Vector3D(ƴ.X > 0 ? ͼ.X : -ͻ.X, ƴ.Y > 0 ? ͼ.Y :
                -ͻ.Y, ƴ.Z > 0 ? ͼ.Z : -ͻ.Z); double Ġ = ƴ.X == 0 || ʱ.X == 0 ? double.MaxValue : ʱ.X / ƴ.X, ȟ = ƴ.Y == 0 || ʱ.Y == 0 ? double.MaxValue : ʱ.Y / ƴ.Y, Ɍ = ƴ.Z == 0 || ʱ
                .Z == 0 ? double.MaxValue : ʱ.Z / ƴ.Z; double Ȟ = Ġ < ȟ ? Ġ < Ɍ ? Ġ : Ɍ : ȟ < Ɍ ? ȟ : Ɍ; return new Vector3D(ƴ.X * Ȟ, ƴ.Y * Ȟ, ƴ.Z * Ȟ);
    }
    public void ȝ(uint Ó)
    {
        ʻ(ˊ.WorldMatrix); foreach (var ȁ in ȇ) { ȁ.Enabled = true; ȁ.Stockpile = false; }
        foreach (var ȃ in ʿ)
        {
            ȃ.Enabled = true; ȃ.ChargeMode =
        ChargeMode.Discharge;
        }
        foreach (var Ȝ in ˍ) Ȝ.Enabled = true; foreach (var Ǌ in ɍ) Ǌ.Disconnect(); foreach (var Ġ in Ͷ) Ġ.Enabled = false; if (ˤ)
            foreach (var ț in ˇ) ț.Trigger(); ˌ = Ó + (uint)(ʄ * 60); ʌ = 1; ˮ = true; ʎ = "Launching...";
    }
    public void ƺ()
    {
        if (ˮ && !ˬ)
        {
            ˊ.DampenersOverride =
            false; foreach (var Ț in ˁ) { Ț.Enabled = true; Ț.ThrustOverridePercentage = 1; }
            foreach (var Ț in ˀ)
            {
                Ț.Enabled = true; Ț.
            ThrustOverridePercentage = 0;
            }
            ͽ.Ě(true);
        }
    }
    public void ș(ɥ ý, Vector3D ǰ, MatrixD Ș, Vector3D ȗ, uint Ó, ɚ Ǯ, bool ǭ)
    {
        if (!Ĕ || ʌ <= 0 || !Ȉ() || ȉ) return; if (ˮ)
        {
            if (Ó > ˌ) { ˮ = false; if (ˣ) ˉ.Enabled = true; ʌ = 2; } else return;
        }
        if (ʉ && ý.Ƀ) { ʉ = false; ʌ = ˏ; }
        var Ǭ = ˊ.WorldMatrix; ͽ.ĺ(true); if (!ý.Ƀ && (ʌ == 2
            || ʌ == 4))
        {
            if (ʉ) { if (Ó >= ˋ) ʌ = 5; else return; }
            else
            {
                ʉ = true; ˋ = Ó + (uint)(ʂ * 60); ˏ = ʌ; ʎ = "Target loss - Holding course..."; ͽ.ľ(0, 0, 0, Ǭ);
                return;
            }
        }
        var Ȗ = Ș.Translation; Vector3D ȕ; switch (Ͱ)
        {
            case 0: ȕ = Ș.Up; break;
            case 1: ȕ = Ș.Down; break;
            case 2: ȕ = Ș.Left; break;
            case 3:
                ȕ = Ș.
                Right; break;
            default: ȕ = Vector3D.Zero; break;
        }
        var Ų = Ǭ.Translation; var ǌ = ǰ - Ų; var Ƞ = !ý.Ƀ ? 0 : (Ǭ.Translation - ý.ɤ).LengthSquared(); var
                ȱ = ˊ.GetNaturalGravity(); bool ȯ = !ȱ.IsZero(); bool Ȯ = ʌ == 2 && (!Ǯ.ɒ || Ǯ.ɔ > Ƞ); double ȭ = Ȯ || ʌ == 2 || ʌ == 4 || ((!ˢ || !ȯ) && ʐ != 0) ? Math.Sqrt(
                Ƞ) : 0; if (Ȯ) { Ǯ.ɔ = Ƞ; Ǯ.ɓ = ȭ; Ǯ.ɒ = true; }
        var Ȭ = ˊ.GetShipVelocities().LinearVelocity; Vector3D ȫ, Ȫ = Vector3D.Zero; switch (ʌ)
        {
            case 2:
                {
                    ȫ = ȡ(ý, ǰ, Ǭ, Ȭ, ˊ.GetNaturalGravity(), ǿ); ʎ = $"Interception - {ʈ = ȭ:0} m"; break;
                }
            case 3:
                {
                    ȫ = (Ȫ = ȕ * ʇ) - Ȭ; ʎ =
                    $"Side Displacement - {ʈ = (Ų - Ȗ).Length():0} m from ship"; break;
                }
            case 4: { ȫ = (Ȫ = (ý.ɤ - Ȗ).Normalized() * ʇ) - Ȭ; ʎ = $"Target Flyby - {ʈ = ȭ:0} m from target"; break; }
            default:
                {
                    ȫ = (Ȫ = ȗ) - Ȭ; ʎ =
                    $"Velocity Matching - {ʈ = Ȭ.Length():0} m/s"; break;
                }
        }
        var ȩ = new Vector3D(ȫ.Dot(Ǭ.Right), ȫ.Dot(Ǭ.Up), ȫ.Dot(Ǭ.Backward)); var Ȩ = ʲ(ȩ); var ȧ = Ȩ.Length(); bool Ȧ = (ǿ && -Ȩ.Z / ȧ >
                    ɿ) || (!ǿ && Ǭ.Forward.Dot(ȫ) / ȫ.Length() > ʀ), ȥ = ʌ == 2 && ǿ && ȫ.LengthSquared() < ͱ * ͱ; var Ȱ = !ȥ || Ȭ.LengthSquared() < ʾ * ʾ ? 1 : 1 - (float)((Ȭ.
                    Length() - ʾ) / (ʇ - ʾ)); foreach (var Ț in ˁ) Ț.ThrustOverride = Ȧ ? -(float)Ȩ.Z * Ȱ : 0; if (ǿ) foreach (var Ț in ˀ)
            {
                float ȣ; var Ą = Ț.WorldMatrix.
                    Backward; if (Ą.Dot(Ǭ.Left) > 0.9f) { if (Ț.Closed || (Ț.Enabled ? !Ț.IsWorking : !Ț.IsFunctional)) { if (!ː[0]) ʻ(Ǭ); continue; } ȣ = -(float)Ȩ.X; }
                else if (Ą.Dot(Ǭ.Right) > 0.9f) { if (Ț.Closed || (Ț.Enabled ? !Ț.IsWorking : !Ț.IsFunctional)) { if (!ː[1]) ʻ(Ǭ); continue; } ȣ = (float)Ȩ.X; }
                else if (Ą.Dot(Ǭ.Up) > 0.9f) { if (Ț.Closed || (Ț.Enabled ? !Ț.IsWorking : !Ț.IsFunctional)) { if (!ː[2]) ʻ(Ǭ); continue; } ȣ = (float)Ȩ.Y; }
                else
                if (Ą.Dot(Ǭ.Down) > 0.9f) { if (Ț.Closed || (Ț.Enabled ? !Ț.IsWorking : !Ț.IsFunctional)) { if (!ː[3]) ʻ(Ǭ); continue; } ȣ = -(float)Ȩ.Y; }
                else
                if (Ț.Closed || (Ț.Enabled ? !Ț.IsWorking : !Ț.IsFunctional)) continue; else ȣ = (float)Ȩ.Z; Ț.ThrustOverride = ȣ / ȧ > ɿ ? ȣ * Ȱ : 0;
            }
        else
            foreach (var Ț in ˀ) Ț.Enabled = false; if (ǌ.LengthSquared() < (ʅ * ʅ)) foreach (var Ȑ in ˆ) Ȑ.IsArmed = true; if (ǌ.LengthSquared() < (ǫ * ǫ)) ȑ(Ǯ,
            ǭ); double Ģ, ě, Ľ; ċ.Ğ(Ǭ, ȥ ? ǌ : ʌ != 2 && Ȫ.Dot(Ȭ) / Math.Sqrt(Ȫ.LengthSquared() * Ȭ.LengthSquared()) < ɾ ? Ȫ : ȫ, out Ģ, out ě); Ģ *= ʁ / О; ě *= ʁ / О;
        double Ȣ = 0; if (ǿ && ˠ)
        {
            if (ː[0]) Ȣ = (ȩ.Y > 0 ? 1 : -1) * (-ȩ.X + 1);
            else if (ː[1]) Ȣ = (ȩ.Y > 0 ? -1 : 1) * (ȩ.X + 1);
            else if (ː[2]) Ȣ = (ȩ.X > 0 ? 1 : -1) * (ȩ.Y + 1);
            else if (ː[3]) Ȣ = (ȩ.X > 0 ? -1 : 1) * (-ȩ.Y + 1);
        }
        Ľ = ǿ && ˠ ? Ȣ : (ˢ && ȯ) ? ȏ(ȱ) : (ʐ == 0 || (ȭ < ʐ) ? (ʆ == 0 ? 0 : (2 * Math.PI / ʆ)) : 0); ͽ.ľ(Ģ, ě, Ľ, Ǭ); if (ˣ && ý.Ƀ)
        {
            ˉ.
            DetectedEntities(Ά); foreach (var ņ in Ά) if (ņ.EntityId == ý.Đ) { ȑ(Ǯ, ǭ); break; }
        }
    }
    Vector3D ȡ(ɥ ý, Vector3D Ȥ, MatrixD Ȕ, Vector3D Ȍ, Vector3D Ȁ,
            bool ǿ)
    {
        var Ǿ = ý.ɢ - Ȍ; var ǽ = Ȥ - Ȕ.Translation; var Ǽ = ǽ.Length(); var ǻ = ǽ / Ǽ; var Ǻ = ʒ * Ǿ.Length() * (ǻ.Dot(Ǿ) * ǻ - Ǿ) / Ǽ + ʮ * (ý.ɣ - ý.ɣ.Dot(ǻ) * ǻ
            ); var ǹ = ˊ.CalculateShipMass().PhysicalMass; var Ǹ = ͻ.Z / ǹ; var Ƿ = Vector3D.Zero; if ((Ǻ - Ȁ).LengthSquared() > Ǹ * Ǹ) Ƿ = (Ǹ - Ȁ.Length()) *
            Ǻ.Normalized() - Ȁ;
        else
        {
            Ƿ += Ǻ + (Vector3D.IsZero(Ȁ) ? Vector3D.Zero : -Ȁ + ǻ * Ȁ.Dot(ǻ)); var Ƕ = Ƿ.Length(); var ǵ = ǿ ? Ƿ + (Ǹ - Ƕ) * ǻ : Vector3D.
            Zero; var Ǵ = ǿ ? ʲ(new Vector3D(ǵ.Dot(Ȕ.Right), ǵ.Dot(Ȕ.Up), ǵ.Dot(Ȕ.Backward))).LengthSquared() / ǹ / ǹ : 0; var ǳ = Ȍ.Dot(ǻ); var ǲ = ǿ ? ǳ > ʾ ?
            Ǵ * (ʇ - ǳ) / (ʇ - ʾ) : Ǵ : Ǹ; Ƿ += ǻ * Math.Max(0, Math.Sqrt(ǲ) - Ƕ);
        }
        return Ƿ;
    }
    public void Ǳ(ɥ ý, Vector3D ǰ, Vector3D ǯ, ɚ Ǯ, bool ǭ)
    {
        if (ʌ < 2)
            return; var Ǭ = ˊ.WorldMatrix; var Ų = Ǭ.Translation; if (ˡ && ý.Ƀ && (ǰ - Ų).LengthSquared() < ɽ * ɽ && ˊ.GetNaturalGravity().IsZero())
        {
            IMyCameraBlock Ǒ = null; double ǫ = Math.Max(ɼ, (ǰ - Ų).Normalized().Dot(ǯ - ˊ.GetShipVelocities().LinearVelocity) / 60f + 2f); foreach (var Ȃ in ͺ) if
            (ȓ((Ǭ.Right * Ȃ.X + Ǭ.Up * Ȃ.Y + Ǭ.Backward * Ȃ.Z * ɻ) * ǫ + Ų, ref Ǒ)) { ȑ(Ǯ, ǭ); return; }
        }
    }
    bool ȓ(Vector3D Ȓ, ref IMyCameraBlock Ǒ) => (Ǒ = Ǒ ==
            null || !Ǒ.CanScan(Ȓ) ? Έ.Find(Ǌ => !Ǌ.Closed && Ǌ.IsFunctional && Ǌ.CanScan(Ȓ)) : Ǒ) != null && !Ǒ.Raycast(Ȓ).IsEmpty(); public void ȑ(ɚ Ǯ,
            bool ǭ)
    {
        if (ʌ <= 1) return; if (ˎ == 0)
        {
            if (ǭ) Ǯ.ɝ.Add(this); foreach (var Ȑ in ˆ) Ȑ.IsArmed = true; Ǯ.ə.Remove(this); Ǯ.Õ.Remove(this); Ǯ.ɖ++
            ; ȉ = true; ȷ.ȶ.Ɋ($"Registering script-based detonation on torp {ʍ} - Is triple detonation? {ǭ}");
        }
        if (ǭ)
        {
            if (ˎ == ˆ.Count)
            {
                Ǯ.ɝ.
            Remove(this); return;
            }
            ȷ.ȶ.Ɋ($"Triggering TripDet of warhead index {ˎ} on torp {ʍ} - WH no longer exists? {ˆ[ˎ].Closed}"); ˆ[ˎ++]
            .Detonate(); if (ˎ == 3) { for (int H = 3; H < ˆ.Count; H++) ˆ[ˎ].Detonate(); Ǯ.ɝ.Remove(this); }
        }
        else foreach (var Ȑ in ˆ) Ȑ.Detonate();
    }
    public double ȏ(Vector3D Ȏ)
    {
        var Ę = ˊ.WorldMatrix.Forward; var ȍ = Ȏ - Ȏ.Dot(Ę) * Ę; if (ȍ.IsZero()) return 0; var ȋ = ˊ.WorldMatrix.Down;
        double Ɛ = ċ.ü(ȋ, ȍ); int Ȋ = ȍ.Cross(ȋ).Dot(ˊ.WorldMatrix.Forward) > 0 ? 1 : -1; double Ľ = Ɛ * (-Ȋ); return (Ɛ <= 1E-6 && ȍ.Dot(ȋ) < 0) ? О : Ľ;
    }
    bool ȉ =
        false; public bool Ȉ()
    {
        if (ȉ) return false; bool ȇ = this.ȇ.Count != 0, Ȇ = true, ȅ = true, Ŀ = true, Ȅ = ˊ.Closed; foreach (var ȁ in this.ȇ) if (!ȁ.
        Closed && ȁ.IsFunctional && ȁ.FilledRatio != 0) { ȇ = false; break; }
        foreach (var ȃ in ʿ) if (!ȃ.Closed && ȃ.IsFunctional) { Ȇ = false; break; }
        foreach (var Ȝ in ˍ) if (!Ȝ.Closed && Ȝ.IsFunctional) { Ȇ = false; break; }
        foreach (var Ț in ˁ) if (!Ț.Closed && Ț.IsFunctional)
            {
                ȅ = false; break;
            }
        foreach (var Č in ͽ.Ń) if (!Č.Closed && Č.IsFunctional) { Ŀ = false; break; }
        ȉ = ȇ || Ȇ || ȅ || Ŀ || Ȅ; if (ʌ > 1 && ȉ) foreach (var Ȑ in ˆ)
            {
                Ȑ.
            IsArmed = true; Ȑ.DetonationTime = (float)ʃ; Ȑ.StartCountdown();
            }
        return !ȉ;
    }
    public void ɜ()
    {
        foreach (var ȁ in ȇ) if (ȁ.FilledRatio != 1)
                return; ʌ = 0;
    }
    public int CompareTo(ɺ ɛ) { return Ĕ ? ɛ.Ĕ ? ʌ == -1 && ɛ.ʌ > -1 ? 1 : ʌ == 0 && ɛ.ʌ > 0 ? 1 : ʌ > 0 && ɛ.ʌ <= 0 ? -1 : ʋ < ɛ.ʋ ? -1 : ʋ == ɛ.ʋ ? 0 : 1 : -1 : 1; }
    public override bool Equals(object ƣ) { var Ⱥ = ƣ as ɺ; return Ⱥ != null && Ⱥ.ʍ == ʍ && Ⱥ.ˊ.IsSameConstructAs(ˊ); }
    public override int
    GetHashCode()
    { return ʍ.GetHashCode() ^ ˊ.CubeGrid.GetHashCode(); }
}
class ɚ
{
    const string R = "[RTAS - Torpedo Config]\n"; public List<ɺ> ə =
    new List<ɺ>(), Õ = new List<ɺ>(), ɝ = new List<ɺ>(); public int ɘ = 2, ɗ = 0, ɖ = 0; List<IMyRemoteControl> ɕ = new List<IMyRemoteControl>();
    public double ɔ = double.MaxValue, ɓ = double.MaxValue; public bool ɒ = false; public void ɑ(IMyGridTerminalSystem ɐ, bool ɏ, bool Ɏ, bool
    ɍ)
    {
        ɐ.GetBlocksOfType(ɕ, ú => ú.CustomData.StartsWith(R)); var ɞ = new HashSet<long>(); foreach (var Ⱥ in ə.ToList()) if (Ⱥ.ʌ < 1)
            {
                if (
    !ɐ.CanAccess(Ⱥ.ˊ)) ə.Remove(Ⱥ);
                else { ɞ.Add(Ⱥ.ˊ.EntityId); if (!Ⱥ.ʯ() || !Ⱥ.Ȉ()) ə.Remove(Ⱥ); }
            }
        foreach (var ɮ in ɕ)
        {
            if (ɞ.Contains(
    ɮ.EntityId)) continue; var Ō = new ɺ(ɐ, ɮ, ɏ, Ɏ, ɍ, this); int ɭ; if (Ō.Ĕ && !ə.Contains(Ō)) ə.Insert((ɭ = ə.BinarySearch(Ō)) < 0 ? ~ɭ : ɭ, Ō);
        }
    }
    public bool ɬ(uint Ó)
    {
        foreach (var Ō in ə) { if (Ō.ʌ != 0 || !Ō.Ĕ || Ō.ʊ || !Ō.Ȉ()) continue; Ō.ȝ(Ó); Õ.Add(Ō); ɗ++; return true; }
        return false;
    }
    public bool ɫ(string ɦ, uint Ó)
    {
        foreach (var Ō in ə)
        {
            if (Ō.ʌ != 0 || !Ō.ʍ.Equals(ɦ) || !Ō.Ĕ || !Ō.Ȉ()) continue; Ō.ȝ(Ó); Õ.Add(Ō); ɗ++;
            return true;
        }
        return false;
    }
    public void ɪ(int ɩ) { ɘ = ɩ; foreach (var Ō in Õ) Ō.ʌ = ɩ; }
    public void ɨ()
    {
        foreach (var Ō in Õ.ToList())
        {
            Ō.ȑ
            (this, true); ɖ--;
        }
    }
    public void ɧ(string ɦ) { foreach (var Ō in Õ.ToList()) if (Ō.ʍ.Equals(ɦ)) { Ō.ȑ(this, true); ɖ--; } }
}
struct ɥ
{
    public Vector3D ɤ, ɣ, ɢ, ɡ; public MatrixD ɠ; public uint ɟ; public long Đ; public string ɋ, Ɇ; public int Ɉ; public bool Ʉ, Ƀ;
}
struct Ǫ
{
    public int ɂ; public double Ɂ, ɀ, ȿ, Ⱦ; public Ǫ(int Ƚ, bool ē)
    {
        ɂ = Ƚ; switch (Ƚ)
        {
            case 0: Ɂ = ē ? 1000 : 2000; ɀ = ē ? 1400 : 2000; ȿ = ē ? 4 : 20; break;
            case
    1:
                Ɂ = 500; ɀ = 2000; ȿ = 10; break;
            default: Ɂ = 500; ɀ = 1400; ȿ = 4.5; break;
        }
        Ⱦ = ɀ / Ɂ;
    }
}
struct ȼ
{
    public IMyWarhead Ȼ; public uint Ⱥ; public ȼ(
    IMyWarhead ȹ, uint ȸ)
    { Ȼ = ȹ; Ⱥ = ȸ; }
}
class ȷ
{
    public static ȷ ȶ; IMyTerminalBlock ȵ; string ȴ = "[NO_TIME] <NO_TICK>"; bool ȳ = false; public ȷ(
    IMyTerminalBlock Ʌ)
    { ȵ = Ʌ; ȳ = ȵ != null; }
    public void Ȳ(DateTime ɇ, uint Ó) => ȴ = $"[{ɇ:yyyy-MM-dd HH:mm:ss.fff}] <{Ó}>"; public void Ɋ(string ɉ)
    {
        if
    (!ȳ) return; ȵ.CustomData += $"\n{ȴ} - {ɉ}";
    }
}
struct RotationController { double _kP, _kI, _kD; public RotationController(double kP, double kI, double kD) { _kP = kP; _kI = kI; _kD = kD; } public Ĳ Initialize(int windowWidth) => new Ĳ(_kP, _kI, _kD, 1f / 60f, windowWidth); }
class BombLauncher
{
    string m, w; bool i = true; uint d = 0; public BombLauncher(string MergeBlock, string Warhead, double Delay_Seconds)
    {
        m = MergeBlock;
        w = Warhead; if (Delay_Seconds >= 0) d = (uint)Math.Round(Delay_Seconds * 60); else i = false;
    }
    public ȼ? Drop(IMyGridTerminalSystem gts, uint tick)
    {
        if (!i) return null; var warhead = gts.GetBlockWithName(w) as IMyWarhead; var merge = gts.GetBlockWithName(m) as IMyShipMergeBlock; if (warhead == null || merge == null) return null;
        merge.Enabled = false; return new ȼ(warhead, tick + d);
    }
}
}
