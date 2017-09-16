﻿using AutoMapper;
using MagazineApp.Contracts.BLLContracts.Services;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.Domain.Enums;
using MagazineApp.Domain.Filters;
using MagazineApp.Web.Areas.Admin.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagazineApp.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        protected readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        // GET: Admin/Users
        public ActionResult Index(UserFilter filter)
        {
            var users = _userService.GetUsersByFilter(filter);
            var model = new UsersListViewModel {
                Filter = filter,
                Users = users.Select(u => Mapper.Map<UserViewModel>(u)).ToList()
            };

            return View(model);
        }

        public ActionResult Detail(Guid id) {
            var user = _userService.GetItem(id);
            var model = Mapper.Map<UserViewModel>(user);

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(UserViewModel model) {
            var user = Mapper.Map<User>(model);
            _userService.ChangeItem(user.Id, user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id) {
            _userService.DeleteItem(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void BlockUser(Guid userId) {
            _userService.BlockUser(userId);
        }

        [HttpPost]
        public void UnblockUser(Guid userId) {
            _userService.UnblockUser(userId);
        }

        public void ChangeUserRole(Guid id, UserType role) {
            _userService.SetRoleToUser(id, role);
        }

    }
}