using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DiagramAPI.Models;
using DiagramService;

namespace DiagramAPI.Controllers
{
    [Route("api/diagrams/{diagramId}/classifiers/{classifierId}/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly DiagramAPIContext _context;

        public RelationshipsController(DiagramAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Models.Relationship>> GetAll(long diagramId, long classifierId)
        {
            var diagram = _context.Diagrams.Find(diagramId);

            if (diagram == null)
            {
                return NotFound();
            }

            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == classifierId);

            if (classifier == null)
            {
                return NotFound();
            }

            return classifier.Relationships.ToList();
        }

        [HttpGet("{id}", Name = "GetRelationship")]
        public ActionResult<Models.Relationship> GetById(long diagramId, long classifierId, long id)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == classifierId);
            
            if (classifier == null)
            {
                return NotFound();
            }

            var relationship = classifier.Relationships.FirstOrDefault(_relationship => _relationship.ID == id);
            
            if (relationship == null)
            {
                return NotFound();
            }

            return relationship;
        }

        [HttpPost]
        public IActionResult Create(long diagramId, long classifierId, Models.Relationship relationship)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == classifierId);
            
            if (classifier == null)
            {
                return NotFound();
            }

            relationship.Classifier = classifier;

            _context.Relationships.Add(relationship);
            _context.SaveChanges();

            return CreatedAtRoute("GetRelationship", new { diagramId = diagram.ID, classifierId = classifier.ID, id = relationship.ID }, relationship);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long diagramId, long classifierId, long id, Models.Relationship relationship)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == classifierId);
            
            if (classifier == null)
            {
                return NotFound();
            }

            var currentRelationship = classifier.Relationships.FirstOrDefault(_relationship => _relationship.ID == id);
            
            if (currentRelationship == null)
            {
                return NotFound();
            }

            currentRelationship.Type = relationship.Type;

            _context.Relationships.Update(currentRelationship);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long diagramId, long classifierId, long id)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == classifierId);
            
            if (classifier == null)
            {
                return NotFound();
            }

            var relationship = classifier.Relationships.FirstOrDefault(_relationship => _relationship.ID == id);
            
            if (relationship == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationship);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
