///////////////////////////////////////////////////////////////
// This is generated code. If you modify this code, be aware
// of the fact that when you re-generate the code, your changes
// are lost. If you want to keep your changes, make this file read-only
// when you have finished your changes, however it is recommended that
// you inherit from this class to extend the functionality of this generated
// class or you modify / extend the templates used to generate this code.
//
// Do not try to run this code on another version of the database than the database
// which was used to generate this code. This means that when you used f.e. SqlServer 2000
// to generate this code, it is likely that you will not be able to use that code on
// SqlServer 7 due to SQL syntax mismatches. Most code is generic code which will work
// with any database, but some code relies on a specific database type/vendor/version used. 
// This code is located in the DaoClasses which target a specific specified database. Also all
// classes target a specific specified Dynamic Query Engine (DQE) in the using/imports
// directives. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 1.0.2003.1
// Code is generated on: woensdag 4 september 2019 12:09:46
// Code is generated using templates: C# template set for SqlServer
// Templates vendor: Solutions Design.
// Templates version: 1.0.2003.3.061104
//////////////////////////////////////////////////////////////
using System;

using LLBL2003.AdventureWorks2008;
using LLBL2003.AdventureWorks2008.FactoryClasses;

using SD.LLBLGen.Pro.ORMSupportClasses2003;

namespace LLBL2003.AdventureWorks2008.RelationClasses
{
	/// <summary>
	/// Implements the static Relations variant for the entity: ProductProductPhoto.
	/// This class is generated. Do not modify.
	/// </summary>
	public class ProductProductPhotoRelations
	{
		/// <summary>
		/// CTor
		/// </summary>
		public ProductProductPhotoRelations()
		{
		}


		#region Class Property Declarations
	
	
	
		/// <summary>
		/// Returns a new IEntityRelation object, between ProductProductPhotoEntity and ProductEntity over the m:1 relation they have,
		/// using the relation between the fields:
		/// ProductProductPhoto.ProductID - Product.ProductID
		/// </summary>
		public IEntityRelation ProductEntityUsingProductID
		{
			get
			{
				IEntityRelation relation = new EntityRelation(RelationType.ManyToOne);
				
				relation.AddEntityFieldPair(EntityFieldFactory.Create(ProductFieldIndex.ProductID), 
						EntityFieldFactory.Create(ProductProductPhotoFieldIndex.ProductID));

				return relation;
			}
		}
	
		/// <summary>
		/// Returns a new IEntityRelation object, between ProductProductPhotoEntity and ProductPhotoEntity over the m:1 relation they have,
		/// using the relation between the fields:
		/// ProductProductPhoto.ProductPhotoID - ProductPhoto.ProductPhotoID
		/// </summary>
		public IEntityRelation ProductPhotoEntityUsingProductPhotoID
		{
			get
			{
				IEntityRelation relation = new EntityRelation(RelationType.ManyToOne);
				
				relation.AddEntityFieldPair(EntityFieldFactory.Create(ProductPhotoFieldIndex.ProductPhotoID), 
						EntityFieldFactory.Create(ProductProductPhotoFieldIndex.ProductPhotoID));

				return relation;
			}
		}
	

		#endregion
	}
}
