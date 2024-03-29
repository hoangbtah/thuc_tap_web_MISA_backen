﻿using Microsoft.AspNetCore.Mvc;
using MISA_WEBHAUI_AMIS_Core.Entities;
using MISA_WEBHAUI_AMIS_Core.Exceptions;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_Infrastructure.Repository;



namespace MISA_WEBHAUI_Api.Controllers
{
    
    public class ProductController : MBaseController<Product>
    {
        #region Fiels
        IProductRepository _productRepository;
        IProductService _productService;
        #endregion

        #region Contructor
        public ProductController(IProductRepository productRepository, IProductService productService) : base(productRepository, productService)
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
        [HttpGet("catagory/{catagoryId}/products")]
        public IActionResult GetProductByCatagory(Guid catagoryId)
        {
            try
            {
                var data = _productRepository.GetProductByCatagory(catagoryId);

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
        [HttpGet("manufactorer/{manufactorerId}/products")]
        public IActionResult GetProductByManufactorer(Guid manufactorerId)
        {
            try
            {
                var data = _productRepository.GetProductByManufactorer(manufactorerId);

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
