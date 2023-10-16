﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain;
using DomainServices;
using GameNight2.Models;
using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLData;

namespace GameNight2.Controllers
{
	public class GameController : Controller
	{
		private readonly ILogger<GameController> _logger;
		private IGameRepository _gameRepository;
		private UserManager<GameNight2User> _userManager;
		private IAccountRepository _accountRepository;

		public GameController(ILogger<GameController> logger, IGameRepository gameRepository, UserManager<GameNight2User> userManager, IAccountRepository accountRepository)
		{
			_gameRepository = gameRepository;
			_logger = logger;
			_userManager = userManager;
			_accountRepository = accountRepository;
		}

		public List<Game> GetGames()
		{
			return _gameRepository.getGames();
		}

		[HttpGet]
		public IActionResult CreateGame()
		{
			NewGameModel model = new NewGameModel();
			return View(model);
		}

		[HttpPost]
		public void CreateGame(NewGameModel gameModel)
		{
			if(!ModelState.IsValid) View(gameModel);
			Game game = gameModel.getGame();

			_gameRepository.addGame(game);
			View();
		}
	}
}