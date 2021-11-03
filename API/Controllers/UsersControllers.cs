using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {  
            _context = context;      
        }


        /*
        //first end point is to get the data for every user - Synchronous Code

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {   
            var users = _context.Users.ToList();
            return users;
            // return _context.Users.ToList();
            
        } 
        */

        //Asynchronous Code
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {   
            // var users = _context.Users.ToList();
            // return users;
            return await _context.Users.ToListAsync();
            
        } 

        //Second end point is to get the data for specific user - Async method
        //api/users/3    
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {   
            return await _context.Users.FindAsync(id);  
        } 
    }
}