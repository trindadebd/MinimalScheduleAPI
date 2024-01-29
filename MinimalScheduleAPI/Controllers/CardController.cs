using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalScheduleAPI.Model;
using MinimalScheduleAPI.Persistence;

namespace MinimalScheduleAPI.Controllers
{
    [Route("api/Card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly List<Card> lCard = new List<Card>();

        private readonly CardDbContext _dbContext;

        public CardController(CardDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Obter todos os cards
        /// </summary>
        /// <returns>Coleção de cards</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Card> GetAll()
        {
            var card = _dbContext.Cards
                .Include(td => td.ListToDo)
                .ToList();

            if (card == null)
                return NotFound();

            return Ok(card);
        }


        /// <summary>
        /// Obter um card especifico pelo id
        /// </summary>
        /// <param name="id">Identificador do card</param>
        /// <returns>Dados do card</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Card> GetById(Guid id)
        {
            var card = _dbContext.Cards
                .FirstOrDefault(e => e.IdCard == id);
            
            if (card == null)
                return NotFound();

            return Ok(card);
        }


        /// <summary>
        /// Cadastrar um card
        /// </summary>
        /// <remarks>
        /// {"sNome":"string","bDiaTodo":true,"sLocal":"string","sDescricao":"string"}
        /// </remarks>
        /// <param name="newCard">Dados do card</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post(Card newCard)
        {
            _dbContext.Cards.Add(newCard);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newCard.IdCard }, newCard);
        }

        /// <summary>
        /// Atualizar um card
        /// </summary>
        /// <remarks>
        /// {"sNome":"string","bDiaTodo":true,"sLocal":"string","sDescricao":"string"}
        /// </remarks>
        /// <param name="id">Identificador do card</param>
        /// <param name="input">Dados do card</param>
        /// <returns>Nada.</returns>
        /// <response code="404">Não encontrado.</response>
        /// <response code="204">Sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(Guid id, Card input)
        {
            var CardExistente = _dbContext.Cards
                .FirstOrDefault(e => e.IdCard == id);
            if (CardExistente == null)
            {
                return NotFound();
            }

            CardExistente.Update(input.SNome, input.BDiaTodo, input.SLocal, input.SDescricao);
            _dbContext.Cards.Update(CardExistente);
            _dbContext.SaveChanges();
            return NoContent();

        }

        /// <summary>
        /// Deletar um card
        /// </summary>
        /// <param name="id">Identificador de card</param>
        /// <returns>Nada</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(Guid id)
        {
            var cardExistente = _dbContext.Cards
                .FirstOrDefault(e => e.IdCard == id);
            if (cardExistente == null)
            {
                return NotFound();
            }

            var temToDo = _dbContext.ToDos
                .Where(e => e.IdCard == id)
                .ToList();

            if (temToDo.Count > 0)
            {
                foreach (var toDo in temToDo)
                {
                    ToDoDelete(toDo.IdToDo);
                }
            }

            _dbContext.Cards.Remove(cardExistente);
            _dbContext.SaveChanges();
            return NoContent();
        }


        //METODOS TODO

        /// <summary>
        /// Cadastrar um toDo
        /// </summary>
        /// <remarks>
        /// {"sTitulo":"string","bConcluido": true}
        /// </remarks>
        /// <param name="id">Id do card que irá pertencer</param>
        /// <param name="newToDo">Dados do ToDo</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost("{id}/todo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult ToDoPost(Guid id,ToDo newToDo)
        {
            newToDo.IdCard = id;

            var cardExistente = _dbContext.Cards
                .Any(e => e.IdCard == id);
            if (!cardExistente)
            {
                return NotFound();
            }
            _dbContext.ToDos.Add(newToDo);
            _dbContext.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Cadastrar um toDo
        /// </summary>
        /// <remarks>
        /// {"sTitulo":"string","bConcluido": true}
        /// </remarks>
        /// <param name="id">Id do ToDo que será atualizado</param>
        /// <param name="input">Dados do ToDo</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPut("{id}/todo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult ToDoPut(Guid id, ToDo input)
        {
            var toDoExistente = _dbContext.ToDos
                .FirstOrDefault(e => e.IdToDo == id);
            if (toDoExistente == null)
            {
                return NotFound();
            }

            toDoExistente.Update(input.STitulo, input.BConcluido, input.SDescricao);
            _dbContext.ToDos.Update(toDoExistente);
            _dbContext.SaveChanges();
            return NoContent();

        }


        /// <summary>
        /// Deletar um toDo
        /// </summary>
        /// <param name="id">Identificador de toDo</param>
        /// <returns>Nada</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete("{id}/todo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult ToDoDelete(Guid id)
        {
            var toDoExistente = _dbContext.ToDos
                .FirstOrDefault(e => e.IdToDo == id);
            if (toDoExistente == null)
            {
                return NotFound();
            }

            _dbContext.ToDos.Remove(toDoExistente);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
