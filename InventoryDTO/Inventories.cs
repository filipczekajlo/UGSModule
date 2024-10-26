namespace InventoryDTO
{
    public class Inventories
    {
        public Inventories()
        {
            _equippedAttacks = new Inventory(true);
            _unequippedAttacks = new Inventory(true);
        }

        public Inventories(Inventory equippedAttacks, Inventory unequippedAttacks)
        {
            _equippedAttacks = equippedAttacks;
            _unequippedAttacks = unequippedAttacks;
        }

        public Inventory EquippedAttacks
        {
            get => _equippedAttacks;
            set => _equippedAttacks = value;
        }

        public Inventory UnequippedAttacks
        {
            get => _unequippedAttacks;
            set => _unequippedAttacks = value;
        }

        private Inventory _equippedAttacks;
        private Inventory _unequippedAttacks;
    }
}