﻿using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;
using System.Timers;



namespace MISA_WEBHAUI_Api.Controllers
{
    
    public class ProductController : MBaseController<Product>
    {
        #region Fiels
        IProductRepository _productRepository;
        IProductService _productService;
        #endregion

        #region Contructor
        public ProductController(IProductRepository productRepository, IProductService productService) 
            : base(productRepository, productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }
        #endregion
        [HttpGet("products")]
        public IActionResult GetProductAllInfor()
        {
            try
            {
                var data = _productRepository.GetProductAllInfor();

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        [HttpGet("product/{productId}")]
        public IActionResult GetProduct(Guid productId)
        {
            try
            {
                var data = _productRepository.GetProduct(productId);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        [HttpGet("products/discounted")]
        public IActionResult GetProductDícount(Guid? manufactorerId, Guid? catagoryId, string? search, decimal? from, decimal? to, int pagenumber, int pagesize)
        {
            try
            {
                var data = _productRepository.GetProductDiscount(manufactorerId, catagoryId, search, from, to, pagenumber, pagesize);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        [HttpGet("manufactorer/products")]
        public IActionResult GetProductByManufactorer(Guid? manufactorerId,Guid? catagoryId, string? search, decimal? from, decimal? to, int pagenumber, int pagesize)
        {
            try
            {
                var data = _productRepository.GetProductByManufactorer(manufactorerId, catagoryId, search, from, to, pagenumber, pagesize);

                return Ok(data);
              
            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
      
        [HttpGet("getProductSale/{month}/{year}")]
        public IActionResult GetProductSale(int month,int year)
        {
            try
            {
                var data = _productRepository.GetProductSale(month,year);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }

        }
        [HttpGet("getProductSaleByYear/{year}")]
        public IActionResult GetProductSaleBYYear(int year)
        {
            try
            {
                var data = _productRepository.GetProductSaleByYear(year);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }

        }
        [HttpGet("getProductSaleByMonthAndYear/{month}/{year}")]
        public IActionResult GetProductSaleBYMonthAndYear(int month,int year)
        {
            try
            {
                var data = _productRepository.GetProductSaleByMonthAndYear(month,year);

                return Ok(data);

            }
            catch (MISAvalidateException ex)
            {

                return HandleMISAException(ex);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }

        }
       



    }
}
