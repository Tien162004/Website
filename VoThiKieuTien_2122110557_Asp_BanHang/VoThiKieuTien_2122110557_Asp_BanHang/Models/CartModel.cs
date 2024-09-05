using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoThiKieuTien_2122110557_Asp_BanHang.Context;

namespace VoThiKieuTien_2122110557_Asp_BanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }  
        public int Quantity { get; set; }
    }
}