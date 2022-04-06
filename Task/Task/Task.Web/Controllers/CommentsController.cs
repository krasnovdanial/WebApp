using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Task.Web.Models;
using Task.Web.Repositories;

namespace Task.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с комментариями.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Repository<UserComment> _commentRepository;
        private readonly Repository<User> _userRepository;

        public CommentsController(Repository<UserComment> commentRepository, Repository<User> userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получает вес комментарии. Возвращает ответ со статусом 200.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<UserComment>> GetAll()
        {
            var userComments = _commentRepository.GetAll();
            return Ok(userComments);
        }

        /// <summary>
        /// Получает комментарий по указанному Id. Если комментарий найден,
        /// то возвращается ответ со статусом 200. Иначе возвращается ответ со статусом 404.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public ActionResult<UserComment> Get(Guid id)
        {
            var comment = _commentRepository
                .GetByPredicate(comment => comment.UserCommentId == id)
                .FirstOrDefault();

            if (comment is null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        /// <summary>
        /// Добавляет комментарий в бд. Если комментарий имеет Id пользователей,
        /// которых нет, то возвращает статус 400.
        /// <br/> Если комментарий успешно добавлен, то статус 200 и добавленный объект в теле ответа.
        /// </summary>
        /// <param name="userComment"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult<UserComment> Post(UserComment userComment)
        {
            if (userComment.CommentContent is null)
            {
                return BadRequest();
            }

            var isExist = CheckOnExistUserWithIdInComment(userComment);

            if (!isExist || !_commentRepository.TryAdd(userComment))
            {
                return BadRequest();
            }

            return Ok(userComment);
        }

        /// <summary>
        /// Удаляет комментарий с указанным id. Возвращает статус 200, если комментарий удален
        /// <br/> 204 и 404, если комментарий, с таким id не существует.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var amountOfDeleted = _commentRepository
                .DeleteByPredicate(comment => comment.UserCommentId == id);

            if (amountOfDeleted > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

        private bool CheckOnExistUserWithIdInComment(UserComment userComment)
        {
            var amountOfUser = _userRepository.GetByPredicate(
                user => user.UserId == userComment.CommentatorUserId || user.UserId == userComment.CommentedUserId);
            return amountOfUser.Count() == 2;
        }
    }
}