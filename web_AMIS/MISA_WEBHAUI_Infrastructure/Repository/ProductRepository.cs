﻿using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MySqlConnector;
using System.Text.RegularExpressions;

namespace MISA_WEBHAUI_Infrastructure.Repository
{

    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public object GetProductAllInfor()
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price, " +
                         "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                         "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                         "FROM Product e " +
                         "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                         "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                         "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId ";
                        
                var products = SqlConnection.Query<object>(sqlCommand);
                return products;
            }
        }
        public object GetProduct(Guid productId)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price, " +
                         "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                         "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                         "FROM Product e " +
                         "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                         "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                         "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId WHERE e.ProductId= @productId";
                var parameters = new DynamicParameters();
                parameters.Add("@productId", productId);

                var product = SqlConnection.QueryFirstOrDefault<object>(sqlCommand,parameters);
                return product;
            }
        }
        public object GetProductDiscount(Guid? manufactorerId, Guid? catagoryId, string search,
            decimal? from, decimal? to, int pagenumber, int pagesize)
        {

            using (SqlConnection = new MySqlConnection(ConnectString))
            {


                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price, " +
                          "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                          "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                          "FROM Product e " +
                          "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                          "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                          "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId " +
                          "WHERE discount.DiscountPercent > 0";


                // Sử dụng '%' để thực hiện tìm kiếm một phần của tên
                var parameters = new DynamicParameters();
                // Kiểm tra xem manufactorerId có giá trị không
                if (manufactorerId.HasValue)
                {
                    sqlCommand += "AND e.ManufactorerId = @manufactorerId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@manufactorerId", manufactorerId);
                }
                if (catagoryId.HasValue)
                {
                    sqlCommand += "AND e.CatagoryId = @catagoryId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@catagoryId", catagoryId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    sqlCommand += "AND e.ProductName LIKE @productName ";
                    parameters.Add("@productName", "%" + search + "%");
                }
                // Thêm điều kiện lọc theo giá nếu có giá trị từ và đến
                if (from.HasValue)
                {
                    sqlCommand += "AND e.Price >= @from ";
                    parameters.Add("@from", from.Value);
                }
                if (to.HasValue)
                {
                    sqlCommand += "AND e.Price <= @to ";
                    parameters.Add("@to", to.Value);
                }



                var employees = SqlConnection.Query<object>(sqlCommand, parameters);
                int totalCount = employees.AsList().Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pagesize);
                //  return totalPages;

                employees = employees.Skip((pagenumber - 1) * pagesize).Take(pagesize);

                return new
                {
                    Data = employees,
                    TotalPages = totalPages,
                    total = totalCount
                };
            }
        }
        public object GetProductByManufactorer(Guid? manufactorerId,Guid? catagoryId, string search, 
            decimal? from, decimal? to, int pagenumber, int pagesize) 
        {
            
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

               
                var sqlCommand = "SELECT e.ProductId, e.ProductName, e.Image, e.Quantity, e.Description, e.Price, " +
                       "e.CatagoryId, e.ManufactorerId, d.CatagoryName, m.ManufactorerName, " +
                       "discount.DiscountId, discount.DiscountPercent, discount.StartDate, discount.EndDate " +
                       "FROM Product e " +
                       "INNER JOIN Catagory d ON e.CatagoryId = d.CatagoryId " +
                       "INNER JOIN Manufactorer m ON e.ManufactorerId = m.ManufactorerId " +
                       "LEFT JOIN Discount discount ON e.ProductId = discount.ProductId " +
                      // "WHERE e.ManufactorerId=@manufactorerId " +
                       "WHERE 1=1 ";


                // Sử dụng '%' để thực hiện tìm kiếm một phần của tên
                var parameters = new DynamicParameters();
                // Kiểm tra xem manufactorerId có giá trị không
                if (manufactorerId.HasValue)
                {
                    sqlCommand += "AND e.ManufactorerId = @manufactorerId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@manufactorerId", manufactorerId);
                }
                if (catagoryId.HasValue)
                {
                    sqlCommand += "AND e.CatagoryId = @catagoryId "; // Thêm điều kiện lọc theo manufactorerId
                    parameters.Add("@catagoryId", catagoryId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    sqlCommand += "AND e.ProductName LIKE @productName ";
                    parameters.Add("@productName", "%" + search + "%");
                }
                // Thêm điều kiện lọc theo giá nếu có giá trị từ và đến
                if (from.HasValue)
                {
                    sqlCommand += "AND e.Price >= @from ";
                    parameters.Add("@from", from.Value);
                }
                if (to.HasValue)
                {
                    sqlCommand += "AND e.Price <= @to ";
                    parameters.Add("@to", to.Value);
                }



                var employees = SqlConnection.Query<object>(sqlCommand, parameters);
                int totalCount = employees.AsList().Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pagesize);
              //  return totalPages;

                employees = employees.Skip((pagenumber - 1) * pagesize).Take(pagesize);

                return new
                {
                    Data = employees,
                    TotalPages = totalPages,
                    total=totalCount
                };
            }
        }
     
        public object GetProductSale(int month,int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                //var sqlCommand = "SELECT n.ProductId, e.ProductName,n.Quantity,e.Price from " +
                //    "OrderDetail n LEFT JOIN Product e ON n.ProductId= e.ProductId " +
                //    "INNER JOIN OrderProduct m ON n.OrderId = m.OrderProductId ";
                var sqlCommand = "SELECT n.ProductId, e.ProductName, e.Price,e.Image,h.ManufactorerName, SUM(n.Quantity) AS TotalQuantity " +
                    "FROM OrderDetail n LEFT JOIN Product e ON n.ProductId = e.ProductId " +
                    "INNER JOIN OrderProduct m ON n.OrderId = m.OrderProductId " +
                    "INNER JOIN Manufactorer h ON e.ManufactorerId= h.ManufactorerId " +
                    "   WHERE YEAR(m.OrderDate) = @year AND MONTH(m.OrderDate) = @month " +
                    "GROUP BY n.ProductId, e.ProductName, e.Price ,e.Image,h.ManufactorerName ";
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                parameters.Add("@month", month);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
        public object GetProductSaleByMonthAndYear(int month,int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = " SELECT(m.ManufactorerName) AS Hang, SUM(s.Quantity) AS Quantity ," +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount" +
                                 " FROM OrderDetail s" +
               " JOIN Product p ON s.ProductId = p.ProductId " +
               " JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
               " Join Manufactorer m ON p.ManufactorerId = m.ManufactorerId " +
             "   WHERE YEAR(o.OrderDate) = @year AND MONTH(o.OrderDate) = @month " +
              "  GROUP BY(m.ManufactorerName)";
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                parameters.Add("@month", month);

                var products = SqlConnection.Query<object>(sqlCommand, parameters);
                return products;
            }
        }
        public object GetProductSaleByYear(int year)
        {
            using (SqlConnection = new MySqlConnection(ConnectString))
            {

                var sqlCommand = "SELECT MONTH(o.OrderDate) AS Month,SUM(s.Quantity) AS Quantity ," +
                                 "SUM(p.Price * s.Quantity) AS SalesAmount " +
                                 "FROM OrderDetail s " +
                                 "JOIN Product p ON s.ProductId = p.ProductId " +
                                 "JOIN OrderProduct o ON s.OrderId = o.OrderProductId " +
                                 "WHERE YEAR(o.OrderDate) = @year " +
                                 "GROUP BY MONTH(o.OrderDate)" +
                                 " ORDER BY Month ";
                var parameters = new DynamicParameters();
                parameters.Add("@year", year);
                var products = SqlConnection.Query<object>(sqlCommand,parameters);
                return products;
            }
        }
      
    }
}
