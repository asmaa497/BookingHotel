using AutoMapper;
using BookingHotel.DTO;
using BookingHotel.Models;
using BookingHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IRepositoryBranch repositoryBranch;
         
        public BranchController(IRepositoryBranch repositoryBranch)
        {
            this.repositoryBranch = repositoryBranch;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = repositoryBranch.GetAll();
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound(new StatusResponse { Message="No data found",Status=false});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}", Name = "getOneRouteBranch")]
        public IActionResult GetOne(int id)
        {
            try
            {
                var branch = repositoryBranch.GetOne(id);
                if (branch != null)
                {
                    return Ok(branch);
                }
                return NotFound(new StatusResponse { Message = "faild no found this branch", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowEffect = repositoryBranch.Delete(id);
                if (rowEffect > 0)
                {
                    return Ok(new StatusResponse { Message=$"Branch deleted",Status=true});
                }
                return BadRequest(new StatusResponse { Message = "Not found any branch", Status = false });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(Branch model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(repositoryBranch.Add(model));
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        public IActionResult Edit(int id,Branch model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int rowEffect = repositoryBranch.Edit(id, model);
                    if(rowEffect > 0)
                    {
                        string url = Url.Link("getOneRouteBranch", new { id = id });
                        return Created(url,model);
                    }
                }
                return BadRequest(new StatusResponse { Message = "Edit faild" ,Status=false});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
