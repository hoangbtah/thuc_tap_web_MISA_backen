﻿using Dapper;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_Infrastructure.Repository
{
    public class ShoppingCartRepository:BaseRepository<Cart>,IShoppingCartRepository
    {
        public async Task<int> AddShoppingCart(Cart product)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = @"
                INSERT INTO Cart (CartId,ProductId, ProductName, Quantity,Price,Image,UserId)
                VALUES (@CartId,@ProductId, @ProductName, @Quantity, @Price,@Image,@UserId);";

                return await SqlConnection.ExecuteAsync(query, product);
            }
        }
        public async Task<Cart> GetCartByUP(Guid userId , Guid productId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = "SELECT * FROM Cart WHERE UserId = @userId AND ProductId =@productId";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);
                parameters.Add("@productId", productId);
                return await SqlConnection.QueryFirstOrDefaultAsync<Cart>(query, parameters);
            }

        }
        public async Task<int> UpdateShoppingCart(Cart product)
        {

            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                string query = @"
                UPDATE Cart
                SET Quantity = @Quantity
                WHERE CartId = @CartId AND ProductId = @ProductId;";
                var parameters = new DynamicParameters();
                parameters.Add("@Quantity", product.Quantity);
                parameters.Add("@CartId", product.CartId);
                parameters.Add("@ProductId", product.ProductId);


                return await SqlConnection.ExecuteAsync(query, parameters);
            }
        }

    }
}