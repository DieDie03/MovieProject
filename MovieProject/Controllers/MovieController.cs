using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieProject.ASP.Models;
using MovieProject.ASP.Tools;
using MovieProject.DAL.Entities;
using MovieProject.DAL.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.ASP.Controllers
{
    public class MovieController : Controller
    {
        private string _connectionString;

        public MovieController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("maConnexion");
        }

        public IActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(
                //@"Data Source=DESKTOP-L26RC4N\TB2019;Initial Catalog=MovieDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                _connectionString
            ))
            {
                MovieService movieService = new MovieService(connection);
                List<Movie> list = movieService.GetWithCategory();
                return View(new MovieIndex
                {
                    Movies = list.Select(m => m.ToMovieModel()).ToList()
                });
            }
        }

        public IActionResult Create()
        {
            

            using (SqlConnection connection = new SqlConnection(
                //@"server=K-PC;initial catalog=MovieDB;integrated security=true"
                _connectionString
                ))
            {
                CategoryService catService = new CategoryService(connection);
                return View(new CreateMovieModel
                {
                    Categories = catService.Get().Select(m => new CategoryModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                    }).ToList()
                }); ;
            }
        }

        [HttpPost]
        public IActionResult Create(CreateMovieModel form)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(
                //@"server=K-PC;initial catalog=MovieDB;integrated security=true"
                _connectionString
                ))
                {
                    MovieService movieService = new MovieService(connection);
                    // inserer dans la db
                    movieService.Add(form.ToDalMovie());
                    return RedirectToAction("Index");
                }
            }
            return View(form);
        }

        public IActionResult Detail(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                MovieService movieService = new MovieService(connection);
                return View(movieService.GetOneById(id).ToDetails());
            }
        }

        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                MovieService movieService = new MovieService(connection);
                return View(movieService.GetOneById(id).ToDetails());
            }
        }

        public IActionResult ConfirmDelete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                MovieService movieService = new MovieService(connection);
                movieService.Delete(id);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Update(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                CategoryService catService = new CategoryService(connection);
                MovieService movieService = new MovieService(connection);

                CreateMovieModel cmm = movieService.GetOneById(id).ToCreate();
                cmm.Categories = catService.Get().Select(m => new CategoryModel
                                    {
                                        Id = m.Id,
                                        Name = m.Name,
                                    }).ToList();
                return View(cmm);
            }
        }

        [HttpPost]
        public IActionResult Update(CreateMovieModel m)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                MovieService movieService = new MovieService(connection);
                movieService.Update(m.ToDalMovie());
                return RedirectToAction("Index");
            }
        }
    }
}
