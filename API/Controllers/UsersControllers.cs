using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        //private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {  
            _mapper = mapper;
            _userRepository = userRepository;
            //_context = context;      
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {   
            // var users = _context.Users.ToList();
            // return users;
            //return await _context.Users.ToListAsync();
            
            
            /********* Another way of writing User Repository using a variable named users*********/
            // var users = await _userRepository.GetUsersAsync();
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            // return Ok(usersToReturn);

            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
            

            //return Ok(await _userRepository.GetUsersAsync());
            
        } 



        //Second end point is to get the data for specific user - Async method
        //api/users/3    
        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {   
            //return await _context.Users.FindAsync(id); 

            // var user = await _userRepository.GetMemberAsync(username);
            // return _mapper.Map<MemberDto>(user);

            return await _userRepository.GetMemberAsync(username);
            
        } 
    }
}