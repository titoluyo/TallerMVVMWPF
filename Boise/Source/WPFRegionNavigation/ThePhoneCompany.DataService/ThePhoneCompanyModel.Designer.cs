﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("ThePhoneCompanyModel", "FK_Item_Category", "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(ThePhoneCompany.DataService.Category), "Item", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ThePhoneCompany.DataService.Item), true)]

#endregion

namespace ThePhoneCompany.DataService
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ThePhoneCompanyEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ThePhoneCompanyEntities object using the connection string found in the 'ThePhoneCompanyEntities' section of the application configuration file.
        /// </summary>
        public ThePhoneCompanyEntities() : base("name=ThePhoneCompanyEntities", "ThePhoneCompanyEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ThePhoneCompanyEntities object.
        /// </summary>
        public ThePhoneCompanyEntities(string connectionString) : base(connectionString, "ThePhoneCompanyEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ThePhoneCompanyEntities object.
        /// </summary>
        public ThePhoneCompanyEntities(EntityConnection connection) : base(connection, "ThePhoneCompanyEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Category> Categories
        {
            get
            {
                if ((_Categories == null))
                {
                    _Categories = base.CreateObjectSet<Category>("Categories");
                }
                return _Categories;
            }
        }
        private ObjectSet<Category> _Categories;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Item> Items
        {
            get
            {
                if ((_Items == null))
                {
                    _Items = base.CreateObjectSet<Item>("Items");
                }
                return _Items;
            }
        }
        private ObjectSet<Item> _Items;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Categories EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCategories(Category category)
        {
            base.AddObject("Categories", category);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Items EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToItems(Item item)
        {
            base.AddObject("Items", item);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ThePhoneCompanyModel", Name="Category")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Category : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="categoryID">Initial value of the CategoryID property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        public static Category CreateCategory(global::System.Int32 categoryID, global::System.String description)
        {
            Category category = new Category();
            category.CategoryID = categoryID;
            category.Description = description;
            return category;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                if (_CategoryID != value)
                {
                    OnCategoryIDChanging(value);
                    ReportPropertyChanging("CategoryID");
                    _CategoryID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("CategoryID");
                    OnCategoryIDChanged();
                }
            }
        }
        private global::System.Int32 _CategoryID;
        partial void OnCategoryIDChanging(global::System.Int32 value);
        partial void OnCategoryIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ThePhoneCompanyModel", "FK_Item_Category", "Item")]
        public EntityCollection<Item> Items
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Item>("ThePhoneCompanyModel.FK_Item_Category", "Item");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Item>("ThePhoneCompanyModel.FK_Item_Category", "Item", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ThePhoneCompanyModel", Name="Item")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Item : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Item object.
        /// </summary>
        /// <param name="itemID">Initial value of the ItemID property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="price">Initial value of the Price property.</param>
        /// <param name="categoryID">Initial value of the CategoryID property.</param>
        public static Item CreateItem(global::System.Int32 itemID, global::System.String description, global::System.Decimal price, global::System.Int32 categoryID)
        {
            Item item = new Item();
            item.ItemID = itemID;
            item.Description = description;
            item.Price = price;
            item.CategoryID = categoryID;
            return item;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                if (_ItemID != value)
                {
                    OnItemIDChanging(value);
                    ReportPropertyChanging("ItemID");
                    _ItemID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ItemID");
                    OnItemIDChanged();
                }
            }
        }
        private global::System.Int32 _ItemID;
        partial void OnItemIDChanging(global::System.Int32 value);
        partial void OnItemIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private global::System.Decimal _Price;
        partial void OnPriceChanging(global::System.Decimal value);
        partial void OnPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                OnCategoryIDChanging(value);
                ReportPropertyChanging("CategoryID");
                _CategoryID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CategoryID");
                OnCategoryIDChanged();
            }
        }
        private global::System.Int32 _CategoryID;
        partial void OnCategoryIDChanging(global::System.Int32 value);
        partial void OnCategoryIDChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("ThePhoneCompanyModel", "FK_Item_Category", "Category")]
        public Category Category
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("ThePhoneCompanyModel.FK_Item_Category", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("ThePhoneCompanyModel.FK_Item_Category", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Category> CategoryReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("ThePhoneCompanyModel.FK_Item_Category", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Category>("ThePhoneCompanyModel.FK_Item_Category", "Category", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
