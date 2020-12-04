

namespace game
{
    public class Property
    {
        public int Price { get; }

        public int RentValue { get; }


        public Player Owner { get; set; }

        public Property(int price, int rentValue)
        {
            Price = price;
            RentValue = rentValue;
        }


        public bool HasOwner()
        {
            return this.Owner != null;
        }
        
        public bool PayRent(Player payer)
        {
            if (this.RentValue <= payer.Coins)
            {
                payer.Coins -= this.RentValue;
                ReciveRent();
                return true;
            }

            payer.Active = false;
            return false;
        }

        private void ReciveRent()
        {
            this.Owner.Coins += this.RentValue;
        }
        
        public void BuyProperty(Player buyer)
        {
            buyer.Coins -= this.Price;
            this.Owner = buyer;
        }
    }
}