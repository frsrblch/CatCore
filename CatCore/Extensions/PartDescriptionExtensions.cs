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
            return description.Contains("SWITCH AS")
                || description.Contains("SWITCH GP");
        }

        public static bool IsCable(this PartDescription description)
        {
            return description.Contains("CABLE AS");
        }

        public static bool IsCompositePart(this PartDescription description)
        {
            return description.Contains(" AS")
                || description.Contains(" GP")
                || description.Contains(" AR");
        }
        public static bool IsKitPart(this PartDescription description)
        {
            return description.Contains("KIT-");
        }

        public static bool IsSensor(this PartDescription description)
        {
            return description.Contains("SENSOR GP")
                || description.Contains("SENSOR AS");
        }

        public static bool IsHarnessAs(this PartDescription description)
        {
            return description.Contains("HARNESS AS")
                || description.Contains("WIRE AS");
        }
    }
}
