using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinalProyect.Entities;
using FinalProyect.Models;
using FinalProyect.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProyect.Controllers
{
    [Produces("application/json")]
    [Route("api/collectives")]
    public class CollectivesController : Controller
    {
        private ICollectiveRepository _repository;

        public CollectivesController(ICollectiveRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetCollectives()
        {
            var collectivesFromRepo = _repository.GetCollectives();

            var collectives = Mapper.Map<IEnumerable<CollectiveDto>>(collectivesFromRepo);
            return Ok(collectives);
        }

        [HttpGet("{id}", Name ="GetCollective")]
        public IActionResult GetCollective(Guid id)
        {
            var collectiveFromRepo = _repository.GetCollective(id);

            if(collectiveFromRepo == null)
            {
                return NotFound();
            }

            var collective = Mapper.Map<CollectiveDto>(collectiveFromRepo);
            return Ok(collective);
        }

        [HttpPost]
        public IActionResult CreateCollective([FromBody] CollectiveDto collective)
        {
            if(collective == null)
            {
                return BadRequest();
            }

            var collectiveEntity = Mapper.Map<Collective>(collective);

            _repository.AddCollective(collectiveEntity);

            if (!_repository.Save())
            {
                throw new Exception("Create a collective failed on save.");
                // return StatusCode(500, "A problem happened with handling your request.");
            }

            var collectiveToReturn = Mapper.Map<CollectiveDto>(collectiveEntity);

            return CreatedAtRoute("GetCollective",
                new { id = collectiveToReturn.Id },
                collectiveToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCollective([FromBody] CollectiveDto collective)
        {

            if (collective == null)
            {
                return NotFound();
            }

            var collectiveEntity = Mapper.Map<Collective>(collective);

            _repository.UpdateCollective(collectiveEntity);

            if (!_repository.Save())
            {
                throw new Exception("Update a collective failed on save.");
                // return StatusCode(500, "A problem happened with handling your request.");
            }

            var collectiveToReturn = Mapper.Map<CollectiveDto>(collectiveEntity);

            return Ok(collectiveToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCollective(Guid id)
        {
            var collectiveFromRepo = _repository.GetCollective(id);
            if (collectiveFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCollective(collectiveFromRepo);

            if (!_repository.Save())
            {
                throw new Exception($"Deleting collective {id} failed on save.");
            }

            return NoContent();
        }
    }
}