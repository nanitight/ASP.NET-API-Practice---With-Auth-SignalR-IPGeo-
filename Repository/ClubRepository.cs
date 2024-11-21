﻿using Microsoft.EntityFrameworkCore;
using RunGroupTUT.Interfaces;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ClubRepository : IClubRepository
	{
		private readonly AppDBContext context;

		public ClubRepository(AppDBContext context)
        {
			this.context = context;
		}

        public bool Add(Club club)
		{
			context.Add(club); //generates the sql. 
			return Save();
		}

		public bool Delete(Club club)
		{
			context.Remove(club);
			return Save();
		}

		public async Task<IEnumerable<Club>> GetAll()
		{
			return await context.Clubs.ToListAsync();
		}

		public async Task<Club> GetByIdAsync(int id)
		{
			return await context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(c => c.Id == id);
		}
		public async Task<Club> GetByIdAsyncNoTracking(int id)
		{
			return await context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Club>> GetClubByCity(string city)
		{
			return await context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
		}

		public bool Save()
		{
			var saved = context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Club club)
		{
			context.Update(club);
			return Save();
		}
	}
}
