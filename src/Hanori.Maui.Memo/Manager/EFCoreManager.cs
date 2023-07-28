using Hanori.Maui.Memo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hanori.Maui.Memo.Manager
{
    public class EFCoreManager<TEntity> where TEntity : class
    {

        private MemoDbContext context;

        public EFCoreManager()
        {
            this.context = new MemoDbContext();
        }

        #region Create
        protected void CreateOne(TEntity entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        protected async Task CreateOneAsync(TEntity entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        protected void CreateAll(List<TEntity> entity)
        {
            context.AddRange(entity);
            context.SaveChanges();
        }

        protected async Task CreateAllAsync(List<TEntity> entity)
        {
            await context.AddRangeAsync(entity);
            await context.SaveChangesAsync();
        }
        #endregion Create

        #region Read
        protected TEntity? ReadOne(object id)
        {
            var entity = context.Find<TEntity>(id);
            return entity;
        }

        protected async Task<TEntity?> ReadOneAsync(object id)
        {
            var entity = await context.FindAsync<TEntity>(id);
            return entity;
        }

        protected List<TEntity?> ReadAll()
        {
            return context.Set<TEntity>().ToList<TEntity?>();
        }

        protected async Task<List<TEntity?>> ReadAllAsync()
        {
            var entities = await context.Set<TEntity>().ToListAsync<TEntity?>();
            return entities;
        }
        #endregion Read

        #region Update
        protected void Update(TEntity entity)
        {
            context.SaveChanges();
        }

        protected async Task UpdateAsync(TEntity entity)
        {
            await context.SaveChangesAsync();
        }
        #endregion Update

        #region Remove
        protected void RemoveOne(object id)
        {
            var entity = context.Find<TEntity>(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        protected async Task RemoveOneAsync(object id)
        {
            var entity = await context.FindAsync<TEntity>(id);
            if (entity != null)
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        protected void RemoveAll()
        {
            var entities = context.Set<TEntity>().ToList();
            context.RemoveRange(entities);
            context.SaveChanges();
        }

        protected async Task RemoveAllAsync()
        {
            var entities = await context.Set<TEntity>().ToListAsync();
            context.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
        #endregion Remove

        #region DeepCopy Test
        private TEntity DeepCopy(object entity)
        {
            Type type = entity.GetType();
            TEntity clone = Activator.CreateInstance<TEntity>();

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.FieldType.IsClass && field.FieldType != typeof(string) && !field.FieldType.IsPrimitive)
                {
                    // 참조 타입인 경우, 재귀적으로 깊은 복사를 수행
                    var fieldValue = field.GetValue(entity);
                    if (fieldValue != null)
                    {
                        var clonedValue = DeepCopy(fieldValue);
                        field.SetValue(clone, clonedValue);
                    }
                }
                else
                {
                    // 값 타입 또는 문자열인 경우, 얕은 복사 수행
                    var fieldValue = field.GetValue(entity);
                    field.SetValue(clone, fieldValue);
                }
            }

            return clone;
        }
        #endregion DeepCopy Test
    }
}
