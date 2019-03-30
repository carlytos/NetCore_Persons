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
    [Route("api/persons")]
    public class PersonsController : Controller
    {
        private IPersonRepository _repository;

        public PersonsController(IPersonRepository repository)
        {
            _repository = repository;
        }


        [HttpGet()]
        public IActionResult GetPersons()
        {
            var personsFromRepo = _repository.GetPersons();

            var persons = Mapper.Map<IEnumerable<PersonDto>>(personsFromRepo);
            return Ok(persons);
        }

        [HttpGet("{id}", Name ="GetPerson")]
        public IActionResult GetPerson(Guid id)
        {
            var personFromRepo = _repository.GetPerson(id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            var person = Mapper.Map<PersonDto>(personFromRepo);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] CreatePersonDto person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            var personEntity = Mapper.Map<Person>(person);

            _repository.AddPerson(personEntity);

            if (!_repository.Save())
            {
                throw new Exception("Create a person failed on save.");
                // return StatusCode(500, "A problem happened with handling your request.");
            }

            var personToReturn = Mapper.Map<PersonDto>(personEntity);

            return CreatedAtRoute("GetPerson",
                new { id = personToReturn.Id },
                personToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson([FromBody] CreatePersonDto person)
        {

            if (person == null)
            {
                return NotFound();
            }

            var personEntity = Mapper.Map<Person>(person);

            _repository.UpdatePerson(personEntity);

            if (!_repository.Save())
            {
                throw new Exception("Update a person failed on save.");
                // return StatusCode(500, "A problem happened with handling your request.");
            }

            var personToReturn = Mapper.Map<PersonDto>(personEntity);

            return Ok(personToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(Guid id)
        {
            var personFromRepo = _repository.GetPerson(id);
            if (personFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePerson(personFromRepo);

            if (!_repository.Save())
            {
                throw new Exception($"Deleting person {id} failed on save.");
            }

            return NoContent();
        }






    }
}