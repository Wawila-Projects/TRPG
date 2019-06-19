using System.Linq;
using System.Collections.Generic;
using Assets.Enums;
using Assets.Spells;

public static partial class Extensions {
    public static IList<SpellBase> GetSpellsFromElement(this ICollection<SpellBase> collection, Elements element) {
        return collection.Where( w => w.Element == element).ToList();
    }
}