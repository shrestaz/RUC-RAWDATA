using System;
using System.Linq;

namespace RDJTPServer.Helpers
{
    public class DbOperations
    {
        InMemoryDb db = new InMemoryDb();
        public string GetAllCategories()
        {
            return db.categories.ToJson();
        }

        public InMemoryDb.Category GetSpecificCategory(int id)
        {
            var categoryInDb = db.categories.Find(v => v.Cid == id);

            return categoryInDb;
        }

        public InMemoryDb.Category UpdateCategory(int id, string updateCategoryObject)
        {
            var categoryInDb = GetSpecificCategory(id);
            if (categoryInDb == null)
            {
                return null;
            }
            categoryInDb.Name = updateCategoryObject.FromJson<InMemoryDb.Category>().Name;
            return categoryInDb;
        }

        public InMemoryDb.Category CreateCategory(string createCategoryObject)
        {
            var parsedCreateCategoryObject = createCategoryObject.FromJson<InMemoryDb.Category>();
            var nextAvailableId = 1;
            foreach (var item in db.categories)
            {
                if (nextAvailableId != item.Cid) break;
                nextAvailableId++;
            };
            var categoryToCreate = new InMemoryDb.Category
            {
                Cid = nextAvailableId,
                Name = parsedCreateCategoryObject.Name
            };
            db.categories.Add(categoryToCreate);
            return categoryToCreate;
        }

        public void DeleteCategory(int idOfCategoryToDelete)
        {
            var categoryObjectToDelete = db.categories.Find(v => v.Cid == idOfCategoryToDelete);
            db.categories.Remove(categoryObjectToDelete);
        }

    }
}