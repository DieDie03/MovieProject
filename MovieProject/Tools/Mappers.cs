using MovieProject.ASP.Models;
using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.ASP.Tools
{
    public static class Mappers
    {
        public static MovieModel ToMovieModel(this Movie m)
        {
            return new MovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                CategoryName = m.Category?.Name,
            };
        }

        public static Movie ToDalMovie(this CreateMovieModel form)
        {
            return new Movie
            {
                Id = form.Id,
                Title = form.Title,
                Poster = form.Poster,
                BoxOffice = form.BoxOffice,
                CategoryId = form.CategoryId,
                Director = form.Realisateur,
                Duration = form.Duration
            };
        }

        public static MovieDetail ToDetails(this Movie m)
        {
            return new MovieDetail
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                BoxOffice = m.BoxOffice,
                CategoryName = m.Category?.Name,
                Duration = m.Duration,
                Realisateur = m.Director
            };
        }

        public static CreateMovieModel ToCreate(this Movie m)
        {
            return new CreateMovieModel
            {
                Id = m.Id,
                Title = m.Title,
                Poster = m.Poster,
                BoxOffice = m.BoxOffice,
                CategoryId = m.CategoryId,
                Duration = m.Duration,
                Realisateur = m.Director
            };
        }
    }
}
