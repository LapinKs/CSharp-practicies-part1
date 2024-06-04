using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting;
public class AccountingModel : ModelBase
{
    private double price, discount, total;
    private int nightsCount;
    public double Total
    {
        get => price * nightsCount * (1 - discount / 100);
        set
        {
            if (0 >= value) throw new ArgumentException();
            total = value;
            discount = 100 * (1 - total / (price * nightsCount));
            Notify(nameof(Total));
            Notify(nameof(Discount));
        }
    }
    public double Discount
    {
        get => discount;
        set
        {
            discount = value;
            total = price * nightsCount * (1 - discount / 100);
            if (0 > total) throw new ArgumentException();
            Notify(nameof(Discount));
            Notify(nameof(Total));
        }
    }
    public double Price
    {
        get => price;
        set
        {
            if (0 > value) throw new ArgumentException();
            price = value;
            Notify(nameof(Price));
            Notify(nameof(Total));
        }
    }
    public int NightsCount
    {
        get => nightsCount;
        set
        {
            if (0 >= value) throw new ArgumentException();
            nightsCount = value;
            Notify(nameof(NightsCount));
            Notify(nameof(Total));
        }
    }
}