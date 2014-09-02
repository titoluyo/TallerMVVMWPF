using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Implements IEntitySet with DbSet.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EntitySet<TEntity> : IEntitySet<TEntity> where TEntity : class
    {
        //Note: Couldn't inherit from DbSet<TEntity> because there are no constructors defined
        public DbSet<TEntity> UnderlyingDbSet { get; set; }

        /// <summary>
        /// Gets an System.Collections.ObjectModel.ObservableCollection<T> that represents a local view of all Added, Unchanged, and Modified entities in this set. 
        /// This local view will stay in sync as entities are added or removed from the context. 
        /// Likewise, entities added to or removed from the local view will automatically be added to or removed from the context.
        /// </summary>
        /// <value>An observable collection of entities.</value>
        /// <remarks>
        /// This property can be used for data binding by populating the set with data, for example by using the Load extension method, and then binding to the local data through this property. 
        /// For WPF bind to this property directly. For Windows Forms bind to the result of calling ToBindingList on this property
        /// </remarks>
        ObservableCollection<TEntity> IEntitySet<TEntity>.Local
        {
            get
            {
                return this.UnderlyingDbSet.Local;
            }
        }

        /// <summary>
        /// Adds the given entity to the context underlying the set in the Added state such that it will be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity.</returns>
        /// <remarks>
        /// Note that entities that are already in the context in some other state will have their state set to Added. 
        /// Add is a no-op if the entity is already in the context in the Added state.
        TEntity IEntitySet<TEntity>.Add(TEntity entity)
        {
            return this.UnderlyingDbSet.Add(entity);
        }

        /// <summary>
        /// Attaches the given entity to the context underlying the set. 
        /// That is, the entity is placed into the context in the Unchanged state, just as if it had been read from the database.
        /// </summary>
        /// <param name="entity">The entity to attach..</param>
        /// <returns>The entity.</returns>
        /// <remarks>
        /// Attach is used to repopulate a context with an entity that is known to already exist in the database.  
        /// SaveChanges will therefore not attempt to insert an attached entity into the database because it is assumed to already be there.  
        /// Note that entities that are already in the context in some other state will have their state set to Unchanged. 
        /// Attach is a no-op if the entity is already in the context in the Unchanged state.
        /// </remarks>
        TEntity IEntitySet<TEntity>.Attach(TEntity entity)
        {
            return this.UnderlyingDbSet.Attach(entity);
        }        

        /// <summary>
        /// Creates a new instance of an entity for the type of this set. 
        /// Note that this instance is NOT added or attached to the set.  
        /// The instance returned will be a proxy if the underlying context is configured to create proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <returns>The entity instance, which may be a proxy.</returns>
        TEntity IEntitySet<TEntity>.Create()
        {
            return this.UnderlyingDbSet.Create();
        }

        /// <summary>
        /// Creates a new instance of an entity for the type of this set or for a type derived from the type of this set.  
        /// Note that this instance is NOT added or attached to the set.  
        /// The instance returned will be a proxy if the underlying context is configured to create proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <typeparam name="TDerivedEntity">The type of the derived entity.</typeparam>
        /// <returns>The entity instance, which may be a proxy.</returns>
        TDerivedEntity IEntitySet<TEntity>.Create<TDerivedEntity>()
        {
            return this.UnderlyingDbSet.Create<TDerivedEntity>();
        }

        /// <summary>
        /// Finds an entity with the given primary key values.  
        /// If an entity with the given primary key values exists in the context, then it is returned immediately without making a request to the store. 
        /// Otherwise, a request is made to the store for an entity with the given primary key values and this entity, if found, is attached to the context and returned. 
        /// If no entity is found in the context or the store, then null is returned.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>The entity found, or null.</returns>
        /// <remarks>
        /// The ordering of composite key values is as defined in the EDM, which is in turn as defined in the designer, by the Code First fluent API, or by the DataMember attribute.
        /// </remarks>
        TEntity IEntitySet<TEntity>.Find(params object[] keyValues)
        {
            return this.UnderlyingDbSet.Find(keyValues);
        }

        /// <summary>
        /// Marks the given entity as Deleted such that it will be deleted from the database when SaveChanges is called. 
        /// Note that the entity must exist in the context in some other state before this method is called.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>The entity.</returns>      
        /// <remarks>
        /// Note that if the entity exists in the context in the Added state, then this method will cause it to be detached from the context. 
        /// This is because an Added entity is assumed not to exist in the database such that trying to delete it does not make sense.
        /// </remarks>
        TEntity IEntitySet<TEntity>.Remove(TEntity entity)
        {
            return this.UnderlyingDbSet.Remove(entity);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return ((IEnumerable<TEntity>)this.UnderlyingDbSet).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.UnderlyingDbSet).GetEnumerator();
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        Type IQueryable.ElementType
        {
            get
            {
                return ((IQueryable)this.UnderlyingDbSet).ElementType;
            }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.</returns>
        Expression IQueryable.Expression
        {
            get
            {
                return ((IQueryable)this.UnderlyingDbSet).Expression;
            }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.</returns>
        IQueryProvider IQueryable.Provider
        {
            get
            {
                return ((IQueryable)this.UnderlyingDbSet).Provider;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is a collection of <see cref="T:System.Collections.IList"/> objects.
        /// </summary>
        /// <returns>true if the collection is a collection of <see cref="T:System.Collections.IList"/> objects; otherwise, false.</returns>
        bool IListSource.ContainsListCollection
        {
            get
            {
                return ((IListSource)this.UnderlyingDbSet).ContainsListCollection;
            }
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IList"/> that can be bound to a data source from an object that does not implement an <see cref="T:System.Collections.IList"/> itself.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IList"/> that can be bound to a data source from the object.
        /// </returns>
        IList IListSource.GetList()
        {
            return ((IListSource)this.UnderlyingDbSet).GetList();
        }
    }
}
