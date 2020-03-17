namespace Cat
{
    public static class PartDescriptionExtensions
    {
        public static bool IsArrangement(this PartDescription description)
        {
            return description.Contains(" AR-") || description.ToString().EndsWith(" AR");
        }

        public static bool IsSwitch(this PartDescription description)
        {
            return description.StartsWith("SWITCH AS")
                || description.StartsWith("SWITCH GP");
        }

        public static bool IsCable(this PartDescription description)
        {
            return description.StartsWith("CABLE AS");
        }

        public static bool IsCompositePart(this PartDescription description)
        {
            return description.Contains(" AS")
                || description.Contains(" GP")
                || description.Contains(" AR");
        }
        public static bool IsKitPart(this PartDescription description)
        {
            return description.StartsWith("KIT-");
        }

        public static bool IsSensor(this PartDescription description)
        {
            return description.StartsWith("SENSOR");
        }

        public static bool IsHarnessAs(this PartDescription description)
        {
            return description.StartsWith("HARNESS AS")
                || description.StartsWith("WIRE AS")
                || description.StartsWith("CABLE AS");
        }

        public static bool IsWasher(this PartDescription description)
        {
            return description.StartsWith("WASHER") && !description.StartsWith("WASHER-THR");
        }

        public static bool IsSpacer(this PartDescription description)
        {
            return description.StartsWith("SPACER");
        }

        public static bool IsBolt(this PartDescription description)
        {
            return description.StartsWith("BOLT");
        }

        public static bool IsNut(this PartDescription description)
        {
            return description.StartsWith("NUT");
        }

        public static bool IsLocknut(this PartDescription description)
        {
            return description.StartsWith("LOCKNUT");
        }

        public static bool IsZinc(this PartDescription description)
        {
            return description.Contains("ZINC");
        }

        public static bool IsSeal(this PartDescription description)
        {
            return description.StartsWith("SEAL");
        }

        public static bool IsGasket(this PartDescription description)
        {
            return description.StartsWith("GASKET");
        }

        public static bool IsGrommet(this PartDescription description)
        {
            return description.StartsWith("GROMMET");
        }

        public static bool IsHose(this PartDescription description)
        {
            return description.StartsWith("HOSE");
        }

        public static bool IsHybridLines(this PartDescription description)
        {
            return description.StartsWith("LINE") && description.Contains("HYBR");
        }

        public static bool IsIntakeExhaustValve(this PartDescription description)
        {
            return description.StartsWith("VALVE-IN") || description.StartsWith("VALVE-EX");
        }

        public static bool IsCylinderHead(this PartDescription description)
        {
            return description.StartsWith("CYLINDER HEAD");
        }

        public static bool IsCopperSealingOrSpringWasher(this PartDescription description)
        {
            return description.StartsWith("WASHER-COPPER")
                || description.StartsWith("WASHER-SEAL")
                || description.StartsWith("WASHER-SPRING");
        }

        public static bool IsBeltOrTensioner(this PartDescription description)
        {
            return description.StartsWith("BELT");
        }

        public static bool IsTensioner(this PartDescription description)
        {
            return description.StartsWith("TENSIONER");
        }

        public static bool IsGearGroup(this PartDescription description)
        {
            return description.StartsWith("GEAR GP-FRONT") || description.StartsWith("GEAR GP-REAR");
        }

        public static bool Is5_8BoltOrM16Bolt(this PartDescription description)
        {
            return description.IsBolt() 
                && (description.Contains("5/8") || description.Contains("M16"));
        }

        public static bool IsValveMech(this PartDescription description)
        {
            return description.StartsWith("VALVE MECH");
        }

        public static bool IsPushrod(this PartDescription description)
        {
            return description.StartsWith("PUSHROD");
        }

        public static bool IsCoreAssembly(this PartDescription description)
        {
            return description.StartsWith("CORE AS");
        }

        public static bool IsControlGroup(this PartDescription description)
        {
            return description.StartsWith("CONTROL GP");
        }

        public static bool IsPumpGroup(this PartDescription description)
        {
            return description.StartsWith("PUMP GP");
        }

        public static bool IsTurbocharger(this PartDescription description)
        {
            return description.StartsWith("TURBO");
        }

        public static bool IsBearingOrBushing(this PartDescription description)
        {
            return description.StartsWith("BUSHING") 
                || description.StartsWith("BEARING");
        }
    }
}
