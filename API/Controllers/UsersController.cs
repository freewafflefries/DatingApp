using System.Security.Claims;
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
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        // api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            
            return _mapper.Map<MemberDto>(user);
        }

        // api/users/byname/{userName}
        [HttpGet("byname/{userName}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string userName)
        {
           
           return await _userRepository.GetMemberAsync(userName);
           
;            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            //The ControllerBase class (from which all our Controllers are inheriting) has a User class property (of Type Security Principal) to help us identify who is using it
            //The FindFirst method takes a ClaimType as its argument to lookup the User
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            //now that we have figured out the userName of our resource calling the API, we can lookup our app's user from the _userRepository
            var user = await _userRepository.GetUserByUsernameAsync(userName);

            //using AutoMapper to map the values from our DTO to the user entity we just fetched from persistence
            _mapper.Map(memberUpdateDto, user);

            //Finally, we need to update the _userRepository with the changes we just applied (essentially, we just hit the "save" button)
            //Apply the changes...
            _userRepository.UpdateProfile(user);

            //Hit "save" and if it is suceesfful, we return NoContent, otherwise, return a BadRequest (because we gotta return something)
            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to Update User");
        }
    }


}