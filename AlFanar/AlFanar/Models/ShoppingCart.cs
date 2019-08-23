using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Services.Building;

namespace AJC.KMS.Models
{
    public class ShoppingCart
    {
        private IBuilding _ItemService;
        public string shoppingCardID { get; set; }

        public const string CartSessionKey = "CardId";

        public static ShoppingCart GetCart()
        {
            var cart = new ShoppingCart();
            cart.shoppingCardID = cart.GetCartId();
            return cart;
        }

        public string GetCartId()
        {

            Guid tempCardId = Guid.NewGuid();
            return tempCardId.ToString();
        }



      

    }
}