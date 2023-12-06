﻿using Microsoft.AspNetCore.Mvc;
using SpotsStore.Models;
using System.Linq;
using SpotsStore.Models.ViewModels;

namespace SpotsStore.Controllers
{
    public class HomeController:Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string category, int productPage = 1) => View(new ProductsListViewModel
        {
            Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
            },
            CurrentCategory = category
        });
    }
}
