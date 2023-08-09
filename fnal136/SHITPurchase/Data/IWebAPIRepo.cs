using System.Collections.Generic;
using SHITPurchase.Models;

namespace SHITPurchase.Data
{
    public interface IWebAPIRepo
    {
        bool ValidLogin(string username, string password);
        User GetUserByUsername(string username);
        //Endpoint 1: Register API(2 marks)
        User RegisterUser(User new_user);

        //Endpoint 2: GetVersionA API(2 marks)
        string GetVersion(User user);

        //Endpoint 3: PurchaseItem API(3 marks)
        //Endpoint 4: PurchaseSingleItem API(3 marks)
        Product GetProductByID(int productID);
        Order GetLastOrder();
        Order AddOrder(Order new_order);

    }
}
