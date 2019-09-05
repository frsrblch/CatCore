using OptionType;
using System;
using System.Collections.Generic;

namespace Cat
{
    public class SisLine
    {
        public PartNumber Number { get; }
        public PartDescription Description { get; }
        public DiagramPosition Position { get; }
        public int Quantity { get; set; }
        public bool Purchasable { get; }
        public bool IsChildGroup { get; }
        public bool IsCcrPart { get; }

        public SisLine(
            PartNumber number,
            PartDescription description,
            DiagramPosition position,
            int quantity,
            bool purchasable = true,
            bool isChildGroup = false,
            bool isCcrPart = false)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Position = position;
            Quantity = Math.Max(1, quantity);
            Purchasable = purchasable;
            IsChildGroup = isChildGroup;
            IsCcrPart = isCcrPart;
        }

        public override string ToString()
        {
            if (IsChildGroup)
            {
                return $"{Position.ToString().PadRight(6)}{Number.ToText()} {Description} x{Quantity} (GROUP)";
            }

            return $"{Position.ToString().PadRight(6)}{Number.ToText()} {Description} x{Quantity}";
        }
    }
}
