using Blog.WebApi.Entities;
using Blog.WebApi.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll(string filter, string sortCol, string sortOrder);
        Post GetById(int id);
        bool Create(Post post);
        bool Update(int id, Post post);
        bool Delete(int id);
    }
    public class PostService : IPostService
    {
        private ISqlClient _sqlClient;
        public PostService(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public bool Create(Post post)
        {
            var result = _sqlClient.ExecuteProcedureReturnString("sp_PostsInsert", new SqlParameter[] {
                new SqlParameter("@Title", post.Title),
                new SqlParameter("@Description", post.Description),
                new SqlParameter("@Content", post.Content),
                new SqlParameter("@CreatedDate", DateTime.Now),
                new SqlParameter("@Author", post.Author)
            });

            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }

        public bool Delete(int id)
        {
            var result = _sqlClient.ExecuteProcedureReturnString("sp_PostsDelete", new SqlParameter[] {
                new SqlParameter("@Id", id),
            });

            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }

        public IEnumerable<Post> GetAll(string filter, string sortCol, string sortOrder)
        {
            return _sqlClient.ExecuteProcedureReturnData<Post>("sp_PostsSelectGetAll", new SqlParameter[] {
                new SqlParameter("@filter", filter),
                new SqlParameter("@sortcolumn", sortCol),
                new SqlParameter("@sortorder", sortOrder)
            });
        }

        public Post GetById(int id)
        {
            SqlParameter[] param = {
                new SqlParameter("@Id", id)
            };

            return _sqlClient.ExecuteProcedureReturnData<Post>("sp_PostsSelectFindById", param).FirstOrDefault();
        }

        public bool Update(int id, Post post)
        {
            var findPost = _sqlClient.ExecuteProcedureReturnData<Post>("sp_PostsSelectFindById", new SqlParameter[] { new SqlParameter("@Id", id) }).FirstOrDefault();

            if (!string.IsNullOrEmpty(post.Title))
                findPost.Title = post.Title;

            if (!string.IsNullOrEmpty(post.Description))
                findPost.Description = post.Description;

            if (!string.IsNullOrEmpty(post.Content))
                findPost.Content = post.Content;

            if (!string.IsNullOrEmpty(post.Author))
                findPost.Author = post.Author;

            var result = _sqlClient.ExecuteProcedureReturnString("sp_PostsUpdate", new SqlParameter[] {
                new SqlParameter("@Id", findPost.Id),
                new SqlParameter("@Title", findPost.Title),
                new SqlParameter("@Description", findPost.Description),
                new SqlParameter("@Content", findPost.Content),
                new SqlParameter("@Author", findPost.Author)
            });

            if (!string.IsNullOrEmpty(result))
                return true;

            return false;
        }
    }
}
