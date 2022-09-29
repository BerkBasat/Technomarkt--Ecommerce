using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class Wishlist
    {
        Dictionary<Guid, WishlistItem> _myWishlist = new Dictionary<Guid, WishlistItem>();
        public List<WishlistItem> myWishlist
        {
            get
            {
                return _myWishlist.Values.ToList();
            }
        }

        public void AddItem(WishlistItem wishlistItem)
        {
            _myWishlist.Add(wishlistItem.Id, wishlistItem);
        }

        public void DeleteItem(Guid id)
        {
            if (_myWishlist.ContainsKey(id))
            {
                _myWishlist.Remove(id);
            }
        }
    }
}