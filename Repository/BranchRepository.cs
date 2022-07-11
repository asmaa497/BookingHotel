using BookingHotel.DTO;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repository
{
    public class BranchRepository : IRepositoryBranch
    {
        private readonly ApplicationContext db;

        public BranchRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public Branch Add(Branch entity)
        {
            if(entity != null)
            {
                db.Branches.Add(entity);
                db.SaveChanges();
                return entity;
            }
            throw new Exception ("Insert faild");
        }
        public int Delete(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch != null)
            {
                db.Branches.Remove(branch);
                return (db.SaveChanges());
            }
            return 0;
        }

        public int Edit(int id, Branch newBranch)
        {
            Branch oldBranch = db.Branches.FirstOrDefault(b => b.Id == id);
            if (oldBranch != null)
            {
                oldBranch.Name = newBranch.Name;
                oldBranch.Location = newBranch.Location;
                oldBranch.City = newBranch.City;
                return (db.SaveChanges());
            }
            return 0;
        }

        public ICollection<Branch> GetAll()
        {
            List<Branch> branches = db.Branches.ToList();
            return (branches);  
        }
        public Branch GetOne(int id)
        {
            Branch branch =  db.Branches.Include(b => b.Rooms).AsSplitQuery().FirstOrDefault(b => b.Id == id);
            return branch;
        }
    }
}
