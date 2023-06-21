﻿using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Bird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdClubAPI.DataAccessLayer.Repositories.Bird
{
    public class BirdRepository : IBirdRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public BirdRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BirdResponseModel? CreateBird(Domain.Entities.Bird bird)
        {
            try
            {
                var result = _context.Add(bird);
                _context.SaveChanges();
                return _mapper.Map<BirdResponseModel>(result.Entity);
            }
            catch
            {
                return null;
            }
        }

        public List<BirdResponseModel> GetBird()
        {
            return _context.Birds.Select(b => new BirdResponseModel
            {
                Id = b.Id,
                Name = b.Name,
                Species = b.Species,
            }).ToList();
        }

        public Domain.Entities.Bird? GetBirds(int id)
        {
            return _context.Birds.Where(e => e.Id ==  id).FirstOrDefault();
        }
    }
}
